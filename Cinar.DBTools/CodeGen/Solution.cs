using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Cinar.DBTools.CodeGen
{
    [Serializable]
    public class Solution : FolderItem
    {
        [XmlIgnore]
        public string FullPath { get; set; }

        public bool Modified { get; set;}

        public Solution()
        {
        }

        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Solution));
            using (StreamWriter sr = new StreamWriter(FullPath))
            {
                ser.Serialize(sr, this);
            }
            Modified = false;
        }
        public static Solution Load(string path)
        {
            Solution res = null;

            if (File.Exists(path))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Solution));
                using (StreamReader sr = new StreamReader(path))
                {
                    try
                    {
                        res = (Solution)ser.Deserialize(sr);
                        res.FullPath = path;
                        res.name = System.IO.Path.GetFileNameWithoutExtension(res.FullPath);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("This is not a valid solution file. Error details:\n" + ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        res = new Solution();
                        res.FullPath = path;
                        res.name = System.IO.Path.GetFileNameWithoutExtension(res.FullPath);
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found:\n" + path, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }
    }
}
