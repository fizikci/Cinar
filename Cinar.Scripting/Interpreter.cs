using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Cinar.Scripting
{
    public class Interpreter
    {
        List<Statement> statements;
        Context context;

        public Interpreter(string code, List<string> usings)
        {
            this.Code = code;
            this.usings = usings;
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

        private List<string> usings;
        public List<string> Usings { get { return usings; } set { usings = value; } }

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
            catch (ParserException ex)
            {
                statements = new List<Statement>();
                statements.Add(new FunctionCallStatement(new FunctionCall("write", new Expression[] { new StringConstant(ex.Message) })));
            }
            watch.Stop();
            this.ParsingTime = watch.ElapsedMilliseconds;
        }

        private string preParse(string code)
        {
            code = code.Replace("\\$", "__backSlashDollor__");
            StringBuilder sb = new StringBuilder(code.Length * 2);
            using (StringReader sr = new StringReader(code))
            {
                string codePart = "", textPart = "";
                bool readingCode = false, shortcutWrite = false;
                int i = sr.Read();
                while (i > 0)
                {
                    char c = (char)i;
                    switch (c)
                    {
                        case '$':
                            if (readingCode)
                            {
                                if (shortcutWrite)
                                {
                                    codePart += ")";
                                    shortcutWrite = false;
                                }
                                sb.AppendLine(codePart);
                                codePart = "";
                                readingCode = false;
                                i = sr.Read();
                            }
                            else
                            {
                                if (textPart != "")
                                    sb.AppendLine("write(\"" + textPart.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"") + "\")");
                                textPart = "";
                                readingCode = true;

                                i = sr.Read();
                                if ((char)i == '=')
                                {
                                    codePart = "write(";
                                    shortcutWrite = true;
                                    i = sr.Read();
                                }
                            }
                            break;
                        default:
                            if (readingCode)
                                codePart += c;
                            else
                                textPart += c;
                            i = sr.Read();
                            break;
                    }
                }
                if (readingCode && codePart != "")
                    sb.AppendLine(codePart);
                if (!readingCode && textPart != "")
                    sb.AppendLine("write(\"" + textPart.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"") + "\")");
            }
            sb.Replace("__backSlashDollor__", "$");
            return sb.ToString();
        }

        public bool LastExecutionSuccessful { get; set; }

        public void Execute(TextWriter output)
        {
            context = new Context();
            context.Output = output;
            context.Variables = attributes;
            context.Variables["true"] = true;
            context.Variables["false"] = false;
            context.Variables["null"] = null;
            context.Using.Add("System");
            if(this.Usings!=null)
                foreach (string nameSpace in this.Usings)
                    if(!string.IsNullOrEmpty(nameSpace))
                        context.Using.Add(nameSpace);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                StatementCollection coll = new StatementCollection(statements);
                coll.Execute(context, null, null);
                this.LastExecutionSuccessful = true;
            }
            catch (Exception ex)
            {
                this.LastExecutionSuccessful = false;
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
        public Hashtable Functions = new Hashtable();
        public List<string> Using = new List<string>();

        public TextWriter Output = null;
        public object ReturnValue = null;
        internal static bool breakLoop = false;
        internal static bool continueLoop = false;
        internal bool debugging = false;
        internal Context parent = null;

        public Type GetType(string className)
        {
            Type t = null;
            foreach (string nameSpace in this.Using)
            {
                string fullClassName = nameSpace + "." + className;
                t = Type.GetType(fullClassName);
                if (t != null) return t;
                t = Assembly.GetAssembly(typeof(System.Net.WebClient)).GetType(fullClassName);
                if (t != null) return t;
                if (Assembly.GetEntryAssembly() != null)
                {
                    t = Assembly.GetEntryAssembly().GetType(fullClassName);
                    if (t != null) return t;
                }
                t = Assembly.GetCallingAssembly().GetType(fullClassName);
                if (t != null) return t;
            }
            return t;
        }
        public object GetVariableValue(string name)
        {
            Context currContext = this;
            while (currContext != null)
            {
                if (currContext.Variables.ContainsKey(name))
                    return currContext.Variables[name];
                currContext = currContext.parent;
            }
            return null;
        }
        public void SetVariableValue(string name, object value)
        {
            Context currContext = this;
            while (currContext != null)
            {
                if (currContext.Variables.ContainsKey(name))
                {
                    currContext.Variables[name] = value;
                    return;
                }
                currContext = currContext.parent;
            }
            this.Variables[name] = value;
        }
    }
}