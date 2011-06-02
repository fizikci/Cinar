﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using Cinar.SQLParser;
using System.Linq;

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
        public List<Hashtable> ResultSet = null;
        public List<string> FieldNames = null;

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
            foreach (Statement statement in coll)
                execute(statement, context);
        }

        private void execute(Statement statement, Context context)
        {
            if (statement is SelectStatement)
            {
                // Join'den tabloları al
                // her bir kayıt için hashtable oluştur, context'e ekle, where expressionı üzerinde excute ettir. true ise listeye ekle.
                // falan filan uzun iş bu. niye yapıyorum ki ben bunu?

                SelectStatement ss = statement as SelectStatement;
                Expression filter = null;
                if (ss.From[0].On == null && ss.Where == null) filter = null;
                else if (ss.From[0].On != null && ss.Where != null) filter = new AndExpression(ss.From[0].On, ss.Where);
                else if (ss.From[0].On != null) filter = ss.From[0].On;
                else filter = ss.Where;

                List<Hashtable> data = context.GetData(ss.From[0], filter);
                this.ResultSet = data;
                this.FieldNames = ss.Select.Select(s => s.Alias).ToList();
            }
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

        internal List<Hashtable> GetData(Join join, Expression where)
        {
            List<Hashtable> list = new List<Hashtable>();

            switch (join.TableName.ToLowerInvariant())
            {
                case "information_schema.tables":
                    list.Add(new Hashtable() { { "table_name", "RSS" }, { "table_type", "table" } });
                    list.Add(new Hashtable() { { "table_name", "POP3" }, { "table_type", "table" } });
                    list.Add(new Hashtable() { { "table_name", "File" }, { "table_type", "table" } });
                    break;
                case "information_schema.columns":
                    throw new NotImplementedException("return table columns");
                    break;
            }
            return list;
        }
    }
}