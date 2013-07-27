using System;
using System.Text;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class FormField : Module
    {
        public FormField()
        {
            FixedValue = "";
            Where = "";
            EntityName = "";
            Label = "";
            UIControlType = "";
            FieldName = "";
        }

        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "entityName:'use#EntityName'")]
        public string FieldName { get; set; }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_UICONTROLTYPE_")]
        public string UIControlType { get; set; }

        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Label { get; set; }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes,hideItems:true")]
        public string EntityName { get; set; }

        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Where { get; set; }

        [EditFormFieldProps(Options = "noHTML:true")]
        public string FixedValue { get; set; }

        internal DataRow data;
        internal string value = "";
        internal Form form;

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(FieldName))
                return Provider.DesignMode ? Provider.GetResource("Select field name") : String.Empty;

            Type entityType = Provider.GetEntityType(EntityName);
            Type fieldType = entityType.GetProperty(FieldName).PropertyType;
            PropertyInfo pi = entityType.GetProperty(FieldName);
            ColumnDetailAttribute attrField = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));
            EditFormFieldPropsAttribute attrEdit = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
            ControlType ct = attrEdit.ControlType;
            if (ct == ControlType.Undefined)
                ct = Provider.GetDefaultControlType(attrField.ColumnType, pi, attrField);

            if (String.IsNullOrEmpty(Label)) Label = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);
            if (String.IsNullOrEmpty(Label)) Label = FieldName;
            if (!String.IsNullOrEmpty(FixedValue))
            {
                if (FixedValue.StartsWith("@") && !String.IsNullOrEmpty(Provider.Request[FixedValue.Substring(1)]))
                    value = Provider.Request[FixedValue.Substring(1)];
                else
                    value = FixedValue;
            }

            // CONTROL
            if (UIControlType == "Hidden")
            {
                if (Provider.DesignMode)
                {
                    sb.AppendFormat("<div class=\"label\">{0} (gizli)</div>", Label);
                    sb.Append(getControlHTML(fieldType, attrField, attrEdit, ct));
                }
                else 
                {
                    sb.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", FieldName, CMSUtility.HtmlEncode(value));
                }
            }
            else
            {
                // eðer PictureEdit ise label'a "Sil" linki ekleyelim
                if (ct == ControlType.PictureEdit)
                    Label += String.Format(" (<span style=\"color:blue;cursor:pointer\" onclick=\"if(ajax({{url:'EntityInfo.ashx?method=setField&entityName={0}&id={1}&fieldName={2}&value=',isJSON:false,noCache:false}})) {{ $('form{3}').{2}.value=''; $('form{3}').submit(); }}\">{4}</span>)",
                        EntityName,
                        (data == null ? 0 : (int)data["Id"]),
                        FieldName,
                        form == null ? 0 : form.Id,
                        Provider.GetModuleResource("Delete"));
                // label'ý gösterelim
                sb.AppendFormat("<div class=\"label\">{0}</div>", Label);
                sb.Append(getControlHTML(fieldType, attrField, attrEdit, ct));
            }

            return sb.ToString();
        }

        private string getControlHTML(Type fieldType, ColumnDetailAttribute attrField, EditFormFieldPropsAttribute attrEdit, ControlType ct)
        {
            StringBuilder sb = new StringBuilder();
            switch (ct)
            {
                case ControlType.StringEdit:
                    if (UIControlType == "Textarea")
                        sb.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" class=\"editWithFCK\">{1}</textarea>", FieldName, CMSUtility.HtmlEncode(value));
                    else
                        sb.AppendFormat("<input type=\"text\" name=\"{0}\" value=\"{1}\"/>", FieldName, CMSUtility.HtmlEncode(value));
                    break;
                case ControlType.IntegerEdit:
                case ControlType.DecimalEdit:
                case ControlType.DateTimeEdit:
                    sb.AppendFormat("<input type=\"text\" name=\"{0}\" value=\"{1}\"/>", FieldName, CMSUtility.HtmlEncode(value));
                    break;
                case ControlType.PictureEdit:
                    sb.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/><input type=\"file\" name=\"{0}File\"/>", FieldName, CMSUtility.HtmlEncode(value));
                    break;
                case ControlType.ComboBox:
                case ControlType.LookUp:
                    if (fieldType == typeof(bool))
                    {
                        bool b = (value == "True" || value == "1");
                        switch (UIControlType)
                        {
                            case "Check":
                                sb.AppendFormat("<input type=\"checkbox\" name=\"{0}\" value=\"1\" {1}/> {2}", FieldName, b ? "checked" : "", Provider.GetResource("Yes"));
                                break;
                            case "Radio":
                                sb.AppendFormat("<input type=\"radio\" name=\"{0}\" {1} value=\"1\"/>{3} <input type=\"radio\" name=\"{0}\" {2} value=\"0\">{4}", FieldName, b ? "checked" : "", b ? "" : "selected", Provider.GetResource("Yes"), Provider.GetResource("No"));
                                break;
                            default:
                                sb.AppendFormat("<select name=\"{0}\"><option {1} value=\"1\">{3}</option><option {2} value=\"0\">{4}</option></select>", FieldName, b ? "selected" : "", b ? "" : "selected", Provider.GetResource("Yes"), Provider.GetResource("No"));
                                break;
                        }
                    }
                    else if (attrField.References != null)
                    {
                        IDatabaseEntity[] entities = Provider.GetIdNameList(attrField.References.Name, "", this.Where);
                        switch (UIControlType)
                        {
                            case "Check":
                                foreach (BaseEntity entity in entities)
                                    sb.AppendFormat("<input type=\"checkbox\" name=\"{0}\" value=\"{1}\">{2}", FieldName, entity.Id, CMSUtility.HtmlEncode(entity.GetNameValue()));
                                break;
                            case "Radio":
                                foreach (BaseEntity entity in entities)
                                    sb.AppendFormat("<input type=\"radio\" name=\"{0}\" value=\"{1}\" {2}>{3}", FieldName, entity.Id, entity.Id.ToString() == value ? "checked" : "", CMSUtility.HtmlEncode(entity.GetNameValue()));
                                break;
                            default:
                                sb.AppendFormat("<select name=\"{0}\">", FieldName);
                                sb.AppendFormat("<option value=\"{0}\" {1}>{2}</value>", 0, "0" == value ? "selected" : "", Provider.GetResource("Select"));
                                foreach (BaseEntity entity in entities)
                                    sb.AppendFormat("<option value=\"{0}\" {1}>{2}</value>", entity.Id, entity.Id.ToString() == value ? "selected" : "", CMSUtility.HtmlEncode(entity.GetNameValue()));
                                sb.AppendFormat("</select>");
                                break;
                        }
                    }
                    else
                    {
                        bool optionsFound = false;
                        // örnek "items:[['Content','Ýçerik'],['Category','Kategori'],['News','Haber'],['Article','Makale'],['Gallery','Galeri'],['Video','Video'],['LastMinute','Son Dakika']]";
                        attrEdit.Options = Provider.ParseOptions(attrEdit.Options);
                        int startIndex = attrEdit.Options.IndexOf("items:[[");
                        int endIndex = -1;
                        if (startIndex > -1) {
                            startIndex += "items:[[".Length;
                            endIndex = attrEdit.Options.IndexOf("]]", startIndex);
                            if (endIndex > -1)
                            {
                                optionsFound = true;
                                string options = attrEdit.Options.Substring(startIndex, endIndex - startIndex);
                                string[] optionsArr = options.Split(new string[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
                                sb.AppendFormat("<select name=\"{0}\">", FieldName);
                                sb.AppendFormat("<option value=\"{0}\" {1}>{2}</value>", "", String.IsNullOrEmpty(value) ? "selected" : "", Provider.GetResource("Select"));
                                for (int i = 0; i < optionsArr.Length; i++)
                                {
                                    string[] keyVal = optionsArr[i].Trim('\'').Split(new string[] { "','" }, StringSplitOptions.RemoveEmptyEntries);
                                    sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", keyVal[0], keyVal[0] == value ? "selected" : "", keyVal[1]);
                                }
                                sb.AppendFormat("</select>");
                            }
                        }
                        if(!optionsFound)
                            sb.AppendFormat("<input type=\"text\" value=\"{0}\"/>", "Kombo olmasý gerekiyor ama henüz enum tarzý kombo problemi çözülmedi."); ;
                    }
                    break;
                case ControlType.CSSEdit:
                case ControlType.MemoEdit:
                    if(UIControlType == "Input")
                        sb.AppendFormat("<input type=\"text\" name=\"{0}\" value=\"{1}\"/>", FieldName, CMSUtility.HtmlEncode(value));
                    else
                        sb.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" class=\"editWithFCK\">{1}</textarea>", FieldName, CMSUtility.HtmlEncode(value));
                    break;
                case ControlType.Undefined:
                case ControlType.FilterEdit:
                default:
                    throw new Exception(Provider.GetResource("This kind of form field is not supported: {0}", ct));
            }
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (String.IsNullOrEmpty(this.EntityName))
            {
                Form parentForm = (Form)Module.Read(this.ParentModuleId);
                this.EntityName = parentForm.EntityName;
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{width:50%;padding:4px 4px 0px 0px;float:left;clear:none}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.label {{width:100%;overflow:hidden}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input {{width:100%}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} select {{width:100%}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} textarea {{width:100%}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
