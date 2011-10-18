using System;
using System.Collections.Generic;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class RegionRepeater : ModuleContainer
    {
        private string page;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string Page
        {
            get { return page; }
            set { page = value; }
        }

        private string regionToRepeat;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.regionNames")]
        public string RegionToCopy
        {
            get { return regionToRepeat; }
            set { regionToRepeat = value; }
        }

        protected override string show()
        {
            if (Provider.DesignMode && (String.IsNullOrEmpty(this.page) || String.IsNullOrEmpty(this.regionToRepeat)))
                return Provider.GetResource("Select a page and a region");
            else if(Provider.DesignMode)
                return Provider.GetRegionInnerHtml(this.ChildModules, false);
            else
                return Provider.GetRegionInnerHtml(this.ChildModules);
        }

        [System.Xml.Serialization.XmlIgnore()]
        public override List<Module> ChildModules
        {
            get
            {
                if (_childModules == null)
                {
                    _childModules = new List<Module>();
                    _childModules.AddRange(Module.Read(Provider.Database.GetDataTable("select * from Module where Template={0} and Region={1} and ParentModuleId=0 order by OrderNo", this.page, this.regionToRepeat)));
                }
                return _childModules;
            }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (this.page == this.Template && this.regionToRepeat == this.Region)
                throw new Exception(Provider.GetResource("Same region of same page cannot be copied"));

            if (this.ChildModules.Exists(delegate(Module mdl) { return mdl is RegionRepeater; }))
                throw new Exception(Provider.GetResource("There mustn't be a Region Repeater in the region selected"));
        }
    }
}
