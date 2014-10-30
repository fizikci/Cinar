using System;
using System.Net;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Handlers;
using Cinar.Database;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;
using Cinar.Scripting;
using System.Collections.Generic;

namespace Cinar.CMS.Library.Modules
{
    public class ModuleHistory : BaseEntity
    {
        public int ModuleId { get; set; }

        public string ElementName { get; set; }

        public string ElementId { get; set; }
        
        public string CSSClass { get; set; }
        
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        public string CSS { get; set; }
        
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        public string TopHtml { get; set; }
        
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        public string BottomHtml { get; set; }
        
        public string RoleToRead { get; set; }
        [ColumnDetail(IsNotNull = true, Length = 30, DefaultValue = "Default")]
        
        public string UseCache { get; set; }
        
        public int CacheLifeTime { get; set; }
        
        [ColumnDetail(IsNotNull = true, Length = 30)]
        public string Name { get; set; }
        
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        public string Details { get; set; }
        
        public int ParentModuleId { get; set; }
    }
}
