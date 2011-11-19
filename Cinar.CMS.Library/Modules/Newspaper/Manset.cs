using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class Manset : ContentListByFilter
    {
        public Manset() 
        {
            this.Cols = 1;
            this.HowManyItems = 10;
            this.FieldOrder = "title,image,description";
            this.DisplayAsTable = false;
        }

        protected int spotLength = 50;
        public int SpotLength
        {
            get { return spotLength; }
            set { spotLength = value; }
        }

        protected bool showLinksBelowPicture = false;
        public bool ShowLinksBelowPicture
        {
            get { return showLinksBelowPicture; }
            set { showLinksBelowPicture = value; }
        }

        protected bool withAnimation = false;
        public bool WithAnimation
        {
            get { return withAnimation; }
            set { withAnimation = value; }
        }

        private static string defaultMansetFieldOrder = "title,spot,image,description";
        private string mansetFieldOrder = Manset.defaultMansetFieldOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string MansetFieldOrder
        {
            get { return mansetFieldOrder; }
            set { mansetFieldOrder = value; }
        }

        protected int pictureWidthM = 0;
        public int PictureWidthM
        {
            get { return pictureWidthM; }
            set { pictureWidthM = value; }
        }

        protected int pictureHeightM = 0;
        public int PictureHeightM
        {
            get { return pictureHeightM; }
            set { pictureHeightM = value; }
        }
        public bool CropPictureM { get; set; }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            Entities.Content firstContent = null;
            if(this.contents!=null && this.contents.Length>0)
                firstContent = (Entities.Content)this.contents[0];

            if (firstContent == null)
                return "";

            Hashtable fields = new Hashtable();
            fields["title"] = String.Format("<tr><td colspan=\"2\" id=\"{0}_{1}_title\" class=\"mansetTitle\">{2}</td></tr>\n", this.Name, this.Id, Utility.StrCrop(firstContent.Title, this.titleLength));
            fields["spot"] = String.Format("<tr><td colspan=\"2\" id=\"{0}_{1}_sTitle\" class=\"mansetSTitle\">{2}</td></tr>\n", this.Name, this.Id, Utility.StrCrop(firstContent.SpotTitle, this.titleLength));
            fields["image"] =
                "<tr>\n\t<td align=\"center\" valign=\"middle\" width=\"1%\">"
                + Provider.GetThumbImgHTML(firstContent.Picture, pictureWidthM, pictureHeightM, null, "mansetImage", "id=\"" + this.Name + "_" + this.Id + "_pic\"", CropPictureM)
                + "</td>\n" + (this.showLinksBelowPicture ? "</tr><tr>" : "") + "\t<td width=\"99%\">"
                + base.show() + "\n"
                + "</td></tr>\n";
            fields["description"] = String.Format("<tr><td colspan=\"2\"><div id=\"{0}_{1}_desc\" class=\"mansetDesc\">{2}</div></td></tr>\n", this.Name, this.Id, Utility.StrCrop(firstContent.Description, this.descriptionLength));

            sb.AppendFormat("<table{0} width=\"100%\" border=\"0\">\n", this.withAnimation ? " onmouseover=\"showManset(event, "+this.Id+")\"" : "");
            foreach (string fieldName in this.MansetFieldOrder.Split(','))
                if(fields.ContainsKey(fieldName))
                    sb.Append(fields[fieldName]);
            sb.Append("</table>\n");

            return sb.ToString();
        }
        protected override string getTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("title") && !this.ShowTitle)
                return String.Format("<div class=\"clTitle\" style=\"display:none\">{0}</div>", Utility.StrCrop(content.Title, this.titleLength));
            else
                return base.getTitleHTML(isFirstItem, template, content, row, col);
        }
        protected override string getSpotTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("spot") && !this.ShowSpotTitle)
                return String.Format("<div class=\"clSTitle\" style=\"display:none\">{0}</div>", Utility.StrCrop(content.SpotTitle, this.titleLength));
            else
                return base.getSpotTitleHTML(isFirstItem, template, content, row, col);
        }
        protected override string getImgHTML(string template, Content content, int row, int col)
        {
            return String.Format("<div style=\"display:none\" class=\"picUrl\">{0}</div>", Provider.GetThumbPath(content.Picture, pictureWidthM, pictureHeightM, CropPictureM))
                + base.getImgHTML(template, content, row, col);
        }
        protected override string getDescriptionHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            string desc = Utility.StrCrop(content.Description, this.descriptionLength);
            if (this.mansetFieldOrder.Contains("description") && !this.ShowDescription)
                return String.Format("<div class=\"clDesc\" style=\"display:none\">{0}</div>", desc);
            else
                return base.getDescriptionHTML(isFirstItem, template, content, row, col);
        }

        protected override IDatabaseEntity[] GetContentList()
        {
            // bunu kaldýrdým çünkü IsManset=1 þartý filtre ile belirtilebilir.
            //this.Filter += (this.Filter == "" ? "" : " AND ") + "IsManset=1";
            return base.GetContentList();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} td.mansetTitle {{font-size:20px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} img.mansetImage {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.mansetDesc {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);
            if (!this.MansetFieldOrder.Contains("image"))
                throw new Exception(Provider.GetResource("\"image\" is essential for the Field Order of the Headline module."));

            if (Regex.Match(this.mansetFieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", Manset.defaultMansetFieldOrder));
            foreach (string fieldName in this.mansetFieldOrder.Split(','))
                if (Manset.defaultMansetFieldOrder.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, Manset.defaultMansetFieldOrder));
        }
    }
}
