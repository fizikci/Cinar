using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cinar.SQLEngine
{
    public class CinarConnection : IDbConnection
    {
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            
        }

        public string ConnectionString
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

        public int ConnectionTimeout
        {
            get { return 30; }
        }

        public IDbCommand CreateCommand()
        {
            return new CinarCommand();
        }

        private string database;
        public string Database
        {
            get { return database; }
        }

        public void Open()
        {
            
        }

        public ConnectionState State
        {
            get { return ConnectionState.Open; }
        }

        public void Dispose()
        {
        }
    }

    public class CinarCommand : IDbCommand
    {
        public void Dispose()
        {
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            return new CinarParameter();
        }

        public int ExecuteNonQuery()
        {
            Interpreter engine = new Interpreter(this.CommandText);
            engine.Parse();
            engine.Execute();

            return int.Parse(engine.Output);
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            Interpreter engine = new Interpreter(this.CommandText);
            engine.Parse();
            engine.Execute();

            return engine.Output;
        }

        public DataSet ExecuteDataSet()
        {
            throw new NotImplementedException();
        }

        CinarConnection con;
        public IDbConnection Connection
        {
            get { return con; }
            set { con = (CinarConnection)value; }
        }

        CinarTransaction tran;
        public IDbTransaction Transaction
        {
            get { return tran; }
            set { tran = (CinarTransaction)tran; }
        }

        private string commandText;
        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }

        private int cmdTimeout = 30;
        public int CommandTimeout
        {
            get { return cmdTimeout; }
            set { cmdTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return System.Data.CommandType.Text; }
            set { throw new NotImplementedException("Only text commands supported"); }
        }

        private CinarParameterCollection parameters = new CinarParameterCollection();
        public CinarParameterCollection Parameters
        {
            get { return parameters; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return UpdateRowSource.None; }
            set { throw new NotImplementedException("we dont yet know what this is."); }
        }


        IDataParameterCollection IDbCommand.Parameters
        {
            get { return Parameters; }
        }
    }

    public class CinarDataAdapter : IDbDataAdapter
    {
        public IDbCommand DeleteCommand
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

        public IDbCommand InsertCommand
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

        private IDbCommand selectCommand;
        public IDbCommand SelectCommand
        {
            get
            {
                return selectCommand;
            }
            set
            {
                selectCommand = value;
            }
        }

        public IDbCommand UpdateCommand
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

        public int Fill(DataSet dataSet)
        {
            return 1;
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
        }

        public IDataParameter[] GetFillParameters()
        {
        }

        public MissingMappingAction MissingMappingAction
        {
            get
            {
                return System.Data.MissingMappingAction.Passthrough;
            }
            set
            {
            }
        }

        public MissingSchemaAction MissingSchemaAction
        {
            get
            {
                return System.Data.MissingSchemaAction.Ignore;
            }
            set
            {
                
            }
        }

        public ITableMappingCollection TableMappings
        {
            get { return null; }
        }

        public int Update(DataSet dataSet)
        {
            throw new NotImplementedException();
        }
    }

    public class CinarTransaction : IDbTransaction
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        internal IDbConnection conn;
        public IDbConnection Connection
        {
            get { throw new NotImplementedException(); }
        }

        public IsolationLevel IsolationLevel
        {
            get { throw new NotImplementedException(); }
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class CinarParameter : IDbDataParameter
    {
        public byte Precision
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

        public byte Scale
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

        public int Size
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

        public DbType DbType
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

        public ParameterDirection Direction
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

        public bool IsNullable
        {
            get { throw new NotImplementedException(); }
        }

        public string ParameterName
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

        public string SourceColumn
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

        public DataRowVersion SourceVersion
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

        public object Value
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

    public class CinarParameterCollection : List<CinarParameter>, IDataParameterCollection
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
