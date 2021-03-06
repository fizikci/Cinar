﻿using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select SourceLang.Id as [SourceLang.Id], SourceLang.Name as [SourceLang.Name], Lang.Name as [Lang.Name], SourceLang.Visible as [SourceLang.Visible] from [SourceLang] left join [Lang] ON Lang.Id = [SourceLang].LangId", QueryOrderBy = "SourceLang.Id desc")]
    public class SourceLang : NamedEntityLang
    {
        private int sourceId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Source)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        private Source _source;
        [XmlIgnore]
        public Source Source
        {
            get
            {
                if (_source == null)
                    _source = (Source)Provider.Database.Read(typeof(Source), this.sourceId);
                return _source;
            }
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            // isimsiz source mi olur! kontrolu
            if (String.IsNullOrEmpty(this.Name))
                throw new Exception(Provider.GetResource("Required field: Name"));
        }
    }

}
