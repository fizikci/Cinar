using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using Cinar.CMS.Library.Modules;

//using System.IO;

namespace Cinar.CMS.Library.Handlers
{
    public class SystemInfo : GenericHandler
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
                case "copyTemplate":
                    {
                        copyTemplate();
                        break;
                    }
                case "renameTemplate":
                    {
                        renameTemplate();
                        break;
                    }
                case "deleteTemplate":
                    {
                        deleteTemplate();
                        break;
                    }
                case "exportTemplates":
                    {
                        exportTemplates();
                        break;
                    }
                case "importTemplates":
                    {
                        importTemplates();
                        break;
                    }
                case "getDefaultStyles":
                    {
                        getDefaultStyles();
                        break;
                    }
                case "saveDefaultStyles":
                    {
                        saveDefaultStyles();
                        break;
                    }
                case "getTemplateSource":
                    {
                        getTemplateSource();
                        break;
                    }
                case "saveTemplateSource":
                    {
                        saveTemplateSource();
                        break;
                    }
                case "clearCache":
                    {
                        clearCache();
                        break;
                    }
                case "getMetadata":
                    {
                        getMetadata();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }

        private void copyTemplate()
        {
            string template = context.Request["template"];
            string newName = context.Request["newName"];
            if (String.IsNullOrEmpty(template) || String.IsNullOrEmpty(newName))
                sendErrorMessage("Kopyalanacak dosya adı veya yeni isim belirtilmemiş.");
            else
            {
                try
                {
                    Provider.Database.Begin();
                    Module[] modules = Module.Read(template);
                    for (int i = 0; i < modules.Length; i++)
                    {
                        modules[i].SaveACopyFor(newName);
                    }

                    Template templateRec = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", template);
                    templateRec.Id = 0;
                    templateRec.FileName = newName;
                    templateRec.Save();
                    //File.Copy(context.Server.MapPath("~/" + template), context.Server.MapPath("~/" + newName));
                    context.Response.Write("Template copied.");
                    Provider.Database.Commit();
                }
                catch (Exception ex)
                {
                    Provider.Database.Rollback();
                    throw ex;
                }
            }
        }

        private void renameTemplate()
        {
            string template = context.Request["template"];
            string newName = context.Request["newName"];
            if (String.IsNullOrEmpty(template) || String.IsNullOrEmpty(newName))
                sendErrorMessage("Adı değiştirilecek dosya adı veya yeni isim belirtilmemiş.");
            else
            {
                try
                {
                    Provider.Database.Begin();
                    Provider.Database.ExecuteNonQuery("update Module set Template={0} where Template={1}", newName, template);
                    Provider.Database.ExecuteNonQuery("update Content set ShowInPage={0} where ShowInPage={1}", newName, template);
                    Provider.Database.ExecuteNonQuery("update Content set ShowContentsInPage={0} where ShowContentsInPage={1}", newName, template);
                    Provider.Database.ExecuteNonQuery("update Content set ShowCategoriesInPage={0} where ShowCategoriesInPage={1}", newName, template);

                    Template templateRec = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", template);
                    templateRec.FileName = newName;
                    templateRec.Save();

                    //File.Move(context.Server.MapPath("~/" + template), context.Server.MapPath("~/" + newName));
                    context.Response.Write("Template renamed.");

                    Provider.Database.Commit();
                }
                catch (Exception ex)
                {
                    Provider.Database.Rollback();
                    throw ex;
                }
            }
        }

        private void deleteTemplate()
        {
            string template = context.Request["template"];

            Provider.DeleteTemplate(template, true);
            context.Response.Write("Template deleted.");

        }

