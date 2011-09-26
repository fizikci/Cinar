using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Xml.Serialization;

namespace Cinar.Database
{
    public interface IDatabase
    {
        [XmlIgnore]
        bool NoTransactions { get; set; }

        [XmlIgnore]
        bool EnableSQLLog { get; set; }

        [XmlIgnore]
        string Name { get; set; }

        [XmlIgnore]
        string Host { get; }

        [XmlIgnore]
        List<string> SQLLog { get; }

        [XmlIgnore]
        Hashtable Cache { get; }

        [XmlIgnore]
        string ConnectionString { get; }

        [XmlIgnore]
        DatabaseProvider Provider { get; }

        /// <summary>
        /// Veritabanýndaki tablolar
        /// </summary>
        TableCollection Tables { get; set; }

        [XmlIgnore]
        IDbConnection Connection { get; }

        void ClearSQLLog();
        void SetCollectionParents();
        void GenerateUIMetadata();
        void CreateDbProvider(bool createDatabaseIfNotExist);
        void SetConnectionString(DatabaseProvider provider, string host, string dbName, string userName, string password, int defaultCommandTimeout);

        /// <summary>
        /// Veritabanýnda deðiþiklik yapýldýysa (alter table gibi) metadatayý tekrar baþtan okumak için kullanýlan metod.
        /// Tek yaptýðý þey ReadMetadata() metodunu çaðýrmaktýr.
        /// DÝKKAT: bütün nesne referanslarýný deðiþtirir!!!
        /// </summary>
        void Refresh();

        Constraint GetConstraint(string constraintName);
        string ToString();
        DbDataAdapter CreateDataAdapter(IDbCommand selectCommand);
        DbDataAdapter CreateDataAdapter(string selectCommandText);
        IDbCommand CreateCommand(string cmdText);
        IDbDataParameter CreateParameter(string parameterName, object value);
        void Begin(IsolationLevel level);
        void Begin();
        void Commit();
        void Rollback();
        void Execute(Action dbAction);
        int ExecuteNonQuery(IDbCommand cmd);
        int ExecuteNonQuery(string sql);
        int ExecuteNonQuery(string sql, params object[] parameters);
        int Insert(string tableName, Hashtable data);
        int Insert(string tableName, Hashtable data, bool bypassAutoIncrementColumn);
        IDbCommand CreateInsertCommand(string tableName, Hashtable data, bool bypassAutoIncrementColumn);
        string GetSQLInsert(string tableName, Hashtable data, bool bypassAutoIncrementColumn);
        int Insert(string tableName, DataRow dataRow, bool bypassAutoIncrementColumn);
        int Insert(string tableName, DataRow dataRow);
        int Insert(string tableName, object record);
        int Update(string tableName, Hashtable data, Hashtable originalData = null);
        int Update(string tableName, DataRow dataRow);
        int Delete(string tableName, Hashtable data);
        int Delete(string tableName, DataRow dataRow);
        string GetIdColumnName(IDatabaseEntity entity);
        string GetIdColumnName(Type entityType);
        void Save(IDatabaseEntity entity);
        Hashtable EntityToHashtable(IDatabaseEntity entity);
        Hashtable DataRowToHashtable(DataRow dataRow);
        DataRow EntityToDataRow(IDatabaseEntity entity);
        IDatabaseEntity DataRowToEntity(Type entityType, DataRow dr);
        T DataRowToEntity<T>(DataRow dr);
        void FillEntity(IDatabaseEntity entity, DataRow dr);
        void FillEntity(IDatabaseEntity entity);
        void FillDataRow(IDatabaseEntity entity, DataRow dr);
        DataSet GetDataSet(DataSet ds, string sql, params object[] parameters);
        DataSet GetDataSet(string sql, params object[] parameters);
        DataSet GetDataSet(string sql);
        DataTable GetDataTable(DataSet ds, string tableName, string sql, params object[] parameters);
        DataTable GetDataTable(string sql, params object[] parameters);
        DataTable GetDataTable(string sql);
        DataTable GetDataTableFor(string tableName, FilterExpression fExp);
        DataTable GetDataTableFor(Type entityType, FilterExpression fExp);
        List<T> GetList<T>(string sql, params object[] parameters);
        Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, params object[] parameters);
        Hashtable GetKeyValueList(string sql, params object[] parameters);
        DataRow GetDataRow(string sql, params object[] parameters);
        DataRow GetDataRow(string sql);
        Hashtable GetHashtable(string sql);
        Hashtable GetHashtable(string sql, params object[] parameters);
        object GetValue(string sql, params object[] parameters);
        object GetValue(string sql);
        string GetString(string sql, params object[] parameters);
        string GetString(string sql);
        DateTime GetDateTime(string sql, params object[] parameters);
        DateTime GetDateTime(string sql);
        bool GetBool(string sql, params object[] parameters);
        bool GetBool(string sql);
        int GetInt(string sql, params object[] parameters);
        int GetInt(string sql);
        IDatabaseEntity Read(Type entityType, int id);
        T Read<T>(int id);
        IDatabaseEntity Read(Type entityType, string where, params object[] parameters);
        T Read<T>(string where, params object[] parameters);
        IDatabaseEntity[] ReadList(Type entityType, string selectSql, params object[] parameters);
        List<T> ReadList<T>(string selectSql, params object[] parameters) where T : IDatabaseEntity;
        List<T> ReadList<T>(FilterExpression fExp, int pageNo, int pageSize) where T : IDatabaseEntity;
        List<T> ReadList<T>(FilterExpression filterExpression) where T : IDatabaseEntity;
        List<T> ReadList<T>() where T : IDatabaseEntity;
        IDatabaseEntity[] ReadList(Type entityType, FilterExpression filterExpression);
        int ReadCount(Type entityType, FilterExpression filterExpression);
        DataTable ReadTable(Type entityType, string selectSql, params object[] parameters);
        string GetFromWithJoin(Type entityType);
        string GetFromWithJoin(Table tbl);

