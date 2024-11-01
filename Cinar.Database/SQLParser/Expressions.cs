﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Cinar.SQLParser
{
    public interface IContext 
    { 
        object GetMaxOf(Expression expression);
        object GetValueOfCurrent(string fName);
        object GetVariableValue(string fName);
    }

    public abstract class Expression : ParserNode
    {
        public bool InParanthesis { get; set; }
        public bool IsDistinct { get; set; }
        public abstract object Calculate(IContext context);

        public abstract string ToCode();

        public override string ToString()
        {
            return (IsDistinct ? "DISTINCT " : "") + (InParanthesis ? "(" + ToCode() + ")" : ToCode());
        }
    }

    public class FunctionCall : Expression
    {
        public FunctionCall(string name, Expression[] arguments)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (arguments == null) throw new ArgumentNullException("arguments");
            if (name.Length == 0) throw new ArgumentException("name cannot be an empty string.", "name");

            fName = name;
            fArguments = arguments;
        }

        readonly string fName;
        public string Name { get { return fName; } }

        readonly Expression[] fArguments;
        public Expression[] Arguments { get { return fArguments; } }

        public override object Calculate(IContext context)
        {
            if (Name == "max")
                return context.GetMaxOf(fArguments[0]);
            else
                throw new Exception("Undefined function: " + this);
        }
        public override string ToCode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Expression expression in fArguments)
                sb.Append(expression + ", ");
            if(sb.Length>=2)
                sb.Remove(sb.Length - 2, 2);

            return String.Format("{0}({1})", fName, sb.ToString());
        }
    }
    public class DbObjectName : Expression
    {
        public DbObjectName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.Length == 0) throw new ArgumentException("name cannot be an empty string.", "name");
            if (ParserNode.ReservedWords.Contains(name)) throw new ArgumentException("name cannot be a variable name", "name");

            fName = name;
        }

        readonly string fName;
        public string Name { get { return fName; } }

        public override object Calculate(IContext context)
        {
            return context.GetValueOfCurrent(fName);
        }
        public override string ToCode()
        {
            return String.Format("{0}", fName);
        }
    }
    public class Variable : Expression
    {
        public Variable(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.Length == 0) throw new ArgumentException("name cannot be an empty string.", "name");
            if (ParserNode.ReservedWords.Contains(name)) throw new ArgumentException("name cannot be a variable name", "name");

            fName = name;
        }

        readonly string fName;
        public string Name { get { return fName; } }

        public override object Calculate(IContext context)
        {
            return context.GetVariableValue(fName);
        }
        public override string ToCode()
        {
            return String.Format("{0}", fName);
        }
    }

    public class IntegerConstant : Expression
    {
        public IntegerConstant(int value) { fValue = value; }

        readonly int fValue;
        public int Value { get { return fValue; } }

        public override object Calculate(IContext context)
        {
            return fValue;
        }
        public override string ToCode()
        {
            return String.Format("{0}", fValue);
        }
    }
    public class DecimalConstant : Expression
    {
        public DecimalConstant(decimal value) { fValue = value; }

        readonly decimal fValue;
        public decimal Value { get { return fValue; } }

        public override object Calculate(IContext context)
        {
            return fValue;
        }
        public override string ToCode()
        {
            return String.Format("{0}", fValue.ToString().Replace(",","."));
        }
    }
    public class StringConstant : Expression
    {
        public StringConstant(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            fValue = value.Trim('\'');
        }

        readonly string fValue;
        public string Value { get { return fValue; } }

        public override object Calculate(IContext context)
        {
            return fValue;
        }
        public override string ToCode()
        {
            return String.Format("'{0}'", fValue.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r"));
        }
    }
    public class CaseWhen : Expression
    {
        public CaseWhen(Dictionary<Expression, Expression> caseWhen, Expression _else)
        {
            this.caseWhen = caseWhen;
            this._else = _else;
        }

        readonly Dictionary<Expression,Expression> caseWhen;
        readonly Expression _else;

        public override object Calculate(IContext context)
        {
            foreach(Expression when in caseWhen.Keys)
                if(when.Calculate(context).Equals(true))
                    return caseWhen[when].Calculate(context);

            if(_else!=null)
                return _else.Calculate(context);

            return null;
        }
        public override string ToCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CASE");
            foreach(Expression when in caseWhen.Keys)
               sb.AppendFormat(" WHEN {0} THEN {1}", when, caseWhen[when]);
            if(_else!=null)
                sb.AppendFormat(" ELSE {0}", _else);
            sb.Append(" END");
            return sb.ToString();
        }
    }
    public class IfShortCut : Expression
    {
        public IfShortCut(Expression boolExp, Expression trueExpression, Expression falseExpression)
        {
            this.boolExp = boolExp;
            this.trueExpression = trueExpression;
            this.falseExpression = falseExpression;
        }

        readonly Expression boolExp;
        public Expression BoolExp { get { return boolExp; } }
        readonly Expression trueExpression;
        public Expression TrueExpression { get { return trueExpression; } }
        readonly Expression falseExpression;
        public Expression FalseExpression { get { return falseExpression; } }

        public override object Calculate(IContext context)
        {
            return AndExpression.ParseBool(boolExp.Calculate(context)) ?
                trueExpression.Calculate(context)
                :
                falseExpression.Calculate(context);
        }
        public override string ToCode()
        {
            return String.Format("{0} ? {1} : {2}", boolExp.ToString(), trueExpression.ToString(), falseExpression.ToString());
        }
    }
    public class IsNullExpression : Expression
    {
        public IsNullExpression(Expression nullableExp, bool isNot)
        {
            this.nullableExp = nullableExp;
            this.isNot = isNot;
        }

        readonly Expression nullableExp;
        readonly bool isNot;

        public override object Calculate(IContext context)
        {
            object val = nullableExp.Calculate(context);
            return (isNot && val != null) || (!isNot && val == null);
        }
        public override string ToCode()
        {
            return String.Format("{0} IS{1} NULL", nullableExp.ToString(), isNot ? " NOT" : "");
        }
    }

    public abstract class BinaryExpression : Expression
    {
        protected BinaryExpression(Expression leftChildExpression, Expression rightChildExpression)
        {
            if (leftChildExpression == null) throw new ArgumentNullException("leftChildExpression");
            if (rightChildExpression == null) throw new ArgumentNullException("rightChildExpression");

            fLeftChildExpression = leftChildExpression;
            fRightChildExpression = rightChildExpression;
        }

        readonly Expression fLeftChildExpression;
        public Expression LeftChildExpression { get { return fLeftChildExpression; } }

        readonly Expression fRightChildExpression;
        public Expression RightChildExpression { get { return fRightChildExpression; } }
    }
    public class AndExpression : BinaryExpression
    {
        public AndExpression(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            bool leftBool = AndExpression.ParseBool(left);
            bool rightBool = AndExpression.ParseBool(right);

            return leftBool && rightBool;
        }
        public override string ToCode()
        {
            return String.Format("{0} AND {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }

        public static bool ParseBool(object obj)
        {
            if (obj == null)
                return false;

            switch (obj.GetType().Name)
            {
                case "Boolean":
                    return (bool)obj;
                case "Int16":
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Double":
                case "Float":
                case "Single":
                    return Convert.ToDecimal(obj) > Decimal.Zero;
                case "String":
                    return obj.ToString().Trim() != "";
                default:
                    return true;
            }
        }
    }
    public class OrExpression : BinaryExpression
    {
        public OrExpression(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            bool leftBool = AndExpression.ParseBool(left);
            bool rightBool = AndExpression.ParseBool(right);

            if (leftBool == true && rightBool == false)
                return left;
            else if (leftBool == false && rightBool == true)
                return right;
            else
                return leftBool || rightBool;
        }
        public override string ToCode()
        {
            return String.Format("{0} OR {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class BinaryOrExpression : BinaryExpression
    {
        public BinaryOrExpression(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left is int && right is int)
                return (int)left | (int)right;
            else if (left is bool && right is bool)
                return (bool)left | (bool)right;
            else
                throw new Exception("Argument type error for binary OR (|) expression");
        }
        public override string ToCode()
        {
            return String.Format("{0} | {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class CastAsTypeExpression : Expression
    {
        private Expression leftChildExpression;
        private string typeName;
        public CastAsTypeExpression(Expression leftChildExpression, string typeName)
        {
            this.leftChildExpression = leftChildExpression;
            this.typeName = typeName.ToLower();
        }

        public override object Calculate(IContext context)
        {
            object left = leftChildExpression.Calculate(context);

            return null;
        }
        public override string ToCode()
        {
            return String.Format("CAST({0} AS {1})", leftChildExpression.ToString(), typeName);
        }
    }
    public class BinaryAndExpression : BinaryExpression
    {
        public BinaryAndExpression(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left is int && right is int)
                return (int)left & (int)right;
            else if (left is bool && right is bool)
                return (bool)left & (bool)right;
            else
                throw new Exception("Argument type error for binary AND (&) expression");
        }
        public override string ToCode()
        {
            return String.Format("{0} & {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Addition : BinaryExpression
    {
        public Addition(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left == null && right == null)
            {
                return "";
            }

            if (left == null)
            {
                if (right.GetType() == typeof(string)) left = ""; else left = 0;
            }

            if (right == null)
            {
                if (left.GetType() == typeof(string)) right = ""; else right = 0;
            }

            if (left.GetType() == typeof(string) || right.GetType() == typeof(string))
                return left.ToString() + right.ToString();
            else if (left.GetType() == typeof(decimal) || right.GetType() == typeof(decimal))
                return Convert.ToDecimal(left) + Convert.ToDecimal(right);
            else if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
                return (int)left + (int)right;
            else
                return left.ToString() + right.ToString();
        }
        public override string ToCode()
        {
            return String.Format("{0} + {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Mod : BinaryExpression
    {
        public Mod(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            left = left ?? 0;
            right = right ?? 0;

            if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
                return (int)left % (int)right;
            else
                return Convert.ToDecimal(left) % Convert.ToDecimal(right);
        }
        public override string ToCode()
        {
            return String.Format("{0} % {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Subtraction : BinaryExpression
    {
        public Subtraction(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left == null && right == null)
            {
                return 0;
            }

            if (left == null)
            {
                if (right.GetType() == typeof(int))
                    left = 0;
                else
                    left = 0m;
            }

            if (right == null)
            {
                if (left.GetType() == typeof(int))
                    right = 0;
                else 
                    right = 0m;
            }

            if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
                return (int)left - (int)right;
            else
                return Convert.ToDecimal(left) - Convert.ToDecimal(right);
        }
        public override string ToCode()
        {
            return String.Format("{0} - {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Division : BinaryExpression
    {
        public Division(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left == null && right == null)
            {
                return 0m;
            }

            if (left == null)
            {
                    left = 0m;
            }

            if (right == null)
            {
                    right = 1m;
            }

            return Convert.ToDecimal(left) / Convert.ToDecimal(right);
        }
        public override string ToCode()
        {
            return String.Format("{0} / {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Multiplication : BinaryExpression
    {
        public Multiplication(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            left = left ?? 0;
            right = right ?? 0;

            if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
                return (int)left * (int)right;
            else
                return Convert.ToDecimal(left) * Convert.ToDecimal(right);
        }
        public override string ToCode()
        {
            return String.Format("{0} * {1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Comparison : BinaryExpression
    {
        public Comparison(ComparisonOperator comparisonOperator, Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression)
        {
            fOperator = comparisonOperator;
        }

        readonly ComparisonOperator fOperator;
        public ComparisonOperator Operator { get { return fOperator; } }

        public override object Calculate(IContext context)
        {
            object left = LeftChildExpression.Calculate(context);
            object right = RightChildExpression.Calculate(context);

            if (left == null && right == null)
            {
                left = right = 0;
            }

            if (left == null)
            {
                if (right.GetType() == typeof(string)) left = ""; else left = 0;
            }

            if (right == null)
            {
                if (left.GetType() == typeof(string)) right = ""; else right = 0;
            }

            if (left.GetType() == typeof(string) && right.GetType() != typeof(string))
                left = left.ChangeType(right.GetType());

            if (right.GetType() == typeof(string) && left.GetType() != typeof(string))
                right = right.ChangeType(left.GetType());

            IComparable leftC = (IComparable)(left ?? 0);
            right = right ?? 0d;

            switch (Operator)
            {
                case ComparisonOperator.None:
                    break;
                case ComparisonOperator.Equal:
                    return leftC.CompareTo(right) == 0;
                case ComparisonOperator.NotEqual:
                    return leftC.CompareTo(right) != 0;
                case ComparisonOperator.LessThan:
                    return leftC.CompareTo(right) < 0;
                case ComparisonOperator.GreaterThan:
                    return leftC.CompareTo(right) > 0;
                case ComparisonOperator.LessThanOrEqual:
                    return leftC.CompareTo(right) < 0 || leftC.CompareTo(right) == 0;
                case ComparisonOperator.GreaterThanOrEqual:
                    return leftC.CompareTo(right) > 0 || leftC.CompareTo(right) == 0;
                case ComparisonOperator.Like:
                    string q = right.ToString();
                    string str = left.ToString();
                    if(q.StartsWith("%"))
                    {
                        if(q.EndsWith("%"))
                            return str.Contains(q.Replace("%",""));
                        return str.EndsWith(q.Replace("%",""));
                    }
                    return str.StartsWith(q.Replace("%",""));
                default:
                    break;
            }

            return true;
        }
        public override string ToCode()
        {
            string op = "";
            switch (Operator)
            {
                case ComparisonOperator.None:
                    break;
                case ComparisonOperator.Equal:
                    op = "=";
                    break;
                case ComparisonOperator.NotEqual:
                    op = "<>";
                    break;
                case ComparisonOperator.LessThan:
                    op = "<";
                    break;
                case ComparisonOperator.GreaterThan:
                    op = ">";
                    break;
                case ComparisonOperator.LessThanOrEqual:
                    op = "<=";
                    break;
                case ComparisonOperator.GreaterThanOrEqual:
                    op = ">=";
                    break;
                default:
                    break;
            }
            return String.Format("{0} {2} {1}", LeftChildExpression.ToString(), RightChildExpression.ToString(), op);
        }
    }
    public enum ComparisonOperator
    {
        None = 0,
        Equal,
        NotEqual,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Like,
        In
    }
    public class MemberAccess : BinaryExpression
    {
        public MemberAccess(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(IContext context)
        {
            throw new NotImplementedException();
        }
        public override string ToCode()
        {
            return String.Format("{0}.{1}", LeftChildExpression, RightChildExpression);
        }
    }

    public abstract class UnaryExpression : Expression
    {
        protected UnaryExpression(Expression childExpression)
        {
            if (childExpression == null) throw new ArgumentNullException("childExpression");

            fChildExpression = childExpression;
        }

        readonly Expression fChildExpression;
        public Expression ChildExpression { get { return fChildExpression; } }
    }
    public class Negation : UnaryExpression
    {
        public Negation(Expression childExpression)
            : base(childExpression) { }

        public override object Calculate(IContext context)
        {
            int res = Convert.ToInt32(ChildExpression.Calculate(context));
            return -1 * res;
        }
        public override string ToCode()
        {
            return String.Format("-{0}", ChildExpression.ToString());
        }
    }
    public class NotExpression : UnaryExpression
    {
        public NotExpression(Expression childExpression)
            : base(childExpression) { }

        public override object Calculate(IContext context)
        {
            bool b = Convert.ToBoolean(ChildExpression.Calculate(context));
            return !b;
        }
        public override string ToCode()
        {
            return String.Format("NOT {0}", ChildExpression.ToString());
        }
    }

    public class SelectExpression : Expression
    {
        public bool All { get; set; }
        public bool Distinct { get; set; }
        public ListSelectPart Select { get; set; }
        public ListJoin From { get; set; }
        public Expression Where { get; set; }
        public List<Order> OrderBy { get; set; }
        public Expression Having { get; set; }
        public List<Expression> GroupBy { get; set; }
        public Expression Limit { get; set; }
        public Expression Offset { get; set; }

        public SelectExpression()
        {
            Select = new ListSelectPart();
            From = new ListJoin();
            OrderBy = new List<Order>();
            GroupBy = new List<Expression>();
        }
        public override object Calculate(IContext context) { return null; }
        public override string ToCode()
        {
            string res = "SELECT";
            if (Distinct)
                res += " DISTINCT";
            if (this.Limit != null)
                res += " TOP " + this.Limit;
            res += "\r\n\t" + string.Join(",\r\n\t", this.Select.Select(s => s.ToString()).ToArray());
            if (this.From.Count > 0)
                res += "\r\nFROM\r\n" + this.From;
            if (this.Where != null)
                res += "\r\nWHERE\r\n\t" + this.Where;
            if (this.GroupBy.Count > 0)
                res += "\r\nGROUP BY\r\n\t" + string.Join(",\r\n\t", this.GroupBy.Select(g => g.ToString()).ToArray());
            if (this.Having != null)
                res += "\r\nHAVING\r\n\t" + this.Having;
            if (this.OrderBy.Count > 0)
                res += "\r\nORDER BY\r\n\t" + string.Join(",\r\n\t", this.OrderBy.Select(o => o.ToString()).ToArray());
            if (this.Offset != null)
                res += " OFFSET " + this.Offset;
            return res;
        }
    }

    public class SelectPart
    {
        public Expression Field { get; set; }
        public string Alias { get; set; }

        public override string ToString()
        {
            string fieldName = Field.ToString();
            if (Field is DbObjectName)
                fieldName = Field.ToString();
            return fieldName + ((!string.IsNullOrEmpty(Alias) && Alias != Field.ToString()) ? " AS " + Alias : "");
        }
    }
    public class ListSelectPart : List<SelectPart>
    {
        public SelectPart this[string aliasName]
        {
            get
            {
                foreach (SelectPart s in this)
                    if (s.Alias == aliasName)
                        return s;
                return null;
            }
        }

        public int IndexOf(string aliasName)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].Alias == aliasName)
                    return i;
            return -1;
        }
    }
    public class ListJoin : List<Join>
    {
        public Join this[string aliasName]
        {
            get
            {
                foreach (Join join in this)
                    if (join.Alias == aliasName)
                        return join;
                return null;
            }
        }
        public override string ToString()
        {
            this[0].TableName = this[0].TableName;
            this[0].Alias = this[0].Alias ?? this[0].Alias;
            string res = "\t" + this[0].TableName + ((!string.IsNullOrEmpty(this[0].Alias) && this[0].Alias != this[0].TableName) ? " AS " + this[0].Alias : "") + "\r\n";
            for (int i = 1; i < this.Count; i++)
                res += "\t" + this[i].ToString() + "\r\n";
            res = res.TrimEnd();
            return res;
        }
    }
    public class Join
    {
        public JoinType JoinType { get; set; }
        public Expression SelectExpression { get; set; }
        public string TableName { get; set; }
        public string Alias { get; set; }
        public Dictionary<string, Expression> CinarTableOptions { get; set; }
        public Expression On { get; set; }

        public Join()
        {
            CinarTableOptions = new Dictionary<string, Expression>();
        }

        public override string ToString()
        {
            return JoinType.ToString().ToUpperInvariant() + " JOIN " + TableName + ((!string.IsNullOrEmpty(Alias) && Alias != TableName) ? " AS " + Alias : "") + (On != null ? " ON " + On : "");
        }

        public string GetTableReference(ListJoin from)
        {
            var mainTableAlias = from.FirstOrDefault().Alias;

            var comp = this.On as Comparison;
            if (comp != null && comp.Operator == ComparisonOperator.Equal) // find relation in ON expression
            {
                var left = comp.LeftChildExpression as MemberAccess;
                var right = comp.RightChildExpression as MemberAccess;

                if (left != null && right != null)
                {
                    if (left.LeftChildExpression is Variable && (left.LeftChildExpression as Variable).Name.ToUpper() == mainTableAlias.ToUpper())
                        return (left.RightChildExpression as Variable).Name + " => " + TableName;
                    if (right.LeftChildExpression is Variable && (right.LeftChildExpression as Variable).Name.ToUpper() == mainTableAlias.ToUpper())
                        return (right.RightChildExpression as Variable).Name + " => " + TableName;
                }
            }
            else // find relation in WHERE clause
            {

            }

            return "";
        }
    }

    public enum JoinType
    {
        Inner, // cartesian product
        Left, // all left table rows and joined right table row with nulls on right if not joined
        Right, // all right table rows and joined left table row with nulls on left if not joined
        Full, // all right & left table rows with nulls on both sides if not joined
        Cross // cartesian product
    }

    public class Order
    {
        public Expression By { get; set; }
        public bool Desc { get; set; }

        public override string ToString()
        {
            string fieldName = By.ToString();
            if (By is DbObjectName)
                fieldName = fieldName;
            return fieldName + (Desc ? " DESC" : "");
        }
    }

    public class ListExpression : Expression
    {
        readonly List<Expression> items;
        public ListExpression(List<Expression> items)
        {
            this.items = items;
        }
        public override object Calculate(IContext context) 
        {
            return items.Select(e=>e.Calculate(context)).ToList();
        }
        public override string ToCode()
        {
            return "(" + items.Select(e=>e.ToString()).StringJoin(", ") + ")";
        }
    }

    public class InExpression : Expression
    {
        readonly Expression item;
        readonly ListExpression list;
        public InExpression(Expression item, ListExpression list)
        {
            this.item = item;
            this.list = list;
        }
        public override object Calculate(IContext context)
        {
            return ((List<object>)list.Calculate(context)).Select(e=>e.ToString()).Contains(item.Calculate(context).ToString());
        }
        public override string ToCode()
        {
            return item + " IN " + list;
        }
    }

}