        private void exportTemplates()
        {
            string templates = context.Request["templates"];
            if (String.IsNullOrEmpty(templates) || Utility.SplitWithTrim(templates, ',').Length == 0)
                sendErrorMessage("Dışarı verilecek sayfaları seçiniz.");
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("<export date=\"{0}\">\n", DateTime.Now);

                foreach (string template in Utility.SplitWithTrim(templates, ','))
                    sb.Append(exportTemplate(template));

                sb.Append("</export>\n");

                context.Response.Write(sb.ToString());
            }
        }

        private void importTemplates()
        {
            string templateData = context.Request["templateData"];
            importTemplate(templateData);
            context.Response.Write("OK");
        }

        private void getDefaultStyles()
        {
            context.Response.Write(Configuration.Read().DefaultStyleSheet);
            //context.Response.Write(File.ReadAllText(context.Server.MapPath("~/default.css")));
        }

        private void saveDefaultStyles()
        {
            Configuration conf = Configuration.Read();
            conf.DefaultStyleSheet = context.Request["style"];
            conf.Save();
            //File.WriteAllText(context.Server.MapPath("~/default.css"), context.Request["style"], Encoding.UTF8);
            context.Response.Write("ok.");
        }

        private void getTemplateSource()
        {
            string fileName = context.Request["template"];
            Template template = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", fileName);

            context.Response.Write(template.HTMLCode);
        }

        private void saveTemplateSource()
        {
            string fileName = context.Request["template"];
            Template template = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", fileName);
            template.HTMLCode = context.Request["source"];
            template.Save();

            context.Response.Write("ok.");
        }

        private void clearCache()
        {
            Provider.Database.ExecuteNonQuery("delete from ModuleCache");
            context.Response.Write("Önbellek temizlendi.");
        }

        private void getMetadata()
        {
            string entityName = context.Request["entityName"];
            context.Response.Write(Utility.ToJSON(Provider.GetEntityType(entityName)));
        }

        #region import/export utility
        private void importTemplate(string templateData)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(templateData);

            foreach (XmlNode templateNode in doc.SelectNodes("/export/template"))
            {
                string templateName = "";
                if (templateNode != null && templateNode.Attributes["name"] != null && !String.IsNullOrEmpty(templateNode.Attributes["name"].Value))
                    templateName = templateNode.Attributes["name"].Value;
                else
                    throw new Exception(Provider.GetResource("The attribute /template[@name] not found or not valid"));

                string templatePath = Provider.Server.MapPath("~/" + templateName);

                XmlNode codeNode = templateNode.SelectSingleNode("code");
                string code = "";
                if (codeNode != null && !String.IsNullOrEmpty(codeNode.InnerText))
                    code = Utility.HtmlDecode(codeNode.InnerText.Trim());
                else
                    throw new Exception(Provider.GetResource("The node /template/code not found or not valid"));

                Provider.Database.Begin();

                try
                {
                    // sonra bu template'i ve içindeki modülleri silelim
                    Provider.DeleteTemplate(templateName, false);

                    // yeni template'i oluşturalım
                    Template t = new Template();
                    t.FileName = templateName;
                    t.HTMLCode = code;
                    t.Save();

                    // import edilen modülleri okuyalım
                    List<Module> modules = getModulesFromXML(templateNode.SelectSingleNode("modules"), null);

                    // bu modülleri kaydedelim
                    foreach (Module mdl in modules)
                        mdl.SaveACopyFor(templateName);

                    Provider.Database.Commit();
                }
                catch (Exception ex)
                {
                    Provider.Database.Rollback();
                    throw ex;
                }
            }
        }
        private StringBuilder exportTemplate(string template)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<template name=\"{0}\">\n", template);
            sb.AppendFormat("\t<code>\n{0}\n\t</code>\n", Utility.HtmlEncode(Provider.Database.GetValue("select HTMLCode from Template where FileName={0}", template).ToString()));
            Module[] modules = Module.Read(template);
            getSerializedModule(sb, modules, "\t");
            sb.Append("</template>\n");
            return sb;
        }
        private void getSerializedModule(StringBuilder sb, IEnumerable modules, string tab)
        {
            sb.AppendFormat("{0}<modules>\n", tab);
            foreach (Module mdl in modules)
            {
                sb.AppendFormat("{0}\t<module name=\"{1}\">\n{0}\t\t<serializedData>\n{2}\n{0}\t\t</serializedData>\n", tab, mdl.Name, Utility.HtmlEncode(mdl.Serialize()));
                if (mdl is ModuleContainer && !(mdl is RegionRepeater))
                {
                    List<Module> childModules = (mdl as ModuleContainer).ChildModules;
                    getSerializedModule(sb, childModules, tab+"\t");
                }
                sb.AppendFormat("{0}\t</module>\n", tab);
            }
            sb.AppendFormat("{0}</modules>\n", tab);
        }
        private List<Module> getModulesFromXML(XmlNode modulesNode, Module parentModule)
        {
            List<Module> res = new List<Module>();

            if (modulesNode == null || modulesNode.ChildNodes.Count==0)
                return res;

            foreach (XmlNode moduleNode in modulesNode.SelectNodes("module"))
            {
                XmlAttribute nameAttr = moduleNode.Attributes["name"];
                XmlNode serDataNode = moduleNode.SelectSingleNode("serializedData");

                if (nameAttr == null || String.IsNullOrEmpty(nameAttr.Value))
                    throw new Exception(Provider.GetResource("One of the module has no name attribute or it is invalid!"));

                if(serDataNode==null || String.IsNullOrEmpty(serDataNode.InnerText))
                    throw new Exception(Provider.GetResource("One of the module has no serializedData node or it is invalid!"));

                Module mdl = Module.Deserialize(nameAttr.Value, serDataNode.InnerText);
                res.Add(mdl);

                if (mdl is ModuleContainer)
                {
                    ModuleContainer container = mdl as ModuleContainer;
                    container._childModules = new List<Module>();
                    container._childModules.AddRange(getModulesFromXML(moduleNode.SelectSingleNode("modules"), mdl));
                }
            }
            return res;
        }
        #endregion
    }

}