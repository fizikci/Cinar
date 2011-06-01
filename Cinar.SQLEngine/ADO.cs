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

        CinarTransaction tran;
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
            throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public override int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public override void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public override bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public override void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            throw new NotImplementedException();
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            throw new NotImplementedException();
        }

        public override object SyncRoot
        {
            get { throw new NotImplementedException(); }
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

        private CinarParameter getParam(string parameterName)
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
}
