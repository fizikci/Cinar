using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Web;

namespace Cinar.Scripting
{
    public abstract class ParserNode
    {
        public static List<string> ReservedWords = new List<string>(new string[] { 
            "if","for","while","else","break","continue","foreach","return","throw","try","catch"
        });
    }

    public abstract class Statement : ParserNode
    {
        public abstract void Execute(Context context, ParserNode parentNode);
        internal int LineNumber { get; set; }
        internal int LastLineNumber { get; set; }
        internal int ColumnNumber { get; set; }
        internal int LastColumnNumber { get; set; }
        internal StatementCollection ParentBlock { get; set; }

        public virtual StatementCollection SubStatements1 { get { return null; } }
        public virtual StatementCollection SubStatements2 { get { return null; } }

        internal bool canBeDebugged() 
        {
            return !(this is FunctionDefinitionStatement || this is TryCatchStatement || this is UsingStatement);
        }
    }
    public class StatementCollection : ICollection<Statement>
    {
        readonly ICollection<Statement> fStatements;
        public bool HasBrace;
        internal Statement ParentStatement;
        internal int BlockIndent 
        { 
            get 
            {
                int res = 0;
                StatementCollection coll = this;
                while (coll != null)
                {
                    res++;
                    coll = this.ParentStatement==null ? null : this.ParentStatement.ParentBlock;
                }
                return res;
            } 
        }

        public StatementCollection(ICollection<Statement> statements, bool hasBrace)
        {
            if (statements == null) throw new ArgumentNullException("statements");

            fStatements = statements;
            foreach (Statement s in fStatements) s.ParentBlock = this;
            HasBrace = hasBrace;
        }

        public void Execute(Context context, ParserNode parentNode, Hashtable arguments)
        {
            Context fContext = new Context();
            fContext.parent = context;
            fContext.Output = context.Output;
            fContext.Functions = context.Functions;
            fContext.Using = context.Using;
            if (arguments != null)
                fContext.Variables = arguments;

            foreach (Statement statement in this)
            {
                if (Context.continueLoop)
                {
                    Context.continueLoop = false;
                    break;
                }

                if (Context.breakLoop)
                    break;

                if (Context.ReturnValue != null)
                {
                    if(!(parentNode is FunctionCall))
                        break;
                    return;// fContext;
                }

                Context.CurrentStatement = statement;

                if (Context.debugRunToLine > 0)
                {
                    Context.debugging = Context.debugRunToLine == statement.LineNumber;
                    if(Context.debugging)
                        Context.debugRunToLine = 0;
                }

                if (Context.debugging && Context.debugRunToLine == 0 && HttpContext.Current == null)
                {
                    if (Context.debuggerWindow != null)
                        Context.debuggerWindow.SetMarker(fContext);

                    if (!statement.canBeDebugged())
                        Context.debugContinue = true;

                    while (!Context.debugContinue)
                        Application.DoEvents();

                    Context.debugContinue = false;

                    try
                    {
                        statement.Execute(fContext, parentNode);
                    }
                    catch (Exception ex)
                    {
                        if (Context.debugging)
                        {
                            MessageBox.Show((ex.InnerException != null ? ex.InnerException.Message : ex.Message), "Cinar Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Context.debugging = false;
                        }
                        throw ex;
                    }
                }
                else
                {
                    try {
                        statement.Execute(fContext, parentNode);
                    }
                    catch (Exception ex)
                    {
                        //context.Interpreter.ExecutionSuccessful = false;
                        context.Output.Write(ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "") + " at line " + (Context.CurrentStatement.LineNumber + 1));
                    }
                }
            }

            //return fContext;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(HasBrace)
                sb.AppendLine("{");
            foreach (Statement statement in this)
                sb.AppendLine(statement.ToString());
            if(HasBrace)
                sb.AppendLine("}");
            return sb.ToString();
        }

        #region ICollection<Statement> Members
        internal void AddRange(ICollection<Statement> statements)
        {
            if (statements == null)
                return;

            foreach(var item in statements)
                fStatements.Add(item);
        }

