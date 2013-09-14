using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select PollAnswerLang.Id as [PollAnswerLang.Id], PollAnswerLang.Answer as [PollAnswerLang.Answer], Lang.Name as [Lang.Name], PollAnswerLang.Visible as [PollAnswerLang.Visible] from [PollAnswerLang] left join [Lang] ON Lang.Id = [PollAnswerLang].LangId", QueryOrderBy = "PollAnswerLang.Id desc")]
    public class PollAnswerLang : BaseEntity
    {
        private int pollAnswerId;
        [ColumnDetail(IsNotNull = true, References = typeof(PollAnswer)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int PollAnswerId
        {
            get { return pollAnswerId; }
            set { pollAnswerId = value; }
        }
        private PollAnswer _pollAnswer;
        [XmlIgnore]
        public PollAnswer PollAnswer
        {
            get
            {
                if (_pollAnswer == null)
                    _pollAnswer = (PollAnswer)Provider.Database.Read(typeof(PollAnswer), this.pollAnswerId);
                return _pollAnswer;
            }
        }

        private int langId;
        [ColumnDetail(IsNotNull = true, References = typeof(Lang))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }
        private Lang _lang;
        [XmlIgnore]
        public Lang Lang
        {
            get
            {
                if (_lang == null)
                    _lang = (Lang)Provider.Database.Read(typeof(Lang), this.langId);
                return _lang;
            }
        }

        private string answer;
        [ColumnDetail(IsNotNull = true)]
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
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
