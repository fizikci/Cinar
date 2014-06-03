using System;
using System.Text;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class Form : StaticHtml
    {
        public Form()
        {
            EntityName = "";
        }

        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName { get; set; }

        bool entityNameChanged = false;

        public override void SetFieldsByPostData(System.Collections.Specialized.NameValueCollection postData)
        {
            string oldEntityName = this.EntityName;
            base.SetFieldsByPostData(postData);
            entityNameChanged = oldEntityName != this.EntityName;
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (entityNameChanged && EntityName != "")
            {
                StringBuilder sb = new StringBuilder();

                foreach (Column field in Provider.Database.Tables[EntityName].Columns)
                {
                    Type fieldType = Provider.GetEntityType(EntityName).GetProperty(field.Name).PropertyType;
                    ColumnDetailAttribute attrField = (ColumnDetailAttribute)CMSUtility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof(ColumnDetailAttribute));
                    EditFormFieldPropsAttribute attrEdit = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof(EditFormFieldPropsAttribute));

                    if (field.IsPrimaryKey || !attrEdit.Visible)
                        continue; //***

                    FormField ff = new FormField();
                    ff.EntityName = this.EntityName;
                    ff.FieldName = field.Name;

                    sb.AppendLine(ff.GetHtml());
                }

                this.InnerHtml = sb.ToString();
            }
        }


        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
