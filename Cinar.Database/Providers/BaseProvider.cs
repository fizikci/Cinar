using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Database.Providers
{
    internal abstract class BaseProvider
    {
        protected System.Data.IDbConnection connection;
        public System.Data.IDbConnection Connection
        {
            get { return connection; }
        }

        protected IDatabase db = null;

        public bool CreatedNow { get; set; }

        /// <summary>
        /// Veritabanından string olarak gelen column tip bilgisini DbType enum'una dönüştürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            if (DEFStringToDbType.ContainsKey(typeName.ToUpperInvariant()))
                return DEFStringToDbType[typeName];
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan column tip bilgisini veritabanın anlayacağı stringe dönüştürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            if (DEFDbTypeToString.ContainsKey(dbType))
                return DEFDbTypeToString[dbType];
            return "???";
        }

        public abstract Dictionary<DbType, string> DEFDbTypeToString { get; }
        public abstract Dictionary<string, DbType> DEFStringToDbType { get; }

        protected string getDefaultValue(Column column)
        {
            if (column.IsStringType())
                return encloseWithQuote(column.DefaultValue.Trim());
            else if (column.IsNumericType())
                return column.DefaultValue.Trim().Trim('\'');
            else
                return encloseWithQuote(column.DefaultValue.Trim());
        }

        protected string encloseWithQuote(string str)
        {
            return "'" + str.Trim().Trim('(', ')', '\'') + "'";
        }

        protected string encloseWithParanthesis(string str)
        {
            return "(" + str.Trim().Trim('(', ')') + ")";
        }

    }
}
