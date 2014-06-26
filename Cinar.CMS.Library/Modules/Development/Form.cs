using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Linq;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class Form : StaticHtml
    {
        public Form()
        {
            EntityName = "";
            InnerHtml = "Select an entity to generate its form";
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
                this.InnerHtml = generateFormHtml();
            }
        }

        private string generateFormHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@"$
var e = new {0}();
var id = int.Parse(Provider.Request.eId ?? ""0"");
if(id>0)
    e = Provider.Database.Read(typeof({0}), id);

if(Provider.Request.cmdName){{
    if(Provider.Request.cmdName==""save"")
    {{
        e.SetFieldsByPostData(Provider.Request.Form);
        e.Save();
        Provider.Response.Redirect(""/{0}View.aspx?eId=""+e.Id);
    }}
}}
$
<script>
    \$(function(){{
        loadForm('#form', $=Utility.ToJSON(e)$, '$=Provider.Request.mode$');
    }});
</script>

<div class=""page-header"">
    <h1>
        {0}
		<small>
			<i class=""icon-double-angle-right""></i>
    		$=e.Id > 0 ? 'Edit details' : 'Add new'$
		</small>
	</h1>
</div>

<form id=""form"" method=""post"" action=""$=Provider.Request.RawUrl$"" enctype=""multipart/form-data"" class=""form-horizontal"" role=""form"" autocomplete=""off"">
<input type=""hidden"" name=""cmdName"" value=""save""/>
<input type=""hidden"" name=""Id"" value=""$=e.Id$""/>", EntityName);

            sb.AppendLine();

            int i = 0;

            foreach (var category in getColumnCategories())
            {
                if (i == 0)
                    sb.AppendLine("<div class=\"row\">\r\n<div class=\"col-sm-9\">");
                else if (i == 1)
                    sb.AppendLine(
                        "<div class=\"row\">\r\n<div class=\"col-xs-12 col-sm-6\">\r\n<h3 class=\"header smaller lighter blue\">" +
                        category + "</h3>");
                else
                    sb.AppendLine("<div class=\"col-xs-12 col-sm-6\">\r\n<h3 class=\"header smaller lighter blue\">" + category +
                                  "</h3>");

                foreach (Column field in Provider.Database.Tables[EntityName].Columns)
                {
                    EditFormFieldPropsAttribute attrEdit = (EditFormFieldPropsAttribute) CMSUtility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof (EditFormFieldPropsAttribute));

                    if (field.IsPrimaryKey || !attrEdit.Visible || attrEdit.Category != category)
                        continue; //***

                    var ff = new FormField {
                        EntityName = this.EntityName, 
                        FieldName = field.Name
                    };

                    sb.AppendLine(ff.GetHtml());
                }

                if (i == 0)
                    sb.AppendLine(@"
    </div>
    <div class=""col-sm-3"">
	    <img name=""Picture"" class=""img-responsive"" src=""/UserFiles/contact.png"" />
        <input type=""file"" id=""Picture"" name=""Picture"" /><br/>
        $ if(e.Id>0){ $
        <em><i class=""icon-ok green""></i>Added by $=e.InsertUser.FullName$ on $=e.InsertDate.ToString(""dd-MM-yyyy"")$</em>
        $ } $
    </div>
</div>");
                else
                    sb.AppendLine("</div>");

                i++;
            }

            sb.AppendLine(@"

<div class=""clearfix form-actions"">
	<div class=""text-right"">
		<button class=""btn btn-info"" type=""submit"">
			<i class=""icon-ok bigger-110""></i>
			Save
		</button>

		&nbsp; &nbsp; &nbsp;
		<button class=""btn"" type=""button"" onclick=""history.go(-1)"">
			<i class=""icon-undo bigger-110""></i>
			Cancel
		</button>
	</div>
</div>

</form>");

            return sb.ToString();
        }

        private List<string> getColumnCategories()
        {
            List<string> res = new List<string>();

            foreach (Column field in Provider.Database.Tables[EntityName].Columns)
            {
                EditFormFieldPropsAttribute attrEdit = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(Provider.GetEntityType(EntityName).GetProperty(field.Name), typeof(EditFormFieldPropsAttribute));

                if (field.IsPrimaryKey || !attrEdit.Visible)
                    continue; //***

                res.Add(attrEdit.Category);
            }

            return res.Distinct().ToList();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
