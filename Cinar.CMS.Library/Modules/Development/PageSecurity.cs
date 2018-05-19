using System;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class PageSecurity : Module
    {
        public PageSecurity()
        {
            RedirectPage = "";
            RoleToChange = "Designer";
            RoleToRead = "";
        }

        [EditFormFieldProps(Options = "noHTML:true")]
        public string RoleToChange { get; set; }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string RedirectPage { get; set; }

        internal override string show()
        {
            if (Provider.DesignMode)
                return "<div style=\"background:crimson;color:white;font-weight:bold;margin:4px;padding:4px\">" +
                    Provider.GetResource("This page can be seen by <i>{0}</i> and modified by <i>{1}</i>", 
                    RoleToRead == "" ? "everyone" : RoleToRead, 
                    RoleToChange == "" ? "everyone" : RoleToChange) + "</div>";
            else
                return String.Empty;
        }

        public override string GetDefaultCSS()
        {
            return base.GetDefaultCSS();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
