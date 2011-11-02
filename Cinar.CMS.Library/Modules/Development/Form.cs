using System;
using System.Text;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class Form : Container
    {
        public Form()
        {
            AfterSaveBehavior = "BackToList";
            EntityName = "";
        }

        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName { get; set; }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_AFTERSAVEBEHAVIOR_")]
        public string AfterSaveBehavior { get; set; }

        DataRow data;
        string error = "";

        protected internal override void beforeShow()
        {
            if (Provider.Request.Form["cmdSave" + this.Id] != null)
            {
                try
                {
                    int id = saveEntity();
                    switch (AfterSaveBehavior)
                    {
                        case "RedirectToSameUrl":
                            Provider.Response.Redirect(Provider.Request.RawUrl, true);
                            return;
                        case "NewRecord":
                            Provider.Response.Redirect(Utility.GetRequestFileName() + "?returnUrl=" + Provider.Server.UrlEncode(Provider.Request["returnUrl"]), true);
                            return;
                        case "ShowSaved":
                            Provider.Response.Redirect(Utility.GetRequestFileName() + "?item=" + id + "&returnUrl=" + Provider.Server.UrlEncode(Provider.Request["returnUrl"]), true);
                            return;
                        case "BackToList":
                            Provider.Response.Redirect(Provider.Server.UrlDecode(Provider.Request["returnUrl"]), true);
                            return;
                    }
                }
                catch (Exception ex)
                {
                    error = string.Format("<div class=\"errMsg\">{0}:<br/>{1}</div>", Provider.GetResource("Error"), ex.Message);
                }
            }

            if (Provider.Request["item"] != null)
                data = Provider.Database.GetDataRow("select * from " + EntityName + " where Id={0}", Provider.Request["item"]);
            if (data == null)
                data = Provider.Database.EntityToDataRow(Provider.CreateEntity(this.EntityName));

            foreach (Module field in this.ChildModules)
            {
                FormField formField = field as FormField;
                if (formField == null) continue;

                if (Provider.Request.Form[formField.FieldName] != null)
                    formField.value = Provider.Request.Form[formField.FieldName];
                else
                {
                    if (data.Table.Columns.Contains(formField.FieldName))
                        formField.value = data[formField.FieldName].ToString();
                }
                formField.data = data;
                formField.form = this;
            }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (error != "")
                sb.Append(error);

            sb.AppendFormat("<form enctype=\"multipart/form-data\" action=\"{0}\" method=\"post\" name=\"form{1}\" id=\"form{1}\">", Provider.Request.RawUrl, this.Id);
            sb.AppendFormat("<input type=\"hidden\" name=\"Id\" value=\"{0}\"/>", (data == null ? 0 : (int)data["Id"]));
            sb.AppendFormat("{0}", base.show());

            sb.Append("<div style=\"clear:both\"></div>");

            sb.AppendFormat("<input type=\"submit\" name=\"cmdSave{0}\" value=\"{1}\"/>", this.Id, Provider.GetModuleResource("Save"));

            sb.Append("</form>");

            return sb.ToString();
        }

        private int saveEntity()
        {
            int id = Int32.Parse(Provider.Request.Form["Id"]);
            BaseEntity entity = null;
            if (id > 0)
                entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(EntityName), id);
            else
                entity = (BaseEntity)Provider.CreateEntity(EntityName);
            entity.SetFieldsByPostData(Provider.Request.Form);
            entity.Save();
            return entity.Id;
        }

        bool entityNameChanged = false;

        public override void SetFieldsByPostData(System.Collections.Specialized.NameValueCollection postData)
        {
            string oldEntityName = this.EntityName;
            base.SetFieldsByPostData(postData);
            entityNameChanged = oldEntityName != this.EntityName;
        }

        protected override void afterSave(bool isUpdate)
        {
            if (entityNameChanged)
            {
                if (isUpdate)
                    foreach (Module mdl in Module.Read(Provider.Database.GetDataTable("select * from Module where Name='FormField' and ParentModuleId=" + this.Id)))
                        mdl.Delete();

                if (EntityName != "")
                    foreach (Column field in Provider.Database.Tables[EntityName].Columns)
                    {
                        Type fieldType = Provider.GetEntityType(EntityName).GetProperty(field.Name).PropertyType;
                        ColumnDetailAttribute attrField = (ColumnDetailAttribute)Utility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof(ColumnDetailAttribute));
                        EditFormFieldPropsAttribute attrEdit = (EditFormFieldPropsAttribute)Utility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof(EditFormFieldPropsAttribute));

                        if (field.IsPrimaryKey || !attrEdit.Visible)
                            continue; //***

                        FormField ff = new FormField();
                        ff.ParentModuleId = this.Id;
                        ff.EntityName = this.EntityName;
                        ff.FieldName = field.Name;
                        ff.Region = "frmRegion" + this.Id;
                        ff.Template = this.Template;
                        ff.Save();
                    }
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input {{height:20px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.infoMsg {{padding:10px;margin:10px;border:1px dashed #80FF80;background:#EFFFEF}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.errMsg {{padding:10px;margin:10px;border:1px dashed #FF8080;background:#FFEFEF}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
