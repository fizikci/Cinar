using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    public class Hit : BaseEntity
    {
        private int contentId = 0;
        [ColumnDetail(IsNotNull = true, References = typeof(Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentId
        {
            get
            {
                if (this.Id == 0) contentId = Provider.Content.Id;
                return contentId;
            }
            set { contentId = value; }
        }

        private string browser;
        public string Browser
        {
            get 
            {
                if (this.Id == 0) this.browser = Provider.Request.Browser.Browser;
                return browser; 
            }
            set { browser = value; }
        }

        private string platform;
        public string Platform
        {
            get 
            {
                if (this.Id == 0) this.platform = Provider.Request.Browser.Platform;
                return platform; 
            }
            set { platform = value; }
        }

        private string browserVersion;
        public string BrowserVersion
        {
            get 
            {
                if (this.Id == 0) this.browserVersion = Provider.Request.Browser.Version;
                return browserVersion; 
            }
            set { browserVersion = value; }
        }

        private string urlReferrer;
        [ColumnDetail(Length=200)]
        public string UrlReferrer
        {
            get
            {
                if (this.Id == 0) this.urlReferrer = Provider.Request.UrlReferrer == null ? "" : Provider.Request.UrlReferrer.ToString();
                return urlReferrer;
            }
            set {
                if (value != null && value.Length > 200)
                    urlReferrer = value.Substring(0, 200);
                else
                    urlReferrer = value; 
            }
        }

        private string url;
        [ColumnDetail(Length = 200)]
        public string Url
        {
            get
            {
                if (this.Id == 0) this.url = Provider.Request.Url == null ? "" : Provider.Request.Url.ToString();
                return url;
            }
            set {
                if (value != null && value.Length > 200)
                    url = value.Substring(0, 200);
                else
                    url = value;
            }
        }

    }
}
