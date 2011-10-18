using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select PollAnswerLang.Id, PollAnswerLang.Answer as [PollAnswerLang.Answer], TLangId.Name as [Lang], PollAnswerLang.Visible as [BaseEntity.Visible] from [PollAnswerLang] left join [Lang] as TLangId ON TLangId.Id = [PollAnswerLang].LangId")]
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
