using System;
using System.Collections.Generic;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Administration")]
    public class DataConverter : Module
    {
        private string entityName = "Content";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        private string userCode = "public override BaseEntity Convert(BaseEntity entity){\nreturn entity;\n}";
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }


        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<b>{0}</b> isimli kayıtlar aşağıdaki kod ile dönüştürülecek:<br><pre style=\"font-family:Lucida Console;border:1px dashed red;padding:4px\">{1}</pre><br><br>", entityName, Utility.HtmlEncode(userCode));

            sb.Append("<center><input type=button value=\"CONVERT\" onclick=\"location.href='?doIt=1';\"></center><br/><br/>");
            if (Provider.Request["doIt"] == "1")
            {
                StringBuilder res = convert();
                sb.AppendFormat("<font color=green><b>YAPILDI</b></font><br/><br/>{0}<br/>", res.ToString());
            }
            return sb.ToString();
        }

        private StringBuilder convert()
        {
            StringBuilder sb = new StringBuilder();
            int total = 0; int converted = 0; int updated = 0;

            List<Type> types = new List<Type>();
            if (String.IsNullOrEmpty(entityName)) types = Provider.GetEntityTypes(); else types.Add(Provider.GetEntityType(entityName));

            BaseConverter converter = null;
            try
            {
                converter = this.createConverter();
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message.Replace("\n","<br>\n"));
                return sb;
            }

            foreach (Type type in types)
            {
                IDatabaseEntity[] entities = Provider.Database.ReadList(type, "select * from " + type.Name);
                for (int i=0; i<entities.Length; i++)
                {
                    BaseEntity entity = (BaseEntity)entities[i];
                    try
                    {
                        total += 1;
                        entity = converter.Convert(entity);
                        converted += 1;
                        entity.Save();
                        updated += 1;
                    }
                    catch (Exception ex)
                    {
                        sb.AppendFormat("{0}<br>", ex.Message);
                    }
                }
            }
            sb.AppendFormat("<b>Toplam</b> : {0}<br>", total);
            sb.AppendFormat("<b>Dönüştürülen</b> : {0}<br>", converted);
            sb.AppendFormat("<b>Kaydedilen</b> : {0}<br>", updated);
            return sb;
        }

        BaseConverter _converter = null;
        private BaseConverter createConverter() 
        {
            if (_converter != null)
                return _converter;


            // CodeDOM'dan C# compiler'ı elde edelim
            Microsoft.CSharp.CSharpCodeProvider cp = new Microsoft.CSharp.CSharpCodeProvider();
            System.CodeDom.Compiler.ICodeCompiler ic = cp.CreateCompiler();

            // compiler parametrelerini ayarlayalım
            System.CodeDom.Compiler.CompilerParameters cpar = new System.CodeDom.Compiler.CompilerParameters();
            cpar.GenerateInMemory = true;
            cpar.GenerateExecutable = false;
            cpar.ReferencedAssemblies.Add("system.dll");
            cpar.ReferencedAssemblies.Add("System.Drawing.dll");
            cpar.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cpar.ReferencedAssemblies.Add("System.Data.dll");
            cpar.ReferencedAssemblies.Add(Provider.Server.MapPath("bin/Cinar.Database.dll"));
            cpar.ReferencedAssemblies.Add(Provider.Server.MapPath("bin/Cinar.CMS.Library.dll"));

            // CodeDOM ile derlenecek olan kodu oluşturalım
            string src = "";
            src += "using System;\r\n";
            src += "using Cinar.CMS.Library.Entities;\r\n";
            src += "using Cinar.CMS.Library.Modules;\r\n";
            src += "using System.Collections;\r\n";
            src += "using System.Data;\r\n";

            src += "public class TheConverter : BaseConverter{\r\n";

            // kullanıcı kodunu ekleyelim
            src += userCode;

            src += "}\r\n";

            // kodu derleyelim ve sonuçları alalım
            System.CodeDom.Compiler.CompilerResults cr = ic.CompileAssemblyFromSource(cpar, src);

            // eğer derleme hatası varsa bu hataları bir bir gösterelim.
            foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            {
                // hata mesajı, hatanın oluştuğu satır, önceki üç satır ve sonraki üç satırı içeren
                // bir hata mesajı hazırlayalım.
                string[] srcArr = src.Split('\n');
                string errMessage = ce.ErrorText + " at line " + (ce.Line - 1) + "\n\n";
                for (int i = ce.Line - 3; i < ce.Line + 3; i++)
                {
                    if (i < 0 || i >= srcArr.Length)
                        continue;
                    errMessage += i + " " + srcArr[i] + "\n";
                }
                // hatayı gösterelim
                throw new Exception(errMessage);
            }

            // kod hatasız derlendiyse test işlemlerine başlayalım.
            if (cr.Errors.Count == 0 && cr.CompiledAssembly != null)
            {
                // test edilecek iki koda (sınıfa) ait tipleri elde edelim.
                Type codeType = cr.CompiledAssembly.GetType("TheConverter");
                _converter = (BaseConverter)Activator.CreateInstance(codeType);
                return _converter;
            }

            throw new Exception(Provider.GetResource("Code creation failed due to an error, please debug."));
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
    public abstract class BaseConverter
    {
        public abstract BaseEntity Convert(BaseEntity entity);
    }
}
