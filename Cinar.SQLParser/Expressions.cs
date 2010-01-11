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
    public abstract class Expression : ParserNode
    {
        public abstract object Calculate(Context context, ParserNode parentNode);
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            if (Name == "write" || Name == "print" || Name == "echo")
            {
                foreach (Expression expression in fArguments)
                    context.Output.Write(expression.Calculate(context, this));
            }
            else if (context.Functions[Name] != null)
            {
                FunctionDefinitionStatement func = (FunctionDefinitionStatement)context.Functions[Name];

                Hashtable arguments = new Hashtable();
                int i = 0;
                foreach (Expression expression in fArguments)
                {
                    object paramVal = expression.Calculate(context, this);
                    if (func.Parameters.Count > i)
                        arguments[func.Parameters[i]] = paramVal;
                    i++;
                }
                return func.Block.Execute(context, this, arguments).ReturnValue;
            }
            else
                throw new Exception("Undefined function: " + this);
            return null;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Expression expression in fArguments)
                sb.Append(expression.ToString() + ", ");
            if(sb.Length>=2)
                sb.Remove(sb.Length - 2, 2);

            return String.Format("{0}({1})", fName, sb.ToString());
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            return context.GetVariableValue(fName);
        }
        public override string ToString()
        {
            return String.Format("{0}", fName);
        }
    }
    public class VariableIncrement : Expression
    {
        public VariableIncrement(Variable var, int amount)
        {
            fVar = var;
            fAmount = amount;
        }

        readonly Variable fVar;
        public Variable Var { get { return fVar; } }

        readonly int fAmount;
        public int Amount { get { return fAmount; } }

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object res = context.GetVariableValue(fVar.Name);
            context.SetVariableValue(fVar.Name, new Addition(Var, new IntegerConstant(Amount)).Calculate(context, this));
            return res;
        }
        public override string ToString()
        {
            return String.Format("{0}{1}", fVar.Name, fAmount > 0 ? "++" : "--");
        }
    }
    public class IntegerConstant : Expression
    {
        public IntegerConstant(int value) { fValue = value; }

        readonly int fValue;
        public int Value { get { return fValue; } }

        public override object Calculate(Context context, ParserNode parentNode)
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

        public override object Calculate(Context context, ParserNode parentNode)
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
            fValue = value;
        }

        readonly string fValue;
        public string Value { get { return fValue; } }

        public override object Calculate(Context context, ParserNode parentNode)
        {
            return fValue;
        }
        public override string ToString()
        {
            return String.Format("\"{0}\"", fValue.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r"));
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            return AndExpression.ParseBool(boolExp.Calculate(context, this)) ?
                trueExpression.Calculate(context, this)
                :
                falseExpression.Calculate(context, this);
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object val = nullableExp.Calculate(context, this);
            if (val == null || val.Equals(""))
                return notNullExp.Calculate(context, this);
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

            bool leftBool = AndExpression.ParseBool(left);
            bool rightBool = AndExpression.ParseBool(right);

            return leftBool && rightBool;
        }
        public override string ToString()
        {
            return String.Format("({0} && {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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
            return String.Format("({0} || {1})", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }
    }
    public class Addition : BinaryExpression
    {
        public Addition(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object left = LeftChildExpression.Calculate(context, this);
            object right = RightChildExpression.Calculate(context, this);

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
                    break;
                case ComparisonOperator.NotEqual:
                    return leftC.CompareTo(right) != 0;
                    break;
                case ComparisonOperator.LessThan:
                    return leftC.CompareTo(right) < 0;
                    break;
                case ComparisonOperator.GreaterThan:
                    return leftC.CompareTo(right) > 0;
                    break;
                case ComparisonOperator.LessThanOrEqual:
                    return leftC.CompareTo(right) < 0 || leftC.CompareTo(right) == 0;
                    break;
                case ComparisonOperator.GreaterThanOrEqual:
                    return leftC.CompareTo(right) > 0 || leftC.CompareTo(right) == 0;
                    break;
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
                    op = "==";
                    break;
                case ComparisonOperator.NotEqual:
                    op = "!=";
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
        GreaterThanOrEqual
    }
    public class MemberAccess : BinaryExpression
    {
        public MemberAccess(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }

        public override object Calculate(Context context, ParserNode parentNode)
        {
            object res = LeftChildExpression.Calculate(context, this);

            // eğer sonuç null ve LeftExp variable ise static call olabilir
            if (res == null && LeftChildExpression is Variable)
            {
                string className = (LeftChildExpression as Variable).Name;
                Type t = context.GetType(className);
                if (t != null)
                {
                    if (RightChildExpression is Variable)
                    {
                        string propName = (RightChildExpression as Variable).Name;
                        PropertyInfo pi = t.GetProperty(propName, BindingFlags.Static | BindingFlags.Public);
                        if (pi != null)
                            res = pi.GetValue(null, null);
                        else
                            res = "";
                    }
                    else if (RightChildExpression is FunctionCall)
                    {
                        FunctionCall fc = (FunctionCall)RightChildExpression;

                        object[] paramValues = new object[fc.Arguments.Length];
                        Type[] paramTypes = new Type[fc.Arguments.Length];
                        int i = 0;
                        foreach (Expression exp in fc.Arguments)
                        {
                            paramValues[i] = exp.Calculate(context, this);
                            paramTypes[i] = paramValues[i].GetType(); //TODO: paramValues[i] null ise ne olacak?
                            i++;
                        }

                        MethodInfo mi = t.GetMethod(fc.Name, BindingFlags.Static | BindingFlags.Public, null, paramTypes, null);
                        if (mi == null)
                            throw new Exception("Undefined param types for static method: " + this);
                        else
                            res = mi.Invoke(null, paramValues);
                    }
                    else
                        res = null;
                }
                else
                    throw new Exception("Undefined static method: " + this);
            }
            else if (RightChildExpression is MemberAccess)
            {
                res = new MemberAccess(new MemberAccess(this.LeftChildExpression, (this.RightChildExpression as MemberAccess).LeftChildExpression), (this.RightChildExpression as MemberAccess).RightChildExpression).Calculate(context, this);
            }
            else if (RightChildExpression is FunctionCall)
            {
                FunctionCall fc = (FunctionCall)RightChildExpression;

                object[] paramValues = new object[fc.Arguments.Length];
                Type[] paramTypes = new Type[fc.Arguments.Length];
                int i = 0;
                foreach (Expression exp in fc.Arguments)
                {
                    paramValues[i] = exp.Calculate(context, this);
                    paramTypes[i] = paramValues[i].GetType(); //TODO: paramValues[i] null ise ne olacak?
                    i++;
                }

                //MethodInfo mi = res.GetType().GetMethod(fc.Name, paramTypes);
                MethodInfo mi = null;
                ParameterInfo[] pinfo = null;
                FindMethod(res.GetType(), fc.Name, paramTypes, out mi, out pinfo);

                if (mi == null)
                    throw new Exception("Undefined method: " + this);
                else
                {
                    for (int j = 0; j < paramTypes.Length; j++)
                        paramValues[j] = Convert.ChangeType(paramValues[j], pinfo[j].ParameterType);
                    res = mi.Invoke(res, paramValues);
                }
            }
            else
            {
                Variable var = (Variable)RightChildExpression;
                PropertyInfo pi = res.GetType().GetProperty(var.Name);
                if (pi != null)
                {
                    try
                    {
                        res = pi.GetValue(res, null);
                    }
                    catch
                    {
                        throw new Exception("Property found but couldnt be read: " + this);
                    }
                }
                else
                {
                    FieldInfo fi = res.GetType().GetField(var.Name);
                    if (fi != null)
                    {
                        try
                        {
                            res = fi.GetValue(res);
                        }
                        catch
                        {
                            throw new Exception("Field found but couldnt be read: " + this);
                        }
                    }
                    else
                    {
                        MethodInfo mi = res.GetType().GetMethod("get_Item", new Type[] { typeof(string) });
                        if (mi != null)
                        {
                            try
                            {
                                object paramObj = Convert.ChangeType(var.Name, mi.GetParameters()[0].ParameterType);
                                res = mi.Invoke(res, new object[] { paramObj });
                            }
                            catch
                            {
                                throw new Exception("Indexer found but couldnt be read: " + this);
                            }
                        }
                        else
                            throw new Exception("Undefined property, field or indexer parameter: " + this);
                    }
                }
            }
            return res;
        }
        public override string ToString()
        {
            return String.Format("{0}.{1}", LeftChildExpression.ToString(), RightChildExpression.ToString());
        }

        public static void FindMethod(Type type, string methodName, Type[] parameterTypes, out MethodInfo methodInfo, out ParameterInfo[] parameters)
        {
            MethodInfo exactMatch = null; 
            methodInfo = null;
            parameters = null;
            methodName = methodName.ToLowerInvariant();

            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo method in methods)
            {
                if (method.Name.ToLowerInvariant() == methodName)
                {
                    parameters = method.GetParameters();

                    // compare the method parameters
                    if (parameters.Length == parameterTypes.Length)
                    {
                        bool birebir = true;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (parameters[i].ParameterType != parameterTypes[i])
                            {
                                birebir = false;
                                continue; // this is not the method we're looking for
                            }

                            // if we're here, we got the right method
                            methodInfo = method;
                            break;
                        }
                        if (birebir)
                        {
                            exactMatch = method;
                            break;
                        }
                        methodInfo = method;
                        //break;
                    }
                }
            }
            methodInfo = exactMatch ?? methodInfo;
        }
        public void SetValue(object obj, object val, Context context)
        {
            if (RightChildExpression is MemberAccess)
                (RightChildExpression as MemberAccess).SetValue(LeftChildExpression.Calculate(context, this), val, context);
            else if (RightChildExpression is Variable)
            {
                if (obj == null)
                    throw new Exception(LeftChildExpression + " is null");
                MemberInfo[] members = obj.GetType().GetMember((RightChildExpression as Variable).Name);
                if(members==null || members.Length==0)
                    throw new Exception("Undefined member: " + this);
                MemberInfo mi = members[0];
                if (mi is FieldInfo)
                    (mi as FieldInfo).SetValue(obj, val);
                else if (mi is PropertyInfo)
                    (mi as PropertyInfo).SetValue(obj, val, null);
            }
            else
                throw new Exception(this + " cannot be set!");
        }
    }
    public class MethodCall : MemberAccess
    {
        public MethodCall(Expression leftChildExpression, Expression rightChildExpression)
            : base(leftChildExpression, rightChildExpression) { }
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            int res = Convert.ToInt32(ChildExpression.Calculate(context, this));
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

        public override object Calculate(Context context, ParserNode parentNode)
        {
            bool b = Convert.ToBoolean(ChildExpression.Calculate(context, this));
            return !b;
        }
        public override string ToString()
        {
            return String.Format("!{0}", ChildExpression.ToString());
        }
    }
    public class NewExpression : UnaryExpression
    {
        public NewExpression(FunctionCall childExpression)
            : base(childExpression) { }

        public override object Calculate(Context context, ParserNode parentNode)
        {
            FunctionCall fc = (FunctionCall)ChildExpression;

            string className = (string)fc.Name;
            Type t = context.GetType(className);
            if (t == null)
                throw new Exception("Undefined type: " + className);

            object[] parameters = null;
            if (fc.Arguments.Length > 0)
            {
                parameters = new object[fc.Arguments.Length];
                for (int i = 0; i < fc.Arguments.Length; i++)
                    parameters[i] = fc.Arguments[i].Calculate(context, this);
            }
            if (parameters == null)
                return Activator.CreateInstance(t);
            else
                return Activator.CreateInstance(t, parameters);
        }
        public override string ToString()
        {
            return String.Format("new {0}", ChildExpression.ToString());
        }
    }
}