using System;
using System.Collections.Generic;
using System.Data;
using Cinar.CMS.Library.Modules;
using Cinar.CMS.Library.Entities;


namespace Cinar.CMS.Library.Handlers
{
    public class ModuleInfo : GenericHandler
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
                case "getDefaultCSS":
                    {
                        getDefaultCSS();
                        break;
                    }
                case "getModuleCSS":
                    {
                        getModuleCSS();
                        break;
                    }
                case "getAllDefaultCSS":
                    {
                        getAllDefaultCSS();
                        break;
                    }
                case "addModule":
                    {
                        addModule();
                        break;
                    }
                case "convertModule":
                    {
                        convertModule();
                        break;
                    }
                case "copyModule":
                    {
                        copyModule();
                        break;
                    }
                case "editModule":
                    {
                        editModule();
                        break;
                    }
                case "deleteModule":
                    {
                        deleteModule();
                        break;
                    }
                case "upModule":
                    {
                        moveModule(false);
                        break;
                    }
                case "downModule":
                    {
                        moveModule(true);
                        break;
                    }
                case "saveModule":
                    {
                        saveModule();
                        break;
                    }
                case "getModuleList":
                    {
                        getModuleList();
                        break;
                    }
                case "insertBatch":
                    {
                        insertBatch();
                        break;
                    }
                case "exportModule":
                    {
                        exportModule();
                        break;
                    }
                case "importModule":
                    {
                        importModule();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }

