using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Cinar.SQLParser
{
    public class Interpreter
    {
        List<Statement> statements;
        Context context;

        public Interpreter(string code)
        {
            this.Code = code;
        }

        public List<Statement> StatementsTree
        {
            get
            {
                return statements;
            }
        }

        Hashtable attributes = new Hashtable();
        public void SetAttribute(string key, object value)
        {
            attributes[key] = value;
        }
        public void ClearAttributes()
        {
            attributes.Clear();
        }

        private string _code;
        public string Code { get { return _code; } set { _code = value; } }

        private long parsingTime;
        public long ParsingTime { get { return parsingTime; } set { parsingTime = value; } }
        private long executingTime;
        public long ExecutingTime { get { return executingTime; } set { executingTime = value; } }

        public string Output
        {
            get
            {
                return context.Output.ToString();
            }
        }

        public void Parse()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            try
            {
                this.Code = preParse(this.Code);

                statements = new List<Statement>();

                using (StringReader lSource = new StringReader(this.Code))
                {
                    Parser lParser = new Parser(lSource);

                    Statement lStatement = lParser.ReadNextStatement();
                    while (lStatement != null)
                    {
                        statements.Add(lStatement);
                        lStatement = lParser.ReadNextStatement();
                    }
                }
            }
            catch
            {
                //statements = new List<Statement>();
                //statements.Add(new FunctionCallStatement(new FunctionCall("write", new Expression[] { new StringConstant(ex.Message) })));
            }
            watch.Stop();
            this.ParsingTime = watch.ElapsedMilliseconds;
        }

        private string preParse(string code)
        {
            return code;
        }

        public void Execute(TextWriter output)
        {
            context = new Context();
            context.Output = output;
            context.Variables = attributes;
            context.Variables["true"] = true;
            context.Variables["false"] = false;
            context.Variables["null"] = null;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                StatementCollection coll = new StatementCollection(statements);
                coll.Execute(context, null, null);
            }
            catch (Exception ex)
            {
                context.Output.Write(ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : ""));
            }
            watch.Stop();
            this.ExecutingTime = watch.ElapsedMilliseconds;
        }
        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            TextWriter stringWriter = new StringWriter(sb);
            this.Execute(stringWriter);
        }
    }

    public class Context
    {
        public Hashtable Variables = new Hashtable();
        public TextWriter Output = null;
        internal Context parent = null;

        internal object GetDbObject(string fName)
        {
            throw new NotImplementedException();
        }

        internal object GetVariableValue(string fName)
        {
            return Variables[fName];
        }
    }
}