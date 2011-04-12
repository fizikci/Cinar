namespace Cinar.DBTools.Tools
{
    partial class FormCodeGenerator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCodeGenerator));
            this.lbTemplates = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.btnGenerateAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbEntities = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.lbCategories = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbTemplates
            // 
            this.lbTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTemplates.FormattingEnabled = true;
            this.lbTemplates.Location = new System.Drawing.Point(12, 112);
            this.lbTemplates.Name = "lbTemplates";
            this.lbTemplates.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTemplates.Size = new System.Drawing.Size(239, 303);
            this.lbTemplates.Sorted = true;
            this.lbTemplates.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(162, 430);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(104, 430);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(52, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(42, 430);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(56, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New...";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateCode.Location = new System.Drawing.Point(366, 430);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(144, 23);
            this.btnGenerateCode.TabIndex = 7;
            this.btnGenerateCode.Text = "Show Generated Code...";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // btnGenerateAll
            // 
            this.btnGenerateAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateAll.Location = new System.Drawing.Point(516, 430);
            this.btnGenerateAll.Name = "btnGenerateAll";
            this.btnGenerateAll.Size = new System.Drawing.Size(147, 23);
            this.btnGenerateAll.TabIndex = 8;
            this.btnGenerateAll.Text = "Publish Generated Code...";
            this.btnGenerateAll.UseVisualStyleBackColor = true;
            this.btnGenerateAll.Click += new System.EventHandler(this.btnGenerateAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Templates";
            // 
            // lbEntities
            // 
            this.lbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEntities.CheckOnClick = true;
            this.lbEntities.FormattingEnabled = true;
            this.lbEntities.Location = new System.Drawing.Point(267, 22);
            this.lbEntities.MultiColumn = true;
            this.lbEntities.Name = "lbEntities";
            this.lbEntities.Size = new System.Drawing.Size(396, 394);
            this.lbEntities.Sorted = true;
            this.lbEntities.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Entities";
            // 
            // cbAll
            // 
            this.cbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(627, 4);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(37, 17);
            this.cbAll.TabIndex = 5;
            this.cbAll.Text = "All";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // lbCategories
            // 
            this.lbCategories.FormattingEnabled = true;
            this.lbCategories.Location = new System.Drawing.Point(12, 22);
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Size = new System.Drawing.Size(239, 82);
            this.lbCategories.Sorted = true;
            this.lbCategories.TabIndex = 0;
            this.lbCategories.SelectedIndexChanged += new System.EventHandler(this.lbCategories_SelectedIndexChanged);
            // 
            // FormCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 465);
            this.Controls.Add(this.lbCategories);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbEntities);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerateAll);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbTemplates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCodeGenerator";
            this.Text = "Code Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbTemplates;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.Button btnGenerateAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox lbEntities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.ListBox lbCategories;


    }
}