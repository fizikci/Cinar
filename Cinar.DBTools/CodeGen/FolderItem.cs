using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.Scripting;
using Cinar.DBTools.Tools;

namespace Cinar.DBTools.CodeGen
{
    public class FolderItem : Item
    {
        [Browsable(false)]
        public ItemCollection Items { get; set; }

        public Item AddNewItem(Item item)
        {
            this.Items.Add(item);
            int i = 2; string newName = item.Name;
            while (File.Exists(item.Path)) item.Name = (newName.Substring(0, newName.LastIndexOf('.')) + " (" + (i++) + ")" + newName.Substring(newName.LastIndexOf('.')));
            File.WriteAllText(item.Path, "", Encoding.UTF8);
            Solution.Modified = true;
            return item;
        }
        public Item AddExistingItem(Item item, string filePathToCopy)
        {
            this.Items.Add(item);
            if (filePathToCopy != item.Path)
            {
                if (!File.Exists(item.Path) || MessageBox.Show("There is a file with the same name. Overwrite?", "Cinar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Copy(filePathToCopy, item.Path, true);
                    Solution.Modified = true;
                }
                else
                {
                    this.Items.Remove(item);
                    throw new Exception("Canceled");
                }
            }
            return item;
        }
        public FolderItem AddNewFolder(FolderItem item)
        {
            this.Items.Add(item);
            int i = 2; string newName = item.Name;
            while (Directory.Exists(item.Path)) item.Name = newName + " (" + (i++) + ")";
            Directory.CreateDirectory(item.Path);
            Solution.Modified = true;
            return item;
        }
        public FolderItem AddExistingFolder(FolderItem folderItem, string path)
        {
            if(this.Items[folderItem.Name]!=null)
                throw new Exception("There is already a folder with the same name");
            this.Items.Add(folderItem);
            return folderItem;
        }

        public override void Delete()
        {
            Directory.Delete(this.Path, true);
            parent.Remove(this);
            Solution.Modified = true;
        }

        public override List<GeneratedCode> GenerateCode(string basePath, List<Table> selectedTables)
        {
            foreach (Table tbl in selectedTables)
                tbl.GenerateUIMetadata();

            List<GeneratedCode> generatedCodes = new List<GeneratedCode>();

            switch (RepeatGeneration)
            {
                case RepeatGenerationTypes.NoRepeat:
                    GeneratedCode gc = GenerateCode(basePath, selectedTables[0]);
                    generatedCodes.Add(gc);
                    foreach (Item item in this.Items)
                        generatedCodes.AddRange(item.GenerateCode(gc.Path, selectedTables));
                    break;
                case RepeatGenerationTypes.ForEachCategory:
                    foreach (string cat in selectedTables.Select(t => t.UIMetadata.ModuleName).Distinct())
                    {
                        GeneratedCode gc2 = GenerateCode(basePath, selectedTables.Where(t => t.UIMetadata.ModuleName == cat).FirstOrDefault());
                        generatedCodes.Add(gc2);
                        foreach (Item item in this.Items)
                            generatedCodes.AddRange(item.GenerateCode(gc2.Path, selectedTables.Where(t => t.UIMetadata.ModuleName == cat).ToList()));
                    }
                    break;
                case RepeatGenerationTypes.ForEachTable:
                    foreach (Table table in selectedTables)
                    {
                        GeneratedCode gc3 = GenerateCode(basePath, table);
                        generatedCodes.Add(gc3);
                        foreach (Item item in this.Items)
                            generatedCodes.AddRange(item.GenerateCode(gc3.Path, new List<Table> { table }));
                    }
                    break;
            }

            return generatedCodes;
        }
        public GeneratedCode GenerateCode(string basePath, Table table)
        {
            string path = Name;
            if (!string.IsNullOrEmpty(this.FileNameTemplate))
            {
                Interpreter engineForPath = new Interpreter(this.FileNameTemplate, null);
                engineForPath.SetAttribute("db", Provider.Database);
                engineForPath.SetAttribute("table", table);
                engineForPath.SetAttribute("util", new Util());
                engineForPath.Parse();
                engineForPath.Execute();
                path = engineForPath.Output;
            }

            return new GeneratedCode
            {
                Path = basePath + "\\" + path,
                IsFolder = true
            };
        }

        public override void CreateCode(string basePath, List<Table> selectedTables)
        {
            foreach (Table tbl in selectedTables)
                tbl.GenerateUIMetadata();

            switch (RepeatGeneration)
            {
                case RepeatGenerationTypes.NoRepeat:
                    basePath = CreateCode(basePath, selectedTables[0]);
                    foreach (Item item in this.Items)
                        item.CreateCode(basePath, selectedTables);
                    break;
                case RepeatGenerationTypes.ForEachCategory:
                    foreach (string cat in selectedTables.Select(t => t.UIMetadata.ModuleName).Distinct())
                    {
                        basePath = CreateCode(basePath, selectedTables.Where(t => t.UIMetadata.ModuleName == cat).FirstOrDefault());
                        foreach (Item item in this.Items)
                            item.CreateCode(basePath, selectedTables.Where(t => t.UIMetadata.ModuleName == cat).ToList());
                    }
                    break;
                case RepeatGenerationTypes.ForEachTable:
                    foreach (Table table in selectedTables)
                    {
                        basePath = CreateCode(basePath, table);
                        foreach (Item item in this.Items)
                            item.CreateCode(basePath, new List<Table> { table });
                    }
                    break;
            }
        }
        public string CreateCode(string basePath, Table table)
        {
            GeneratedCode cg = GenerateCode(basePath, table);
            if(!Directory.Exists(cg.Path))
                Directory.CreateDirectory(cg.Path);
            return cg.Path;
        }

        public FolderItem() 
        {
            Items = new ItemCollection(this);
            RepeatGeneration = RepeatGenerationTypes.ForEachCategory;
        }

    }
}
