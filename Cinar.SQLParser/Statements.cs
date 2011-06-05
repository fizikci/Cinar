using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cinar.Database;

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
    }

    public class StatementCollection : ICollection<Statement>
    {
        readonly ICollection<Statement> fStatements;
        public StatementCollection(ICollection<Statement> statements)
        {
            if (statements == null) throw new ArgumentNullException("statements");

            fStatements = statements;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Statement statement in this)
                sb.AppendLine(statement.ToString());
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

    #region select statement
    public class SelectStatement : Statement
    {
        public bool All { get; set; }
        public bool Distinct { get; set; }
        public ListSelect Select { get; set; }
        public ListJoin From { get; set; }
        public Expression Where { get; set; }
        public List<Order> OrderBy { get; set; }
        public Expression Having { get; set; }
        public List<Expression> GroupBy { get; set; }
        public Expression Limit { get; set; }
        public Expression Offset { get; set; }

        public SelectStatement() 
        {
            Select = new ListSelect();
            From = new ListJoin();
            OrderBy = new List<Order>();
            GroupBy = new List<Expression>();
        }

        public override string ToString()
        {
            string res = "SELECT\r\n\t" + string.Join(",\r\n\t", this.Select.Select(s => s.ToString()).ToArray());
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
            if(this.Limit!=null)
                res += "\r\nLIMIT " + this.Limit;
            if (this.Offset != null)
                res += " OFFSET " + this.Offset;
            res += ";\r\n";
            return res;
        }
    }

    public class Select
    {
        public Expression Field { get; set; }
        public string Alias { get; set; }

        public override string ToString()
        {
            string fieldName = Field.ToString();
            if (Field is DbObjectName)
                fieldName = "[" + Field.ToString() + "]";
            return fieldName + ((!string.IsNullOrEmpty(Alias) && Alias != Field.ToString()) ? " AS [" + Alias + "]" : "");
        }
    }
    public class ListSelect : List<Select>
    {
        public Select this[string aliasName]
        {
            get
            {
                foreach (Select s in this)
                    if (s.Alias == aliasName)
                        return s;
                return null;
            }
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
            string res = "\t[" + this[0].TableName + "]" + ((!string.IsNullOrEmpty(this[0].Alias) && this[0].Alias != this[0].TableName) ? " AS [" + this[0].Alias + "]" : "") + "\r\n";
            for (int i = 1; i < this.Count; i++)
                res += "\t" + this[i].ToString() + "\r\n";
            res = res.TrimEnd();
            return res;
        }
    }
    public class Join
    {
        public JoinType JoinType { get; set; }
        public string TableName { get; set; }
        public string Alias { get; set; }
        public Dictionary<string, Expression> CinarTableOptions { get; set; }
        public Expression On { get; set; }

        public override string ToString()
        {
            return JoinType.ToString().ToUpperInvariant() + " JOIN [" + TableName +"]" + ((!string.IsNullOrEmpty(Alias) && Alias != TableName) ? " AS [" + Alias + "]" : "") + (On!=null ? " ON " + On : "");
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
                fieldName = "[" + fieldName + "]";
            return fieldName + (Desc ? " DESC" : "");
        }
    }
    #endregion

    #region insert statement
    public class InsertStatement : Statement
    {
        public string TableName { get; set; }
        public List<string> Fields { get; set; }
        public List<List<Expression>> Values { get; set; }

        public InsertStatement()
        {
            Fields = new List<string>();
            Values = new List<List<Expression>>();
        }

        public override string ToString()
        {
            string res = "INSERT INTO [" + TableName + "]";
            if (Fields.Count > 0)
                res += "(\r\n\t" + string.Join(",\r\n\t", Fields.Select(f => "[" + f + "]").ToArray()) + "\r\n)";
            res += " VALUES \r\n\t(";
            res += string.Join("),\r\n\t(", Values.Select(values => string.Join(", ", values.Select(e => e.ToString()).ToArray())).ToArray()) + ")";
            res += ";\r\n";
            return res;
        }
    }
    #endregion

    #region update statement
    public class UpdateStatement : Statement
    {
        public string TableName { get; set; }
        public Dictionary<string, Expression> Set { get; set; }
        public List<Expression> Values { get; set; }
        public ListJoin From { get; set; }
        public Expression Where { get; set; }

        public UpdateStatement()
        {
            Set = new Dictionary<string, Expression>();
            Values = new List<Expression>();
            From = new ListJoin();
        }

        public override string ToString()
        {
            string res = "UPDATE [" + TableName + "] SET\r\n\t";
            res += string.Join(",\r\n\t", this.Set.Select(g => "[" + g.Key + "] = " + g.Value).ToArray());
            if (this.From.Count > 0)
                res += "\r\nFROM\r\n" + this.From;
            if (this.Where != null)
                res += "\r\nWHERE\r\n\t" + this.Where;
            res += ";\r\n";
            return res;
        }
    }
    #endregion

    #region delete statement
    public class DeleteStatement : Statement
    {
        public string TableName { get; set; }
        public Expression Where { get; set; }

        public DeleteStatement()
        {
        }

        public override string ToString()
        {
            string res = "DELETE FROM\r\n\t[" + TableName + "]";
            if (this.Where != null)
                res += "\r\nWHERE\r\n\t" + this.Where;
            res += ";\r\n";
            return res;
        }
    }
    #endregion

    #region create database statement
    public class CreateDatabaseStatement : Statement
    {
        public string DatabaseName { get; set; }

        public override string ToString()
        {
            return "CREATE DATABASE [" + DatabaseName + "];\r\n";
        }
    }
    #endregion

    #region create table statement
    public class CreateTableStatement : Statement
    {
        public string TableName { get; set; }
        public List<ColumnDef> Columns { get; set; }
        public List<TableConstraint> Constraints { get; set; }

        public CreateTableStatement()
        {
            Columns = new List<ColumnDef>();
            Constraints = new List<TableConstraint>();
        }

        public override string ToString()
        {
            string res = "CREATE TABLE [" + TableName + "] (\r\n\t";
            res += string.Join(",\r\n\t", this.Columns.Select(c => c.ToString()).ToArray());
            if (this.Constraints.Count > 0)
                res += ",\r\n\t";
            res += string.Join(",\r\n\t", this.Constraints.Select(c => c.ToString()).ToArray());
            res += "\r\n)";
            res += ";\r\n";
            return res;
        }
    }
    public class ColumnDef
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public List<ColumnConstraint> Constraints { get; set; }
        public int Length { get; set; }
        public int Scale { get; set; }

        public ColumnDef()
        {
            Constraints = new List<ColumnConstraint>();
        }

        public override string ToString()
        {
            string res = "[" + ColumnName + "] " + ColumnType;
            res += (Length > 0 ? "(" + Length + (Scale > 0 ? "," + Scale : "") + ")" : "");
            if (Constraints.Count > 0)
                res += " " + string.Join(" ", this.Constraints.Select(c => c.ToString()).ToArray());
            return res;
        }
    }
    public class ColumnConstraint
    {
        public ConstraintTypes ConstraintType { get; set; }
        public string Name { get; set; }
        public Expression Check { get; set; }
        public Expression Default { get; set; }
        public List<string> Columns { get; set; }
        public string RefTable { get; set; }
        public List<string> RefColumns { get; set; }
        public string CharacterSet { get; set; }
        public string Collate { get; set; }

        public ColumnConstraint()
        {
            Columns = new List<string>();
            RefColumns = new List<string>();
        }

        public override string ToString()
        {
            string res = "";
            if (!string.IsNullOrEmpty(Name))
                res += "CONSTRAINT [" + Name + "] ";
            switch (ConstraintType)
            {
                case ConstraintTypes.NotNull:
                    res += "NOT NULL";
                    break;
                case ConstraintTypes.Null:
                    res += "NULL";
                    break;
                case ConstraintTypes.Check:
                    res += "CHECK " + Check;
                    break;
                case ConstraintTypes.Unique:
                    res += "UNIQUE";
                    if (Columns.Count > 0)
                        res += " ([" + string.Join("], [", Columns.ToArray()) + "])";
                    break;
                case ConstraintTypes.PrimaryKey:
                    res += "PRIMARY KEY";
                    if (Columns.Count > 0)
                        res += " ([" + string.Join("], [", Columns.ToArray()) + "])";
                    break;
                case ConstraintTypes.ForeignKey:
                    if (Columns.Count > 0)
                    {
                        res += "FOREIGN KEY";
                        res += " ([" + string.Join("], [", Columns.ToArray()) + "]) ";
                    }
                    res += "REFERENCES [" + RefTable + "]";
                    res += " ([" + string.Join("], [", RefColumns.ToArray()) + "])";
                    break;
                case ConstraintTypes.AutoIncrement:
                    res += "AUTO_INCREMENT";
                    break;
                case ConstraintTypes.Default:
                    res += "DEFAULT " + Default;
                    break;
                case ConstraintTypes.CharacterSet:
                    res += "CHARACTER SET " + CharacterSet;
                    break;
                case ConstraintTypes.Collate:
                    res += "COLLATE " + Collate;
                    break;
                default:
                    break;
            }
            return res;
        }
    }
    public class TableConstraint : ColumnConstraint
    {
    }
    public enum ConstraintTypes
    {
        NotNull,
        Null,
        Check,
        Unique,
        PrimaryKey,
        ForeignKey,
        AutoIncrement,
        CharacterSet,
        Collate,
        Default
    }
    #endregion

    public static class Utility
    {
        public static string TrimQuotation(this string str)
        {
            return str.Trim('[', ']', '`', '"');
        }
    }
}