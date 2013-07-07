using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [EditFormDetails(DetailType = typeof(PollAnswerLang), RelatedFieldName = "PollAnswerId")]
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select PollAnswer.Id as [PollAnswer.Id], PollAnswer.Answer as [PollAnswer.Answer], PollAnswer.Visible as [PollAnswer.Visible] from [PollAnswer] left join [PollQuestion] ON PollQuestion.Id = [PollAnswer].PollQuestionId", QueryOrderBy = "PollAnswer.Id")]
    public class PollAnswer : BaseEntity
    {
        private string answer;
        [ColumnDetail(IsNotNull=true)]
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        private int hit;
        [ColumnDetail(IsNotNull = true, DefaultValue="0")]
        public int Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        private int pollQuestionId;
        [ColumnDetail(IsNotNull = true, References = typeof(PollQuestion)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int PollQuestionId
        {
            get { return pollQuestionId; }
            set { pollQuestionId = value; }
        }
        private PollQuestion _pollQuestion;
        [XmlIgnore]
        public PollQuestion PollQuestion
        {
            get
            {
                if (_pollQuestion == null)
                    _pollQuestion = (PollQuestion)Provider.Database.Read(typeof(PollQuestion), this.pollQuestionId);
                return _pollQuestion;
            }
        }

        public override string GetNameValue()
        {
            return this.answer;
        }
        public override string GetNameColumn()
        {
            return "Answer";
        }
    }
}
