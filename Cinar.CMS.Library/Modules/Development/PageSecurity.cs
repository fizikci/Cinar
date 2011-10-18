using System;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class PageSecurity : Module
    {
        private string roleToRead = "";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string RoleToRead
        {
            get { return roleToRead; }
            set { roleToRead = value; }
        }
        private string roleToChange = "Designer";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string RoleToChange
        {
            get { return roleToChange; }
            set { roleToChange = value; }
        }

        protected string redirectPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string RedirectPage
        {
            get { return redirectPage; }
            set { redirectPage = value; }
        }

        protected override string show()
        {
            if (Provider.DesignMode)
                return "<div style=\"background:crimson;color:white;font-weight:bold;margin:4px;padding:4px\">" +
                    Provider.GetResource("This page can be seen by <i>{0}</i> and modified by <i>{1}</i>", 
                    roleToRead == "" ? "herkes" : (roleToRead + " rolündeki kullanýcýlar "), 
                    roleToChange == "" ? "herkes" : (roleToChange + " rolündeki kullanýcýlar ")) + "</div>";
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
