using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class AutoContentListByFilter : ContentListByFilter
    {
        private int contentSourceId;
        [ColumnDetail(IsNotNull = true, References = typeof(ContentSource)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentSourceId
        {
            get { return contentSourceId; }
            set { contentSourceId = value; }
        }

        protected bool showOnlyAutoContent = true;
        public bool ShowOnlyAutoContent
        {
            get { return showOnlyAutoContent; }
            set { showOnlyAutoContent = value; }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (this.contentSourceId == 0)
                return Provider.GetResource("Select the content source");

            ContentSource contentSource = (ContentSource)Provider.Database.Read(typeof(ContentSource), this.contentSourceId);
            if (contentSource == null)
                return Provider.GetResource("The content source couldn't be found. It may be deleted.");

            try
            {
                Provider.FetchAutoContent(contentSource);
            }
            catch (Exception ex)
            {
                sb.Append(Provider.GetResource("There was an error while reading auto content") + "<br/>" + ex.Message);
            }

            if (this.showOnlyAutoContent)
                this.Filter += (String.IsNullOrEmpty(this.Filter) ? "" : " AND ") + "ContentSourceId="+this.contentSourceId;

            sb.Append(base.show());

            return sb.ToString();
        }

    }

}
