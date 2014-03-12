using System;
using Cinar.Database;
using System.Xml.Serialization;
using System.Drawing;

//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentPicture : BaseEntity
    {
        private int contentId;
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private string title = "";
        [ColumnDetail(Length = 200)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description = "";
        [ColumnDetail(ColumnType=DbType.Text)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string tag = "";
        [ColumnDetail(Length = 200)]
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private string tagData = "";
        public string TagData
        {
            get { return tagData; }
            set { tagData = value; }
        }

        public int LikeIt
        {
            get;
            set;
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

    }

}
