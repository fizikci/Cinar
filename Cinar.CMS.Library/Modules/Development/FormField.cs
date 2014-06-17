using System;
using System.Text;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using DbType = Cinar.Database.DbType;


namespace Cinar.CMS.Library.Modules
{
    public class FormField
    {
        public FormField()
        {
            EntityName = "";
            FieldName = "";
        }

        public string FieldName { get; set; }
        public string EntityName { get; set; }

        internal DataRow data;
        internal string value = "";
        internal Form form;

        public string GetHtml()
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

            string label = FieldName.HumanReadable();

            sb.AppendFormat(@"
        <div class=""form-group"">
            <label for=""{0}"" class=""col-sm-4 control-label no-padding-right""> {1} </label>
    		<div class=""{3}"">
                {2}
            </div>
        </div>", FieldName, label, GetControlHtml(fieldType, attrField, attrEdit, ct), ct == ControlType.DateTimeEdit ? "col-sm-4 col-xs-6 input-group" : "col-sm-8");

            return sb.ToString();
        }

        private string GetControlHtml(Type fieldType, ColumnDetailAttribute attrField, EditFormFieldPropsAttribute attrEdit, ControlType ct)
        {
            StringBuilder sb = new StringBuilder();
            switch (ct)
            {
                case ControlType.TagEdit:
                    if (attrField.References != null)
                        sb.AppendFormat(@"<select multiple="""" class=""width-80 chosen-select form-control"" id=""{0}"" name=""{0}"" data-placeholder=""Select tags"">
                $ foreach(var e in Provider.Database.ReadList(typeof({1}), ""select Id, Name from {1} order by Name"")) {{ $
	                <option>$=e.Name$</option>
	            $ }} $
            </select>", FieldName, attrField.References.Name);
                    else
                        sb.AppendFormat(@"<select multiple="""" class=""width-80 chosen-select form-control"" id=""{0}"" name=""{0}"" data-placeholder=""Select tags"">
                <option>Tag1</option>
                <option>Tag2</option>
                <option>Tag3</option>
            </select>", FieldName);
                    break;
                case ControlType.StringEdit:
                    if (attrField.Length>100 || attrField.ColumnType== DbType.Text)
                        sb.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" rows=\"3\" class=\"col-sm-8 form-control\"></textarea>", FieldName);
                    else
                        sb.AppendFormat("<input type=\"text\" name=\"{0}\" id=\"{0}\" placeholder=\"\" class=\"col-sm-6 form-control\"/>", FieldName);
                    break;
                case ControlType.IntegerEdit:
                case ControlType.DecimalEdit:
                    sb.AppendFormat("<input class=\"input-mini bkspinner form-control\" type=\"text\" name=\"{0}\" id=\"{0}\"/>", FieldName);
                    break;
                case ControlType.DateTimeEdit:
                    sb.AppendFormat("<input class=\"date-picker form-control\" type=\"text\" name=\"{0}\" id=\"{0}\" data-date-format=\"dd-mm-yyyy\"/><span class=\"input-group-addon\"><i class=\"icon-calendar bigger-110\"></i></span>", FieldName);
                    break;
                case ControlType.PictureEdit:
                    sb.AppendFormat("<img name=\"{0}\" class=\"img-responsive\" src=\"/UserFiles/contact.png\" /><input class=\"form-control\" type=\"file\" name=\"{0}\" id=\"{0}\"/>", FieldName);
                    break;
                case ControlType.ComboBox:
                    if (fieldType == typeof(bool))
                        sb.AppendFormat("<input type=\"checkbox\" name=\"{0}\" id=\"{0}\" class=\"ace ace-switch ace-switch-5 form-control\" value=\"1\"/><span class=\"lbl\"></span>", FieldName);
                    if(attrField.References!=null)
                        sb.AppendFormat(@"<select id=""{0}"" name=""{0}"" class=""form-control"">
                $ foreach(var e in Provider.Database.ReadList(typeof({1}), ""select Id, Name from {1} order by Name"")) {{ $
	                <option value=""$=e.Id$"">$=e.Name$</option>
	            $ }} $
            </select>", FieldName, attrField.References.Name);
                    if(fieldType.IsEnum)
                        sb.AppendFormat(@"<select id=""{0}"" name=""{0}"" class=""form-control"">
                $ foreach(var ev in Enum.GetNames(typeof({1}))) {{ $
	                <option>$=ev$</option>
	            $ }} $
            </select>", FieldName, fieldType.Name);
                    break;
                case ControlType.LookUp:
                    sb.AppendFormat(@"<input type=""hidden"" name=""{0}"" id=""{0}""/><input type=""text"" placeholder=""Search..."" class=""cinarChooser form-control"" 
            entityName=""{1}"" id=""{0}Chooser"" 
            selectedCallback=""on{0}ChooserSelected""
            autocomplete=""off"" />
        <div id=""{0}ChooserTemplate"" style=""display:none""><img height=""40"" src=""{{Picture}}""/> {{Name}}</div>
        <script>
            function on{0}ChooserSelected(id, name){{
                \$('{0}').val(id);
            }}
        </script>", FieldName, attrField.References.Name);
                    break;
                case ControlType.CSSEdit:
                case ControlType.MemoEdit:
                    sb.AppendFormat("<textarea name=\"{0}\" id=\"{0}\" rows=\"3\" class=\"col-sm-8 form-control\"></textarea>", FieldName);
                    break;
                case ControlType.Undefined:
                case ControlType.FilterEdit:
                default:
                    throw new Exception(Provider.GetResource("This kind of form field is not supported: {0}", ct));
            }
            return sb.ToString();
        }
    }
}
