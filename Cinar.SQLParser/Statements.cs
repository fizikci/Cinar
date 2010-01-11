﻿using System;
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
    public abstract class ParserNode
    {
        public static List<string> ReservedWords = new List<string>(new string[] { 
            "if","for","while","else","break","continue","foreach","return"
        });
    }

    
    public abstract class Statement : ParserNode
    {
        public abstract void Execute(Context context, ParserNode parentNode);
    }
    public class StatementCollection : ICollection<Statement>
    {
        readonly ICollection<Statement> fStatements;
        public StatementCollection(ICollection<Statement> statements)
        {
            if (statements == null) throw new ArgumentNullException("statements");

            fStatements = statements;
        }

        public Context Execute(Context context, ParserNode parentNode, Hashtable arguments)
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
                if (context.continueLoop)
                {
                    context.continueLoop = false;
                    break;
                }
                if (context.breakLoop)
                {
                    break;
                }
                statement.Execute(fContext, parentNode);
            }

            return fContext;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            foreach (Statement statement in this)
                sb.AppendLine(statement.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }

        #region ICollection<Statement> Members

        void ICollection<Statement>.Add(Statement item) { throw new NotSupportedException(); }
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
            return "var " + fVariable.Name + (fValue != null ? " = " + fValue : "");
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
                    fMember.SetValue(null, fValue.Calculate(context, this), context);
                    break;
                case "+=":
                    fMember.SetValue(null, new Addition(fMember, fValue).Calculate(context, this), context);
                    break;
                case "-=":
                    fMember.SetValue(null, new Subtraction(fMember, fValue).Calculate(context, this), context);
                    break;
                case "/=":
                    fMember.SetValue(null, new Division(fMember, fValue).Calculate(context, this), context);
                    break;
                case "*=":
                    fMember.SetValue(null, new Multiplication(fMember, fValue).Calculate(context, this), context);
                    break;
                case "++":
                    fMember.SetValue(null, new Addition(fMember, new IntegerConstant(1)).Calculate(context, this), context);
                    break;
                case "--":
                    fMember.SetValue(null, new Subtraction(fMember, new IntegerConstant(1)).Calculate(context, this), context);
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
                if (context.breakLoop)
                {
                    context.breakLoop = false;
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
                compare = new Variable("true");

            for (; AndExpression.ParseBool(compare.Calculate(context, this)); )
            {
                fBlock.Execute(context, this, null);
                if (context.breakLoop)
                {
                    context.breakLoop = false;
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
    }
    public class FunctionDefinitionStatement : Statement
    {
        public FunctionDefinitionStatement(string name, List<string> parameters, StatementCollection block)
        {
            if (name == null) throw new ArgumentNullException("name");

            fName = name;
            fParameters = parameters;
            fBlock = block;
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
            fFalseBlock = falseBlock;
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
    }
    public class WhileStatement : Statement
    {
        public WhileStatement(Expression condition, StatementCollection block)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (block == null) throw new ArgumentNullException("block");

            fCondition = condition;
            fBlock = block;
        }

        readonly Expression fCondition;
        public Expression Condition { get { return fCondition; } }

        readonly StatementCollection fBlock;
        public StatementCollection Block { get { return fBlock; } }

        public override void Execute(Context context, ParserNode parentNode)
        {
            while (AndExpression.ParseBool(fCondition.Calculate(context, this)))
            {
                if (context.breakLoop)
                {
                    context.breakLoop = false;
                    break;
                }

                fBlock.Execute(context, this, null);
            }
        }
        public override string ToString()
        {
            return String.Format("while({0}) {1}", fCondition.ToString(), fBlock.ToString());
        }
    }
    public class BreakStatement : Statement
    {
        public BreakStatement()
        {
        }

        public override void Execute(Context context, ParserNode parentNode)
        {
            context.breakLoop = true;
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
            context.continueLoop = true;
        }
        public override string ToString()
        {
            return "continue";
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
            context.ReturnValue = Value.Calculate(context, this);
            context.breakLoop = true;
        }
        public override string ToString()
        {
            return String.Format("return {0}", fValue.ToString());
        }
    }
    public class UsingStatement : Statement
    {
        public UsingStatement(string nameSpace)
        {
            fNamespace = nameSpace;
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
}