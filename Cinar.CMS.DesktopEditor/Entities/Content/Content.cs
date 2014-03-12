using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Drawing;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Linq;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class Content : BaseEntity
    {
        public string Keyword { get; set; }

        private string className = "Content";
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private int catId = 1;
        public int CategoryId
        {
            get { return catId; }
            set { catId = value; }
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
        public string Metin
        {
            get { return metin; }
            set { metin = value; }
        }

        private DateTime publishDate = DateTime.Now;
        public DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private string picture;
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string picture2;
        public string Picture2
        {
            get { return picture2; }
            set { picture2 = value; }
        }

        private string keywords;
        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        private string hierarchy = "";
        public string Hierarchy
        {
            get { return hierarchy; }
            set { hierarchy = value; }
        }

        private int authorId = 1;
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        private int sourceId = 1;
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        private string tags = "";
        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private string tagRanks = "";
        public string TagRanks
        {
            get { return tagRanks; }
            set { tagRanks = value; }
        }

        private string showInPage = "";
        public string ShowInPage
        {
            get { return showInPage; }
            set { showInPage = value; }
        }

        private string showContentsInPage = "";
        public string ShowContentsInPage
        {
            get { return showContentsInPage; }
            set { showContentsInPage = value; }
        }

        private string showCategoriesInPage = "";
        public string ShowCategoriesInPage
        {
            get { return showCategoriesInPage; }
            set { showCategoriesInPage = value; }
        }

        private bool isManset;
        public bool IsManset
        {
            get { return isManset; }
            set { isManset = value; }
        }

        private string spotTitle;
        public string SpotTitle
        {
            get { return spotTitle; }
            set { spotTitle = value; }
        }

        private int contentSourceId;
        public int ContentSourceId
        {
            get { return contentSourceId; }
            set { contentSourceId = value; }
        }

        private string sourceLink;
        public string SourceLink
        {
            get { return sourceLink; }
            set { sourceLink = value; }
        }

        private int viewCount = 0;
        public int ViewCount
        {
            get { return viewCount; }
            set { viewCount = value; }
        }

        private int commentCount = 0;
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }

        private int recommendCount = 0;
        public int RecommendCount
        {
            get { return recommendCount; }
            set { recommendCount = value; }
        }

        public int LikeIt
        {
            get;
            set;
        }

        private string extraField1 = "";
        private string extraField2 = "";
        private string extraField3 = "";
        private string extraField4 = "";
        private string extraField5 = "";

        public string ExtraField1 { get { return extraField1; } set { extraField1 = value; } }
        public string ExtraField2 { get { return extraField2; } set { extraField2 = value; } }
        public string ExtraField3 { get { return extraField3; } set { extraField3 = value; } }
        public string ExtraField4 { get { return extraField4; } set { extraField4 = value; } }
        public string ExtraField5 { get { return extraField5; } set { extraField5 = value; } }
    }

}
