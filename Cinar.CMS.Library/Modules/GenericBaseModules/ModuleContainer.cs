using System.Collections.Generic;

namespace Cinar.CMS.Library.Modules
{
    public abstract class ModuleContainer : Module
    {
        internal List<Module> _childModules;
        [System.Xml.Serialization.XmlIgnore()]
        public virtual List<Module> ChildModules
        {
            get
            {
                if (_childModules == null)
                {
                    _childModules = new List<Module>();
                    _childModules.AddRange(Module.Read(Provider.Database.GetDataTable("select * from Module where ParentModuleId={0} order by OrderNo", this.Id)));
                }
                return _childModules;
            }
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }

    public interface IRegionContainer { }
}
