using System;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library
{
    public class Configuration : BaseEntity
    {
        #region visual props
        private string siteName = "isimsiz.com";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo=1, Category = "Genel Ayarlar")]
        public string SiteName
        {
            get { return siteName; }
            set { siteName = value; }
        }

        private string siteAddress = "www.isimsiz.com";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 2, Category = "Genel Ayarlar")]
        public string SiteAddress
        {
            get { return siteAddress; }
            set { siteAddress = value; }
        }

        private string siteDescription = "Sitenin tanımı, bu site ne hakkındadır?";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 3, Category = "SEO Ayarları")]
        public string SiteDescription
        {
            get { return siteDescription; }
            set { siteDescription = value; }
        }

        private string siteKeywords = "sitenizle,ilgili,anahtar,kelimleri,böyle,yazınız";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 4, Category = "SEO Ayarları")]
        public string SiteKeywords
        {
            get { return siteKeywords; }
            set { siteKeywords = value; }
        }

        protected string siteLogo = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit, OrderNo = 5, Category = "Genel Ayarlar")]
        public string SiteLogo
        {
            get { return siteLogo; }
            set { siteLogo = value; }
        }

        protected string siteIcon = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit, OrderNo = 6, Category = "Genel Ayarlar")]
        public string SiteIcon
        {
            get { return siteIcon; }
            set { siteIcon = value; }
        }
        #endregion

        #region runtime props

        protected string useExternalLibrary = "Bootstrap + jQuery";
        [ColumnDetail(IsNotNull = true, Length = 30, DefaultValue = "False"), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:[['None','-'],['Bootstrap + jQuery','Bootstrap + jQuery'],['jQuery','jQuery']]", OrderNo = 7, Category = "Uygulama Ayarları")]
        public string UseExternalLibrary
        {
            get { return useExternalLibrary; }
            set { useExternalLibrary = value; }
        }

        protected int sessionTimeout = 120;
        [EditFormFieldProps(OrderNo = 7, Category = "Uygulama Ayarları")]
        public int SessionTimeout
        {
            get { return sessionTimeout; }
            set
            {
                if (value < 5) throw new Exception(Provider.GetResource("Session timeout cannot be less than 5 minutes!"));
                sessionTimeout = value;
            }
        }

        protected bool bufferOutput = true;
        [EditFormFieldProps(OrderNo = 8, Category = "Uygulama Ayarları")]
        public bool BufferOutput
        {
            get { return bufferOutput; }
            set { bufferOutput = value; }
        }

        protected bool multiLang = false;
        [EditFormFieldProps(OrderNo = 9, Category = "Uygulama Ayarları")]
        public bool MultiLang
        {
            get { return multiLang; }
            set { multiLang = value; }
        }

        private int defaultLang = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Lang))]
        [EditFormFieldProps(ControlType = ControlType.LookUp, OrderNo = 10, Category = "Uygulama Ayarları")]
        public int DefaultLang
        {
            get { return defaultLang; }
            set { defaultLang = value; }
        }

        [EditFormFieldProps(OrderNo = 11, Category = "Uygulama Ayarları")]
        public bool LogHit { get; set; }

        private string defaultStyleSheet = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(Visible = false)]
        public string DefaultStyleSheet
        {
            get { return defaultStyleSheet; }
            set { defaultStyleSheet = value; }
        }

        private string defaultJavascript = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(Visible = false)]
        public string DefaultJavascript
        {
            get { return defaultJavascript; }
            set { defaultJavascript = value; }
        }

        private string defaultPageLoadScript = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(Visible = false)]
        public string DefaultPageLoadScript
        {
            get { return defaultPageLoadScript; }
            set { defaultPageLoadScript = value; }
        }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(OrderNo = 11, Category = "Uygulama Ayarları")]
        public string Routes { get; set; }

        protected bool countTags = true;
        [EditFormFieldProps(OrderNo = 11, Category = "Uygulama Ayarları")]
        public bool CountTags
        {
            get { return countTags; }
            set { countTags = value; }
        }

        private string defaultDateFormat = "dd MMMM yyyy";
        [ColumnDetail(Length = 100), EditFormFieldProps(Options = "noHTML:true", OrderNo = 12, Category = "Uygulama Ayarları")]
        public string DefaultDateFormat
        {
            get { return defaultDateFormat; }
            set { defaultDateFormat = value; }
        }

        protected string noPicture = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit, OrderNo = 13, Category = "Uygulama Ayarları")]
        public string NoPicture
        {
            get { return noPicture; }
            set { noPicture = value; }
        }

        protected int thumbQuality = 90;
        [EditFormFieldProps(OrderNo = 14, Category = "Uygulama Ayarları")]
        public int ThumbQuality
        {
            get { return thumbQuality; }
            set { thumbQuality = value; }
        }

        protected int imageUploadMaxWidth = 960;
        [EditFormFieldProps(OrderNo = 15, Category = "Uygulama Ayarları")]
        public int ImageUploadMaxWidth
        {
            get { return imageUploadMaxWidth; }
            set { imageUploadMaxWidth = value; }
        }

        #endregion

        #region mail props
        private string authEmail = "info@isimsiz.com";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 16, Category = "Mail Ayarları")]
        public string AuthEmail
        {
            get { return authEmail; }
            set { authEmail = value; }
        }

        private string mailHost = "mail.isimsiz.com";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 17, Category = "Mail Ayarları")]
        public string MailHost
        {
            get { return mailHost; }
            set { mailHost = value; }
        }

        private int mailPort = 25;
        [EditFormFieldProps(OrderNo = 18, Category = "Mail Ayarları")]
        public int MailPort
        {
            get { return mailPort; }
            set { mailPort = value; }
        }

        private string mailUsername = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 19, Category = "Mail Ayarları")]
        public string MailUsername
        {
            get { return mailUsername; }
            set { mailUsername = value; }
        }

        private string mailPassword = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(Options = "noHTML:true", OrderNo = 20, Category = "Mail Ayarları")]
        public string MailPassword
        {
            get { return mailPassword; }
            set { mailPassword = value; }
        }
        #endregion

        #region cache props
        protected string useCache = "False";
        [ColumnDetail(IsNotNull = true, Length = 30, DefaultValue = "False"), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_USECACHEFORCONF_", OrderNo = 21, Category = "Önbellek Ayarları")]
        public string UseCache
        {
            get { return useCache; }
            set { useCache = value; }
        }

        protected int cacheLifeTime = 15;
        [EditFormFieldProps(OrderNo = 22, Category = "Önbellek Ayarları")]
        public int CacheLifeTime
        {
            get { return cacheLifeTime; }
            set { cacheLifeTime = value; }
        }
        #endregion

        #region special pages
        private string mainPage = "Default.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 23, Category = "Özel Sayfalar")]
        public string MainPage
        {
            get { return mainPage; }
            set { mainPage = value; }
        }

        private string categoryPage = "Category.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 24, Category = "Özel Sayfalar")]
        public string CategoryPage
        {
            get { return categoryPage; }
            set { categoryPage = value; }
        }

        private string contentPage = "Content.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 25, Category = "Özel Sayfalar")]
        public string ContentPage
        {
            get { return contentPage; }
            set { contentPage = value; }
        }

        private string loginPage = "Login.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 26, Category = "Özel Sayfalar")]
        public string LoginPage
        {
            get { return loginPage; }
            set { loginPage = value; }
        }

        private string membershipFormPage = "Membership.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 27, Category = "Özel Sayfalar")]
        public string MembershipFormPage
        {
            get { return membershipFormPage; }
            set { membershipFormPage = value; }
        }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 27, Category = "Özel Sayfalar")]
        public string AfterMembershipPage { get; set; }

        private string membershipProfilePage = "Profile.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 28, Category = "Özel Sayfalar")]
        public string MembershipProfilePage
        {
            get { return membershipProfilePage; }
            set { membershipProfilePage = value; }
        }

        private string rememberPasswordFormPage = "RememberPassword.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 29, Category = "Özel Sayfalar")]
        public string RememberPasswordFormPage
        {
            get { return rememberPasswordFormPage; }
            set { rememberPasswordFormPage = value; }
        }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 29, Category = "Özel Sayfalar")]
        public string AfterRememberPasswordPage { get; set; }

        private string userActivationPage = "Activation.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 30, Category = "Özel Sayfalar")]
        public string UserActivationPage
        {
            get { return userActivationPage; }
            set { userActivationPage = value; }
        }

        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 30, Category = "Özel Sayfalar")]
        public string AfterUserActivationPage { get; set; }

        private string adminPage = "Admin.aspx";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates", OrderNo = 31, Category = "Özel Sayfalar")]
        public string AdminPage
        {
            get { return adminPage; }
            set { adminPage = value; }
        }
        #endregion

        public new void Save()
        {
            // default dil değişmiş olabilir, set edelim
            Lang lang = Provider.Database.Read<Lang>(this.DefaultLang);
            if (lang != null)
                Provider.CurrentCulture = lang.Code;

            // routes'i sıfırla
            Provider.routes = null;

            // kaydedelim gari..
            base.Save();

            Provider.Configuration = null;
        }

        public static Configuration Read()
        {
            Configuration conf = Provider.Database.Read<Configuration>(1);
            if (conf == null)
            {
                conf = new Configuration();
                conf.DefaultStyleSheet = Properties.Resources.conf_default_css;
                conf.Save();
            }

            return conf;
        }
    }
}
