namespace Cinar.DBTools.Tools
{
    partial class FormCompareDatabases
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
            this.cbDstDb = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSrcDb = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.cbName = new System.Windows.Forms.CheckBox();
            this.cbType = new System.Windows.Forms.CheckBox();
            this.cbLength = new System.Windows.Forms.CheckBox();
            this.cbNullable = new System.Windows.Forms.CheckBox();
            this.cbCount = new System.Windows.Forms.CheckBox();
            this.cbMaxId = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbDstDb
            // 
            this.cbDstDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstDb.FormattingEnabled = true;
            this.cbDstDb.Location = new System.Drawing.Point(250, 14);
            this.cbDstDb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDstDb.Name = "cbDstDb";
            this.cbDstDb.Size = new System.Drawing.Size(186, 23);
            this.cbDstDb.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 15);
            this.label6.TabIndex = 44;
            this.label6.Text = "Vs";
            // 
            // cbSrcDb
            // 
            this.cbSrcDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcDb.FormattingEnabled = true;
            this.cbSrcDb.Location = new System.Drawing.Point(14, 14);
            this.cbSrcDb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSrcDb.Name = "cbSrcDb";
            this.cbSrcDb.Size = new System.Drawing.Size(198, 23);
            this.cbSrcDb.TabIndex = 0;
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(932, 14);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(87, 26);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(14, 48);
            this.panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1005, 514);
            this.panel.TabIndex = 7;
            // 
            // cbName
            // 
            this.cbName.AutoSize = true;
            this.cbName.Location = new System.Drawing.Point(457, 18);
            this.cbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(58, 19);
            this.cbName.TabIndex = 2;
            this.cbName.Text = "Name";
            this.cbName.UseVisualStyleBackColor = true;
            // 
            // cbType
            // 
            this.cbType.AutoSize = true;
            this.cbType.Location = new System.Drawing.Point(527, 18);
            this.cbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(52, 19);
            this.cbType.TabIndex = 3;
            this.cbType.Text = "Type";
            this.cbType.UseVisualStyleBackColor = true;
            // 
            // cbLength
            // 
            this.cbLength.AutoSize = true;
            this.cbLength.Location = new System.Drawing.Point(593, 18);
            this.cbLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbLength.Name = "cbLength";
            this.cbLength.Size = new System.Drawing.Size(63, 19);
            this.cbLength.TabIndex = 4;
            this.cbLength.Text = "Length";
            this.cbLength.UseVisualStyleBackColor = true;
            // 
            // cbNullable
            // 
            this.cbNullable.AutoSize = true;
            this.cbNullable.Location = new System.Drawing.Point(668, 18);
            this.cbNullable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbNullable.Name = "cbNullable";
            this.cbNullable.Size = new System.Drawing.Size(70, 19);
            this.cbNullable.TabIndex = 5;
            this.cbNullable.Text = "Nullable";
            this.cbNullable.UseVisualStyleBackColor = true;
            // 
            // cbCount
            // 
            this.cbCount.AutoSize = true;
            this.cbCount.Location = new System.Drawing.Point(744, 18);
            this.cbCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbCount.Name = "cbCount";
            this.cbCount.Size = new System.Drawing.Size(70, 19);
            this.cbCount.TabIndex = 45;
            this.cbCount.Text = "count(*)";
            this.cbCount.UseVisualStyleBackColor = true;
            // 
            // cbMaxId
            // 
            this.cbMaxId.AutoSize = true;
            this.cbMaxId.Location = new System.Drawing.Point(820, 18);
            this.cbMaxId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMaxId.Name = "cbMaxId";
            this.cbMaxId.Size = new System.Drawing.Size(66, 19);
            this.cbMaxId.TabIndex = 46;
            this.cbMaxId.Text = "max(Id)";
            this.cbMaxId.UseVisualStyleBackColor = true;
            // 
            // FormCompareDatabases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 576);
            this.Controls.Add(this.cbMaxId);
            this.Controls.Add(this.cbCount);
            this.Controls.Add(this.cbNullable);
            this.Controls.Add(this.cbLength);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.cbDstDb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSrcDb);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCompareDatabases";
            this.Text = "Compare Databases";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDstDb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSrcDb;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox cbName;
        private System.Windows.Forms.CheckBox cbType;
        private System.Windows.Forms.CheckBox cbLength;
        private System.Windows.Forms.CheckBox cbNullable;
        private System.Windows.Forms.CheckBox cbCount;
        private System.Windows.Forms.CheckBox cbMaxId;
    }
}