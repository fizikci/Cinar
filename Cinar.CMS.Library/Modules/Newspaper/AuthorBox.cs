using System;
using System.Text;
using System.Xml.Serialization;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class AuthorBox : StaticHtml
    {
        public AuthorBox()
        {
            this.InnerHtml = @"
$this.PictureThumb$
<div class=""name"">$Context.Content.Author.Name$</div>
<div class=""desc"">$Context.Content.Author.Description$</div>
$this.AddToFavoritesLink$
";
        }

        protected int pictureWidth = 0;
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

        private string _addToFavoritesLink;
        [XmlIgnore]
        public string AddToFavoritesLink
        {
            get {
                if (_addToFavoritesLink == null)
                {
                    if (Provider.User.IsAnonim())
                        _addToFavoritesLink = ""; //***
                    else
                    {
                        UserPrefferedAuthor upa = (UserPrefferedAuthor)Provider.Database.Read(typeof(UserPrefferedAuthor), "UserId={0} AND AuthorId={1}", Provider.User.Id, Provider.Content.AuthorId);
                        if (upa == null)
                            _addToFavoritesLink = @"<a class=""fav"" href=""#"" onclick=""addToFavorites()"">Favori yazarlarıma ekle</a>";
                        else
                            _addToFavoritesLink = @"<a class=""fav"" href=""#"" onclick=""removeFromFavorites()"">Favori yazarlarımdan çıkar</a>";
                    }
                }
                return _addToFavoritesLink;
            }
        }

        private string _pictureThumb;
        [XmlIgnore]
        public string PictureThumb
        {
            get
            {
                if (_pictureThumb == null)
                {
                    if (String.IsNullOrEmpty(Provider.Content.Author.Picture))
                        _pictureThumb = Provider.GetResource("There is no related picture");
                    else
                        _pictureThumb = Provider.GetThumbImgHTML(Provider.Content.Author.Picture, this.pictureWidth, this.pictureHeight, Provider.Content.Author.Name, null, null, CropPicture);
                }
                return _pictureThumb;
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} img {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.name {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.desc {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.fav {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string AddToFavorites() 
        {
            UserPrefferedAuthor upa = (UserPrefferedAuthor)Provider.Database.Read(typeof(UserPrefferedAuthor), "UserId={0} AND AuthorId={1}", Provider.User.Id, Provider.Content.AuthorId);
            if (upa == null)
            {
                upa = new UserPrefferedAuthor();
                upa.AuthorId = Provider.Content.AuthorId;
                upa.UserId = Provider.User.Id;
                upa.Save();

                return Provider.GetResource("{0} is added to your Favorites authors.", Provider.Content.Author.Name);
            }
            else
            {
                upa.Delete();

                return Provider.GetResource("{0} is removed from your Favorites authors.", Provider.Content.Author.Name);
            }
        }
    }

}
