using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using Cinar.Database;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel;

namespace Cinar.CMS.Library.Handlers
{
    public class EntityInfo : GenericHandler
    {
        public override bool RequiresAuthorization
        {
            get { return true; }
        }

        public override string RequiredRole
        {
            get { return "Editor"; }
        }

        public override void ProcessRequest()
        {
            switch (context.Request["method"])
            {
                case "getList":
                    {
                        getList();
                        break;
                    }
                case "getEntityNameValue":
                    {
                        getEntityNameValue();
                        break;
                    }
                case "getEntity":
                    {
                        getEntity();
                        break;
                    }
                case "getEntityList":
                    {
                        getEntityList();
                        break;
                    }
                case "getEntityId":
                    {
                        getEntityId();
                        break;
                    }
                case "new":
                    {
                        newEntity();
                        break;
                    }
                case "insertNew":
                    {
                        insertNew();
                        break;
                    }
                case "edit":
                    {
                        edit();
                        break;
                    }
                case "save":
                    {
                        save();
                        break;
                    }
                case "delete":
                    {
                        delete();
                        break;
                    }
                case "info":
                    {
                        info();
                        break;
                    }
                case "setField":
                    {
                        setField();
                        break;
                    }
                case "getFieldsList":
                    {
                        getFieldsList();
                        break;
                    }
                case "getGridList":
                    {
                        getGridList();
                        break;
                    }
                case "getTreeList":
                    {
                        getTreeList();
                        break;
                    }
                case "addTag":
                    {
                        addTag();
                        break;
                    }
                case "sortEntities":
                    {
                        sortEntities();
                        break;
                    }
                case "getEntityDocs":
                    {
                        getEntityDocs();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }

        private void addTag()
        {
            string tag = context.Request["tag"];

            if (Provider.Content != null && !String.IsNullOrEmpty(tag))
            {
                Provider.Content.Tags = String.IsNullOrEmpty(Provider.Content.Tags) ? tag : (Provider.Content.Tags + "," + tag);
                Provider.Content.Save();
                context.Response.Write("tag added");
            }
            else
                context.Response.Write("tag NOT added");
        }

        private void getTreeList()
        {
            int catId = Int32.Parse(context.Request["catId"]);
            DataTable dtCats = Provider.Database.GetDataTable("select Id, Title from Content where CategoryId={0} and ClassName='Category' order by Title", catId) ?? new DataTable();
            DataTable dtCons = Provider.Database.GetDataTable("select top 10 Id, Title from Content where CategoryId={0} and ClassName<>'Category' order by InsertDate desc", catId) ?? new DataTable();

            string[] items = new string[dtCats.Rows.Count + dtCons.Rows.Count];
            int i = 0;

            foreach (DataRow drCat in dtCats.Rows)
                items[i++] = "{data:" + drCat["Id"] + ", text:" + drCat["Title"].ToJS() + ", type:'category'}";
            foreach (DataRow drCon in dtCons.Rows)
                items[i++] = "{data:" + drCon["Id"] + ", text:" + drCon["Title"].ToJS() + ", type:'content'}";

            context.Response.Write("[" + String.Join(",", items) + "]");
        }

        private void getGridList()
        {
            string entityName = context.Request["entityName"];
            string where = context.Request["extraFilter"] ?? "";
            string orderBy = context.Request["orderBy"] ?? "";
            string page = String.IsNullOrEmpty(context.Request["page"]) ? "0" : context.Request["page"];
            string limit = String.IsNullOrEmpty(context.Request["limit"]) ? "20" : context.Request["limit"];
            int fieldNo = 0;

            while (context.Request.Form["f_" + fieldNo] != null)
            {
                string op = context.Request.Form["o_" + fieldNo];
                string field = context.Request.Form["f_" + fieldNo];
                string val = context.Request.Form["c_" + fieldNo];
                where += (where == "" ? "" : " AND ") + field + op + val;
                fieldNo++;
            }

            FilterParser filterParser = new FilterParser(where, entityName);
            where = filterParser.GetWhere();

            DataTable dt = Provider.ReadList(Provider.GetEntityType(entityName), Int32.Parse(page), Int32.Parse(limit), orderBy, where, filterParser.GetParams());

            context.Response.Write("<table class=\"bk-grid\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">\n");
            if (dt != null)
            {
                context.Response.Write("\t<tr>\n");
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == "_CinarRowNumber") continue; //***
                    string columnName = dc.ColumnName;
                    string columnTitle = Provider.TranslateColumnName(entityName, columnName);
                    context.Response.Write("\t\t<th id=\"h_" + dc.ColumnName + "\">" + columnTitle + "</th>\n");
                }
                context.Response.Write("\t</tr>\n");
            }
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                    context.Response.Write("\t<tr><td style=\"padding:30px;text-align:center\" colspan=\"50\">" + Provider.GetResource("No record") + "</td></tr>\n");
                else
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        context.Response.Write("\t<tr id=\"r_" + dr[0] + "\">\n");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "_CinarRowNumber") continue; //***

                            object valObj = dr[dc.ColumnName];
                            string dispVal = "";
                            if (dr.IsNull(dc))
                                dispVal = "";
                            else if (dc.DataType == typeof(Boolean))
                                dispVal = ((bool)valObj) ? Provider.GetResource("Yes") : Provider.GetResource("No");
                            else if (dc.DataType == typeof(String))
                            {
                                string str = Regex.Replace(valObj.ToString(), "<.*?>", string.Empty);
                                if (str.Length > 50)
                                    dispVal = CMSUtility.HtmlEncode(str.StrCrop(50)); // str.Substring(0, 50) + "..."
                                else
                                    dispVal = str;
                            }
                            else if (dc.DataType == typeof(DateTime))
                            {
                                dispVal = ((DateTime)valObj).ToString(Provider.Configuration.DefaultDateFormat);
                            }
                            else
                                dispVal = valObj.ToString();
                            context.Response.Write("\t\t<td value=\"" + CMSUtility.HtmlEncode(valObj) + "\">" + dispVal + "</td>\n");
                        }
                        context.Response.Write("\t</tr>\n");
                    }
            }
            context.Response.Write("</table>");
        }

        private void getFieldsList()
        {
            string entityName = context.Request["entityName"];

            // bu istisnai durum, sadece konfigürasyon veritabanına kaydedilmiyor
            if (entityName == "Configuration")
            {
                context.Response.Write(Provider.GetPropertyEditorJSON(Provider.Configuration, true));
                return;
            }

            context.Response.Write(Provider.GetPropertyEditorJSON(Provider.CreateEntity(entityName), true));
        }

        private void setField()
        {
            string entityName = context.Request["entityName"];
            string id = context.Request["id"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            string fieldName = context.Request["fieldName"];
            string value = context.Request["value"];

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            PropertyInfo pi = entity.GetType().GetProperty(fieldName);
            object val = Convert.ChangeType(value, pi.PropertyType);
            pi.SetValue(entity, val, null);
            entity.Save();

            context.Response.Write("OK");
        }

        private void delete()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            if (entity != null)
                entity.Delete();
        }

        private void info()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.AppendFormat("<tr><td>{0}</td><td>: {1}</td></tr>", Provider.GetResource("BaseEntity.Id"), entity.Id);
            sb.AppendFormat("<tr><td>{0}</td><td>: {1}</td></tr>", Provider.GetResource("BaseEntity.InsertUserId"), Provider.Database.GetValue("select Nick from User where Id={0}", entity.InsertUserId));
            sb.AppendFormat("<tr><td>{0}</td><td>: {1}</td></tr>", Provider.GetResource("BaseEntity.InsertDate"), entity.InsertDate);
            sb.AppendFormat("<tr><td>{0}</td><td>: {1}</td></tr>", Provider.GetResource("BaseEntity.UpdateUserId"), Provider.Database.GetValue("select Nick from User where Id={0}", entity.UpdateUserId));
            sb.AppendFormat("<tr><td>{0}</td><td>: {1}</td></tr>", Provider.GetResource("BaseEntity.UpdateDate"), entity.UpdateDate);
            sb.Append("</table>");

            context.Response.Write(sb.ToString());
        }

        private void save()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            // bu istisnai durum, sadece konfigürasyon veritabanına kaydedilmiyor
            if (entityName == "Configuration")
            {
                Provider.Configuration.SetFieldsByPostData(context.Request.Form);
                Provider.Configuration.Save();
                context.Response.Write("OK");
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            entity.SetFieldsByPostData(context.Request.Form);
            entity.Save();
            context.Response.Write("OK");
        }

        private void edit()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            // bu istisnai durum, sadece konfigürasyon veritabanına kaydedilmiyor
            if (entityName == "Configuration")
            {
                context.Response.Write(Provider.GetPropertyEditorJSON(Configuration.Read(), false));
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            context.Response.Write(entity.GetPropertyEditorJSON());
        }

        private void insertNew()
        {
            string entityName = context.Request["entityName"];
            BaseEntity entity = Provider.CreateEntity(entityName);
            entity.SetFieldsByPostData(context.Request.Form);

            entity.Save();
            context.Response.Write(entity.Id);

        }

        private void newEntity()
        {
            string entityName = context.Request["entityName"];
            BaseEntity entity = Provider.CreateEntity(entityName);

            context.Response.Write(entity.GetPropertyEditorJSON());
        }

        private void getList()
        {
            string entityName = context.Request["entityName"];
            string extraFilter = context.Request["extraFilter"] ?? "";
            IDatabaseEntity[] entities = Provider.GetIdNameList(entityName, extraFilter, "");
            context.Response.Write("[\n");
            context.Response.Write("[0, '" + Provider.GetResource("Select") + "']\n");
            for (int i = 0; i < entities.Length; i++)
            {
                IDatabaseEntity entity = entities[i];
                context.Response.Write(",[" + entity.Id.ToJS() + "," + entity.GetNameValue().ToJS() + "]");
                //if (i < entities.Length - 1) context.Response.Write(",");

            }
            context.Response.Write("]");
        }
        private void getEntityNameValue()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            context.Response.Write(entity.GetNameValue());
        }
        private void getEntityId()
        {
            string name = context.Request.Form["name"];
            string entityName = context.Request["entityName"];
            string extraFilter = context.Request.Form["extraFilter"];
            BaseEntity ent = Provider.CreateEntity(entityName);
            string filter = (String.IsNullOrEmpty(extraFilter) ? "" : extraFilter + " AND ") + ent.GetNameColumn() + "like" + name + "%";

            BaseEntity[] entities = (BaseEntity[])Provider.GetIdNameList(entityName, filter, "");
            if (entities != null && entities.Length>0)
                context.Response.Write(String.Format("{{id:{0},name:{1}}}", entities[0].Id, entities[0].GetNameValue().ToJS()));
            else
                context.Response.Write("{id:0,name:''}");
        }
        private void getEntityList()
        {
            string entityName = context.Request["entityName"];
            Type tip = Provider.GetEntityType(entityName);
            string filter = context.Request["filter"] ?? "";

            FilterParser filterParser = new FilterParser(filter, entityName);
            filter = filterParser.GetWhere();

            string where = "where " + (String.IsNullOrEmpty(filter) ? "1=1" : "(" + filter + ")");

            IDatabaseEntity[] entities = Provider.Database.ReadList(tip, "select * from [" + entityName + "] " + where + " order by OrderNo", filterParser.GetParams());

            context.Response.Write(entities.ToJSON());
        }
        private void getEntity()
        {
            string id = context.Request["id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            context.Response.Write(entity.ToJSON());
        }

        private void sortEntities() {
            string entityName = context.Request["entityName"];
            Type t = Provider.GetEntityType(entityName);
            if (t == null) return; //***

            string sortOrder = context.Request["sortOrder"];
            int[] ids = sortOrder.SplitWithTrim(',').Select(s => int.Parse(s)).ToArray();

            for (int i = 0; i < ids.Length; i++)
                Provider.Database.ExecuteNonQuery("update ["+entityName+"] set [OrderNo]={0} where Id={1}", i, ids[i]);

            context.Response.Write("OK");
        }

        private string getShortName(Type type)
        {
            if (type == typeof (string))
                return "string";
            if (type == typeof (int))
                return "int";
            if (type == typeof (bool))
                return "bool";

            if (type.IsGenericType && type.GetGenericTypeDefinition()== typeof(List<>))
            {
                return "List<"+ getShortName(type.GetGenericArguments()[0]) + ">"; // use this...
            }
            return type.Name;
        }

        private void getEntityDocs()
        {
            string entityName = context.Request["entityName"];
            Type entityType = Provider.GetEntityType(entityName);
            object obj = Provider.CreateEntity(entityType);

            StringBuilder sb = new StringBuilder();

            sb.Append("<h2>Database Properties</h2>");
            sb.Append("<table>\n");
            sb.AppendFormat("<tr class=\"header\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>\n",
                "Type",
                "Name",
                "Declaring Type",
                "Description",
                "Default Value");

            int i = 0;

            foreach (PropertyInfo pi in entityType.GetProperties())
            {
                if (!pi.CanWrite) continue;
                if (pi.Name == "Item") continue;

                string caption = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);
                if (caption.StartsWith(pi.DeclaringType.Name + ".") && !Provider.DesignMode)
                    caption = pi.Name;
                string description = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name + "Desc");

                sb.AppendFormat("<tr{5}><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>\n",
                    getShortName(pi.PropertyType),
                    pi.Name,
                    pi.DeclaringType.Name,
                    caption == description ? "" : (caption + ". ") + description,
                    pi.GetValue(obj, null),
                    i%2==0 ? "":" class=\"odd\"");
                i++;
            }
            sb.Append("</table>\n");

            i = 0;
            sb.Append("<h2>Read Only Properties</h2>");
            sb.Append("<table>\n");
            sb.AppendFormat("<tr class=\"header\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                "Type",
                "Name",
                "Declaring Type",
                "Description");

            foreach (PropertyInfo pi in entityType.GetProperties())
            {
                if (pi.CanWrite) continue;
                if (pi.Name == "Item") continue;

                DescriptionAttribute desc = pi.GetAttribute<DescriptionAttribute>() ?? new DescriptionAttribute();

                sb.AppendFormat("<tr{4}><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                    getShortName(pi.PropertyType),
                    pi.Name,
                    pi.DeclaringType.Name,
                    desc.Description,
                    i % 2 == 0 ? "" : " class=\"odd\"");
                i++;
            }
            sb.Append("</table>\n");

            i = 0;

            sb.Append("<h2>Methods</h2>");
            sb.Append("<table>\n");
            sb.AppendFormat("<tr class=\"header\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                "Type",
                "Name",
                "Declaring Type",
                "Description");

            foreach (MethodInfo pi in entityType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pi.IsSpecialName) continue;

                DescriptionAttribute desc = pi.GetAttribute<DescriptionAttribute>() ?? new DescriptionAttribute();

                sb.AppendFormat("<tr{4}><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                    getShortName(pi.ReturnType),
                    pi.Name,
                    pi.DeclaringType.Name,
                    desc.Description,
                    i % 2 == 0 ? "" : " class=\"odd\"");
                i++;
            }
            sb.Append("</table>\n");

            i = 0;

            if (obj.GetType() != typeof(Lang))
            {
                sb.Append("<h2>Related Entities</h2>");
                sb.Append("<table>\n");
                sb.AppendFormat("<tr class=\"header\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                    "Entity Name",
                    "Related Field Name",
                    "Label",
                    "Description");
                foreach (Type type in Provider.GetEntityTypes())
                    foreach (PropertyInfo pi in type.GetProperties())
                    {
                        if (pi.DeclaringType == typeof(BaseEntity)) continue; //*** BaseEntity'deki UpdateUserId ve InsertUserId bütün entitilerde var, gereksiz bir ilişki kalabalığı oluşturuyor

                        ColumnDetailAttribute fda = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));
                        if (fda.References == entityType)
                        {
                            sb.AppendFormat("<tr{4}><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\n",
                                type.Name,
                                pi.Name,
                                Provider.GetResource(type.Name).ToHTMLString(),
                                Provider.GetResource(type.Name + "Desc").ToHTMLString(),
                                i % 2 == 0 ? "" : " class=\"odd\"");
                            i++;
                        }
                    }
                sb.Append("</table>\n");
            }

            Provider.Response.Write(sb.ToString());
        }

    }
}
