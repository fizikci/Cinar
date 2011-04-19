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
    internal interface IDatabaseProvider
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
        string GetFieldDDL(Field field);

        string[] GetFieldTypes();
    }
}
