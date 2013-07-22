using System;
using System.Text;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class ImageGallery : Module
    {
        private static string defaultFieldOrder = "paging,picture,title,description";
        private string fieldOrder = ImageGallery.defaultFieldOrder;
        [EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string FieldOrder
        {
            get { return fieldOrder; }
            set { fieldOrder = value; }
        }

        private string pagingSeperator = " | ";
        public string PagingSeperator
        {
            get { return pagingSeperator; }
            set { pagingSeperator = value; }
        }

        private int pictureWidth = 0;
        public int PictureWidth
        {
            get { return pictureWidth; }
            set { pictureWidth = value; }
        }

        protected int pictureHeight = 0;
        public int PictureHeight
        {
            get { return pictureHeight; }
            set { pictureHeight = value; }
        }

        public bool CropPicture { get; set; }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            Entities.Content content = Provider.Content;

            if (content == null)
            {
                if(Provider.DesignMode)
                    sb.Append(Provider.GetResource("There is no picture to show because there is no related content"));
                return sb.ToString(); //***
            }

            // resimleri alalım.
            IDatabaseEntity[] pics = Provider.Database.ReadList(typeof(Entities.ContentPicture), "select * from ContentPicture where ContentId={0} order by OrderNo, Id", content.Id);
            Provider.Translate(pics);

            if (pics.Length == 0)
            {
                if (Provider.DesignMode)
                    sb.Append(Provider.GetResource("There is no picture added for this content (gallery)"));
                return sb.ToString(); //***
            }
            
            StringBuilder sbPaging = new StringBuilder();
            if (String.IsNullOrEmpty(this.pagingSeperator)) this.pagingSeperator = " | ";
            for (int i = 0; i < pics.Length; i++)
            {
                sbPaging.AppendFormat("<a id=\"pg{0}_{1}\"{3} href=\"javascript:showImage{0}({1})\">{2}</a>", this.Id, i, i + 1, i == 0 ? " class=\"sel\"" : "");
                if (i < pics.Length - 1)
                    sbPaging.Append(this.pagingSeperator);
            }

            sb.AppendFormat("<script type=\"text/javascript\">\n");
            sb.AppendFormat("var currImg{0} = 0;\n", this.Id);
            sb.AppendFormat("var imgGal{0} = [\n", this.Id);
            foreach (Entities.ContentPicture pic in pics)
                sb.AppendFormat("\t{{fileName:{0}, title:{1}, desc:{2}}},\n", pic.FileName.ToJS(), pic.Title.ToJS(), pic.Description.ToJS());
            sb.Remove(sb.Length - 2, 2);
            sb.Append("\n];\n");
            sb.AppendFormat("var defTitle{0} = {1};\n", this.Id, content.Title.ToJS());
            sb.AppendFormat("var defDesc{0} = {1};\n", this.Id, content.Description.ToJS());
            sb.AppendFormat("function showImage{0}(i){{\n", this.Id);
            sb.AppendFormat("\tif(i==imgGal{0}.length) i = 0;\n", this.Id);
            sb.AppendFormat("\t$('pg{0}_'+i).addClassName('sel');\n", this.Id);
            sb.AppendFormat("\t$('pg{0}_'+currImg{0}).removeClassName('sel');\n", this.Id);
            sb.AppendFormat("\tvar pic = imgGal{0}[i];\n", this.Id);
            sb.AppendFormat("\t$('imgGalPic{0}').src = pic.fileName;\n", this.Id);
            sb.AppendFormat("\t$('imgGalTit{0}').innerHTML = pic.title?pic.title:defTitle{0};\n", this.Id);
            sb.AppendFormat("\t$('imgGalDesc{0}').innerHTML = pic.desc?pic.desc:defDesc{0};\n", this.Id);
            sb.AppendFormat("\tcurrImg{0} = i;\n", this.Id);
            sb.AppendFormat("}}\n");
            sb.AppendFormat("</script>\n");

            Hashtable fields = new Hashtable();
            Entities.ContentPicture pic0 = (Entities.ContentPicture)pics[0];
            fields["paging"] = "<div class=\"paging\">" + sbPaging + "</div>\n";
            fields["picture"] = String.Format("<img id=\"imgGalPic{1}\" src=\"{0}\" onclick=\"showImage{1}(currImg{1}+1)\"{2}{3}/>\n", pic0.FileName, this.Id, this.pictureWidth > 0 ? " width=\"" + this.pictureWidth + "\"" : "", this.pictureHeight > 0 ? " height=\"" + this.pictureHeight + "\"" : "");
            fields["title"] = String.Format("<div id=\"imgGalTit{0}\" class=\"title\">{1}</div>\n", this.Id, String.IsNullOrEmpty(pic0.Title) ? content.Title : pic0.Title);
            fields["description"] = String.Format("<div id=\"imgGalDesc{0}\" class=\"desc\">{1}</div>\n", this.Id, String.IsNullOrEmpty(pic0.Description) ? content.Description : pic0.Description);

            foreach (string fieldName in this.fieldOrder.Split(','))
            {
                if (fields.ContainsKey(fieldName))
                    sb.Append(fields[fieldName]);
            }

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.paging {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging a {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging a.sel {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} img {{cursor:pointer}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.title {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.desc {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (Regex.Match(this.fieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", ImageGallery.defaultFieldOrder));
            foreach (string fieldName in this.fieldOrder.Split(','))
                if (ImageGallery.defaultFieldOrder.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, ImageGallery.defaultFieldOrder));
        }
    }

}
