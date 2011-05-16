using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Cinar.Database;
using Cinar.Scripting;

namespace Cinar.DBTools.Tools
{
    public partial class FormCodeGenerator : Form, IDBToolsForm
    {
        CodeGenerator gen;
        string path;

        public FormMain MainForm { get; set; }

        public FormCodeGenerator()
        {
            InitializeComponent();

            gen = new CodeGenerator();

            path = Path.GetDirectoryName(Application.ExecutablePath) + "\\codegen.xml";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(CodeGenerator));
                    gen = (CodeGenerator)ser.Deserialize(sr);
                }
            }

            showCategories(null);

            foreach (Table tbl in Provider.Database.Tables)
                lbEntities.Items.Add(tbl);
        }

        private void showCategories(string selectedCat)
        {
            lbCategories.Items.Clear();
            foreach (string cat in gen.Templates.Select(c => c.Category).Distinct().OrderBy(s => s))
                lbCategories.Items.Add(cat??"");

            if (!string.IsNullOrEmpty(selectedCat))
                lbCategories.SelectedIndex = lbCategories.Items.IndexOf(selectedCat);

            showList();
        }

        private void showList()
        {
            int oldIndex = lbTemplates.SelectedIndex;

            string selectedCat = (lbCategories.SelectedItem == null || lbCategories.SelectedItem.ToString()=="") ? null : lbCategories.SelectedItem.ToString();
            lbTemplates.Items.Clear();
            foreach (Template tmp in gen.Templates.Where(t=>t.Category==selectedCat))
                lbTemplates.Items.Add(tmp);

            if (lbTemplates.Items.Count > oldIndex)
                lbTemplates.SelectedIndex = oldIndex;
            else
                lbTemplates.SelectedIndex = lbTemplates.Items.Count - 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Template tmp = (Template)lbTemplates.SelectedItem;
            if (MessageBox.Show("Are you sure to delete " + tmp, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gen.Templates.Remove(tmp);
                save();
                showCategories(tmp.Category);
            }
        }

        private void save()
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                XmlSerializer ser = new XmlSerializer(typeof(CodeGenerator));
                ser.Serialize(sw, gen);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Template tmp = new Template();
            tmp.Name = "New Template";
            tmp.Code = "Code...";

            FormTemplate form = new FormTemplate(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                gen.Templates.Add(form.Template);
                showCategories(form.Template.Category);
                save();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Template tmp = (Template)lbTemplates.SelectedItem;
            FormTemplate form = new FormTemplate(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                gen.Templates[gen.Templates.IndexOf(tmp)] = form.Template;
                showCategories(form.Template.Category);
                save();
            }
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            if (lbEntities.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check one or more entity first", "Warning");
                return;
            }
            if (lbTemplates.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please check one or more template first", "Warning");
                return;
            }

            List<GeneratedCode> generatedCodes = new List<GeneratedCode>();

            foreach (Template tmp in lbTemplates.SelectedItems)
            {
                foreach (Table selectedTable in lbEntities.CheckedItems)
                {
                    Interpreter engine = new Interpreter(tmp.Code, null);
                    engine.SetAttribute("table", selectedTable);
                    engine.SetAttribute("util", new Util());
                    engine.Parse();
                    engine.Execute();

                    string path = "";
                    if (!string.IsNullOrEmpty(tmp.FileNameFormat))
                    {
                        Interpreter engine2 = new Interpreter(tmp.FileNameFormat.Replace("\\", "|"), null);
                        engine2.SetAttribute("table", selectedTable);
                        engine2.SetAttribute("util", new Util());
                        engine2.Parse();
                        engine2.Execute();
                        //TODO: put code generation base path to settings
                        path = "E:\\kodlar\\projects\\Interpress\\" + engine2.Output.Replace("|", "\\");
                    }

                    generatedCodes.Add(new GeneratedCode { 
                        Code = engine.Output,
                        Path = path,
                        Template = tmp
                    });

                }
            }

            FormGeneratedCode form = new FormGeneratedCode(generatedCodes);
            //form.Text = " - " + tmp.Name + " code for " + selectedTable.Name;
            //form.Path = path;
            form.Show();
        }

        private void btnGenerateAll_Click(object sender, EventArgs e)
        {
            if (lbEntities.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check one or more entity first", "Warning");
                return;
            }
            if (lbTemplates.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please check one or more template first", "Warning");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the directory in which the code is gonna be generated";
            fbd.SelectedPath = @"c:\temp";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                foreach (Template tmp in lbTemplates.SelectedItems)
                {
                    foreach (Table selectedTable in lbEntities.CheckedItems)
                    {
                        Interpreter engine = new Interpreter(tmp.Code, null);
                        engine.SetAttribute("table", selectedTable);
                        engine.SetAttribute("util", new Util());
                        engine.Parse();
                        engine.Execute();

                        string code = engine.Output;

                        Interpreter engine2 = new Interpreter(tmp.FileNameFormat.Replace("\\", "|"), null);
                        engine2.SetAttribute("table", selectedTable);
                        engine2.SetAttribute("util", new Util());
                        engine2.Parse();
                        engine2.Execute();

                        string path = fbd.SelectedPath + "\\" + engine2.Output.Replace("|", "\\");

                        createDirectories(Path.GetDirectoryName(path));
                        File.WriteAllText(path, code, Encoding.UTF8);
                    }
                }
                System.Diagnostics.Process.Start(fbd.SelectedPath);
            }
        }

        private void createDirectories(string path)
        {
            if (Directory.Exists(path))
                return;
            else
            {
                if(Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(path);
                else
                {
                    createDirectories(Path.GetDirectoryName(path));
                    createDirectories(path);
                }
            }
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lbEntities.Items.Count; i++)
                lbEntities.SetItemChecked(i, cbAll.Checked);
        }

        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            showList();
        }
    }

    public class GeneratedCode
    {
        public string Path { get; set; }
        public Template Template { get; set; }
        public string Code { get; set; }
    }

    public class Util
    {
        public string Cap(string str)
        {
            string res = str.Substring(0, 1).ToUpperInvariant();
            for (var i = 1; i < str.Length; i++)
            {
                var harf = str.Substring(i, 1);
                if (harf == "_")
                {
                    i++;
                    res = res + str.Substring(i, 1).ToUpperInvariant();
                }
                else
                    res = res + harf;
            }
            return res;
        }
        public string CapWithDash(string str)
        {
            string res = str.Substring(0, 1).ToUpperInvariant();
            for (var i = 1; i < str.Length; i++)
            {
                var harf = str.Substring(i, 1);
                if (harf == "_")
                {
                    i++;
                    res = res + "_" + str.Substring(i, 1).ToUpperInvariant();
                }
                else
                    res = res + harf;
            }
            return res;
        }
        public string Camel(string str)
        {
            string res = Cap(str);
            return res.Substring(0, 1).ToLowerInvariant() + res.Substring(1);
        }

        public string CSType(string columnType)
        {
            Cinar.Database.DbType dbType = (Cinar.Database.DbType)Enum.Parse(typeof(Cinar.Database.DbType), columnType);
            switch (dbType)
            {
                case Cinar.Database.DbType.Binary:
                case Cinar.Database.DbType.Blob:
                case Cinar.Database.DbType.BlobLong:
                case Cinar.Database.DbType.BlobMedium:
                case Cinar.Database.DbType.BlobTiny:
                case Cinar.Database.DbType.Image:
                case Cinar.Database.DbType.VarBinary:
                case Cinar.Database.DbType.Variant:
                    return "string";

                case Cinar.Database.DbType.Boolean:
                    return "bool";

                case Cinar.Database.DbType.Time:
                case Cinar.Database.DbType.Timetz:
                    return "Time";

                case Cinar.Database.DbType.Timestamp:
                case Cinar.Database.DbType.Timestamptz:
                case Cinar.Database.DbType.Date:
                case Cinar.Database.DbType.DateTime:
                case Cinar.Database.DbType.DateTimeSmall:
                    return "DateTime";

                case Cinar.Database.DbType.Currency:
                case Cinar.Database.DbType.CurrencySmall:
                case Cinar.Database.DbType.Decimal:
                case Cinar.Database.DbType.Numeric:
                case Cinar.Database.DbType.Real:
                    return "decimal";

                case Cinar.Database.DbType.Double:
                    return "double";

                case Cinar.Database.DbType.Float:
                    return "float";

                case Cinar.Database.DbType.Int16:
                case Cinar.Database.DbType.Int32:
                case Cinar.Database.DbType.Enum:
                case Cinar.Database.DbType.Set:
                case Cinar.Database.DbType.Byte:
                case Cinar.Database.DbType.Char:
                    return "int";

                case Cinar.Database.DbType.Int64:
                    return "long";

                case Cinar.Database.DbType.Guid:
                case Cinar.Database.DbType.NChar:
                case Cinar.Database.DbType.NText:
                case Cinar.Database.DbType.NVarChar:
                case Cinar.Database.DbType.Text:
                case Cinar.Database.DbType.TextLong:
                case Cinar.Database.DbType.TextMedium:
                case Cinar.Database.DbType.TextTiny:
                case Cinar.Database.DbType.VarChar:
                case Cinar.Database.DbType.Xml:
                case Cinar.Database.DbType.Undefined:
                default:
                    return "string";
            }
        }

        public string CSTypeConstant(string columnType)
        {
            Cinar.Database.DbType dbType = (Cinar.Database.DbType)Enum.Parse(typeof(Cinar.Database.DbType), columnType);
            switch (dbType)
            {
                case Cinar.Database.DbType.Binary:
                case Cinar.Database.DbType.Blob:
                case Cinar.Database.DbType.BlobLong:
                case Cinar.Database.DbType.BlobMedium:
                case Cinar.Database.DbType.BlobTiny:
                case Cinar.Database.DbType.Image:
                case Cinar.Database.DbType.VarBinary:
                case Cinar.Database.DbType.Variant:
                    return "\"\"";

                case Cinar.Database.DbType.Boolean:
                    return "false";

                case Cinar.Database.DbType.Time:
                case Cinar.Database.DbType.Timetz:
                    return "Datetime.Now";

                case Cinar.Database.DbType.Timestamp:
                case Cinar.Database.DbType.Timestamptz:
                case Cinar.Database.DbType.Date:
                case Cinar.Database.DbType.DateTime:
                case Cinar.Database.DbType.DateTimeSmall:
                    return "DateTime.Now";

                case Cinar.Database.DbType.Double:
                    return "0d";

                case Cinar.Database.DbType.Float:
                    return "0f";

                case Cinar.Database.DbType.Currency:
                case Cinar.Database.DbType.CurrencySmall:
                case Cinar.Database.DbType.Decimal:
                case Cinar.Database.DbType.Numeric:
                case Cinar.Database.DbType.Real:
                    return "0M";

                case Cinar.Database.DbType.Int16:
                case Cinar.Database.DbType.Int32:
                case Cinar.Database.DbType.Enum:
                case Cinar.Database.DbType.Set:
                case Cinar.Database.DbType.Byte:
                case Cinar.Database.DbType.Char:
                    return "0";

                case Cinar.Database.DbType.Int64:
                    return "0";

                case Cinar.Database.DbType.Guid:
                case Cinar.Database.DbType.NChar:
                case Cinar.Database.DbType.NText:
                case Cinar.Database.DbType.NVarChar:
                case Cinar.Database.DbType.Text:
                case Cinar.Database.DbType.TextLong:
                case Cinar.Database.DbType.TextMedium:
                case Cinar.Database.DbType.TextTiny:
                case Cinar.Database.DbType.VarChar:
                case Cinar.Database.DbType.Xml:
                case Cinar.Database.DbType.Undefined:
                default:
                    return "\"\"";
            }
        }
    }
}