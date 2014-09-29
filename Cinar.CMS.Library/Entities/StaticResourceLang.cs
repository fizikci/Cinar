using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
using System.Web;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public class StaticResourceLang : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, References = typeof(StaticResource)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int StaticResourceId { get; set; }

        [XmlIgnore]
        public StaticResource StaticResource
        {
            get { return Provider.Database.Read<StaticResource>(this.StaticResourceId); }
        }

        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId { get; set; }

        [XmlIgnore]
        public Lang Lang
        {
            get { return Provider.Database.Read<Lang>(this.LangId); }
        }

        [ColumnDetail(IsNotNull = true, Length = 200)]
        public string Translation { get; set; }

        public override string GetNameValue()
        {
            return this.Translation;
        }
        public override string GetNameColumn()
        {
            return "Translation";
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            var srl = (Dictionary<string, string>)HttpContext.Current.Cache["StaticResourceLang"];
            if (srl == null) return; //***
            if (!isUpdate)
                srl.Add(StaticResourceId + "_" + LangId, this.Translation);
            else
            {
                srl.Remove(this.GetOriginalValues()["StaticResourceId"] + "_" + this.GetOriginalValues()["LangId"]);
                srl.Add(StaticResourceId + "_" + LangId, this.Translation);
            }
        }

        protected override void afterDelete()
        {
            var srl = (Dictionary<string, string>)HttpContext.Current.Cache["StaticResourceLang"];
            if (srl == null) return; //***
            srl.Remove(StaticResourceId + "_" + LangId);
        }
    }

}
