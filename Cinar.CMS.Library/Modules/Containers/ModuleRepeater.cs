using System;
using System.Collections.Generic;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class ModuleRepeater : Module
    {
        public int ModuleId { get; set; }

        internal override string show()
        {
            if (ModuleId == 0)
            {
                if (Provider.DesignMode)
                    return Provider.GetResource("Enter the module id to be repeated here");
                else
                    return "";
            }

            return Module.Read(ModuleId).show();
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (ModuleId > 0)
            {
                var m = Module.Read(ModuleId);
                if (m == null)
                    throw new Exception(Provider.GetResource("There is no such module with the id " + ModuleId));

                if (!this.ModuleId.Equals(this.GetOriginalValues()["ModuleId"]))
                {
                    this.BottomHtml = m.BottomHtml;
                    this.CSSClass = m.CSSClass;
                    this.TopHtml = m.TopHtml;
                }
            }
        }

        public override string GetDefaultCSS()
        {
            var m = Module.Read(ModuleId);

            if (ModuleId == 0)
                return "";

            string css = m.CSS;
            if (string.IsNullOrWhiteSpace(css))
                css = m.GetDefaultCSS();

            string mId = String.Format("#{0}_{1}", m.Name, m.Id);
            string thisId = String.Format("#{0}_{1}", this.Name, this.Id);

            return css.Replace(mId, thisId);
        }
    }
}
