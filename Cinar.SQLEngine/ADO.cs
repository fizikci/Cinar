using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;

namespace Cinar.SQLEngine
{
    public class CinarConnection : DbConnection
    {
        public CinarConnection(string dbPath)
        {
            this.database = dbPath;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
        }

        public override string ConnectionString
        {
            get
            {
                return Database;
            }
            set
            {
                database = value;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            return new CinarCommand();
        }

        public override string DataSource
        {
            get { throw new NotImplementedException(); }
        }

        private string database;
        public override string Database
        {
            get { return database; }
        }

        public override void Open()
        {
        }

        public override string ServerVersion
        {
            get { return "1.0"; }
        }

        public override ConnectionState State
        {
            get { return ConnectionState.Open; }
        }

    }

    public class CinarCommand : DbCommand
    {

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        private string commandText;
        public override string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        private int cmdTimeout = 30;
        public override int CommandTimeout
        {
            get { return cmdTimeout; }
            set { cmdTimeout = value; }
        }

        public override CommandType CommandType
        {
            get { return System.Data.CommandType.Text; }
            set { throw new NotImplementedException("Only text commands supported"); }
        }

        protected override DbParameter CreateDbParameter()
        {
            return new CinarParameter();
        }

        CinarConnection con;
        protected override DbConnection DbConnection
        {
            get { return con; }
            set { con = (CinarConnection)value; }
        }

        CinarParameterCollection parameters = new CinarParameterCollection();
        protected override DbParameterCollection DbParameterCollection
        {
            get { return parameters; }
        }
        public new CinarParameterCollection Parameters { get { return parameters; } }

        CinarTransaction tran;
        public CinarCommand(string cmdText, CinarConnection cinarConnection, CinarTransaction cinarTransaction)
        {
            // TODO: Complete member initialization
            this.commandText = cmdText;
            this.con = cinarConnection;
            this.tran = cinarTransaction;
        }

        public CinarCommand(string cmdText, CinarConnection cinarConnection)
        {
            // TODO: Complete member initialization
            this.commandText = cmdText;
            this.con = cinarConnection;
        }
        internal CinarCommand() { }
        protected override DbTransaction DbTransaction
        {
            get { return tran; }
            set { tran = (CinarTransaction)value; }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            Interpreter engine = new Interpreter(this.CommandText);
            engine.Parse();
            engine.Execute();

            CinarDataReader cdr = new CinarDataReader(engine.ResultSet, engine.FieldNames);
            return cdr;
        }

        public override int ExecuteNonQuery()
        {
            Interpreter engine = new Interpreter(this.CommandText);
            engine.Parse();
            engine.Execute();

            return int.Parse(engine.Output);
        }

        public override object ExecuteScalar()
        {
            Interpreter engine = new Interpreter(this.CommandText);
            engine.Parse();
            engine.Execute();

            return engine.Output;
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get { return UpdateRowSource.None; }
            set { throw new NotImplementedException("we dont yet know what this is."); }
        }
    }

    public class CinarDataAdapter : DbDataAdapter
    {
        public CinarDataAdapter(CinarCommand cinarCommand)
        {
            // TODO: Complete member initialization
            this.SelectCommand = cinarCommand;
        }
 
    }

    public class CinarTransaction : DbTransaction
    {
        public override void Commit()
        {
            throw new NotImplementedException();
        }

        private CinarConnection conn;
        protected override DbConnection DbConnection
        {
            get { return conn; }
        }

        private IsolationLevel isolationLevel;
        public override IsolationLevel IsolationLevel
        {
            get { return isolationLevel; }
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }
    }

    public class CinarParameter : DbParameter
    {
        private string parameterName;
        private object parameterValue;

        public CinarParameter(string parameterName, object value)
        {
            // TODO: Complete member initialization
            this.parameterName = parameterName;
            this.parameterValue = value;
        }
        internal CinarParameter() { }
        public override DbType DbType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override ParameterDirection Direction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsNullable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string ParameterName
        {
            get
            {
                return parameterName;
            }
            set
            {
                parameterName = value;
            }
        }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }

        public override int Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string SourceColumn
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool SourceColumnNullMapping
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override DataRowVersion SourceVersion
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override object Value
        {
            get
            {
                return parameterValue;
            }
            set
            {
                parameterValue = value;
            }
        }
    }

    public class CinarParameterCollection : DbParameterCollection
    {
        private InternalParameterCollection coll = new InternalParameterCollection();

        public override int Add(object value)
        {
            coll.Add((CinarParameter)value);
            return coll.Count;
        }

        public override void AddRange(Array values)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            coll.Clear();
        }

        public override bool Contains(string value)
        {
            return coll.Contains(value);
        }

        public override bool Contains(object value)
        {
            return coll.Contains((CinarParameter)value);
        }

        public override void CopyTo(Array array, int index)
        {
            coll.CopyTo(array, index);
        }

        public override int Count
        {
            get { return coll.Count; }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return coll.GetEnumerator();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return coll.getParam(parameterName);
        }

        protected override DbParameter GetParameter(int index)
        {
            return (DbParameter)coll[index];
        }

        public override int IndexOf(string parameterName)
        {
            return coll.IndexOf(parameterName);
        }

        public override int IndexOf(object value)
        {
            return coll.IndexOf(value);
        }

