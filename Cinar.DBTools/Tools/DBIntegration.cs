using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.DBTools.Tools
{
    public class DBIntegration
    {
        public DBIntegration()
        {
            Tasks = new List<DBIntegrationTask>();
        }

        public List<DBIntegrationTask> Tasks { get; set; }
        public string ScriptIncludeCode { get; set; }
    }
    public class DBIntegrationTask
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int ExecInterval { get; set; }
        public string SourceDB { get; set; }
        public string DestDB { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            string sourceDB = string.IsNullOrEmpty(SourceDB) ? "???" : SourceDB.Split(' ').First();
            string destDB = string.IsNullOrEmpty(DestDB) ? "???" : DestDB.Split(' ').First();
            return string.Format("{0,-25} {2,25} -> {3,-25}  {1,5} ms", Name, ExecInterval, sourceDB, destDB);
        }
    }
}
