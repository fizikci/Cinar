using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select PollQuestionLang.Id as [PollQuestionLang.Id], PollQuestionLang.Question as [PollQuestionLang.Question], Lang.Name as [Lang.Name], PollQuestionLang.Visible as [PollQuestionLang.Visible] from [PollQuestionLang] left join [Lang] ON Lang.Id = [PollQuestionLang].LangId", QueryOrderBy = "PollQuestionLang.Id desc")]
    public class PollQuestionLang : BaseEntity
    {
        private int pollQuestionId;
        [ColumnDetail(IsNotNull = true, References = typeof(PollQuestion)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int PollQuestionId
        {
            get { return pollQuestionId; }
            set { pollQuestionId = value; }
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
    }
}
