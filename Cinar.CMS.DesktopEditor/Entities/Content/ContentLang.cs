using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentLang : BaseEntity
    {
        private int contentId = 1;
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private int langId;
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string metin;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Metin
        {
            get { return metin; }
            set { metin = value; }
        }

        private string sourceLink;
        [ColumnDetail(Length = 200)]
        public string SourceLink
        {
            get { return sourceLink; }
            set { sourceLink = value; }
        }


        public string ExtraField1 { get; set; }
        public string ExtraField2 { get; set; }
        public string ExtraField3 { get; set; }
        public string ExtraField4 { get; set; }
        public string ExtraField5 { get; set; }


        private string picture;
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string spotTitle;
        public string SpotTitle
        {
            get { return spotTitle; }
            set { spotTitle = value; }
        }
    }

}
