﻿using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [EditFormDetails(DetailType = typeof(PollAnswer), RelatedFieldName = "PollQuestionId")]
    [EditFormDetails(DetailType = typeof(PollQuestionLang), RelatedFieldName = "PollQuestionId")]
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Question, Visible from [PollQuestion]")]
    public class PollQuestion : BaseEntity
    {
        private string question;
        [ColumnDetail(IsNotNull = true)]
        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        public override string GetNameValue()
        {
            return this.question;
        }
        public override string GetNameColumn()
        {
            return "Question";
        }

        protected override bool beforeDelete()
        {
            base.beforeDelete();

            foreach (PollAnswer pa in Provider.Database.ReadList(typeof(PollAnswer), "select * from [PollAnswer] where PollQuestionId={0}", this.Id))
                pa.Delete();

            return true;
        }
    }
}
