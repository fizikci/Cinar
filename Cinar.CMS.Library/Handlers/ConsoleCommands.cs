using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Text.RegularExpressions;
using System.IO;

//using System.IO;

namespace Cinar.CMS.Library.Handlers
{
    public class ConsoleCommands
    {
        public static string Hello()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CMS Console 1.1\n");
            sb.AppendFormat("Welcome {0}. Write help to find out commands.\n", Provider.User.Nick);
            return sb.ToString();
        }

        [Description("displays this help (for specific help try \"help <command_name>\")")]
        public static string Help([Description("command name to get help")] 
            string command)
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(command))
            {
                string methodName = command.Substring(0, 1).ToUpper() + command.Substring(1).ToLower();

                MethodInfo mi = typeof(ConsoleCommands).GetMethod(methodName);
                if (mi == null)
                    throw new Exception("No such command.");

                sb.Append("   usage : " + mi.Name.ToLower());
                StringBuilder sb2 = new StringBuilder();
                foreach (ParameterInfo pi in mi.GetParameters())
                {
                    string desc = (Utility.GetAttribute(pi, typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
                    sb.Append(" <" + pi.Name.ToLower() + ">");
                    sb2.Append("      " + pi.Name.PadRight(15) + " : " + desc + "\n");
                }
                sb.Append("\n" + sb2.ToString());
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Command Name");
                dt.Columns.Add("Description");
                foreach (MethodInfo mi in typeof(ConsoleCommands).GetMethods(BindingFlags.Static | BindingFlags.Public))
                {
                    string desc = (Utility.GetAttribute(mi, typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
                    if (!String.IsNullOrEmpty(desc))
                    {
                        DataRow dr = dt.NewRow();
                        dr["Command Name"] = mi.Name.ToLower();
                        dr["Description"] = desc;
                        dt.Rows.Add(dr);
                    }
                }
                DataRow dr2 = dt.NewRow();
                dr2["Command Name"] = "exit";
                dr2["Description"] = "exits this window";
                dt.Rows.Add(dr2);

                sb.Append(Utility.ToStringTable(dt));
            }
            return sb.ToString();
        }

        [Description("displays miscellaneous information")]
        public static string Show([Description("enter stat type such as tables, pages, tags, sql_log, drive_info")] 
            string type)
        {
            StringBuilder sb = new StringBuilder();

            switch (type)
            {
                case "tables":
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Table Name");
                        dt.Columns.Add("Details");
                        foreach (Type t in Provider.GetEntityTypes())
                        {
                            DataRow dr = dt.NewRow();
                            dr["Table Name"] = String.Format("{0} ({1})", Provider.GetResource(t.Name), t.Name);
                            try
                            {
                                dr["Details"] = Provider.Database.GetValue("select count(*) from [" + t.Name + "]") + " rows";
                            }
                            catch (Exception ex)
                            {
                                dr["Details"] = ex.Message;
                            }
                            dt.Rows.Add(dr);
                        }
                        sb.Append(Utility.ToStringTable(dt));
                        break;
                    }
                case "pages":
                    {
                        DataTable dt = Provider.Database.GetDataTable(@"
                            select 
                                m.Template as Page, count(*) as [Module Count], t.FileName as Info 
                            from 
                                Module m 
                                left join Template t on t.FileName=m.Template
                            group by m.Template, t.FileName 
                            order by m.Template;");
                        foreach (DataRow dr in dt.Rows)
                        {
                            if(dr.IsNull("Info"))
                                dr["Info"] = "Template not exist!";
                        }
                        foreach(Template template in Provider.Database.ReadList(typeof(Template),"select FileName from Template"))
                        {
                            bool pageExistsInDT = false;
                            foreach (DataRow dr in dt.Rows)
                                if (dr["Page"].Equals(template.FileName))
                                {
                                    pageExistsInDT = true;
                                    dr["Info"] = "";
                                }

                            if (!pageExistsInDT)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Page"] = template.FileName;
                                dr["Module Count"] = 0;
                                dt.Rows.Add(dr);
                            }
                        }
                        sb.Append(Utility.ToStringTable(dt));
                        break;
                    }
                case "tags":
                    sb.Append("this stat not ready yet\n");
                    break;
                case "sql_log":
                    for (int i = 0; i < Provider.Database.SQLLog.Count; i++)
                        sb.AppendFormat("{0}. {1}\n", i + 1, Provider.Database.SQLLog[i]);
                    break;
                case "drive_info":
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Name");
                        dt.Columns.Add("Label");
                        dt.Columns.Add("Type");
                        dt.Columns.Add("Format");
                        dt.Columns.Add("Ready");
                        dt.Columns.Add("Free Space");
                        dt.Columns.Add("Size");
                        foreach (System.IO.DriveInfo di in System.IO.DriveInfo.GetDrives())
                        {
                            DataRow dr = dt.NewRow();
                            dr["Name"] = di.Name;
                            dr["Label"] = di.IsReady ? di.VolumeLabel : "";
                            dr["Type"] = di.DriveType;
                            dr["Format"] = di.IsReady ? di.DriveFormat : "";
                            dr["Ready"] = di.IsReady ? "yes" : "no";
                            dr["Free Space"] = di.IsReady ? di.AvailableFreeSpace.ToString() : "";
                            dr["Size"] = di.IsReady ? di.TotalSize.ToString() : "";
                            dt.Rows.Add(dr);
                        }
                        sb.Append(Utility.ToStringTable(dt));
                    }
                    break;
                default:
                    sb.Append("there is no this type of stat\n");
                    break;
            }
            return sb.ToString();
        }

        [Description("deletes objects")]
        public static string Delete(
            [Description("object type such as page, region")] string objectType,
            [Description("object name such as APage.aspx, APage.aspx/ARegion")] string objectName)
        {
            StringBuilder sb = new StringBuilder();

            switch (objectType)
            {
                case "page":
                    {
                        Provider.DeleteTemplate(objectName, true);
                        sb.Append("Page deleted.");
                        break;
                    }
                case "region":
                    sb.Append("this delete type not ready yet\n");
                    break;
                default:
                    sb.Append("there is no this type of delete\n");
                    break;
            }
            return sb.ToString();
        }

        [Description("updates some spoiled data")]
        public static string Update([Description("enter update type such as hier, tags")] 
            string type)
        {
            StringBuilder sb = new StringBuilder();

            switch (type)
            {
                case "hier":
                    doArrangeHierarchies(sb, "", 1);
                    sb.Append("all hierarchies updated.\n");
                    break;
                case "tags":
                    int updatedTagCount = Provider.Database.ExecuteNonQuery(@"
                        update 
                            Tag 
                        set 
                            ContentCount = (select 
                                                count(*) 
                                            from 
                                                ContentTag t, 
                                                Content c 
                                            where 
                                                t.ContentId=c.Id and 
                                                t.TagId=Tag.Id and 
                                                c.Visible=1);");
                    int deletedTagCount = Provider.Database.ExecuteNonQuery("delete from Tag where ContentCount=0");
                    sb.AppendFormat("{0} tags updated and {1} of them decided to be deleted.\n");
                    break;
                default:
                    sb.Append("there is no this type of update\n");
                    break;
            }
            return sb.ToString();
        }

        [Description("fixes some bad characters")]
        public static string Fix_encoding([Description("enter table name such as Content, Comment, PollQuestion, etc.")] 
            string table)
        {
            StringBuilder sb = new StringBuilder();
 
            Type theType = null;
            if(!String.IsNullOrEmpty(table))
                theType = Provider.GetEntityType(table);
            int total = 0; int updated = 0;
            List<Type> types = new List<Type>();
            if (theType != null) types.Add(theType); else types = Provider.GetEntityTypes();
            foreach (Type type in types)
            {
                //Type type = typeof(Content);
                IDatabaseEntity[] entities = Provider.Database.ReadList(type, "select * from " + type.Name);
                foreach (BaseEntity entity in entities)
                {
                    foreach (PropertyInfo pi in type.GetProperties())
                    {
                        if (pi.PropertyType == typeof(string) && pi.GetSetMethod() != null)
                        {
                            object currVal = pi.GetValue(entity, null);
                            if (currVal == null) currVal = "";
                            pi.SetValue(entity, convert(currVal.ToString()), null);
                        }
                    }
                    try
                    {
                        total += 1;
                        entity.Save();
                        updated += 1;
                    }
                    catch (Exception ex)
                    {
                        sb.AppendFormat("{0}\n", ex.Message);
                    }
                }
            }
            sb.AppendFormat("Total   : {0} records\n", total);
            sb.AppendFormat("Updated : {0}\n", updated);

            return sb.ToString();
        }

        private static TextWriter textWriterForDump = null;
        [Description("dumps database schema or data or both")]
        public static string Dump(
            [Description("choose one of schema, data, both")] string dump_what,
            [Description("choose one of PostgreSQL, MySQL, SQLServer")] string db_vendor,
            [Description("enter comma seperated table names such as Content,Comment,PollQuestion or all")] string tables)
        {
            dump_what = dump_what.Trim();
            if (!(dump_what == "schema" || dump_what == "data" || dump_what == "both"))
                return String.Format("{0} is not a valid dumpWhat option! Choose one of schema, data, both.", dump_what);

            DatabaseProvider dbProvider = DatabaseProvider.MySQL;
            try
            {
                dbProvider = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), db_vendor, true);
            }
            catch {
                return String.Format("{0} is not a valid database vendor name! Choose one of PostgreSQL, MySQL, SQLServer.", db_vendor);
            }

            List<Table> tableList = new List<Table>();
            if (tables.Trim() == "all")
            {
                foreach(Table tbl in Provider.Database.Tables)
                    tableList.Add(tbl);
            }
            else
            {
                string[] arrTables = Utility.SplitWithTrim(tables, ',');
                foreach (string tableName in arrTables)
                {
                    Table tbl = Provider.Database.Tables[tableName];
                    if (tbl == null)
                        return String.Format("{0} is not a valid table name!", tableName);
                    tableList.Add(tbl);
                }
            }

            tableList.Sort(delegate(Table tbl1, Table tbl2) { return tbl1.Name.CompareTo(tbl2.Name); });

            StringBuilder sb = null;
            if (textWriterForDump == null)
            {
                sb = new StringBuilder();
                textWriterForDump = new StringWriter(sb);
            }

            foreach (Table table in tableList)
            {
                if (dump_what == "schema" || dump_what == "both")
                {
                    textWriterForDump.Write(Provider.Database.GetTableDDL(table, dbProvider));
                    textWriterForDump.WriteLine();
                }
                if (dump_what == "data" || dump_what == "both")
                {
                    dumpTable(textWriterForDump, table, dbProvider);
                    textWriterForDump.WriteLine();
                }
            }
            if (sb != null)
                return sb.ToString();
            else
                return "";
        }

        [Description("backups UserFiles folder and the database into a zip file")]
        public static string Backup(
            [Description("choose one of data, images, both")] string backup_what)
        {
            Provider.Server.ScriptTimeout = 300;

            if (string.IsNullOrEmpty(backup_what))
                backup_what = "data";

            // 1. create backup_yyyy_MM_dd folder
            string userFilesPath = Provider.Server.MapPath("UserFiles");
            string backupName = "backup_" + DateTime.Now.ToString("yyyy_MM_dd");
            string newBackupFolder = userFilesPath + "\\" + backupName;
            if (Directory.Exists(newBackupFolder)) Directory.Delete(newBackupFolder, true);
            Directory.CreateDirectory(newBackupFolder);
            // 2. create data sql dump into the backup folder
            if (backup_what == "data" || backup_what == "both")
            {
                string newBackupPath = newBackupFolder + "\\dump.sql";
                using (StreamWriter sw = new StreamWriter(newBackupPath, false, Encoding.UTF8))
                {
                    textWriterForDump = sw;
                    sw.WriteLine();
                    Dump("both", "MySQL", "all");
                    sw.Close();
                }
            }
            // 3. copy image folder into the backup folder
            if (backup_what == "images" || backup_what == "both")
            {
                Utility.CopyDirectory(userFilesPath + "\\Image", newBackupFolder + "\\");
            }
            // 4. zip the backup folder
            ICSharpCode.SharpZipLib.Zip.FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            string zipUrl = newBackupFolder + ".zip";
            zip.CreateZip(zipUrl, newBackupFolder, true, null);
            // 5. delete the backup folder
            Directory.Delete(newBackupFolder, true);
            // write download link
            return "Download backup file from : http://" + Provider.Configuration.SiteAddress + "/UserFiles/" + backupName + ".zip";
        }


        [Description("checks database tables against the last version")]
        public static string Check_tables()
        {
            StringBuilder sb = new StringBuilder();

            StringBuilder sbNotCreatedTables = new StringBuilder();
            StringBuilder sbNotAddedFields = new StringBuilder();
            StringBuilder sbIndexFaults = new StringBuilder();
            StringBuilder sbWrongTypedFields = new StringBuilder();
            StringBuilder sbWrongLengthFields = new StringBuilder();
            StringBuilder sbWrongDefaultFields = new StringBuilder();
            StringBuilder sbUnneccessaryFields = new StringBuilder();
            StringBuilder sbUnneccessaryTables = new StringBuilder();

            foreach (Type entityType in Provider.GetEntityTypes(false))
            {
                Table existingTable = Provider.Database.Tables[entityType.Name];
                if (existingTable == null)
                {
                    sbNotCreatedTables.AppendFormat("\t{0}\n", entityType.Name);
                    continue;
                }
                Table tableOughtToBe = Provider.Database.GetTableForEntityType(entityType);
                foreach (Column fieldOughtToBe in tableOughtToBe.Columns)
                {
                    Column existingField = existingTable.Columns[fieldOughtToBe.Name];
                    if (existingField == null)
                    {
                        sbNotAddedFields.AppendFormat("\t{0}.{1}\n", entityType.Name, fieldOughtToBe.Name);
                        continue;
                    }
                    if (existingField.IsPrimaryKey != fieldOughtToBe.IsPrimaryKey)
                        sbIndexFaults.AppendFormat("\t{0}.{1} should be {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsPrimaryKey ? "primary key" : "NOT primary key");
                    if (existingField.IsAutoIncrement != fieldOughtToBe.IsAutoIncrement)
                        sbIndexFaults.AppendFormat("\t{0}.{1} should be {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsAutoIncrement ? "auto increment" : "NOT auto increment");
                    if (existingField.IsNullable != fieldOughtToBe.IsNullable)
                        sbIndexFaults.AppendFormat("\t{0}.{1} should be {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsNullable ? "nullable" : "NOT nullable");
                    if (existingField.ColumnType != fieldOughtToBe.ColumnType)
                    {
                        sbWrongTypedFields.AppendFormat("\t{0}.{1} should be {2}. It is {3}.\n", entityType.Name, existingField.Name, fieldOughtToBe.ColumnType, existingField.ColumnType);
                        continue;
                    }
                    if ((existingField.ColumnType == Cinar.Database.DbType.VarChar || fieldOughtToBe.ColumnType == Cinar.Database.DbType.VarChar) && existingField.Length != fieldOughtToBe.Length)
                        sbWrongLengthFields.AppendFormat("\t{0}.{1} length should be {2}. It is {3}.\n", entityType.Name, existingField.Name, fieldOughtToBe.Length, existingField.Length);
                    if ((existingField.DefaultValue ?? "") != (fieldOughtToBe.DefaultValue ?? ""))
                        sbWrongDefaultFields.AppendFormat("\t{0}.{1} default value should be {2}. It is {3}.\n", entityType.Name, existingField.Name, fieldOughtToBe.DefaultValue, existingField.DefaultValue);
                }
                foreach (Column existingField in existingTable.Columns)
                {
                    if (tableOughtToBe.Columns[existingField.Name] == null)
                    {
                        sbUnneccessaryFields.AppendFormat("\t{0}.{1}\n", entityType.Name, existingField.Name);
                        continue;
                    }
                }
            }
            foreach (Table existingTable in Provider.Database.Tables)
            {
                try
                {
                    Type t = Provider.GetEntityType(existingTable.Name);
                }
                catch
                {
                    sbUnneccessaryTables.AppendFormat("\t{0}\n", existingTable.Name);
                }
            }

            if (sbNotCreatedTables.Length > 0) sb.AppendFormat("\n{0}\n{1}", "NONEXISTING TABLES :", sbNotCreatedTables.ToString());
            if (sbNotAddedFields.Length > 0) sb.AppendFormat("\n{0}\n{1}", "NONEXISTING FIELDS :", sbNotAddedFields.ToString());
            if (sbUnneccessaryTables.Length > 0) sb.AppendFormat("\n{0}\n{1}", "UNNECCESSARY TABLES", sbUnneccessaryTables.ToString());
            if (sbUnneccessaryFields.Length > 0) sb.AppendFormat("\n{0}\n{1}", "UNNECCESSARY FIELDS", sbUnneccessaryFields.ToString());
            if (sbIndexFaults.Length > 0) sb.AppendFormat("\n{0}\n{1}", "INDEX ERRORS :", sbIndexFaults.ToString());
            if (sbWrongTypedFields.Length > 0) sb.AppendFormat("\n{0}\n{1}", "FIELD TYPE ERRORS :", sbWrongTypedFields.ToString());
            if (sbWrongLengthFields.Length > 0) sb.AppendFormat("\n{0}\n{1}", "WRONG VARCHAR LENGTH :", sbWrongLengthFields.ToString());
            if (sbWrongDefaultFields.Length > 0) sb.AppendFormat("\n{0}\n{1}", "WRONG DEFAULT VALUES :", sbWrongDefaultFields.ToString());

            sb.Append("\ncheck completed.\n");

            return sb.ToString();
        }

        [Description("fixes database tables (enter \"help fix\" to see options)")]
        public static string Fix([Description("enter fix type such as nonexisting, unneccessary, field_definition")] 
            string options)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Type entityType in Provider.GetEntityTypes(false))
            {
                Table existingTable = Provider.Database.Tables[entityType.Name];
                if (existingTable == null)
                {
                    if (options.Contains("nonexisting"))
                    {
                        try
                        {
                            Provider.Database.CreateTableForType(entityType);
                            sb.AppendFormat("{0} created.\n", entityType.Name);
                        }
                        catch (Exception ex)
                        {
                            sb.AppendFormat("{0} cannot be created!\n\tDatabase said: {1}\n", entityType.Name, ex.Message);
                        }
                    }
                    continue;
                }
                Table tableOughtToBe = Provider.Database.GetTableForEntityType(entityType);
                foreach (Column fieldOughtToBe in tableOughtToBe.Columns)
                {
                    Column existingField = existingTable.Columns[fieldOughtToBe.Name];
                    if (existingField == null)
                    {
                        if (options.Contains("nonexisting"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " add " + Provider.Database.GetColumnDDL(fieldOughtToBe));
                                sb.AppendFormat("{0}.{1} added.\n", entityType.Name, fieldOughtToBe.Name);
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} cannot be added!\n\tDatabase said: {2}\n", entityType.Name, fieldOughtToBe.Name, ex.Message);
                            }
                        }
                        continue;
                    }
                    //if (existingField.IsPrimaryKey != fieldOughtToBe.IsPrimaryKey)
                    //    sb.AppendFormat("{0}.{1} should be {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsPrimaryKey ? "primary key" : "NOT primary key");
                    //if (existingField.IsAutoIncrement != fieldOughtToBe.IsAutoIncrement)
                    //    sb.AppendFormat("{0}.{1} should be {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsAutoIncrement ? "auto increment" : "NOT auto increment");
                    if (existingField.IsNullable != fieldOughtToBe.IsNullable)
                    {
                        if (options.Contains("field_definition"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " modify " + Provider.Database.GetColumnDDL(fieldOughtToBe));
                                sb.AppendFormat("{0}.{1} converted to {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.IsNullable ? "nullable" : "NOT nullable");
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} cannot be converted to {2}!\n\tDatabase said: {3}\n", entityType.Name, existingField.Name, fieldOughtToBe.IsNullable ? "nullable" : "NOT nullable", ex.Message);
                            }
                        }
                    }
                    if (existingField.ColumnType != fieldOughtToBe.ColumnType)
                    {
                        if (options.Contains("field_definition"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " modify " + Provider.Database.GetColumnDDL(fieldOughtToBe));
                                sb.AppendFormat("{0}.{1} type converted to {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.ColumnType);
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} type cannot be converted to {2}!\n\tDatabase said: {3}\n", entityType.Name, existingField.Name, fieldOughtToBe.ColumnType, ex.Message);
                            }
                        }
                        continue;
                    }
                    if ((existingField.ColumnType == Cinar.Database.DbType.VarChar || fieldOughtToBe.ColumnType == Cinar.Database.DbType.VarChar) && existingField.Length != fieldOughtToBe.Length)
                    {
                        if (options.Contains("field_definition"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " modify " + Provider.Database.GetColumnDDL(fieldOughtToBe));
                                sb.AppendFormat("{0}.{1} length changed to {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.Length);
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} length cannot be changed to {2}!\n\tDatabase said: {3}\n", entityType.Name, existingField.Name, fieldOughtToBe.Length, ex.Message);
                            }
                        }
                    }
                    if ((existingField.DefaultValue ?? "") != (fieldOughtToBe.DefaultValue ?? ""))
                    {
                        if (options.Contains("field_definition"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " modify " + Provider.Database.GetColumnDDL(fieldOughtToBe));
                                sb.AppendFormat("{0}.{1} default value changed to {2}.\n", entityType.Name, existingField.Name, fieldOughtToBe.DefaultValue);
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} default value cannot be changed to {2}!\n\tDatabase said: {3}\n", entityType.Name, existingField.Name, fieldOughtToBe.DefaultValue, ex.Message);
                            }
                        }
                    }
                }
                foreach (Column existingField in existingTable.Columns)
                {
                    if (tableOughtToBe.Columns[existingField.Name] == null)
                    {
                        if (options.Contains("unneccessary"))
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery("alter table " + entityType.Name + " drop " + existingField.Name);
                                sb.AppendFormat("{0}.{1} dropped.\n", entityType.Name, existingField.Name);
                            }
                            catch (Exception ex)
                            {
                                sb.AppendFormat("{0}.{1} cannot be added!\n\tDatabase said: {2}\n", entityType.Name, existingField.Name, ex.Message);
                            }
                        }
                        continue;
                    }
                }
            }
            foreach (Table existingTable in Provider.Database.Tables)
            {
                try
                {
                    Type t = Provider.GetEntityType(existingTable.Name);
                }
                catch
                {
                    if (options.Contains("unneccessary"))
                    {
                        try
                        {
                            Provider.Database.ExecuteNonQuery("drop table " + existingTable.Name);
                            sb.AppendFormat("{0} dropped.\n", existingTable.Name);
                        }
                        catch (Exception ex)
                        {
                            sb.AppendFormat("{0} cannot be dropped!\n\tDatabase said: {1}\n", existingTable.Name, ex.Message);
                        }
                    }
                }
            }
            sb.Append("\nfix completed.\n");

            Provider.Database.Refresh();

            return sb.ToString();
        }

        [Description("executes SQL")]
        public static string Sql([Description("SQL query to execute")] 
            string query)
        {
            if (query.StartsWith("insert") || query.StartsWith("update") || query.StartsWith("delete"))
            {
                int affectedRowCount = Provider.Database.ExecuteNonQuery(query);
                return String.Format("{0} rows affected.\n", affectedRowCount);
            }
            else
            {
                DataTable dt = Provider.Database.GetDataTable(query);
                if (dt != null && dt.Rows.Count > 100)
                    throw new Exception("Too much results returned. Please use \"top\" or \"limit\" to limit your query result set.");
                return Utility.ToStringTable(dt);
            }
        }

        [Description("creates templates from existing ASPX files")]
        public static string Generate_templates()
        {
            StringBuilder sb = new StringBuilder();

            Provider.Database.Begin();

            try
            {

                foreach (string aspxPath in System.IO.Directory.GetFiles(Provider.Server.MapPath("~"), "*.aspx"))
                {
                    string fileName = System.IO.Path.GetFileName(aspxPath);

                    Template template = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", fileName);
                    if (template == null)
                    {
                        template = new Template();
                        template.FileName = fileName;
                        sb.AppendFormat("{0} created.\n", fileName);
                    }
                    else
                        sb.AppendFormat("{0} updated.\n", fileName);

                    string code = System.IO.File.ReadAllText(aspxPath, Encoding.UTF8);
                    code = code.Replace(@"<%@ Page Language=""C#"" %>", "");
                    code = code.Replace(@" runat=""server""", "");
                    code = code.Replace("</head>", "$=this.HeadSection$\r\n</head>");

                    Regex regexObj = new Regex(@"\<sm\:Region.+ID=""(?<id>.+?)"".+\/\>", RegexOptions.Multiline);
                    Match match = regexObj.Match(code);
                    while (match.Success)
                    {
                        code = code.Replace(match.Value, String.Format(@"<div id=""{0}"" class=""Region {0}"">$=this.{0}$</div>", match.Groups["id"].Value));
                        match = match.NextMatch();
                    }

                    template.HTMLCode = code;
                    template.Save();
                }

                string styles = Properties.Resources._default;
                if (System.IO.File.Exists(Provider.Server.MapPath("~/default.css")))
                {
                    styles = System.IO.File.ReadAllText(Provider.Server.MapPath("~/default.css"), Encoding.UTF8);
                    sb.Append("default.css added.\n");
                }

                if (System.IO.File.Exists(Provider.Server.MapPath("~/CMS.conf")))
                {
                    Configuration conf = new Configuration();

                    System.IO.StreamReader sr = null;
                    try
                    {
                        sr = new System.IO.StreamReader(Provider.Server.MapPath("~/CMS.conf"), Encoding.UTF8);
                        System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
                        conf = (Configuration)ser.Deserialize(sr);
                        sr.Close();
                        sb.Append("Configuration updated.\n");
                    }
                    catch
                    {
                        conf = Configuration.Read();
                    }

                    conf.Id = 1;
                    conf.DefaultStyleSheet = styles;
                    conf.Save();
                }

                Provider.Database.Commit();

                sb.Append("\n");
                sb.Append("Please check if the website is OK then delete *.aspx, default.css and CMS.conf files.");
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }

            return sb.ToString();
        }



        private static void dumpTable(TextWriter tw, Cinar.Database.Table tbl, DatabaseProvider dbProvider)
        {
            if (tbl == null)
                throw new Exception("No such table.");

            string delimitL = "[", delimitR = "]";
            switch (dbProvider)
            {
                case DatabaseProvider.PostgreSQL:
                    delimitL = "\""; delimitR = "\"";
                    break;
                case DatabaseProvider.MySQL:
                    delimitL = "`"; delimitR = "`";
                    break;
                case DatabaseProvider.SQLServer:
                    delimitL = "["; delimitR = "]";
                    break;
                default:
                    break;
            }

            string fields = delimitL + String.Join(delimitR + ", " + delimitL, tbl.Columns.ToStringArray()) + delimitR;
            string sql = String.Format("insert into {2}{0}{3} ({1}) values ({{0}});", tbl.Name, fields, delimitL, delimitR);

            DataTable dt = Provider.Database.GetDataTable("select * from [" + tbl.Name + "]");
            foreach (DataRow dr in dt.Rows)
            {
                string[] values = new string[tbl.Columns.Count];
                for (int i = 0; i < tbl.Columns.Count; i++)
                {
                    string fieldName = tbl.Columns[i].Name;
                    if (dt.Columns[i].DataType == typeof(bool))
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + (dr[tbl.Columns[i].Name].Equals(true) ? 1 : 0) + "'";
                    else if (dt.Columns[i].DataType == typeof(DateTime))
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + ((DateTime)dr[tbl.Columns[i].Name]).ToString("yyyy-MM-dd HH:mm") + "'";
                    else
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + dr[tbl.Columns[i].Name].ToString().Replace("'", "''").Replace("\n", "\\n").Replace("\r", "\\r") + "'";
                }
                tw.Write(sql, String.Join(", ", values));
                tw.WriteLine();
            }
        }
        private static void doArrangeHierarchies(StringBuilder sb, string baseHier, int catId)
        {
            string hierarchy = baseHier + catId.ToString().PadLeft(5, '0');

            sb.Append(hierarchy + "\n");

            Provider.Database.ExecuteNonQuery("update content set Hierarchy={0} where CategoryId={1}", hierarchy, catId);

            DataTable dt = Provider.Database.GetDataTable("select Id from Content where CategoryId={0}", catId);
            if (dt != null)
                foreach (DataRow dr in dt.Rows)
                    doArrangeHierarchies(sb, hierarchy + ",", (int)dr["Id"]);
        }
        private static string convert(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.
                Replace("Ã‡", "Ç").
                Replace("Ã–", "Ö").
                Replace("Ä°", "İ").
                Replace("Åž", "Ş").
                Replace("Ãœ", "Ü").
                Replace("Ã§", "ç").
                Replace("Ã¶", "ö").
                Replace("Ä±", "ı").
                Replace("ÅŸ", "ş").
                Replace("Ã¼", "ü").
                Replace("ÄŸ", "ğ").
                Replace("Ã¢", "â").
                Replace("Â", "");
        }
    }
}