        void ICollection<Statement>.Add(Statement item) { fStatements.Add(item); item.ParentBlock = this; }
        void ICollection<Statement>.Clear() { throw new NotSupportedException(); }
        bool ICollection<Statement>.Remove(Statement item) { throw new NotSupportedException(); }

        public bool Contains(Statement item) { return fStatements.Contains(item); }

        public void CopyTo(Statement[] array, int arrayIndex) { fStatements.CopyTo(array, arrayIndex); }

        public int Count { get { return fStatements.Count; } }

        public bool IsReadOnly { get { return true; } }

        #endregion

        #region IEnumerable<Statement> Members

        public IEnumerator<Statement> GetEnumerator() { return fStatements.GetEnumerator(); }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return fStatements.GetEnumerator(); }

        #endregion
    }

    public class Assignment : Statement
    {
        public Assignment(Variable variable, string op, Expression value)
        {
            if (variable == null) throw new ArgumentNullException("variable");

            fVariable = variable;
            fOp = op;
            fValue = value;
        }

        readonly Variable fVariable;
        public Variable Variable { get { return fVariable; } }

        readonly string fOp;
        public string Op { get { return fOp; } }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            switch (Op)
            {
                case "=":
                    context.SetVariableValue(fVariable.Name, fValue.Calculate(context, this));
                    break;
                case "+=":
                    context.SetVariableValue(fVariable.Name, new Addition(fVariable, fValue).Calculate(context, this));
                    break;
                case "-=":
                    context.SetVariableValue(fVariable.Name, new Subtraction(fVariable, fValue).Calculate(context, this));
                    break;
                case "/=":
                    context.SetVariableValue(fVariable.Name, new Division(fVariable, fValue).Calculate(context, this));
                    break;
                case "*=":
                    context.SetVariableValue(fVariable.Name, new Multiplication(fVariable, fValue).Calculate(context, this));
                    break;
                case "++":
                    context.SetVariableValue(fVariable.Name, new Addition(fVariable, new IntegerConstant(1)).Calculate(context, this));
                    break;
                case "--":
                    context.SetVariableValue(fVariable.Name, new Subtraction(fVariable, new IntegerConstant(1)).Calculate(context, this));
                    break;
            }
        }

        public override string ToString()
        {
            return fVariable.Name + " " + fOp + " " + fValue;
        }
    }
    public class VariableDefinition : Statement
    {
        public VariableDefinition(Variable variable, Expression value)
        {
            if (variable == null) throw new ArgumentNullException("variable");

            fVariable = variable;
            fValue = value;

            Context.ParsedVariables.Add(this);
        }

        readonly Variable fVariable;
        public Variable Variable { get { return fVariable; } }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            if (context.Variables.ContainsKey(fVariable.Name))
                throw new Exception("Already defined variable: " + fVariable.Name);

            if (fValue == null)
                context.Variables[fVariable.Name] = null;
            else
                context.Variables[fVariable.Name] = fValue.Calculate(context, this);
        }
        public override string ToString()
        {
            return fVariable.Type + " " + fVariable.Name + (fValue != null ? " = " + fValue : "");
        }
    }
    public class MemberAssignment : Statement
    {
        public MemberAssignment(MemberAccess member, string op, Expression value)
        {
            if (member == null) throw new ArgumentNullException("variable");

            fMember = member;
            fOp = op;
            fValue = value;
        }

        readonly MemberAccess fMember;
        public MemberAccess Member { get { return fMember; } }

        readonly string fOp;
        public string Op { get { return fOp; } }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            switch (Op)
            {
                case "=":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), fValue.Calculate(context, this), context);
                    break;
                case "+=":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Addition(fMember, fValue).Calculate(context, this), context);
                    break;
                case "-=":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Subtraction(fMember, fValue).Calculate(context, this), context);
                    break;
                case "/=":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Division(fMember, fValue).Calculate(context, this), context);
                    break;
                case "*=":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Multiplication(fMember, fValue).Calculate(context, this), context);
                    break;
                case "++":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Addition(fMember, new IntegerConstant(1)).Calculate(context, this), context);
                    break;
                case "--":
                    fMember.SetValue(fMember.LeftChildExpression.Calculate(context, this), new Subtraction(fMember, new IntegerConstant(1)).Calculate(context, this), context);
                    break;
            }
        }
        public override string ToString()
        {
            return fMember + " " + fOp + " " + fValue;
        }
    }
    public class ForEachStatement : Statement
    {
        public ForEachStatement(Variable item, Expression collection, StatementCollection block)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (collection == null) throw new ArgumentNullException("collection");
            if (block == null) throw new ArgumentNullException("block");

            fItem = item;
            fCollection = collection;
            fBlock = block;
            fBlock.ParentStatement = this;
        }

        readonly Variable fItem;
        public Variable Item { get { return fItem; } }

        readonly Expression fCollection;
        public Expression Collection { get { return fCollection; } }

        readonly StatementCollection fBlock;
        public StatementCollection Block { get { return fBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            object collection = fCollection.Calculate(context, this);

            foreach (object item in (IEnumerable)collection)
            {
                if (Context.breakLoop)
                {
                    Context.breakLoop = false;
                    break;
                }

                Hashtable arguments = new Hashtable();
                arguments[fItem.Name] = item;

                fBlock.Execute(context, this, arguments);
            }
        }
        public override string ToString()
        {
            return String.Format("foreach({0} in {1}) {2}", fItem.Name, fCollection.ToString(), fBlock.ToString());
        }

        public override StatementCollection SubStatements1
        {
            get { return fBlock; }
        }
    }
    public class ForStatement : Statement
    {
        public ForStatement(Statement start, Expression compare, Statement end, StatementCollection block)
        {
            //if (start == null) throw new ArgumentNullException("startValue");
            //if (compare == null) throw new ArgumentNullException("compare");
            //if (end == null) throw new ArgumentNullException("endValue");
            if (block == null) throw new ArgumentNullException("block");

            fStart = start;
            fCompare = compare;
            fEnd = end;
            fBlock = block;
            fBlock.ParentStatement = this;
        }

        readonly Statement fStart;
        public Statement Start { get { return fStart; } }

        readonly Expression fCompare;
        public Expression Compare { get { return fCompare; } }

        readonly Statement fEnd;
        public Statement End { get { return fEnd; } }

        readonly StatementCollection fBlock;
        public StatementCollection Block { get { return fBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            if(fStart!=null)
                fStart.Execute(context, this);
            Expression compare = fCompare;
            if (compare == null)
                compare = new Variable("true", "var");

            for (; AndExpression.ParseBool(compare.Calculate(context, this)); )
            {
                fBlock.Execute(context, this, null);
                if (Context.breakLoop)
                {
                    Context.breakLoop = false;
                    break;
                }

                if (fEnd != null)
                    fEnd.Execute(context, this);
            }
        }
        public override string ToString()
        {
            return String.Format("for({0}; {1}; {2}) {3}", fStart.ToString(), fCompare.ToString(), fEnd.ToString(), fBlock.ToString());
        }

        public override StatementCollection SubStatements1
        {
            get { return fBlock; }
        }
    }
    public class FunctionDefinitionStatement : Statement
    {
        public FunctionDefinitionStatement(string name, List<string> parameters, StatementCollection block)
        {
            if (name == null) throw new ArgumentNullException("name");

            fName = name;
            fParameters = parameters;
            fBlock = block;
            fBlock.ParentStatement = this;

            Context.ParsedFunctions.Add(this);
        }

        readonly string fName;
        public string Name { get { return fName; } }

        readonly List<string> fParameters;
        public List<string> Parameters { get { return fParameters; } }

        readonly StatementCollection fBlock;
        public StatementCollection Block { get { return fBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            context.Functions[this.Name] = this;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string p in fParameters)
                sb.Append(p + ", ");
            if(sb.Length>0)
                sb.Remove(sb.Length - 2, 2);

            return String.Format("function {0}({1}) {2}", fName, sb.ToString(), fBlock.ToString());
        }

        public override StatementCollection SubStatements1
        {
            get { return fBlock; }
        }
    }
    public class FunctionCallStatement : Statement
    {
        public FunctionCallStatement(FunctionCall functionCall)
        {
            if (functionCall == null) throw new ArgumentNullException("functionCall");

            fFunctionCall = functionCall;
        }

        readonly FunctionCall fFunctionCall;
        public FunctionCall FunctionCall { get { return fFunctionCall; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            this.FunctionCall.Calculate(context, this);
        }
        public override string ToString()
        {
            return fFunctionCall.ToString();
        }
    }
    public class MethodCallStatement : Statement
    {
        public MethodCallStatement(MemberAccess memberAccess)
        {
            if (memberAccess == null) throw new ArgumentNullException("memberAccess");

            fMemberAccess = memberAccess;
        }

        readonly MemberAccess fMemberAccess;
        public MemberAccess MemberAccess { get { return fMemberAccess; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            this.MemberAccess.Calculate(context, this);
        }
        public override string ToString()
        {
            return fMemberAccess.ToString();
        }
    }
    public class IfStatement : Statement
    {
        public IfStatement(Expression condition, StatementCollection trueBlock, StatementCollection falseBlock)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (trueBlock == null) throw new ArgumentNullException("trueBlock");
            if (falseBlock == null) throw new ArgumentNullException("falseBlock");

            fCondition = condition;
            fTrueBlock = trueBlock;
            fTrueBlock.ParentStatement = this;
            fFalseBlock = falseBlock;
            fFalseBlock.ParentStatement = this;
        }

        readonly Expression fCondition;
        public Expression Condition { get { return fCondition; } }

        readonly StatementCollection fTrueBlock;
        public StatementCollection TrueBlock { get { return fTrueBlock; } }

        readonly StatementCollection fFalseBlock;
        public StatementCollection FalseBlock { get { return fFalseBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            if (AndExpression.ParseBool(fCondition.Calculate(context, this)))
                TrueBlock.Execute(context, this, null);
            else
                FalseBlock.Execute(context, this, null);
        }
        public override string ToString()
        {
            return String.Format("if({0}) {1} else {2}", fCondition.ToString(), fTrueBlock.ToString(), fFalseBlock.ToString());
        }

        public override StatementCollection SubStatements1
        {
            get { return fTrueBlock; }
        }

        public override StatementCollection SubStatements2
        {
            get { return fFalseBlock; }
        }
    }
    public class TryCatchStatement : Statement
    {
        public TryCatchStatement(StatementCollection tryBlock, string exVariableName, StatementCollection catchBlock)
        {
            if (tryBlock == null) throw new ArgumentNullException("tryBlock");
            if (catchBlock == null) throw new ArgumentNullException("catchBlock");

            fTryBlock = tryBlock;
            fTryBlock.ParentStatement = this;
            fExVariableName = exVariableName;
            fCatchBlock = catchBlock;
            fCatchBlock.ParentStatement = this;
        }

        readonly string fExVariableName;
        public string ExVariableName { get { return fExVariableName; } }

        readonly StatementCollection fTryBlock;
        public StatementCollection TryBlock { get { return fTryBlock; } }

        readonly StatementCollection fCatchBlock;
        public StatementCollection CatchBlock { get { return fCatchBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            try
            {
                TryBlock.Execute(context, this, null);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ExVariableName))
                    context.Variables[ExVariableName] = ex is CinarScriptException ? (ex as CinarScriptException).Value : ex;
                CatchBlock.Execute(context, this, null);
            }
        }
        public override string ToString()
        {
            return String.Format("try {0} catch {1}", fTryBlock, fCatchBlock);
        }

        public override StatementCollection SubStatements1
        {
            get { return fTryBlock; }
        }
        public override StatementCollection SubStatements2
        {
            get { return fCatchBlock; }
        }
    }
    public class WhileStatement : Statement
    {
        public WhileStatement(Expression condition, StatementCollection block)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (block == null) throw new ArgumentNullException("block");

            fCondition = condition;
            fBlock = block;
            fBlock.ParentStatement = this;
        }

        readonly Expression fCondition;
        public Expression Condition { get { return fCondition; } }

        readonly StatementCollection fBlock;
        public StatementCollection Block { get { return fBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            while (AndExpression.ParseBool(fCondition.Calculate(context, this)))
            {
                if (Context.breakLoop)
                {
                    Context.breakLoop = false;
                    break;
                }

                fBlock.Execute(context, this, null);
            }
        }
        public override string ToString()
        {
            return String.Format("while({0}) {1}", fCondition.ToString(), fBlock.ToString());
        }

        public override StatementCollection SubStatements1
        {
            get { return fBlock; }
        }
    }
    public class BreakStatement : Statement
    {
        public BreakStatement()
        {
        }

        public override void Execute(Context context, ParserNode parentNode)
        {
            Context.breakLoop = true;
        }
        public override string ToString()
        {
            return "break";
        }
    }
    public class ContinueStatement : Statement
    {
        public ContinueStatement()
        {
        }

        public override void Execute(Context context, ParserNode parentNode)
        {
            Context.continueLoop = true;
        }
        public override string ToString()
        {
            return "continue";
        }
    }
    public class DebuggerStatement : Statement
    {
        public DebuggerStatement()
        {
        }

        public override void Execute(Context context, ParserNode parentNode)
        {
            if (HttpContext.Current==null && MessageBox.Show("Would you like to debug current execution?", "Cinar Script", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Context.debugging = true;
                CinarDebugger cd = new CinarDebugger(context);
                cd.WindowState = FormWindowState.Maximized;
                Context.debuggerWindow = cd;
                cd.Show();
            }
        }
        public override string ToString()
        {
            return "debugger";
        }
    }
    public class ReturnStatement : Statement
    {
        public ReturnStatement(Expression value)
        {
            fValue = value;
        }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            Context.ReturnValue = Value.Calculate(context, this);
        }
        public override string ToString()
        {
            return String.Format("return {0}", fValue.ToString());
        }
    }
    public class ThrowStatement : Statement
    {
        public ThrowStatement(Expression value)
        {
            fValue = value;
        }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            CinarScriptException ex = new CinarScriptException();
            ex.Value = Value.Calculate(context, this);
            throw ex;
        }

        public override string ToString()
        {
            return String.Format("throw {0}", fValue);
        }
    }
    public class UsingStatement : Statement
    {
        public UsingStatement(string nameSpace)
        {
            fNamespace = nameSpace;

            Context.ParsedUsing.Add(nameSpace);
        }

        readonly string fNamespace;
        public string Namespace { get { return fNamespace; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            context.Using.Add(this.Namespace);
        }
        public override string ToString()
        {
            return String.Format("using {0}", fNamespace);
        }
    }
    public class EchoBlockStatement : Statement
    {
        public EchoBlockStatement(string val)
        {
            fVal = val;
        }

        readonly string fVal;
        public string Value { get { return fVal; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            context.Output.Write(Value);
        }
        public override string ToString()
        {
            return Value;
        }
    }
    public class ShortCutEchoStatement : Statement
    {
        public ShortCutEchoStatement(Expression value)
        {
            fValue = value;
        }

        readonly Expression fValue;
        public Expression Value { get { return fValue; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            context.Output.Write(Value.Calculate(context, this));
        }

        public override string ToString()
        {
            return String.Format("$={0}$", fValue);
        }
    }

    [Serializable]
    public class CinarScriptException : Exception
    {
        public object Value { get; set; }

        public CinarScriptException()
        {
        }

        public CinarScriptException(string message) : base(message)
        {
        }

        public CinarScriptException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CinarScriptException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}