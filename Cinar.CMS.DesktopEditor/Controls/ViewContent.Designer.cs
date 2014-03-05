namespace Cinar.CMS.DesktopEditor.Controls
{
    partial class ViewContent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSelectContent = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.PictureBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.editPicture = new System.Windows.Forms.PictureBox();
            this.editIsManset = new System.Windows.Forms.CheckBox();
            this.editDescription = new System.Windows.Forms.RichTextBox();
            this.editSpotTitle = new System.Windows.Forms.TextBox();
            this.editTitle = new System.Windows.Forms.TextBox();
            this.editMetin = new System.Windows.Forms.RichTextBox();
            this.editCategoryId = new System.Windows.Forms.ComboBox();
            this.editPublishDate = new System.Windows.Forms.DateTimePicker();
            this.editTags = new System.Windows.Forms.RichTextBox();
            this.editAuthor = new System.Windows.Forms.ComboBox();
            this.btnYeni = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(488, 349);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(65, 23);
            this.btnCopy.TabIndex = 21;
            this.btnCopy.Text = "Kopyala";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSelectContent
            // 
            this.btnSelectContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectContent.Location = new System.Drawing.Point(439, 42);
            this.btnSelectContent.Name = "btnSelectContent";
            this.btnSelectContent.Size = new System.Drawing.Size(32, 20);
            this.btnSelectContent.TabIndex = 20;
            this.btnSelectContent.Text = "...";
            this.btnSelectContent.UseVisualStyleBackColor = true;
            this.btnSelectContent.Click += new System.EventHandler(this.btnSelectContent_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.BackgroundImage = Properties.Resources.system_config_services;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Location = new System.Drawing.Point(596, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(26, 28);
            this.btnSettings.TabIndex = 19;
            this.btnSettings.TabStop = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKaydet.Location = new System.Drawing.Point(178, 349);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(173, 23);
            this.btnKaydet.TabIndex = 18;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // editPicture
            // 
            this.editPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.editPicture.Image = Properties.Resources.image_add;
            this.editPicture.InitialImage = null;
            this.editPicture.Location = new System.Drawing.Point(477, 42);
            this.editPicture.Name = "editPicture";
            this.editPicture.Size = new System.Drawing.Size(145, 114);
            this.editPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.editPicture.TabIndex = 15;
            this.editPicture.TabStop = false;
            this.editPicture.Click += new System.EventHandler(this.editPicture_Click);
            // 
            // editIsManset
            // 
            this.editIsManset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editIsManset.AutoSize = true;
            this.editIsManset.Location = new System.Drawing.Point(477, 16);
            this.editIsManset.Name = "editIsManset";
            this.editIsManset.Size = new System.Drawing.Size(91, 17);
            this.editIsManset.TabIndex = 17;
            this.editIsManset.Text = "Manşet Olsun";
            this.editIsManset.UseVisualStyleBackColor = true;
            // 
            // editDescription
            // 
            this.editDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editDescription.Location = new System.Drawing.Point(8, 94);
            this.editDescription.Name = "editDescription";
            this.editDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.editDescription.Size = new System.Drawing.Size(463, 62);
            this.editDescription.TabIndex = 14;
            this.editDescription.Text = "Kısa Açıklama";
            // 
            // editSpotTitle
            // 
            this.editSpotTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editSpotTitle.Location = new System.Drawing.Point(8, 68);
            this.editSpotTitle.Name = "editSpotTitle";
            this.editSpotTitle.Size = new System.Drawing.Size(300, 20);
            this.editSpotTitle.TabIndex = 13;
            this.editSpotTitle.Text = "Spot Başlık";
            // 
            // editTitle
            // 
            this.editTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTitle.Location = new System.Drawing.Point(8, 42);
            this.editTitle.Name = "editTitle";
            this.editTitle.Size = new System.Drawing.Size(429, 20);
            this.editTitle.TabIndex = 12;
            this.editTitle.Text = "Başlık";
            // 
            // editMetin
            // 
            this.editMetin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editMetin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editMetin.Location = new System.Drawing.Point(9, 162);
            this.editMetin.Name = "editMetin";
            this.editMetin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.editMetin.Size = new System.Drawing.Size(613, 141);
            this.editMetin.TabIndex = 16;
            this.editMetin.Text = "Haber Detayı";
            // 
            // editCategoryId
            // 
            this.editCategoryId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editCategoryId.FormattingEnabled = true;
            this.editCategoryId.Location = new System.Drawing.Point(8, 14);
            this.editCategoryId.Name = "editCategoryId";
            this.editCategoryId.Size = new System.Drawing.Size(300, 21);
            this.editCategoryId.TabIndex = 11;
            this.editCategoryId.Text = "Katagori Seç";
            // 
            // editPublishDate
            // 
            this.editPublishDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editPublishDate.Location = new System.Drawing.Point(315, 14);
            this.editPublishDate.Name = "editPublishDate";
            this.editPublishDate.Size = new System.Drawing.Size(156, 20);
            this.editPublishDate.TabIndex = 22;
            // 
            // editTags
            // 
            this.editTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editTags.Location = new System.Drawing.Point(9, 309);
            this.editTags.Name = "editTags";
            this.editTags.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.editTags.Size = new System.Drawing.Size(613, 34);
            this.editTags.TabIndex = 23;
            this.editTags.Text = "Etiketler";
            // 
            // editAuthor
            // 
            this.editAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editAuthor.FormattingEnabled = true;
            this.editAuthor.Location = new System.Drawing.Point(315, 68);
            this.editAuthor.Name = "editAuthor";
            this.editAuthor.Size = new System.Drawing.Size(156, 21);
            this.editAuthor.TabIndex = 24;
            this.editAuthor.Text = "Yazar Seç";
            // 
            // btnYeni
            // 
            this.btnYeni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYeni.Location = new System.Drawing.Point(9, 349);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(108, 23);
            this.btnYeni.TabIndex = 25;
            this.btnYeni.Text = "Yeni";
            this.btnYeni.UseVisualStyleBackColor = true;
            this.btnYeni.Click += new System.EventHandler(this.btnYeni_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaste.Location = new System.Drawing.Point(557, 349);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(65, 23);
            this.btnPaste.TabIndex = 26;
            this.btnPaste.Text = "Yapıştır";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // ViewContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnYeni);
            this.Controls.Add(this.editAuthor);
            this.Controls.Add(this.editTags);
            this.Controls.Add(this.editPublishDate);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnSelectContent);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.editPicture);
            this.Controls.Add(this.editIsManset);
            this.Controls.Add(this.editDescription);
            this.Controls.Add(this.editSpotTitle);
            this.Controls.Add(this.editTitle);
            this.Controls.Add(this.editMetin);
            this.Controls.Add(this.editCategoryId);
            this.Name = "ViewContent";
            this.Size = new System.Drawing.Size(630, 386);
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnSelectContent;
        private System.Windows.Forms.PictureBox btnSettings;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.PictureBox editPicture;
        private System.Windows.Forms.CheckBox editIsManset;
        private System.Windows.Forms.RichTextBox editDescription;
        private System.Windows.Forms.TextBox editSpotTitle;
        private System.Windows.Forms.TextBox editTitle;
        private System.Windows.Forms.RichTextBox editMetin;
        private System.Windows.Forms.ComboBox editCategoryId;
        private System.Windows.Forms.DateTimePicker editPublishDate;
        private System.Windows.Forms.RichTextBox editTags;
        private System.Windows.Forms.ComboBox editAuthor;
        private System.Windows.Forms.Button btnYeni;
        private System.Windows.Forms.Button btnPaste;

    }
}
