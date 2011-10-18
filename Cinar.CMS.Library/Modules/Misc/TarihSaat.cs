using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class TarihSaat : Module
    {
        private string format = "D";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        protected override string show()
        {
            return DateTime.Now.ToString(this.format);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{padding:4px;margin:4px;background:yellow;border:1px solid red;text-align:center}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
