using System;
using Cinar.Database;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Name as [BannerAd.Name], StartDate as [BannerAd.StartDate], FinishDate as [BannerAd.FinishDate], ViewCount as [BannerAd.ViewCount], ClickCount as [BannerAd.ClickCount] from BannerAd")]
    public class BannerAd : BaseEntity
    {
        public BannerAd()
        {
            this.Visible = false;
        }

        private string name;
        [ColumnDetail(IsNotNull = true, Length = 50), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string href;
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        private string bannerFile = "";
        [ColumnDetail(Length = 100, IsNotNull = true), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = false, UseYearMonthDayFolders = false)]
        public string BannerFile
        {
            get { return bannerFile; }
            set { bannerFile = value; }
        }

        private string tags = "";
        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private string viewCondition = "Click"; // or View or DateInterval
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_VIEWCONDITION_")]
        public string ViewCondition
        {
            get { return viewCondition; }
            set { viewCondition = value; }
        }

        private DateTime startDate = DateTime.Now;
        [ColumnDetail(IsNotNull=true)]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime finishDate = DateTime.Now.AddMonths(1);
        [ColumnDetail(IsNotNull=true)]
        public DateTime FinishDate
        {
            get { return finishDate; }
            set { finishDate = value; }
        }

        private int maxViewCount = -1;
        public int MaxViewCount
        {
            get { return maxViewCount; }
            set { maxViewCount = value; }
        }

        private int viewCount = 0;
        [EditFormFieldProps(Options = "readOnly:true")]
        public int ViewCount
        {
            get { return viewCount; }
            set { viewCount = value; }
        }

        private int maxClickCount = -1;
        public int MaxClickCount
        {
            get { return maxClickCount; }
            set { maxClickCount = value; }
        }

        private int clickCount = 0;
        [EditFormFieldProps(Options = "readOnly:true")]
        public int ClickCount
        {
            get { return clickCount; }
            set { clickCount = value; }
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (this.Tags.Length > 0)
                if (Regex.Match(this.Tags, "[^\\w,\\,\\s]").Success)
                    throw new Exception(Provider.GetResource("You may use only letters, space and comma for the field Tags"));

            this.startDate = this.startDate.Date;
            this.finishDate = this.finishDate.Date;
        }

        public override string GetNameColumn()
        {
            return "Name";
        }
        public override string GetNameValue()
        {
            return this.name;
        }
    }

}
