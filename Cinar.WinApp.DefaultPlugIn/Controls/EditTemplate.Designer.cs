namespace Cinar.WinApp.DefaultPlugIn.Controls
{
    partial class EditTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTemplate));
            this.layoutEntity = new DevExpress.XtraLayout.LayoutControl();
            this.editHtml = new DevExpress.XtraEditors.MemoEdit();
            this.editSQLQuery = new DevExpress.XtraEditors.MemoEdit();
            this.editName = new Cinar.WinUI.LookUp();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.panelDetail = new DevExpress.XtraEditors.PanelControl();
            this.listEntityTemplateParam = new Cinar.WinUI.ListEntitySimple();
            this.btnDeleteTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditTemplateDesign = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).BeginInit();
            this.layoutEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editHtml.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSQLQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutEntity
            // 
            this.layoutEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutEntity.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutEntity.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutEntity.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutEntity.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutEntity.Controls.Add(this.editHtml);
            this.layoutEntity.Controls.Add(this.editSQLQuery);
            this.layoutEntity.Controls.Add(this.editName);
            this.layoutEntity.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4});
            this.layoutEntity.Location = new System.Drawing.Point(3, 3);
            this.layoutEntity.Name = "layoutEntity";
            this.layoutEntity.Root = this.layoutControlGroup1;
            this.layoutEntity.Size = new System.Drawing.Size(733, 437);
            this.layoutEntity.TabIndex = 0;
            // 
            // editHtml
            // 
            this.editHtml.Location = new System.Drawing.Point(16, 69);
            this.editHtml.Name = "editHtml";
            this.editHtml.Size = new System.Drawing.Size(699, 350);
            this.editHtml.TabIndex = 8;
            // 
            // editSQLQuery
            // 
            this.editSQLQuery.Location = new System.Drawing.Point(16, 69);
            this.editSQLQuery.Name = "editSQLQuery";
            this.editSQLQuery.Size = new System.Drawing.Size(699, 350);
            this.editSQLQuery.TabIndex = 7;
            // 
            // editName
            // 
            this.editName.DependentFieldName = null;
            this.editName.DependsOn = null;
            this.editName.Filter = null;
            this.editName.Location = new System.Drawing.Point(27, 7);
            this.editName.Name = "editName";
            this.editName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editName.ShowEllipsisButton = false;
            this.editName.Size = new System.Drawing.Size(700, 20);
            this.editName.TabIndex = 4;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Tekrarlanan HTML";
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(710, 299);
            this.layoutControlGroup4.Text = "Tekrarlanan HTML";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.tabbedControlGroup1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(733, 437);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.editName;
            this.layoutControlItemName.CustomizationFormText = "Adý";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(731, 31);
            this.layoutControlItemName.Text = "Adý";
            this.layoutControlItemName.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemName.TextSize = new System.Drawing.Size(15, 13);
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.CustomizationFormText = "tabbedControlGroup1";
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 31);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup2;
            this.tabbedControlGroup1.SelectedTabPageIndex = 0;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(731, 404);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.tabbedControlGroup1.Text = "tabbedControlGroup1";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "SQL Sorgusu";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(710, 361);
            this.layoutControlGroup2.Text = "SQL Sorgusu";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.editSQLQuery;
            this.layoutControlItem4.CustomizationFormText = "Sql Sorgusu";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(710, 361);
            this.layoutControlItem4.Text = "SQL Sorgusu";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Master HTML";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(710, 361);
            this.layoutControlGroup3.Text = "HTML + Çýnar Script";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.editHtml;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(710, 361);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTemplate.Image")));
            this.btnAddTemplate.ImageIndex = 1;
            this.btnAddTemplate.Location = new System.Drawing.Point(3, 446);
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(123, 22);
            this.btnAddTemplate.TabIndex = 13;
            this.btnAddTemplate.Text = "Yeni Rapor Hazýrla";
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveTemplate.Image")));
            this.btnSaveTemplate.ImageIndex = 0;
            this.btnSaveTemplate.Location = new System.Drawing.Point(132, 446);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(82, 22);
            this.btnSaveTemplate.TabIndex = 14;
            this.btnSaveTemplate.Text = "Kaydet";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDetail.Controls.Add(this.listEntityTemplateParam);
            this.panelDetail.Location = new System.Drawing.Point(3, 477);
            this.panelDetail.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(734, 224);
            this.panelDetail.TabIndex = 23;
            // 
            // listEntityTemplateParam
            // 
            this.listEntityTemplateParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityTemplateParam.DeleteWarningMessage = null;
            this.listEntityTemplateParam.EntityType = typeof(Cinar.Entities.Standart.ReportParam);
            this.listEntityTemplateParam.ImageForEntity = ((System.Drawing.Bitmap)(resources.GetObject("listEntityTemplateParam.ImageForEntity")));
            this.listEntityTemplateParam.Location = new System.Drawing.Point(4, 4);
            this.listEntityTemplateParam.Name = "listEntityTemplateParam";
            this.listEntityTemplateParam.PageSize = 0;
            this.listEntityTemplateParam.ShowAddButton = true;
            this.listEntityTemplateParam.ShowDeleteButton = true;
            this.listEntityTemplateParam.ShowEditButton = true;
            this.listEntityTemplateParam.ShowExcelButton = false;
            this.listEntityTemplateParam.ShowHistoryButton = false;
            this.listEntityTemplateParam.Size = new System.Drawing.Size(727, 222);
            this.listEntityTemplateParam.TabIndex = 27;
            this.listEntityTemplateParam.Title = "SQL Parametreleri";
            // 
            // btnDeleteTemplate
            // 
            this.btnDeleteTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteTemplate.Image")));
            this.btnDeleteTemplate.ImageIndex = 2;
            this.btnDeleteTemplate.Location = new System.Drawing.Point(638, 446);
            this.btnDeleteTemplate.Name = "btnDeleteTemplate";
            this.btnDeleteTemplate.Size = new System.Drawing.Size(98, 22);
            this.btnDeleteTemplate.TabIndex = 24;
            this.btnDeleteTemplate.Text = "Raporu Sil";
            // 
            // btnEditTemplateDesign
            // 
            this.btnEditTemplateDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditTemplateDesign.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTemplateDesign.Image")));
            this.btnEditTemplateDesign.ImageIndex = 0;
            this.btnEditTemplateDesign.Location = new System.Drawing.Point(430, 446);
            this.btnEditTemplateDesign.Name = "btnEditTemplateDesign";
            this.btnEditTemplateDesign.Size = new System.Drawing.Size(108, 22);
            this.btnEditTemplateDesign.TabIndex = 25;
            this.btnEditTemplateDesign.Text = "Rapor Tasarýmý";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageIndex = 0;
            this.btnPreview.Location = new System.Drawing.Point(544, 446);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(78, 22);
            this.btnPreview.TabIndex = 26;
            this.btnPreview.Text = "Önizleme";
            // 
            // btnCopy
            // 
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageIndex = 1;
            this.btnCopy.Location = new System.Drawing.Point(231, 446);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(86, 22);
            this.btnCopy.TabIndex = 27;
            this.btnCopy.Text = "Kopyala";
            // 
            // EditTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnEditTemplateDesign);
            this.Controls.Add(this.btnDeleteTemplate);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.layoutEntity);
            this.Controls.Add(this.btnAddTemplate);
            this.Controls.Add(this.btnSaveTemplate);
            this.Name = "EditTemplate";
            this.Size = new System.Drawing.Size(761, 710);
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).EndInit();
            this.layoutEntity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editHtml.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSQLQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutEntity;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Cinar.WinUI.LookUp editName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraEditors.SimpleButton btnSaveTemplate;
        private DevExpress.XtraEditors.SimpleButton btnAddTemplate;
        private DevExpress.XtraEditors.PanelControl panelDetail;
        private DevExpress.XtraEditors.SimpleButton btnDeleteTemplate;
        private DevExpress.XtraEditors.SimpleButton btnEditTemplateDesign;
        private DevExpress.XtraEditors.MemoEdit editSQLQuery;
		private Cinar.WinUI.ListEntitySimple listEntityTemplateParam;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.MemoEdit editHtml;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
    }
}