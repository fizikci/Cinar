using Cinar.Database;
using System;
using System.Web;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Name from [StaticResource]", QueryOrderBy = "Name")]
    public class StaticResource : BaseEntity, IComparable<StaticResource>
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

        protected override void afterSave(bool isUpdate)
        {
            base.afterSave(isUpdate);

            HttpContext.Current.Cache.Remove("StaticResource");
        }

        public int CompareTo(StaticResource other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
