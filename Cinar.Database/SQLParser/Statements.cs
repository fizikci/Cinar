using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        public SelectExpression SelectExpression = new SelectExpression();

        public override string ToString()
        {
            return SelectExpression.ToString() + ";";
        }
    }
    #endregion

    #region insert statement
    public class InsertStatement : Statement
    {
        public string TableName { get; set; }
        public List<string> Fields { get; set; }
        public List<List<Expression>> Values { get; set; }
        public SelectStatement Select { get; set; }

        public InsertStatement()
        {
            Fields = new List<string>();
            Values = new List<List<Expression>>();
        }

        public override string ToString()
        {
            string res = "INSERT INTO " + TableName + " ";
            if (Fields.Count > 0)
                res += "(\r\n\t" + string.Join(",\r\n\t", Fields) + "\r\n) ";
            if (Select != null)
                res += Select.ToString();
            else
            {
                res += "VALUES \r\n\t(";
                res += string.Join("),\r\n\t(", Values.Select(values => string.Join(", ", values.Select(e => e.ToString()).ToArray())).ToArray()) + ")";
            }
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
            string res = "UPDATE " + TableName + " SET\r\n\t";
            res += string.Join(",\r\n\t", this.Set.Select(g => g.Key + " = " + g.Value).ToArray());
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
            string res = "DELETE FROM\r\n\t" + TableName;
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
            return "CREATE DATABASE " + DatabaseName + ";\r\n";
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
            string res = "CREATE TABLE " + TableName + " (\r\n\t";
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
            string res = ColumnName + " " + ColumnType;
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
                res += "CONSTRAINT " + Name + " ";
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
                        res += " (" + string.Join(", ", Columns.ToArray()) + ")";
                    break;
                case ConstraintTypes.PrimaryKey:
                    res += "PRIMARY KEY";
                    if (Columns.Count > 0)
                        res += " (" + string.Join(", ", Columns.ToArray()) + "" +
                            ")";
                    break;
                case ConstraintTypes.ForeignKey:
                    if (Columns.Count > 0)
                    {
                        res += "FOREIGN KEY";
                        res += " (" + string.Join(", ", Columns.ToArray()) + ") ";
                    }
                    res += "REFERENCES " + RefTable;
                    res += " (" + string.Join(", ", RefColumns.ToArray()) + ")";
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