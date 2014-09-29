using System.Collections.Generic;
using Cinar.Database;
using System;
using System.Web;
using System.Xml.Serialization;
using System.Linq;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Name from [StaticResource]", QueryOrderBy = "Name")]
    public class StaticResource : BaseEntity
    {
        [ColumnDetail(IsNotNull=true), EditFormFieldProps(Options = "isHTML:false")]
        public string Name { get; set; }

        public override string GetNameValue()
        {
            return this.Name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            var sr = (Dictionary<string, int>)HttpContext.Current.Cache["StaticResource"];
            if (sr == null) return;//***
            if (!isUpdate)
                sr.Add(Name, Id);
            // StaticResource update edilemez!!
        }

        protected override void afterDelete()
        {
            base.afterDelete();

            var sr = (Dictionary<string, int>)HttpContext.Current.Cache["StaticResource"];
            if (sr == null) return;//***
            sr.Remove(Name);
        }
    }
}
