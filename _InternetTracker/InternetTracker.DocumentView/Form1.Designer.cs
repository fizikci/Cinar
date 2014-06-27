namespace InternetTracker.DocumentView
{
    partial class Form1
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.txtToString = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtXPath = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 37);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(356, 580);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(374, 37);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(374, 580);
            this.propertyGrid.TabIndex = 1;
            // 
            // txtToString
            // 
            this.txtToString.Location = new System.Drawing.Point(754, 37);
            this.txtToString.Multiline = true;
            this.txtToString.Name = "txtToString";
            this.txtToString.Size = new System.Drawing.Size(461, 580);
            this.txtToString.TabIndex = 2;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 11);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(355, 20);
            this.txtUrl.TabIndex = 3;
            this.txtUrl.Text = "http://www.hurriyet.com.tr/dunya/26609652.asp";
            this.txtUrl.Click += new System.EventHandler(this.txtUrl_Click);
            // 
            // txtXPath
            // 
            this.txtXPath.Location = new System.Drawing.Point(404, 11);
            this.txtXPath.Name = "txtXPath";
            this.txtXPath.Size = new System.Drawing.Size(811, 20);
            this.txtXPath.TabIndex = 4;
            this.txtXPath.Text = "http://www.hurriyet.com.tr/dunya/26609652.asp";
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(374, 11);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(24, 20);
            this.btnParse.TabIndex = 5;
            this.btnParse.Text = ">";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 629);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.txtXPath);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtToString);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.TextBox txtToString;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtXPath;
        private System.Windows.Forms.Button btnParse;
    }
}

