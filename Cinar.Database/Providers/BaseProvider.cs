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

        protected Database db = null;

        /// <summary>
        /// Veritabanından string olarak gelen field tip bilgisini DbType enum'una dönüştürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            if (DEFStringToDbType.ContainsKey(typeName.ToUpperInvariant()))
                return DEFStringToDbType[typeName];
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanın anlayacağı stringe dönüştürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            if (DEFDbTypeToString.ContainsKey(dbType))
                return DEFDbTypeToString[dbType];
            return "???";
        }

        public abstract Dictionary<DbType, string> DEFDbTypeToString { get; }
        public abstract Dictionary<string, DbType> DEFStringToDbType { get; }

    }
}
