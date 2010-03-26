using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Cinar.Database.Tools
{
    public class DBTransfer
    {
        Database dbSrc;
        Database dbDst;
        List<string> tables;
        int pageSize = 100;
        Action<string> _log;
        int limit;
        string prefix;

        public DBTransfer(Database dbSrc, Database dbDst, List<string> tables, int pageSize, int limit, string prefix, Action<string> _log)
        {
            this.dbSrc = dbSrc;
            this.dbDst = dbDst;
            this.tables = tables;
            this.pageSize = pageSize;
            this._log = _log;
            this.limit = limit;
            this.prefix = prefix;

            if (dbSrc == null || dbDst == null)
                throw new Exception("Specify both source and destination databases.");
            if (String.IsNullOrEmpty(prefix) && dbSrc == dbDst)
                throw new Exception("Cannot transfer to the same database without using prefix!");
            if(tables==null || tables.Count==0)
                throw new Exception("Specify one ore more tables to transfer.");
            if (pageSize == 0)
                throw new Exception("Row transfer limit cannot be less than 100");
        }

        public void Transfer(bool transferData)
        {
            foreach (string tblName in tables)
                if (dbSrc.Tables[tblName] == null)
                {
                    log(String.Format("Error: A table with the name {0} doesn't exist.", tblName));
                    return;
                }

            foreach (string tableName in tables)
                transferTable(tableName, limit, transferData);

            logLine("Transfer completed. :)");
            logLine("");
        }
        private void transferTable(string tableName, int maxRowToTransfer, bool transferData)
        {
            int i = 0;
            int rowCount = maxRowToTransfer == 0 ? Int32.MaxValue : maxRowToTransfer;
            logLine("Transferring table {0}", tableName);
            if (dbDst.Tables[prefix + tableName] == null)
            {
                Table srcTable = dbSrc.Tables[tableName];
                Table newTable = srcTable.CloneForDatabase(dbDst, prefix + tableName);
                dbDst.CreateTable(newTable, null, true);
            }
            else
            {
                if (transferData)
                {
                    i = dbDst.GetInt("select count(*) from " + prefix + tableName);
                    if (i >= rowCount)
                        logLine("..............no new records.");
                }
            }

            if (transferData)
            {
                Table srcTable = dbSrc.Tables[tableName];
                foreach (Field f in srcTable.Fields)
                    if (f.IsDateType())
                    {
                        int res = dbSrc.ExecuteNonQuery("update " + srcTable.Name + " set " + f.Name + "='1980-01-01 00:00' where " + f.Name + "<'1980-01-01 00:00'");
                        if (res > 0)
                            logLine(res + " kayıt için " + srcTable.Name + "." + f.Name + " alanı düzeltildi");
                    }

                for (; i < rowCount; i += pageSize)
                {
                    if (maxRowToTransfer > 0 && i >= maxRowToTransfer)
                        break; //****

                    int transferedRows = 0;

                    DataTable dt;
                    if(dbSrc.Provider== DatabaseProvider.MySQL)
                    dt = dbSrc.GetDataTable("select * from " + tableName + " limit " + pageSize + " offset " + i);
                    else
                        dt = dbSrc.GetDataTable("select top " + pageSize + " * from " + tableName);

                    DataTable dtDestSample = dbDst.GetDataTable("select top 0 * from " + tableName);
                    foreach (DataRow dr in dt.Rows)
                    {
                        System.Collections.Hashtable drDest = new System.Collections.Hashtable();//newRow(dbDst, prefix + tableName);
                        foreach (DataColumn dc in dt.Columns)
                            if (!dr.IsNull(dc) && dtDestSample.Columns[dc.ColumnName]!=null)
                                drDest[dc.ColumnName] = Convert.ChangeType(dr[dc], dtDestSample.Columns[dc.ColumnName].DataType);

                        dbDst.Insert(prefix + tableName, drDest, false);
                        transferedRows++;
                    }
                    log(".");
                    //GC.Collect();
                    if (dt.Rows.Count < pageSize)
                    {
                        log(" (" + (i + dt.Rows.Count + 1) + " kayıt, " + transferedRows + " yeni)");
                        break; //***
                    }
                }
                logLine("");
            }
        }

        private DataRow newRow(Database db, string tableName) 
        {
            DataTable dtDestSample = dbDst.GetDataTable("select top 0 * from " + tableName);
            return dtDestSample.NewRow();
        }

        private void log(string str, params object[] values)
        {
            if(_log!=null)
                _log.Invoke(String.Format(str, values));
        }
        private void logLine(string str, params object[] values)
        {
            log(str + Environment.NewLine, values);
        }
    }
}
