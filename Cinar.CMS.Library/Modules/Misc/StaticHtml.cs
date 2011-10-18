using System;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class StaticHtml : Module
    {
        private string innerHtml = "";
        [ColumnDetail(ColumnType = DbType.Text)]
        public string InnerHtml
        {
            get { return innerHtml; }
            set { innerHtml = value; }
        }

        private int langId1 = 0;
        [ColumnDetail(References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId1
        {
            get { return langId1; }
            set { langId1 = value; }
        }

        private string innerHtml1 = "";
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps()]
        public string InnerHtml1
        {
            get { return innerHtml1; }
            set { innerHtml1 = value; }
        }

        private int langId2 = 0;
        [ColumnDetail(References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId2
        {
            get { return langId2; }
            set { langId2 = value; }
        }

        private string innerHtml2 = "";
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps()]
        public string InnerHtml2
        {
            get { return innerHtml2; }
            set { innerHtml2 = value; }
        }

        private int langId3 = 0;
        [ColumnDetail(References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId3
        {
            get { return langId3; }
            set { langId3 = value; }
        }

        private string innerHtml3 = "";
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps()]
        public string InnerHtml3
        {
            get { return innerHtml3; }
            set { innerHtml3 = value; }
        }
        
        protected override string show()
        {
            string html = "";

            // dili tesbit edelüm
            if (Provider.CurrentLanguage.Id == Provider.Configuration.DefaultLang)
                html = this.innerHtml;
            else if (Provider.CurrentLanguage.Id == langId1)
                html = this.innerHtml1;
            else if (Provider.CurrentLanguage.Id == langId2)
                html = this.innerHtml2;
            else if (Provider.CurrentLanguage.Id == langId3)
                html = this.innerHtml3;

            if (String.IsNullOrEmpty(html)) html = this.innerHtml;

            Interpreter engine = Provider.GetInterpreter(html, this);
            engine.Parse();
            engine.Execute();
            html = engine.Output;

            return html;
        }
    }
}
