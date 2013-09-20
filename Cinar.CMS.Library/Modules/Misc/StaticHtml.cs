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

        internal override string show()
        {
            Interpreter engine = Provider.GetInterpreter(this.innerHtml, this);
            engine.Parse();
            engine.Execute();

            return engine.Output;
        }
    }
}
