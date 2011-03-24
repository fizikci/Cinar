using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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

        internal List<Statement> ParsedStatementsWithoutError;

        public void Parse()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Context.ParsedFunctions = new List<FunctionDefinitionStatement>();
            Context.ParsedUsing = new List<string> { "System"};
            Context.ParsedVariables = new List<VariableDefinition>();
            statements = new List<Statement>();

            using (StringReader lSource = new StringReader(this.Code))
            {
                Parser lParser = new Parser(lSource);
                try
                {
                    Statement lStatement = lParser.ReadNextStatement();
                    while (lStatement != null)
                    {
                        statements.Add(lStatement);
                        lStatement = lParser.ReadNextStatement();
                    }
                    ParsingSuccessful = true;
                }
                catch (ParserException ex)
                {
                    ParsingSuccessful = false;

                    ParsedStatementsWithoutError = statements;

                    statements = new List<Statement>();
                    statements.Add(new FunctionCallStatement(new FunctionCall("write", new Expression[] { new StringConstant(ex.Message + " at line " + (lParser.CurrentLineNumber + 1)) })));
                }
            }

            watch.Stop();
            this.ParsingTime = watch.ElapsedMilliseconds;
        }

        public bool ExecutionSuccessful { get; set; }
        public bool ParsingSuccessful { get; set; }
        public bool Successful { get { return ExecutionSuccessful && ParsingSuccessful; } }

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
            Context.code = this.Code;
            context.Interpreter = this;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                StatementCollection coll = new StatementCollection(statements, false);
                coll.Execute(context, null, null);
                this.ExecutionSuccessful = true;
            }
            catch (Exception ex)
            {
                this.ExecutionSuccessful = false;
                context.Output.Write(ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "") + " at line " + (Context.CurrentStatement.LineNumber + 1));
            }
            watch.Stop();
            this.ExecutingTime = watch.ElapsedMilliseconds;

            if (Context.debuggerWindow != null)
            {
                Context.debugging = false;
                Context.debuggerWindow.Close();
                Context.debuggerWindow = null;
            }
        }
        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            TextWriter stringWriter = new StringWriter(sb);
            this.Execute(stringWriter);
        }

        public void AddAssembly(Assembly assembly)
        {
            Context.AddAssembly(assembly);
        }
    }
    public class Context
    {
        public Hashtable Variables = new Hashtable();
        public Hashtable Functions = new Hashtable();
        public List<string> Using = new List<string>();

        internal Interpreter Interpreter = null;
        public TextWriter Output = null;
        internal static object ReturnValue = null;
        internal static bool breakLoop = false;
        internal static bool continueLoop = false;
        internal static bool debugging = false;
        internal static bool debugContinue = false;
        internal static int debugRunToLine = 0;
        internal static Action debugStatementExecuted = null;
        internal static CinarDebugger debuggerWindow = null;
        internal static string code = "";
        internal Context parent = null;

        internal static List<Assembly> AdditionalAssemblies = new List<Assembly>();
        public static Type GetType(string className, List<string> usings)
        {
            if (className == "int") return typeof(int);
            else if (className == "long") return typeof(long);
            else if (className == "byte") return typeof(byte);
            else if (className == "decimal") return typeof(decimal);
            else if (className == "double") return typeof(double);
            else if (className == "float") return typeof(float);
            else if (className == "string") return typeof(string);
            else if (className == "char") return typeof(char);
            else if (className == "bool") return typeof(bool);

            Type t = null;
            foreach (string nameSpace in usings)
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
                    foreach (AssemblyName asmblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                    {
                        t = Assembly.Load(asmblyName).GetType(fullClassName);
                        if (t != null) return t;
                    }
                }
                t = Assembly.GetCallingAssembly().GetType(fullClassName);
                if (t != null) return t;

                foreach (Assembly assembly in AdditionalAssemblies)
                {
                    t = assembly.GetType(fullClassName);
                    if (t != null) return t;
                }
            }
            return t;
        }
        public static List<Type> GetTypeNames(string nameSpace)
        {
            List<Type> res = new List<Type>();
            if (Assembly.GetEntryAssembly() != null)
            {
                Assembly asm = Assembly.GetEntryAssembly();
                foreach (Type t in asm.GetTypes())
                    if (t.Namespace == nameSpace && t.IsPublic && t.IsClass)
                        res.Add(t);

                foreach (AssemblyName asmblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                {
                    Assembly asm2 = Assembly.Load(asmblyName);
                    foreach (Type t in asm2.GetTypes())
                        if (t.Namespace == nameSpace && t.IsPublic && !t.IsGenericType)
                            res.Add(t);
                }
            }

            return res;
        }
        public static List<string> GetNamespaceNames(string startsWith)
        {
            List<string> res = new List<string>();
            if (Assembly.GetEntryAssembly() != null)
            {
                Assembly asm = Assembly.GetEntryAssembly();
                foreach (Type t in asm.GetTypes())
                    if (!string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith(startsWith) && t.Namespace.Length > startsWith.Length)
                    {
                        string ns = t.Namespace.Substring(startsWith.Length + 1).Split('.')[0];
                        if(!res.Contains(ns))
                            res.Add(ns);
                    }

                foreach (AssemblyName asmblyName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                {
                    Assembly asm2 = Assembly.Load(asmblyName);
                    foreach (Type t in asm2.GetTypes())
                        if (!string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith(startsWith) && t.Namespace.Length > startsWith.Length)
                        {
                            string ns = t.Namespace.Substring(startsWith.Length + 1).Split('.')[0];
                            if (!res.Contains(ns))
                                res.Add(ns);
                        }
                }
            }

            return res;
        }
        public Context RootContext
        {
            get {
                Context currContext = this;
                while (currContext.parent != null)
                    currContext = currContext.parent;
                return currContext;
            }
        }
        internal static Statement CurrentStatement { get; set; }
        internal static List<string> ParsedUsing = new List<string> {"System" };
        internal static List<VariableDefinition> ParsedVariables = new List<VariableDefinition>();
        internal static List<FunctionDefinitionStatement> ParsedFunctions = new List<FunctionDefinitionStatement>();

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

        internal static void AddAssembly(Assembly assembly)
        {
            if (!AdditionalAssemblies.Contains(assembly))
                AdditionalAssemblies.Add(assembly);
        }
    }
}