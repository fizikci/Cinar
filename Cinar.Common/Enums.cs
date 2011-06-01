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

namespace Cinar.Database
{
    /// <summary>
    /// Desteklenen veritabaný türleri
    /// </summary>
    public enum DatabaseProvider
    {
        PostgreSQL,
        MySQL,
        SQLServer,
        Cinar
    }

    public enum DbType
    {
        Boolean,

        Byte, Int16, Int32, Int64, Real, Float, Decimal,
        Double, Numeric, Currency, CurrencySmall,

        DateTimeSmall, DateTime, Date, Time,
        Timetz, Timestamp, Timestamptz,

        Char, VarChar, NChar, NVarChar,

        Binary, VarBinary,
        Text, NText, TextTiny, TextMedium, TextLong,
        Image, Blob, BlobTiny, BlobMedium, BlobLong,
        Variant,

        Guid, Xml,

        Set, Enum,

        Undefined
    }

    public enum SimpleDbType
    {
        Boolean,
        Integer,
        Float,
        DateTime,
        String,
        Text,
        ByteArray,
        Other
    }
}