        public override void Insert(int index, object value)
        {
            coll.Insert(index, value);
        }

        public override bool IsFixedSize
        {
            get { return coll.IsFixedSize; }
        }

        public override bool IsReadOnly
        {
            get { return coll.IsReadOnly; }
        }

        public override bool IsSynchronized
        {
            get { return coll.IsSynchronized; }
        }

        public override void Remove(object value)
        {
            coll.Remove(value);
        }

        public override void RemoveAt(string parameterName)
        {
            coll.RemoveAt(parameterName);
        }

        public override void RemoveAt(int index)
        {
            coll.RemoveAt(index);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            coll[parameterName] = value;
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            coll[index] = value;
        }

        public override object SyncRoot
        {
            get { return coll.SyncRoot; }
        }

        public void AddWithValue(string paramName, object paramValue)
        {
            coll.AddWithValue(paramName, paramValue);
        }
    }
    
    internal class InternalParameterCollection : ArrayList, IDataParameterCollection
    {
        public bool Contains(string parameterName)
        {
            CinarParameter p = getParam(parameterName);
            return p != null;
        }

        public int IndexOf(string parameterName)
        {
            CinarParameter p = getParam(parameterName);
            if (p != null)
                return this.IndexOf(p);
            return -1;
        }

        public void RemoveAt(string parameterName)
        {
            CinarParameter p = getParam(parameterName);
            if (p != null)
                this.Remove(p);
        }

        internal CinarParameter getParam(string parameterName)
        {
            foreach (CinarParameter cinarParameter in this)
                if (parameterName == cinarParameter.ParameterName)
                    return cinarParameter;
            return null;
        }

        public object this[string parameterName]
        {
            get
            {
                CinarParameter p = getParam(parameterName);
                return p==null ? null : p.Value;
            }
            set
            {
                CinarParameter p = getParam(parameterName);
                if(p != null) p.Value = value;
            }
        }

        public void AddWithValue(string paramName, object paramValue)
        {
            this.Add(new CinarParameter() {ParameterName = paramName, Value = paramValue});
        }
    }

    public class CinarDataReader : DbDataReader
    {
        private List<Hashtable> list;
        private int currentIndex = 0;
        private List<string> fieldNames;

        public CinarDataReader(List<Hashtable> list, List<string> fieldNames)
        {
            this.list = list;
            this.fieldNames = fieldNames;
        }
        public override void Close()
        {
            isClosed = true;
        }

        public override int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public override int FieldCount
        {
            get { return list==null ? 0 : list[0].Keys.Count; }
        }

        private object getValue(int ordinal)
        {
            if (ordinal < 0 || ordinal >= fieldNames.Count)
                return DBNull.Value;
            return list[currentIndex][fieldNames[ordinal]];
        }

        public override bool GetBoolean(int ordinal)
        {
            return Convert.ToBoolean(getValue(ordinal));
        }

        public override byte GetByte(int ordinal)
        {
            return Convert.ToByte(getValue(ordinal));
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            return Convert.ToChar(getValue(ordinal));
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            return getValue(0).GetType().Name;
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return Convert.ToDateTime(getValue(ordinal));
        }

        public override decimal GetDecimal(int ordinal)
        {
            return Convert.ToDecimal(getValue(ordinal));
        }

        public override double GetDouble(int ordinal)
        {
            return Convert.ToDouble(getValue(ordinal));
        }

        public override IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public override Type GetFieldType(int ordinal)
        {
            return getValue(ordinal).GetType();
        }

        public override float GetFloat(int ordinal)
        {
            return Convert.ToSingle(getValue(ordinal));
        }

        public override Guid GetGuid(int ordinal)
        {
            return new Guid(GetString(ordinal));
        }

        public override short GetInt16(int ordinal)
        {
            return Convert.ToInt16(getValue(ordinal));
        }

        public override int GetInt32(int ordinal)
        {
            return Convert.ToInt32(getValue(ordinal));
        }

        public override long GetInt64(int ordinal)
        {
            return Convert.ToInt64(getValue(ordinal));
        }

        public override string GetName(int ordinal)
        {
            if (ordinal < 0 || ordinal >= fieldNames.Count)
                return null;
            return fieldNames[ordinal];
        }

        public override int GetOrdinal(string name)
        {
            return fieldNames.IndexOf(name);
        }

        public override DataTable GetSchemaTable()
        {
            return new DataTable();
        }

        public override string GetString(int ordinal)
        {
            return Convert.ToString(getValue(ordinal));
        }

        public override object GetValue(int ordinal)
        {
            return getValue(ordinal);
        }

        public override int GetValues(object[] values)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = getValue(i);
            return fieldNames.Count;
        }

        public override bool HasRows
        {
            get { return list!=null && list.Count>0; }
        }

        private bool isClosed = false;
        public override bool IsClosed
        {
            get { return isClosed; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return getValue(ordinal) == null;
        }

        public override bool NextResult()
        {
            return currentIndex < list.Count - 1;
        }

        public override bool Read()
        {
            currentIndex++;
            return currentIndex < list.Count;
        }

        public override int RecordsAffected
        {
            get { return list.Count; }
        }

        public override object this[string name]
        {
            get { return getValue(GetOrdinal(name)); }
        }

        public override object this[int ordinal]
        {
            get { return getValue(ordinal); }
        }
    }
}
