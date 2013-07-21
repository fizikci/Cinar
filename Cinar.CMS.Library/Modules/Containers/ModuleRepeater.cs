using System;
using System.Collections.Generic;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class ModuleRepeater : Module
    {
        public int ModuleId { get; set; }

        protected override string show()
        {
            if (Provider.DesignMode && ModuleId==0)
                return Provider.GetResource("Enter the module id to be copied");

            return Module.Read(ModuleId).Show();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (ModuleId > 0 && Module.Read(ModuleId) == null)
                throw new Exception(Provider.GetResource("There is no such module with the id " + ModuleId));
        }
    }
}
