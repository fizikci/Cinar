using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using $=util.Cap(table.Database.Name)$.Library.Entities;
using Cinar.Database;
using Cinar.Scripting;
using System.Configuration;
using System.Runtime.InteropServices;

namespace $=util.Cap(table.Database.Name)$.Library
{
    public class Provider
    {
        private static Database db;
        public static Database Database
        {
            get
            {
                if (db == null)
                {
                    string sqlCon = ConfigurationSettings.AppSettings["sqlConnection"];
                    DatabaseProvider sqlPro = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), ConfigurationSettings.AppSettings["sqlProvider"]);
                    db = new Database(sqlCon, sqlPro);
                }
                return db;
            }
        }

        public static Type GetEntityType(string entityName)
        {
            return Assembly.GetExecutingAssembly().GetType("$=util.Cap(table.Database.Name)$.Library.Entities." + entityName, false, true);
        }
        public static Entities.BaseEntity CreateEntity(Type entityType)
        {
            return (Entities.BaseEntity)entityType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }
        public static Entities.BaseEntity CreateEntity(string entityName)
        {
            return CreateEntity(GetEntityType(entityName));
        }

        public static List<Type> GetEntityTypes()
        {
            return GetEntityTypes(false);
        }
        public static List<Type> GetEntityTypes(bool abstractEntities)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.IsSubclassOf(typeof(Entities.BaseEntity)) && type.IsAbstract==abstractEntities)
                    types.Add(type);
            return types;
        }
        public static IDatabaseEntity[] GetIdNameList(string entityName, string extraWhere)
        {
            Type tip = Provider.GetEntityType(entityName);
            BaseEntity sampleEntity = Provider.CreateEntity(entityName);

            string where = "where 1=1" + (String.IsNullOrEmpty(extraWhere) ? "" : " AND (" + extraWhere + ")");

            switch (entityName)
            {
                case "Content":
                    where += " AND ClassName='Category'";
                    break;
            }
            IDatabaseEntity[] entities = Provider.Database.ReadList(tip, "select Id, [" + sampleEntity.GetNameField() + "] from [" + entityName + "]" + where + " order by [" + sampleEntity.GetNameField() + "]");
            return entities;
        }

        private static User user;
        public static User User
        {
            get
            {
                if (user == null) user = new User();
                return user;
            }
            set { user = value; }
        }

        private static string currentCulture;
        public static string CurrentCulture
        {
            get
            {
                return currentCulture;
            }
            internal set
            {
                currentCulture = value;
            }
        }

        private static Configuration configuration;
        public static Configuration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = Configuration.Read();
                }
                return configuration;
            }
            set
            {
                configuration = value;
            }
        }

        private static string tmpFolderPath;
        public static string TempFolderPath
        {
            get 
            {
                if (tmpFolderPath == null)
                {
                    tmpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\$=util.Cap(table.Database.Name)$";
                    if (!Directory.Exists(tmpFolderPath))
                        Directory.CreateDirectory(tmpFolderPath);
                }
                return tmpFolderPath;
            }
        }

        public static string GetResource(string code) 
        {
            return "?" + code;
        }

        private static Server _server;
        public static Server Server
        {
            get {
                if (_server == null)
                    _server = new Server();
                return _server;
            }
        }

        public static Interpreter GetInterpreter(string template, object forThis)
        {
            Interpreter engine = new Interpreter(template, new List<string>() { "$=util.Cap(table.Database.Name)$.Library" });
            engine.SetAttribute("Context", new ProviderWrapper());
            engine.SetAttribute("this", forThis);
            return engine;
        }


    }

    public class Server
    {
        public string MapPath(string path)
        {
            string rootPath = Assembly.GetEntryAssembly().Location;
            string fileName = Path.GetFileName(rootPath);
            return rootPath.Replace(fileName, path);
        }
    }

    public class ProviderWrapper
    {
        public DataTable GetDataTable(string sql)
        {
            return Provider.Database.GetDataTable(sql);
        }
        public DataRow GetDataRow(string sql)
        {
            return Provider.Database.GetDataRow(sql);
        }
        public object GetValue(string sql)
        {
            return Provider.Database.GetValue(sql);
        }
        public int ExecuteNonQuery(string sql)
        {
            return Provider.Database.ExecuteNonQuery(sql);
        }
    }


}
