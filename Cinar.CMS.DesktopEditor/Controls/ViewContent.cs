﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cinar.Database;
using System.Net;
using System.Collections.Specialized;
using Krystalware.UploadHelper;

namespace Cinar.CMS.DesktopEditor.Controls
{
    public partial class ViewContent : UserControl
    {
        public ViewContent()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            setPlaceHolder(editTitle, "Başlık");
            setPlaceHolder(editCategoryId, "Kategori");
            setPlaceHolder(editSpotTitle, "Spot Başlık");
            setPlaceHolder(editDescription, "Açıklama");
            setPlaceHolder(editMetin, "Metin");
            setPlaceHolder(editAuthor, "Yazar");
            setPlaceHolder(editTags, "Etiketler");

            loadCategories(true);
            loadAuthors(true);
        }

        private void setPlaceHolder(Control ctrl, string placeHolder)
        {
            ctrl.Text = placeHolder;
            ctrl.ForeColor = Color.Gray;
            ctrl.TextChanged += (sender, args) => {
                if (ctrl.Text == placeHolder)
                    ctrl.ForeColor = Color.Gray;
                else
                    ctrl.ForeColor = Color.Black;

                if (ctrl.Text == " ")
                {
                    ctrl.Text = placeHolder;
                    ctrl.ForeColor = Color.Gray;
                }
            };
            ctrl.GotFocus += (sender, args) =>
            {
                if (ctrl.Text == placeHolder)
                {
                    ctrl.Text = "";
                    ctrl.ForeColor = Color.Black;
                }
            };
            ctrl.LostFocus += (sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(ctrl.Text))
                {
                    ctrl.Text = placeHolder;
                    ctrl.ForeColor = Color.Gray;
                }
            };
        }

        public int Index { get; set; }

        private void loadCategories(bool silent)
        {
            editCategoryId.Items.Clear();

            Database.Database db = Provider.GetDb(Index);
            if (db == null)
            {
                if (!silent)
                    MessageBox.Show((Index + 1) + ". kutu için veritabanı ayarları geçersiz.", "Çınar CMS Desktop Editor");
                return;
            }

            foreach (DataRow dr in db.GetDataTable("select Id, Title from Content where ClassName='Category'").Rows)
                editCategoryId.Items.Add(new Item(dr));
        }
        private void loadAuthors(bool silent)
        {
            editAuthor.Items.Clear();

            Database.Database db = Provider.GetDb(Index);
            if (db == null)
            {
                if (!silent)
                    MessageBox.Show((Index + 1) + ". kutu için veritabanı ayarları geçersiz.", "Çınar CMS Desktop Editor");
                return;
            }

            foreach (DataRow dr in db.GetDataTable("select Id, Name as Title from Author").Rows)
                editAuthor.Items.Add(new Item(dr));
        }

        public bool ShowCopyButton {
            get { return btnCopy.Visible; }
            set { btnCopy.Visible = value; } 
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            NameValueCollection postData = getContentDataAsNameValue();
            if (CurrentContentId == 0)
                postData["Metin"] = postData["Metin"].Replace("\r", "").Replace("\n", "<br/>\r\n");

            if (PictureChanged)
            {
                var files = new UploadFile[1];
                files[0] = new UploadFile(editPicture.Tag.ToString(), "Picture", "image/jpg");

                HttpUploadHelper.Upload(Settings.Load().SiteAddress[Index].Trim('/') + "/UploadContent.ashx", files, postData);
            }
            else
            {
                HttpUploadHelper.Upload(Settings.Load().SiteAddress[Index].Trim('/') + "/UploadContent.ashx", new UploadFile[0], postData);
            }
        }

        private NameValueCollection getContentDataAsNameValue()
        {
            NameValueCollection postData = new NameValueCollection();
            postData.Add("Id", CurrentContentId.ToString());
            postData.Add("ClassName", "Content");
            postData.Add("Title", editTitle.Text);
            if (editCategoryId.SelectedItem != null)
                postData.Add("CategoryId", ((Item)editCategoryId.SelectedItem).Id.ToString());
            postData.Add("Description", editDescription.Text);
            postData.Add("Tags", editTags.Text);
            postData.Add("PublishDate", editPublishDate.Value.ToString("dd.MM.yyyy"));
            postData.Add("Metin", editMetin.Text);
            postData.Add("IsManset", editIsManset.Checked ? "1" : "0");

            if (editAuthor.SelectedItem != null)
                postData.Add("AuthorId", ((Item)editAuthor.SelectedItem).Id.ToString());
            return postData;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings f = new FormSettings(Index);

            if (f.ShowDialog() == DialogResult.OK)
            {
                Settings s = Settings.Load();
                s.SiteAddress[Index] = f.SiteAddress;
                s.Providers[Index] = f.ConnectionProvider;
                s.ConnectionStrings[Index] = f.ConnectingString;
                s.Save();

                loadCategories(false);
            }
        }

        private void editPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Resim Seç";
                dlg.Filter = "Resim Dosyaları | *.jpg; *.jpeg; *.jpe; *.png; *.gif";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureBox picture = sender as PictureBox;
                    picture.Image = new Bitmap(dlg.FileName);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.BackgroundImage = null;
                    picture.Refresh();

                    editPicture.Tag = dlg.FileName;

                    PictureChanged = true;
                }
            }
        }

        private void btnSelectContent_Click(object sender, EventArgs e)
        {
            FormSelectContent f = new FormSelectContent(Index);

            if (f.ShowDialog() == DialogResult.OK)
            {
                DataRow dr = Provider.GetDb(Index).GetDataRow("select * from Content where Id={0}", f.SelectedContentId);
                PictureChanged = false;

                this.CurrentContentId = f.SelectedContentId;

                editSpotTitle.Text = dr["SpotTitle"].ToString();
                editTitle.Text = dr["Title"].ToString();
                editDescription.Text = dr["Description"].ToString();
                editMetin.Text = dr["Metin"].ToString();
                editIsManset.Checked = Convert.ToBoolean(dr["IsManset"]);
                editCategoryId.SelectedIndex = editCategoryId.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(dr["CategoryId"]));
                editAuthor.SelectedIndex = editAuthor.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(dr["AuthorId"]));
                editTags.Text = dr["Tags"].ToString();
                editPublishDate.Value = (DateTime)dr["PublishDate"];

                editPicture.ImageLocation = Settings.Load().SiteAddress[Index].Trim('/') + dr["Picture"];
                editPicture.Refresh();
            }
        }

        public int CurrentContentId { get; set; }
        public bool PictureChanged { get; set; }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            this.CurrentContentId = 0;

            editSpotTitle.Text = " ";
            editTitle.Text = " ";
            editDescription.Text = " ";
            editMetin.Text = " ";
            editIsManset.Checked = false;
            editCategoryId.Text = " ";
            editAuthor.Text = " ";
            editTags.Text = " ";
            editPublishDate.Value = DateTime.Now.Date;

            editPicture.Image = Properties.Resources.image_add;
            editPicture.Refresh();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Provider.CopiedContent = getContentDataAsNameValue();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Provider.CopiedContent == null) {
                MessageBox.Show("Önce bir kutuda kopyala butonuna tıklayınız");
                return;
            }

            editSpotTitle.Text = Provider.CopiedContent["SpotTitle"];
            editTitle.Text = Provider.CopiedContent["Title"];
            editDescription.Text = Provider.CopiedContent["Description"];
            editMetin.Text = Provider.CopiedContent["Metin"];
            editIsManset.Checked = Provider.CopiedContent["IsManset"] == "1";
            editTags.Text = Provider.CopiedContent["Tags"];
            editPublishDate.Value = DateTime.Parse(Provider.CopiedContent["PublishDate"]);
        }
    }

    // Content item for the combo box
    public class Item
    {
        public string Title;
        public int Id;
        public Item(DataRow dr)
        {
            Title = dr["Title"].ToString(); 
            Id = (int)dr["Id"];
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