        void ClearEntityWebCache(Type entityType, int id);

        /// <summary>
        /// Parametre olarak verilen tipe uygun tablo create eder.
        /// DÝKKAT: bütün nesne referanslarýný deðiþtirir!!!
        /// </summary>
        Table CreateTableForType(Type type);

        Table CreateTableForType(Type type, bool refreshMetadata);
        void CreateTable(Table tbl, DefaultDataAttribute[] defaultDataArr, bool refreshMetadata);
        Table CreateTableMetadataForType(Type type);
        void CreateTablesForAllTypesIn(Assembly assembly);
        Table GetTableForEntityType(Type entityType);
        Column GetColumnForProperty(PropertyInfo propertyInfo);
        PropertyInfo GetPropertyInfoForColumn(Column column);
        Type GetEntityTypeForTable(Table table);
        string DbTypeToString(DbType dbType);
        DbType StringToDbType(string dbType);
        string[] GetOriginalColumnTypes();
        string GetDatabaseDDL(bool addDropTable);
        string GetTableDDL(Table table);
        string GetTableDDL(Table table, DatabaseProvider provider);
        string GetColumnDDL(Column column);
        string GetSQLTableList();
        string GetSQLTableRename(string oldName, string newName);
        string GetSQLTableDrop(Table table);
        string GetSQLColumnList(string tableName);
        string GetSQLColumnAdd(string toTable, Column column);
        string GetSQLColumnRemove(Column column);
        string GetSQLColumnRename(string oldColumnName, Column column);
        string GetSQLColumnChangeDataType(Column column);
        string GetSQLColumnChangeDefault(Column column);
        string GetSQLConstraintList();
        string GetSQLConstraintRemove(Constraint constraint);
        string GetSQLConstraintAdd(Constraint constraint);
        string GetSQLConstraintAdd(CheckConstraint constraint);
        string GetSQLConstraintAdd(UniqueConstraint constraint);
        string GetSQLConstraintAdd(ForeignKeyConstraint constraint);
        string GetSQLConstraintAdd(PrimaryKeyConstraint constraint);
        string GetSQLConstraintRemove(PrimaryKeyConstraint constraint);
        string GetSQLColumnAddNotNull(Column column);
        string GetSQLColumnRemoveNotNull(Column column);
        string GetSQLColumnSetAutoIncrement(Column column);
        string GetSQLColumnRemoveAutoIncrement(Column column);
        string GetSQLIndexAdd(Index index);
        string GetSQLIndexRemove(Index index);
        string GetSQLBaseIndexConstraintAdd(BaseIndexConstraint obj);
        string GetSQLBaseIndexConstraintRemove(BaseIndexConstraint obj);
        string GetSQLViewCreate(Table view);
        List<string> GetDatabases();
        string GetVersion();
    }
}