using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;

namespace Cinar.LocalizationTool
{
    public partial class MainForm : Form
    {
        Dictionary<string, DataRow> turkceIndex;
        DataTable turkceTable;

        Dictionary<string, DataRow> englishIndex;
        DataTable englishTable;

        public MainForm()
        {
            InitializeComponent();
        }

        private void dizinSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            labelDizin.Text = dialog.SelectedPath;
        }

        private void başlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdGenerateInfo();
        }

        void logClear()
        {
            listLog.Items.Clear();
        }

        void log(string type, string message)
        {
            listLog.Items.Add(DateTime.Now.ToString("hh:mm:ss, ") + message);
        }

        DataTable createLocalTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Scope", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("LocalValue", typeof(string));

            return table;
        }

        void cmdLedOn()
        {
            labelLed.BackColor = Color.LawnGreen;
        }

        void cmdLedOff()
        {
            labelLed.BackColor = Color.DarkGray;
        }

        void cmdSave()
        {
            DataTable[] tables = new DataTable[] { turkceTable, englishTable };
            foreach (DataTable table in tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if ((bool)row["Deleted"])
                    {
                        Program.Database.ExecuteNonQuery("delete from LocalText where Id=" + row["Id"]);
                    }
                    else if ((bool)row["IsNew"])
                    {
                        Program.Database.ExecuteNonQuery(string.Format(
                            @"insert into LocalText
                                (Scope, Code, Prop, Lang, LocalValue)
                              values
                                ('{0}', '{1}', '{2}', {3}, '{4}')",
                            row["Scope"], row["Code"], row["Prop"], row["Lang"], row["LocalValue"]));
                    }
                    else if(row.RowState == DataRowState.Modified)
                    {
                        Program.Database.ExecuteNonQuery("update LocalText set LocalValue={0} where Id={1}", row["LocalValue"], row["Id"]);
                    }
                }
            }
        }

        void cmdGenerateInfo()
        {
            logClear();
            log("Info", "İşlem Başlatıldı");
            cmdLedOn();

            turkceIndex = new Dictionary<string, DataRow>();
            turkceTable = Program.Database.GetDataTable("select Id, Deleted, IsNew, Scope, Code, Prop, LocalValue, Lang from LocalText where Lang=0 order by Scope, Code, Prop");
            foreach (DataRow row in turkceTable.Rows) { row["Deleted"] = true; row["IsNew"] = false; }
            //
            foreach (DataRow row in turkceTable.Rows) turkceIndex[row["Scope"] + " " + row["Code"] + " " + row["Prop"]] = row;
            gridTurkce.DataSource = turkceTable;

            englishIndex = new Dictionary<string, DataRow>();
            englishTable = Program.Database.GetDataTable("select Id, Deleted, IsNew, Scope, Code, Prop, LocalValue, Lang from LocalText where Lang=1 order by Scope, Code, Prop");
            foreach (DataRow row in englishTable.Rows) { row["Deleted"] = true; row["IsNew"] = false; }
            //
            foreach (DataRow row in englishTable.Rows) englishIndex[row["Scope"] + " " + row["Code"] + " " + row["Prop"]] = row;
            gridEnglish.DataSource = englishTable; 

            string[] files = Directory.GetFiles(labelDizin.Text, "*.cs", SearchOption.AllDirectories);           
            foreach (string file in files)
            {
                if(file.EndsWith(".Designer.cs"))
                    generateDesignerInfo(file);
                else
                    generateFileInfo(file, turkceTable);

                this.Refresh();
            }

            turkceTable.AcceptChanges();
            englishTable.AcceptChanges();

            log("Info", "İşlem Tamamlandı");
            cmdLedOff();
        }

        void generateDesignerInfo(string file)
        {
            string fileText = File.ReadAllText(file, getFileEncoding(file));

            string nspace = getNamespace(fileText);
            string className = getDesignerClassName(fileText);
            string lastControl = "";

            string[] lines = getLines(fileText);
            for (int index = 0; index < lines.Length; index++)
            {
                string temp = lines[index].Trim();
                int i = temp.IndexOf(".Text = \"");
                string prop = "Text";
                if (i < 0)
                {
                    i = temp.IndexOf(".Caption = \"");
                    prop = "Caption";
                }
                if (i < 0) continue;                
                if (temp.Contains(".Text = \"\";")
                    || temp.Contains(".Caption = \"\";")) continue;

                string scope = nspace + "." + className;
                string name = "";
                bool isControl = true;
                if (temp.StartsWith("this."))
                {
                    name = (i - "this.".Length <= 0 && temp.StartsWith("this.Text = \""))
                         ? "this" : temp.Substring("this.".Length, i - "this.".Length);
                }
                else
                {
                    isControl = false;
                    name = lastControl;
                    prop = ":" + temp.Substring(0, temp.IndexOf("."));
                }
                int j = temp.LastIndexOf('"');
                string value = temp.Substring(temp.IndexOf('"') + 1, j - temp.IndexOf('"') - 1);

                while (lines[index].Trim().EndsWith("+"))
                {
                    temp = lines[index+1].Trim();
                    value += temp.Substring(1, temp.LastIndexOf('"') - 1);
                    index++;
                }

                
                string key = scope + " " + name + " " + prop;
                updateLocalItem(turkceIndex, turkceTable, key, scope, prop, name, value, 0);
                updateLocalItem(englishIndex, englishTable, key, scope, prop, name, value, 1);

                if(isControl) lastControl = name;

                log("Info", ">> " + scope + ", " + name + " = " + value);
            }
        }

        void updateLocalItem(Dictionary<string, DataRow> dictionary, DataTable table, string key, string scope, string prop, string code, string value, int lang)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key]["Deleted"] = false;
            else
            {
                DataRow row = table.NewRow();
                row["Scope"] = scope;
                row["Code"] = code;
                row["Prop"] = prop;
                row["LocalValue"] = value;
                row["Lang"] = lang;
                row["Deleted"] = false;
                row["IsNew"] = true;

                table.Rows.Add(row);
            }
        }

        string[] getLines(string text)
        {
            return text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        void generateFileInfo(string file, DataTable table)
        {
            string fileText = File.ReadAllText(file, getFileEncoding(file));

            string nspace = getNamespace(fileText);
            string className = getDesignerClassName(fileText);

            string localizer = "Localization.Get" + "(this"; //böyle yazmazsam arayınca bu satırı da buluyor.

            int index = fileText.IndexOf(localizer);
            while (index > 0)
            {
                index += localizer.Length;
                while (fileText[index] != '"') index++;
                index++;

                bool atChar = 
                    fileText[index - 1] == '@'; //@
   
                int endIndex = index;
                while (fileText[endIndex] != '"')
                {
                    endIndex++;
                    if (!atChar && fileText[endIndex] == '\\') endIndex++; //varsa, escape edilmiş karakteri atlıyoruz
                    if (atChar
                        && fileText[endIndex] == '"'
                        && fileText[endIndex + 1] == '"') endIndex += 2; //varsa, escape edilmiş karakteri atlıyoruz
                }

                string value = fileText.Substring(index, endIndex - index);                
                string scope = nspace + "." + className;
                string key = scope + " " + value + " *";//Not: row["Scope"] + " " + row["Code"] + " " + row["Prop"]
                string code = value;

                bool saveThis = true;
                if (value.StartsWith("#")) //# means global scpoe
                {
                    if (value.Contains("|"))
                    {
                        scope = "GLOBAL";
                        code = value.Substring(1, value.IndexOf('|') - 1);
                        value = value.Substring(value.IndexOf('|') + 1);
                        key = scope + " " + code + " " + "*";
                    }
                    else
                        saveThis = false;
                }
                if (saveThis)
                {
                    updateLocalItem(turkceIndex, turkceTable, key, scope, "*", code, value, 0);
                    updateLocalItem(englishIndex, englishTable, key, scope, "*", code, value, 1);

                    log("Info", ">> " + scope + ", * = " + value);
                }
                index = fileText.IndexOf(localizer, endIndex);
            }
        }

        string getNamespace(string text)
        {
            string ns = "namespace ";
            int i = text.IndexOf(ns);
            if (i == -1) return "";
            i += ns.Length;

            string temp = "";
            while (i<text.Length && text[i] != '{')
            {
                temp += text[i];
                i++;
            }
            
            return temp.Trim();
        }

        string getDesignerClassName(string text)
        {
            string keyword = "partial class ";
            int i = text.IndexOf(keyword);
            if (i == -1) return "";
            i += keyword.Length;

            string temp = "";
            while (i<text.Length && text[i] != '{')
            {
                temp += text[i];
                i++;
            }

            if (temp.Contains(":"))
                temp = temp.Substring(0, temp.IndexOf(":")); //inheritance filan...

            return temp.Trim();
        }

        Encoding getFileEncoding(string file)
        {
            byte[] encodingBytes = Encoding.UTF8.GetPreamble();
            byte[] fileBytes = new byte[encodingBytes.Length];
            FileStream stream = File.OpenRead(file);
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();

            Encoding encoding = Encoding.Default;
            bool isUtf = true;
            for (int i = 0; i < fileBytes.Length; i++)
                if (fileBytes[i] != encodingBytes[i]) isUtf = false;

            if (isUtf) encoding = Encoding.UTF8;

            return encoding;
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdSave();
        }
    }
}