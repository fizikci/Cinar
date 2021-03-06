﻿using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Recommendation.Id as [Recommendation.Id], Content.Title as [Content.Title], Recommendation.EmailFrom as [Recommendation.EmailFrom], Recommendation.EmailTo as [Recommendation.EmailTo], Recommendation.Visible as [Recommendation.Visible] from Recommendation left join Content on Content.Id = Recommendation.ContentId", QueryOrderBy = "Recommendation.Id desc")]
    public class Recommendation : BaseEntity
    {
        private int contentId;
        [ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }
        private Content _content;
        [XmlIgnore]
        public Content Content
        {
            get
            {
                if (_content == null)
                    _content = (Content)Provider.Database.Read(typeof(Content), this.contentId);
                return _content;
            }
        }

        private string nameFrom;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string NameFrom
        {
            get { return nameFrom; }
            set { nameFrom = value; }
        }

        private string emailFrom;
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string EmailFrom
        {
            get { return emailFrom; }
            set { emailFrom = value; }
        }

        private string nameTo;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string NameTo
        {
            get { return nameTo; }
            set { nameTo = value; }
        }

        private string emailTo;
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string EmailTo
        {
            get { return emailTo; }
            set { emailTo = value; }
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            // eğer content varsa content'in recommendCount'unu arttıralım
            if (this.contentId > 0)
                Provider.Database.ExecuteNonQuery("update Content set RecommendCount = {1} where Id={0}",
                    this.contentId,
                    Provider.Database.GetValue("select count(*) from Recommendation where ContentId={0} and Visible=1", this.contentId));
        }
    }

}
