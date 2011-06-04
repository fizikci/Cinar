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
    public interface IContext 
    { 
        object GetMaxOf(Expression expression);
        object GetValueOfCurrent(string fName);
        object GetVariableValue(string fName);
    }

    public abstract class Expression : ParserNode
    {
        public abstract object Calculate(IContext context);
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
        public override string ToString()
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
        public override string ToString()
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
        public override string ToString()
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
        public override string ToString()
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
        public override string ToString()
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
        public override string ToString()
        {
            return String.Format("{0}", fValue.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r"));
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
        public override string ToString()
        {
            return String.Format("{0} ? {1} : {2}", boolExp.ToString(), trueExpression.ToString(), falseExpression.ToString());
        }
    }
    public class IsNullShortCut : Expression
    {
        public IsNullShortCut(Expression nullableExp, Expression notNullExp)
        {
            this.nullableExp = nullableExp;
            this.notNullExp = notNullExp;
        }

        readonly Expression nullableExp;
        public Expression NullableExp { get { return nullableExp; } }
        readonly Expression notNullExp;
        public Expression NotNullExp { get { return notNullExp; } }

        public override object Calculate(IContext context)
        {
            object val = nullableExp.Calculate(context);
            if (val == null || val.Equals(""))
                return notNullExp.Calculate(context);
            else
                return val;
        }
        public override string ToString()
        {
            return String.Format("{0} ?? {1}", nullableExp.ToString(), notNullExp.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} AND {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} OR {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} + {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} % {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} - {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} / {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
        public override string ToString()
        {
            return String.Format("({0} * {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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
                left = Convert.ChangeType(left, right.GetType());

            if (right.GetType() == typeof(string) && left.GetType() != typeof(string))
                right = Convert.ChangeType(right, left.GetType());

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
                default:
                    break;
            }

            return true;
        }
        public override string ToString()
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
            return String.Format("({0} {2} {1})", LeftChildExpression.ToString(), RightChildExpression.ToString(), op);
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
        public override string ToString()
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
        public override string ToString()
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
        public override string ToString()
        {
            return String.Format("NOT {0}", ChildExpression.ToString());
        }
    }
}