        private void getDefaultCSS()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Module module = Module.Read(moduleName, mid);
            if (module == null)
            {
                sendErrorMessage("Modül okunamadı.");
                return;
            }
            string css = module.GetDefaultCSS().Replace("{","{\n\t").Replace(";",";\n\t").Replace("\t}","}");
            context.Response.Write(css);
        }

        private void getModuleCSS()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Module module = Module.Read(moduleName, mid);
            if (module == null)
            {
                sendErrorMessage("Modül okunamadı.");
                return;
            }
            context.Response.Write(module.CSS);
        }

        private void getAllDefaultCSS()
        {
            context.Response.Write(Provider.GetAllModulesDefaultCSS());
        }

        private void addModule()
        {
            string moduleName = context.Request["moduleType"];
            string region = context.Request["region"];
            string template = context.Request["template"];
            int parentModuleId = Int32.Parse(context.Request["parentModuleId"]);
            Module module = Module.CreateModule(moduleName, template, region, parentModuleId);
            if (module == null)
            {
                sendErrorMessage("Cinar.CMS.Library.Modules." + moduleName + " isimli modül create edilemedi.");
                return;
            }
            module.Save();
            context.Response.Write(module.Show());
        }

        private void convertModule()
        {
            string newModuleName = context.Request["moduleType"];
            string moduleName = context.Request["moduleName"];

            string id = context.Request["id"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Module module = Module.Read(moduleName, mid);
            if (module == null)
            {
                sendErrorMessage("Modül okunamadı.");
                return;
            }
            Module newModule = Module.CreateModule(newModuleName, module.Template, module.Region, module.ParentModuleId);
            module.CopyPropertiesWithSameName(newModule);

            newModule.CSS = module.CSS.Replace("#" + moduleName + "_" + module.Id, "#" + newModuleName + "_" + module.Id);
            newModule.Name = newModuleName;
            newModule.Save();

            context.Response.Write(newModule.Show());
        }

        private void copyModule()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            string region = context.Request["region"];
            string template = context.Request["template"];
            int parentModuleId = Int32.Parse(context.Request["parentModuleId"]);

            Module module = Module.Read(moduleName, mid);
            module.Id = 0;
            module.Region = region;
            module.Template = template;
            module.ParentModuleId = parentModuleId;

            Provider.Database.Begin();
            try
            {
                module.Save();
                module.CSS = module.CSS.Replace(moduleName + "_" + id, moduleName + "_" + module.Id);
                module.Save();
                Provider.Database.Commit();
                context.Response.Write(module.Show());
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                sendErrorMessage("Modül kopyalanamadı. Sebep: " + ex.Message);
            }
        }

        private void editModule()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Module module = Module.Read(moduleName, mid);
            if (module == null)
            {
                sendErrorMessage("Modül okunamadı.");
                return;
            }
            context.Response.Write(module.GetPropertyEditorJSON());
        }

        private void deleteModule()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Module module = Module.Read(moduleName, mid);
            if (module != null)
                module.Delete();
            else
                Provider.Database.ExecuteNonQuery("delete from Module where Id={0}", mid);
        }

        private void saveModule()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            Module module = Module.Read(moduleName, mid);
            module.SetFieldsByPostData(context.Request.Form);
            module.Save();
            context.Response.Write(module.Show());
        }

        private void getModuleList()
        {
            string page = context.Request["page"];
            DataTable dtModules = Provider.Database.GetDataTable("select Id, Region, Name from Module where Template={0} order by Region, OrderNo", page) ?? new DataTable();

            string[] items = new string[dtModules.Rows.Count];
            int i = 0;

            foreach (DataRow drModule in dtModules.Rows)
                items[i++] = "{data:" + drModule["Id"] + ", text:" + (Provider.GetResource(drModule["Name"].ToString()) + " - " + drModule["Region"]).ToJS() + ", type:" + drModule["Name"].ToJS() + "}";

            context.Response.Write("[" + String.Join(",", items) + "]");
        }

        private void insertBatch()
        {
            string entityName = context.Request["entityName"];
            string fieldName = context.Request["fieldName"];
            string values = context.Request["values"];

            if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(values))
            {
                sendErrorMessage("Eksik veya bozuk data!");
                return;
            }

            Type entityType = Provider.GetEntityType(entityName);
            foreach(string val in values.Split(new []{"#NL#"}, StringSplitOptions.RemoveEmptyEntries))
            {
                BaseEntity entity = Provider.CreateEntity(entityName);
                entity.SetFieldsByPostData(context.Request.Form);
                entity.SetMemberValue(fieldName, val);
                entity.Save();
            }
        }

        private void moveModule(bool down)
        {
            string id = context.Request["id"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }
            Provider.Database.Begin();
            try
            {
                DataRow drModule = Provider.Database.GetDataRow("select Id, Template, Region, OrderNo from Module where Id={0}", mid);
                if (drModule == null) { sendErrorMessage(Provider.GetResource("Module ID {0} does not exist", mid)); return; }
                DataRow drModuleFrom = Provider.Database.GetDataRow("select top 1 Id, OrderNo from Module where Template={0} and Region={1} and OrderNo" + (down ? ">=" : "<=") + "{2} and Id<>{3} order by OrderNo " + (down ? "asc" : "desc"), drModule["Template"], drModule["Region"], drModule["OrderNo"], mid);
                if (drModuleFrom == null) { sendErrorMessage(Provider.GetResource("This module is only module in its region")); return; }

                if (drModule["OrderNo"].Equals(drModuleFrom["OrderNo"]))
                {
                    if (down) drModuleFrom["OrderNo"] = ((int)drModule["OrderNo"]) + 1;
                    else drModule["OrderNo"] = ((int)drModuleFrom["OrderNo"]) + 1;
                }
                Provider.Database.ExecuteNonQuery(String.Format("update Module set OrderNo={0} where Id={1}", drModule["OrderNo"], drModuleFrom["Id"]));
                Provider.Database.ExecuteNonQuery(String.Format("update Module set OrderNo={0} where Id={1}", drModuleFrom["OrderNo"], drModule["Id"]));
                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }

        private void exportModule()
        {
            string id = context.Request["id"];
            string moduleName = context.Request["name"];
            int mid = 0;
            if (!Int32.TryParse(id, out mid))
            {
                sendErrorMessage("ID geçersiz!");
                return;
            }

            try
            {
                Module module = Module.Read(moduleName, mid);
                context.Response.Write(CinarSerialization.Serialize(module));
            }
            catch (Exception ex)
            {
                sendErrorMessage("Modül export edilemedi. Sebep: " + ex.Message);
            }
        }
        private void importModule()
        {
            string region = context.Request["region"];
            string template = context.Request["template"];
            int parentModuleId = Int32.Parse(context.Request["parentModuleId"]);

            try
            {
                Dictionary<string, string> dict = CinarSerialization.Deserialize(context.Request["data"]);

                Module module = Provider.CreateModule(dict["Name"]);
                CinarSerialization.Deserialize(module, context.Request["data"]);

                string moduleName = module.Name;
                int id = module.Id;

                module.Id = 0;
                module.Region = region;
                module.Template = template;
                module.ParentModuleId = parentModuleId;

                Provider.Database.Begin();
                module.Save();
                module.CSS = module.CSS.Replace(moduleName + "_" + id, moduleName + "_" + module.Id);
                module.Save();
                Provider.Database.Commit();

                context.Response.Write(module.Show());
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                sendErrorMessage("Modül import edilemedi. Sebep: " + ex.Message);
            }
        }

    }
}