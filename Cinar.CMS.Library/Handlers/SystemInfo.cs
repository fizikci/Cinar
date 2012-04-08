using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                case "getDefaultJavascript":
                    {
                        getDefaultJavascript();
                        break;
                    }
                case "saveDefaultJavascript":
                    {
                        saveDefaultJavascript();
                        break;
                    }
                case "getDefaultPageLoadScript":
                    {
                        getDefaultPageLoadScript();
                        break;
                    }
                case "saveDefaultPageLoadScript":
                    {
                        saveDefaultPageLoadScript();
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
                case "renameFile":
                    {
                        renameFile();
                        break;
                    }
                case "createFolder":
                    {
                        createFolder();
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
                if(Provider.CopyTemplate(template, newName))
                    context.Response.Write("Template copied.");
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
                if(Provider.RenameTemplate(template, newName))
                    context.Response.Write("Template renamed.");
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
            if (String.IsNullOrEmpty(templates) || templates.SplitWithTrim(',').Length == 0)
                sendErrorMessage("Dışarı verilecek sayfaları seçiniz.");
            else
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("<export date=\"{0}\">\n", DateTime.Now);

                foreach (string template in templates.SplitWithTrim(','))
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
        }
        private void saveDefaultStyles()
        {
            Configuration conf = Configuration.Read();
            conf.DefaultStyleSheet = context.Request["style"];
            conf.Save();
            context.Response.Write("ok.");
        }

        private void getDefaultJavascript()
        {
            context.Response.Write(Configuration.Read().DefaultJavascript);
        }
        private void saveDefaultJavascript()
        {
            Configuration conf = Configuration.Read();
            conf.DefaultJavascript = context.Request["code"];
            conf.Save();
            context.Response.Write("ok.");
        }

        private void getDefaultPageLoadScript()
        {
            context.Response.Write(Configuration.Read().DefaultPageLoadScript);
        }
        private void saveDefaultPageLoadScript()
        {
            Configuration conf = Configuration.Read();
            conf.DefaultPageLoadScript = context.Request["code"];
            conf.Save();
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
            context.Response.Write(CMSUtility.ToJSON(Provider.GetEntityType(entityName)));
        }

        private void getFileList()
        {
            string folderName = context.Request["folder"] ?? "";
            string path = Provider.MapPath(folderName);
            if (!path.StartsWith(Provider.MapPath(Provider.AppSettings["userFilesDir"])))
                path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);

            List<string> resList = new List<string>();

            string[] items = Directory.GetDirectories(path).OrderBy(s => s).ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    DirectoryInfo d = new DirectoryInfo(items[i]);
                    resList.Add("{name:" + d.Name.ToJS() + ", size:-1, date:" + d.LastWriteTime.ToJS() + "}");
                }
            }

            items = Directory.GetFiles(path).OrderBy(s => s).ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                bool isHidden = ((File.GetAttributes(items[i]) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (!isHidden)
                {
                    FileInfo f = new FileInfo(items[i]);
                    resList.Add("{name:" + f.Name.ToJS() + ", size:" + f.Length.ToJS() + ", date:" + f.LastWriteTime.ToJS() + "}");
                }
            }

            context.Response.Write("{success:true, root:[" + String.Join(",", resList.ToArray()) + "]}");
        }
        private void uploadFile()
        {
            try
            {
                string folderName = context.Request["folder"] ?? "";
                string path = Provider.MapPath(folderName);
                if (!path.StartsWith(Provider.MapPath(Provider.AppSettings["userFilesDir"])))
                    path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);

                if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                    path = Path.GetDirectoryName(path);

                string fileName = Path.GetFileName(context.Request.Files["upload"].FileName).MakeFileName();
                context.Request.Files["upload"].SaveAs(Path.Combine(path, fileName));
                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya yüklendi.', '" + folderName + "/" + fileName + "');</script>");
            }
            catch
            {
                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Yükleme başarısız.');</script>");
            }
        }
        private void deleteFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName;
            string path = Provider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(Provider.MapPath(Provider.AppSettings["userFilesDir"])))
                path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");

            foreach (string fileName in fileNames.Split(new []{"#NL#"}, StringSplitOptions.RemoveEmptyEntries))
            {
                string filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                else if (Directory.Exists(filePath))
                    Directory.Delete(filePath, true);
            }
            context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya silindi');</script>");
        }
        private void renameFile()
        {
            string folderName = context.Request["folder"] ?? "";
            if (!folderName.StartsWith("/")) folderName = "/" + folderName.Substring(1);
            //else folderName = "~/" + folderName;
            string path = Provider.MapPath(folderName);
            if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory)
                path = Path.GetDirectoryName(path);
            if (!path.StartsWith(Provider.MapPath(Provider.AppSettings["userFilesDir"])))
                path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);

            string fileNames = context.Request["name"];
            if (string.IsNullOrEmpty(fileNames) || fileNames.Trim() == "")
                throw new Exception("Dosya seçiniz");
            string fileName = fileNames.Split(new[] {"#NL#"}, StringSplitOptions.RemoveEmptyEntries)[0];

            string newFileName = context.Request["newName"].MakeFileName();
            string newPath = Path.Combine(path, newFileName);

            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
                File.Move(filePath, newPath);
            else if (Directory.Exists(filePath))
                Directory.Move(filePath, newPath);

            context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Dosya adı değiştirildi');</script>");
        }
        private void createFolder()
        {
            try
            {
                string folderName = context.Request["folder"] ?? "";
                string path = Provider.MapPath(folderName);
                if (!path.StartsWith(Provider.MapPath(Provider.AppSettings["userFilesDir"])))
                {
                    path = Provider.MapPath(Provider.AppSettings["userFilesDir"]);
                    context.Response.Write(@"<script>window.parent.alert('Error. There is no such folder.');</script>");
                    return;
                }

                string newFolderName = context.Request["name"].MakeFileName();
                path = Path.Combine(path, newFolderName);
                Directory.CreateDirectory(path);

                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Klasör oluşturuldu.');</script>");
            }
            catch
            {
                context.Response.Write(@"<script>window.parent.fileBrowserUploadFeedback('Hata');</script>");
            }
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

                string templatePath = Provider.MapPath("/" + templateName);

                XmlNode codeNode = templateNode.SelectSingleNode("code");
                string code = "";
                if (codeNode != null && !String.IsNullOrEmpty(codeNode.InnerText))
                    code = CMSUtility.HtmlDecode(codeNode.InnerText.Trim());
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
            sb.AppendFormat("\t<code>\n{0}\n\t</code>\n", CMSUtility.HtmlEncode(Provider.Database.GetValue("select HTMLCode from Template where FileName={0}", template).ToString()));
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
                sb.AppendFormat("{0}\t<module name=\"{1}\">\n{0}\t\t<serializedData>\n{2}\n{0}\t\t</serializedData>\n", tab, mdl.Name, CMSUtility.HtmlEncode(mdl.Serialize()));
                if (mdl is IRegionContainer)
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