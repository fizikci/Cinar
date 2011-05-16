using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using Cinar.Database;

namespace Cinar.DBTools.CodeGen
{
    [Serializable]
    [XmlInclude(typeof(FileItem))]
    [XmlInclude(typeof(FolderItem))]
    public abstract class Item
    {
        internal string name;
        [Description("Name of the template file or folder")]
        public string Name {
            get { return name; }
            set {
                string oldName = name;
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(name) && name != value)
                {
                    try
                    {
                        string oldPath = this.Path;
                        name = value;
                        string newPath = this.Path;
                        if(this is FolderItem)
                            Directory.Move(oldPath, newPath);
                        else
                            File.Move(oldPath, newPath);
                        Solution.Modified = true;
                        this.Solution.Save();
                    }
                    catch (Exception ex)
                    {
                        name = oldName;
                        throw new Exception(ex.Message);
                    }
                }
                else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                name = value;
            } 
        }
        private string fileNameTemplate;
        [Description("Name of the generated file such as $=table.Name$.xml")]
        public string FileNameTemplate { get { return fileNameTemplate; } set { if (value != fileNameTemplate && Solution != null) Solution.Modified = true; fileNameTemplate = value; } }
        private RepeatGenerationTypes repeatGeneration;
        [Description("How many copies to be generated?")]
        public RepeatGenerationTypes RepeatGeneration { get { return repeatGeneration; } set { if (value != repeatGeneration && Solution != null) Solution.Modified = true; repeatGeneration = value; } }

        [XmlIgnore, Browsable(false)]
        internal ItemCollection parent;
        [Browsable(false)]
        public Item ParentItem { get { return parent == null ? null : parent.parent; } }
        [Browsable(false)]
        public string Path
        {
            get
            {
                if (this is Solution)
                    return System.IO.Path.GetDirectoryName((this as Solution).FullPath);

                Item p = ParentItem;
                string res = Name;
                while (!(p is Solution))
                {
                    res = p.Name + "\\" + res;
                    p = p.ParentItem;
                }
                res = System.IO.Path.GetDirectoryName((p as Solution).FullPath) + "\\" + res;
                return res;
            }
        }
        [Browsable(false)]
        public Solution Solution
        {
            get
            {
                if (this is Solution)
                    return this as Solution;

                Item p = ParentItem;
                if (p == null)
                    return null;

                while (!(p is Solution))
                    p = p.ParentItem;
                return p as Solution;
            }
        }

        public abstract void Delete();

        public abstract List<GeneratedCode> GenerateCode(string basePath, List<Table> selectedTables);
        public abstract void CreateCode(string basePath, List<Table> selectedTables);
    }

    //public enum ItemType
    //{
    //    Folder,
    //    File
    //}
    public enum RepeatGenerationTypes
    {
        NoRepeat,
        ForEachCategory,
        ForEachTable
    }

    [Serializable]
    public class ItemCollection : List<Item>
    {
        internal Item parent;
        public Item Parent { get { return parent; } }

        public ItemCollection()
        {
        }
        public ItemCollection(Item parent)
        {
            this.parent = parent;
        }

        public Item this[string name]
        {
            get
            {
                foreach (Item item in this)
                    if (item.Name == name)
                        return item;
                return null;
            }
        }

        public new int Add(Item item)
        {
            item.parent = this;
            base.Add(item);
            return base.Count;
        }

    }
}
