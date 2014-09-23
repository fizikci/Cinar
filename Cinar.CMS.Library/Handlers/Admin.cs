using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using Cinar.Database;
using System.Threading;
using System.Globalization;

namespace Cinar.CMS.Library.Handlers
{
    public class Admin : GenericHandler
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Provider.CurrentCulture);
            try
            {
                switch (context.Request["method"])
                {
                    case "getCategories":
                        {
                            getTreeList();
                            break;
                        }
                    case "getList":
                        {
                            getList();
                            break;
                        }
                    case "getComboList":
                        {
                            getComboList();
                            break;
                        }
                    case "getEntity":
                        {
                            getEntity();
                            break;
                        }
                    case "newEntity":
                        {
                            newEntity();
                            break;
                        }
                    case "deleteEntity":
                        {
                            deleteEntity();
                            break;
                        }
                    case "saveEntity":
                        {
                            saveEntity();
                            break;
                        }
                    case "getFolderList":
                        {
                            getFolderList();
                            break;
                        }
                    case "getFileList":
                        {
                            getFileList();
                            break;
                        }
                    case "uploadFile":
                        {
                            uploadFile();
                            break;
                        }
                    case "deleteFile":
                        {
                            deleteFile();
                            break;
                        }
                    case "getCSS":
                        {
                            getCSS();
                            break;
                        }
                    case "saveCSS":
                        {
                            saveCSS();
                            break;
                        }
                    default:
                        {
                            sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{success:false, errorMessage:'" + ex.Message.Replace("'", "\\'") + "'}");
            }
        }

        private void getTreeList()
        {
            int catId = 1;
            Int32.TryParse(context.Request["node"], out catId);
            DataTable dtCats = Provider.Database.GetDataTable("select Id, Title from Content where CategoryId={0} and ClassName='Category' order by Title", catId) ?? new DataTable();

            string[] items = new string[dtCats.Rows.Count];
            int i = 0;

            foreach (DataRow drCat in dtCats.Rows)
                items[i++] = "{id:" + drCat["Id"] + ", text:" + drCat["Title"].ToJS() + "}";

            context.Response.Write("[" + String.Join(",", items) + "]");
        }

        private void getList()
        {
            string entityName = context.Request["entityName"];
            string where = context.Request["extraFilter"] ?? "";
            string orderBy = (context.Request["sort"] ?? "") + " " + (context.Request["dir"] ?? "");
            if (orderBy != null) orderBy = orderBy.Replace("__", ".");
            string page = String.IsNullOrEmpty(context.Request["start"]) ? "0" : context.Request["start"];
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
            object[] parameters = filterParser.GetParams();

            DataTable dt = Provider.ReadList(Provider.GetEntityType(entityName), Int32.Parse(page) / Int32.Parse(limit), Int32.Parse(limit), orderBy, where, parameters);
            int totalCount = Provider.ReadListTotalCount(Provider.GetEntityType(entityName), where, parameters);

            List<string> jsonItems = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string jsonItem = "{";

                foreach (DataColumn dc in dr.Table.Columns)
                    jsonItem += string.Format("{0}:{1},", dc.ColumnName.Replace(".", "__").ToJS(), dr[dc].ToJS());
                jsonItem = jsonItem.Remove(jsonItem.Length - 1, 1);
                jsonItem += "}";
                jsonItems.Add(jsonItem);
            }

            string res = "{root:[" + string.Join(",", jsonItems.ToArray()) + "], totalCount:" + totalCount + "}";
            context.Response.Write(res);
        }

        private void getComboList()
        {
            string entityName = context.Request["entityName"];
            string extraFilter = context.Request.Form["extraFilter"] ?? "";
            context.Response.Write(Provider.GetIdNameList(entityName, extraFilter, ""));
        }

        private void getEntity()
        {
            string error = "";
            string entityName = context.Request["entityName"];
            Type entityType = Provider.GetEntityType(entityName);
            if (entityType != null)
            {
                int id = 0;
                int.TryParse(context.Request["id"], out id);
                if (id > 0)
                {
                    IDatabaseEntity entity = Provider.Database.Read(entityType, id);
                    if (entity != null)
                    {
                        Hashtable ht = Provider.Database.EntityToHashtable(entity);
                        Type langEntityType = Provider.GetEntityType(entityName + "Lang");
                        if (langEntityType != null)
                        {
                            List<IDatabaseEntity> listLang = new List<IDatabaseEntity>(Provider.Database.ReadList(langEntityType, "select * from " + langEntityType.Name + " where " + entityName + "Id = {0}", id).SafeCastToArray<IDatabaseEntity>());
                            foreach(Lang l in Provider.Database.ReadList(typeof(Lang), "select * from Lang where Id<>{0}", Provider.Configuration.DefaultLang))
                            {
                                var langEntity = listLang.Count==0 ? null : listLang.FirstOrDefault(e=>l.Id.Equals(e.GetMemberValue("LangId")));
                                if(langEntity==null)
                                {
                                    langEntity = Provider.CreateEntity(langEntityType);
                                    langEntity.SetMemberValue("LangId", l.Id);
                                    langEntity.SetMemberValue(entityName+"Id", entity.Id);
                                    listLang.Add(langEntity);
                                }
                            }
                            foreach (var langEntity in listLang)
                            {
                                Hashtable langHt = Provider.Database.EntityToHashtable(langEntity);
                                foreach (var key in langHt.Keys)
                                    ht[key + "_lang_" + langEntity.GetMemberValue("LangId")] = langHt[key];
                            }
                        }
                        
                        // entitiye özel muameleler
                        if (entityType == typeof(User))
                            ht["Password2"] = "";

                        context.Response.Write(@"{ success: true, data: " + ht.ToJSON() + "}");
                        return;
                    }
                    error = "Entity bulunamadı.";
                }
                else
                    error = "Id geçersiz!";
            }
            else
                error = "Entity tipi bulunamadı.";

            context.Response.Write(@"{ success: false, errorMessage: " + error.ToJS() + "}");
        }

        private void newEntity()
        {
            string error = "";
            string where = context.Request["filter"] ?? "";
            string entityName = context.Request["entityName"];
            Type entityType = Provider.GetEntityType(entityName);
            if (entityType != null)
            {
                IDatabaseEntity entity = Provider.CreateEntity(entityType);
                if (!string.IsNullOrEmpty(where))
                {
                    FilterParser filterParser = new FilterParser(where, entityName);
                    where = filterParser.GetWhere();
                    foreach (var item in filterParser.GetNameValuePairs())
                        entity.SetMemberValue(item.Key, item.Value);
                }
                context.Response.Write(@"{ success: true, data: " + entity.ToJSON() + "}");
                return;
            }
            else
                error = "Entity tipi bulunamadı.";

            context.Response.Write(@"{ success: false, errorMessage: " + error.ToJS() + "}");
        }

        private void deleteEntity()
        {
            string error = "";
            string id = context.Request["Id"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                context.Response.Write("ERR: Id geçersiz");
                return;
            }
            Type entityType = Provider.GetEntityType(entityName);
            if (entityType != null)
            {
                BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
                entity.Delete();
                context.Response.Write(@"Kayıt silindi");
                return;
            }
            else
                error = "Entity tipi bulunamadı.";

            context.Response.Write("ERR: " + error);
        }

        private void saveEntity()
        {
            string id = context.Request["Id"];
            string where = context.Request["filter"];
            string entityName = context.Request["entityName"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                context.Response.Write("{success:false, errorMessage:'ID geçersiz!'}");
                return;
            }

            // bu istisnai durum, sadece konfigürasyon veritabanına kaydedilmiyor
            if (entityName == "Configuration")
            {
                Provider.Configuration.SetFieldsByPostData(context.Request.Form);
                Provider.Configuration.Save();
                context.Response.Write("{success:true}");
                return;
            }

            // entitiyi kaydedelim
            BaseEntity entity = null;
            if (mid > 0)
                entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), mid);
            else
                entity = Provider.CreateEntity(entityName);
            entity.SetFieldsByPostData(context.Request.Form);
            if (!string.IsNullOrEmpty(where))
            {
                FilterParser filterParser = new FilterParser(where, entityName);
                where = filterParser.GetWhere();
                foreach (var item in filterParser.GetNameValuePairs())
                    if(string.IsNullOrEmpty(context.Request.Form[item.Key]))
                        entity.SetMemberValue(item.Key, item.Value);
            }
            entity.Save();

            // entitiye ait Lang kayıtlarını kaydedelim
            Type langEntityType = Provider.GetEntityType(entityName + "Lang");
            if (langEntityType != null)
            {
                Dictionary<int, NameValueCollection> langEntities = new Dictionary<int, NameValueCollection>();
                Dictionary<int, string> langEntityFieldSum = new Dictionary<int, string>();
                for (int i = 0; i < context.Request.Form.Count; i++)
                {
                    string key = context.Request.Form.GetKey(i);
                    if (key.Contains("_lang_"))
                    {
                        string[] parts = key.Split(new string[] { "_lang_" }, StringSplitOptions.None);
                        int langId = int.Parse(parts[1]);
                        string fieldName = parts[0];
                        string fieldVal = context.Request.Form[key];

                        if (!langEntities.ContainsKey(langId))
                            langEntities.Add(langId, new NameValueCollection());
                        langEntities[langId].Add(fieldName, fieldVal);

                        if (!langEntityFieldSum.ContainsKey(langId))
                            langEntityFieldSum.Add(langId, "");
                        langEntityFieldSum[langId] += fieldVal;
                    }
                }
                // içi boş olan langEntitileri kaldıralım
                foreach (var item in langEntityFieldSum)
                    if (item.Value.Trim() == "")
                        langEntities.Remove(item.Key);

                foreach (var item in langEntities)
                {
                    BaseEntity langEntity = (BaseEntity)Provider.Database.Read(langEntityType, entityName + "Id = {0} AND LangId={1}", entity.Id, item.Key);
                    if (langEntity == null)
                        langEntity = Provider.CreateEntity(langEntityType);
                    langEntity.SetFieldsByPostData(item.Value);
                    langEntity.SetMemberValue(entityName + "Id", entity.Id);
                    langEntity.SetMemberValue("LangId", item.Key);
                    langEntity.Save();
                }
            }

            context.Response.Write("{success:true}");
        }

        private void getFolderList()
        {
            string folderName = context.Request["node"] ?? "";
            string path = Provider.MapPath(Provider.AppSettings["userFilesDir"] + folderName);
            if (!Directory.Exists(path))
            {
                path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);
                folderName = "";
            }

            List<string> resList = new List<string>();

            string[] items = Directory.GetDirectories(path);

            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                    resList.Add("{id:" + (folderName + "/" + Path.GetFileName(items[i])).ToJS() + ", text:" + Path.GetFileName(items[i]).ToJS() + "}");
            }

            context.Response.Write("[" + String.Join(",", resList.ToArray()) + "]");
        }

        private void getFileList()
        {
            string folderName = context.Request["folder"] ?? "";
            string path = Provider.MapPath(Provider.AppSettings["userFilesDir"] + folderName);

            List<string> resList = new List<string>();

            string[] items = Directory.GetDirectories(path);
            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    DirectoryInfo d = new DirectoryInfo(items[i]);
                    resList.Add("{FileName:" + d.Name.ToJS() + ", Size:0, DateModified:" + d.LastWriteTime.ToJS() + "}");
                }
            }

            items = Directory.GetFiles(path);

            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    FileInfo f = new FileInfo(items[i]);
                    resList.Add("{FileName:" + f.Name.ToJS() + ", Size:" + (f.Length / 1024 + 1).ToJS() + ", DateModified:" + f.LastWriteTime.ToJS() + "}");
                }
            }

            context.Response.Write("{root:[" + String.Join(",", resList.ToArray()) + "]}");
        }

        private void uploadFile()
        {
            try
            {
                string folderName = context.Request["folder"] ?? "";
                string path = Provider.MapPath(Provider.AppSettings["userFilesDir"] + folderName);

                string fileName = Path.GetFileName(context.Request.Files["upload"].FileName).MakeFileName();
                context.Request.Files["upload"].SaveAs(Path.Combine(path, fileName));
                context.Response.Write(@"{success: true, error: 'Yüklendi'}");
            }
            catch
            {
                context.Response.Write(@"{success: false, error: 'Yükleme başarısız'}");
            }
        }

        private void deleteFile()
        {
            string folderName = context.Request["folder"] ?? "";
            string path = Provider.MapPath(Provider.AppSettings["userFilesDir"] + folderName);

            string fileName = context.Request["fileName"];
            if (string.IsNullOrEmpty(fileName) || fileName.Trim() == "")
                throw new Exception("Dosya seçiniz");

            path = Path.Combine(path, fileName);
            File.Delete(path);
            context.Response.Write(@"Dosya silindi");
        }

        private void getCSS()
        {
            context.Response.Write(@"{ success: true, data: {text:" + Provider.Configuration.DefaultStyleSheet.ToJS() + "}}");
        }

        private void saveCSS()
        {
            Provider.Configuration.DefaultStyleSheet = context.Request["text"];
            Provider.Configuration.Save();
            context.Response.Write("{success:true}");
        }

        private void getJavascript()
        {
            context.Response.Write(@"{ success: true, data: {text:" + Provider.Configuration.DefaultJavascript.ToJS() + "}}");
        }

        private void saveJavascript()
        {
            Provider.Configuration.DefaultJavascript = context.Request["text"];
            Provider.Configuration.Save();
            context.Response.Write("{success:true}");
        }

        private void getPageLoadScript()
        {
            context.Response.Write(@"{ success: true, data: {text:" + Provider.Configuration.DefaultPageLoadScript.ToJS() + "}}");
        }

        private void savePageLoadScript()
        {
            Provider.Configuration.DefaultPageLoadScript = context.Request["text"];
            Provider.Configuration.Save();
            context.Response.Write("{success:true}");
        }
    }
}
