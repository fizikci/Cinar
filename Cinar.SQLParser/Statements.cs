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
    public abstract class ParserNode
    {
        public static List<string> ReservedWords = new List<string>(new string[] { 
            "select","from","as","where","or","and","like","not","in","between","order","by","asc","desc","limit","offset","group","having"
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

    public class SelectStatement : Statement
    {
        public ListSelect Select { get; set; }
        public ListJoin From { get; set; }
        public Expression Where { get; set; }
        public List<Order> OrderBy { get; set; }
        public Expression Having { get; set; }
        public List<Expression> GroupBy { get; set; }
        
        public override void Execute(Context context, ParserNode parentNode)
        {
            throw new NotImplementedException();
        }
    }

    public class ListSelect : List<Select> { }
    public class Select
    {
        public Expression Field { get; set; }
        public string Alias { get; set; }
    }
    public class ListJoin : List<Join>
    {
        public Join this[string aliasName] {
            get {
                foreach (Join join in this)
                    if (join.Alias == aliasName)
                        return join;
                return null;
            }
        }
    }
    public class Join
    {
        public JoinType JoinType { get; set; }
        public string TableName { get; set; }
        public string Alias { get; set; }
        public Expression On { get; set; }
    }

    public enum JoinType
    {
        Inner, Left, Right, Outer
    }

    public class Order
    {
        public Expression By { get; set; }
        public bool Desc { get; set; }
    }

}