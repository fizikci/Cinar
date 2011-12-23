using System;
using System.Text;
using System.Reflection;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using Cinar.Database;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

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
                items[i++] = "{data:" + drCat["Id"] + ", text:" + Utility.ToJS(drCat["Title"]) + ", type:'category'}";
            foreach (DataRow drCon in dtCons.Rows)
                items[i++] = "{data:" + drCon["Id"] + ", text:" + Utility.ToJS(drCon["Title"]) + ", type:'content'}";

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
                    //if (dc.ColumnName != "Id")
                    context.Response.Write("\t\t<th id=\"h_" + dc.ColumnName + "\">" + Provider.GetResource(dc.ColumnName) + "</th>\n");
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
                        context.Response.Write("\t<tr id=\"r_" + (dt.Columns.Contains("Id") ? dr["Id"] : dr["BaseEntity.Id"]) + "\">\n");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            object valObj = dr[dc.ColumnName];
                            string val = "";
                            if (dr.IsNull(dc))
                                val = "";
                            else if (dc.DataType == typeof(Boolean))
                                val = ((bool)valObj) ? Provider.GetResource("Yes") : Provider.GetResource("No");
                            else if (dc.DataType == typeof(String))
                            {
                                string str = Regex.Replace(valObj.ToString(), "<.*?>", string.Empty);
                                if (str.Length > 50)
                                    val = Utility.HtmlEncode(Utility.StrCrop(str, 50)); // str.Substring(0, 50) + "..."
                                else
                                    val = str;
                            }
                            else if (dc.DataType == typeof(DateTime))
                            {
                                val = ((DateTime)valObj).ToString(Provider.Configuration.DefaultDateFormat);
                            }
                            else
                                val = valObj.ToString();
                            context.Response.Write("\t\t<td>" + val + "</td>\n");
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
            string extraFilter = context.Request.Form["extraFilter"] ?? "";
            IDatabaseEntity[] entities = Provider.GetIdNameList(entityName, extraFilter, "");
            context.Response.Write("[\n");
            context.Response.Write("[0, '" + Provider.GetResource("Select") + "']\n");
            for (int i = 0; i < entities.Length; i++)
            {
                IDatabaseEntity entity = entities[i];
                context.Response.Write(",[" + Utility.ToJS(entity.Id) + "," + Utility.ToJS(entity.GetNameValue()) + "]");
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
                context.Response.Write(String.Format("{{id:{0},name:{1}}}", entities[0].Id, Utility.ToJS(entities[0].GetNameValue())));
            else
                context.Response.Write("{id:0,name:''}");
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
    }
}
