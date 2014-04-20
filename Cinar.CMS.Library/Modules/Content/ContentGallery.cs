using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentGallery : TableView
    {
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

        private bool showTitle = true;
        public bool ShowTitle
        {
            get { return showTitle; }
            set { showTitle = value; }
        }

        private bool showTitleFirst = false;
        public bool ShowTitleFirst
        {
            get { return showTitleFirst; }
            set { showTitleFirst = value; }
        }

        private IDatabaseEntity[] _pictures;
        private IDatabaseEntity[] pictures
        {
            get
            {
                if (_pictures == null)
                {
                    Entities.Content content = Provider.Content;
                    if (content == null)
                        throw new Exception(Provider.GetResource("There is no picture to show because there is no related content"));
                    _pictures = Provider.Database.ReadList(typeof(Entities.ContentPicture), "select * from ContentPicture where ContentId={0} order by OrderNo", content.Id);
                    if (_pictures == null)
                        _pictures = new IDatabaseEntity[0];
                    Provider.Translate(_pictures);
                }
                return _pictures;
            }
        }

        protected override int rowCount
        {
            get { return (int)Math.Ceiling((double)pictures.Length / (double)this.cols); }
        }

        protected override string getCellHTML(int row, int col)
        {
            StringBuilder sb = new StringBuilder();

            int index = row * this.cols + col;
            if (pictures.Length <= index)
                return String.Empty;

            Entities.ContentPicture contentPic = (Entities.ContentPicture)pictures[index];

            if (this.showTitle && this.showTitleFirst)
                sb.AppendFormat("<div class=\"picTitle\">{0}</div>", contentPic.Title);
            sb.AppendFormat("<a href=\"{0}\" rel=\"lightbox[{1}]\" title=\"{2}\">", contentPic.FileName,
                "gal" + this.Id,
                contentPic.Title.Replace("\"", "\\\""));
            sb.Append(Provider.GetThumbImgHTML(contentPic.FileName, this.pictureWidth, this.pictureHeight, "", null, null, CropPicture));
            sb.Append("</a>");

            if (this.showTitle && !this.showTitleFirst)
                sb.AppendFormat("<div class=\"picTitle\">{0}</div>", contentPic.Title);

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} img {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.picTitle {{font-weight:bold}}\n", getCSSId());
            return sb.ToString();
        }

    }

}
