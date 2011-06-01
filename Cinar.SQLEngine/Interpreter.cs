using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using Cinar.SQLParser;

namespace Cinar.SQLEngine
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
                execute(coll, context);
            }
            catch (Exception ex)
            {
                context.Output.Write(ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : ""));
            }
            watch.Stop();
            this.ExecutingTime = watch.ElapsedMilliseconds;
        }

        private void execute(StatementCollection coll, Context context)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            TextWriter stringWriter = new StringWriter(sb);
            this.Execute(stringWriter);
        }
    }

    public class Context : IContext
    {
        public TextWriter Output = null;
        public Hashtable Variables = null;

        public object GetMaxOf(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object GetValueOfCurrent(string fName)
        {
            throw new NotImplementedException();
        }

        public object GetVariableValue(string fName)
        {
            throw new NotImplementedException();
        }
    }
}