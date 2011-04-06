namespace Cinar.DBTools.Tools
{
    partial class FormCompareDirectories
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
            this.label6 = new System.Windows.Forms.Label();
            this.btnCompare = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.cbName = new System.Windows.Forms.CheckBox();
            this.cbLength = new System.Windows.Forms.CheckBox();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.tbSrcDir = new System.Windows.Forms.TextBox();
            this.tbDstDir = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(660, 12);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 46;
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
            this.panel.TabIndex = 47;
            // 
            // cbName
            // 
            this.cbName.AutoSize = true;
            this.cbName.Location = new System.Drawing.Point(420, 15);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(54, 17);
            this.cbName.TabIndex = 48;
            this.cbName.Text = "Name";
            this.cbName.UseVisualStyleBackColor = true;
            // 
            // cbLength
            // 
            this.cbLength.AutoSize = true;
            this.cbLength.Location = new System.Drawing.Point(489, 15);
            this.cbLength.Name = "cbLength";
            this.cbLength.Size = new System.Drawing.Size(59, 17);
            this.cbLength.TabIndex = 50;
            this.cbLength.Text = "Length";
            this.cbLength.UseVisualStyleBackColor = true;
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Location = new System.Drawing.Point(554, 15);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(49, 17);
            this.cbDate.TabIndex = 51;
            this.cbDate.Text = "Date";
            this.cbDate.UseVisualStyleBackColor = true;
            // 
            // tbSrcDir
            // 
            this.tbSrcDir.Location = new System.Drawing.Point(12, 12);
            this.tbSrcDir.Name = "tbSrcDir";
            this.tbSrcDir.Size = new System.Drawing.Size(170, 20);
            this.tbSrcDir.TabIndex = 52;
            this.tbSrcDir.Click += new System.EventHandler(this.tbSrcDir_Click);
            // 
            // tbDstDir
            // 
            this.tbDstDir.Location = new System.Drawing.Point(213, 12);
            this.tbDstDir.Name = "tbDstDir";
            this.tbDstDir.Size = new System.Drawing.Size(170, 20);
            this.tbDstDir.TabIndex = 53;
            this.tbDstDir.Click += new System.EventHandler(this.tbSrcDir_Click);
            // 
            // FormCompareDirectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 499);
            this.Controls.Add(this.tbDstDir);
            this.Controls.Add(this.tbSrcDir);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.cbLength);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.label6);
            this.Name = "FormCompareDirectories";
            this.Text = "Compare Directories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox cbName;
        private System.Windows.Forms.CheckBox cbLength;
        private System.Windows.Forms.CheckBox cbDate;
        private System.Windows.Forms.TextBox tbSrcDir;
        private System.Windows.Forms.TextBox tbDstDir;
    }
}