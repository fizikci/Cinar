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
            this.SuspendLayout();
            // 
            // cbDstDb
            // 
            this.cbDstDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstDb.FormattingEnabled = true;
            this.cbDstDb.Location = new System.Drawing.Point(214, 12);
            this.cbDstDb.Name = "cbDstDb";
            this.cbDstDb.Size = new System.Drawing.Size(160, 21);
            this.cbDstDb.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Vs";
            // 
            // cbSrcDb
            // 
            this.cbSrcDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcDb.FormattingEnabled = true;
            this.cbSrcDb.Location = new System.Drawing.Point(12, 12);
            this.cbSrcDb.Name = "cbSrcDb";
            this.cbSrcDb.Size = new System.Drawing.Size(170, 21);
            this.cbSrcDb.TabIndex = 0;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(660, 12);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
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
            this.panel.Location = new System.Drawing.Point(12, 41);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(814, 446);
            this.panel.TabIndex = 7;
            // 
            // cbName
            // 
            this.cbName.AutoSize = true;
            this.cbName.Location = new System.Drawing.Point(392, 15);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(54, 17);
            this.cbName.TabIndex = 2;
            this.cbName.Text = "Name";
            this.cbName.UseVisualStyleBackColor = true;
            // 
            // cbType
            // 
            this.cbType.AutoSize = true;
            this.cbType.Location = new System.Drawing.Point(452, 15);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(50, 17);
            this.cbType.TabIndex = 3;
            this.cbType.Text = "Type";
            this.cbType.UseVisualStyleBackColor = true;
            // 
            // cbLength
            // 
            this.cbLength.AutoSize = true;
            this.cbLength.Location = new System.Drawing.Point(508, 15);
            this.cbLength.Name = "cbLength";
            this.cbLength.Size = new System.Drawing.Size(59, 17);
            this.cbLength.TabIndex = 4;
            this.cbLength.Text = "Length";
            this.cbLength.UseVisualStyleBackColor = true;
            // 
            // cbNullable
            // 
            this.cbNullable.AutoSize = true;
            this.cbNullable.Location = new System.Drawing.Point(573, 15);
            this.cbNullable.Name = "cbNullable";
            this.cbNullable.Size = new System.Drawing.Size(64, 17);
            this.cbNullable.TabIndex = 5;
            this.cbNullable.Text = "Nullable";
            this.cbNullable.UseVisualStyleBackColor = true;
            // 
            // FormCompareDatabases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 499);
            this.Controls.Add(this.cbNullable);
            this.Controls.Add(this.cbLength);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.cbDstDb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSrcDb);
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
    }
}