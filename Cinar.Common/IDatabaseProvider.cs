/*
Copyright (C) 2006  Bülent Keskin

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Cinar.Database
{
    public interface IDatabaseProvider
    {
        void ReadDatabaseMetadata();
        DbType StringToDbType(string typeName);
        string DbTypeToString(DbType dbType);

        IDbConnection Connection{ get; }
        IDbConnection CreateConnection();
        DbDataAdapter CreateDataAdapter(IDbCommand selectCommand);
        DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters);
        IDbCommand CreateCommand(string cmdText, params object[] parameters);
        IDbCommand CreateCommand(string cmdText, IDbTransaction transaction, params object[] parameters);
        IDbDataParameter CreateParameter(string parameterName, object value);

        string GetTableDDL(Table table);
        string GetColumnDDL(Column column);

        string[] GetColumnTypes();

        string GetSQLTableList();
        string GetSQLTableRename(string oldName, string newName);
        string GetSQLTableDrop(Table table, bool addIfExists);
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
        string GetSQLViewCreate(Table view);
        string GetSQLDateYearMonthPart(string columnName);
        string GetSQLDateYearMonthDayPart(string columnName);
        string GetSQLDateYearMonthDayHourPart(string columnName);

        bool CreatedNow { get; set; }
    }
    public interface IMetadata
    { 
    }
    
}
