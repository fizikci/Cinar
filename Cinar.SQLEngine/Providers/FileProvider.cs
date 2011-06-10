using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Cinar.SQLParser;
using System.IO;

namespace Cinar.SQLEngine.Providers
{
    public class FileProvider
    {
        private string path;
        private bool recursive;

        public FileProvider(string path, bool recursive)
        {
            this.path = path;
            this.recursive = recursive;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = getFileList(context, where, fieldNames, path);

            return list;

        }

        private List<Hashtable> getFileList(Context context, Expression where, ListSelect fieldNames, string dirPath)
        {
            List<Hashtable> list = new List<Hashtable>();
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileSystemInfo fi in di.GetDirectories())
            {
                FileItem item = new FileItem(fi);
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);//context.Variables[fieldName];
                    list.Add(ht);
                    if (recursive)
                        list.AddRange(getFileList(context, where, fieldNames, fi.FullName));
                }
            }
            foreach (FileSystemInfo fi in di.GetFiles())
            {
                FileItem item = new FileItem(fi);
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);//context.Variables[fieldName];
                    list.Add(ht);
                }
            }
            return list;
        }
    }
    public class FileItem : BaseItem
    {
        public bool IsDirectory { get; set; }
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public byte[] BinaryContent { get; set; }
        public string TextContent { get; set; }

        public FileItem(FileSystemInfo fi)
        {
            this.IsDirectory = fi is DirectoryInfo;
            this.CreationTime = fi.CreationTime;
            this.Extension = fi.Extension;
            this.FullPath = fi.FullName;
            this.LastAccessTime = fi.LastAccessTime;
            this.LastWriteTime = fi.LastWriteTime;
            this.Name = fi.Name;
        }
    }
}
