namespace Cinar.DBTools.Tools
{
    partial class FormHTMLDeneme
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnParse = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.htmlDocument = new Cinar.HTMLParser.HTMLDocument();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.btnParse);
            this.splitContainer.Panel1.Controls.Add(this.richTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.htmlDocument);
            this.splitContainer.Size = new System.Drawing.Size(694, 476);
            this.splitContainer.SplitterDistance = 231;
            this.splitContainer.TabIndex = 0;
            // 
            // btnParse
            // 
            this.btnParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParse.Location = new System.Drawing.Point(577, 201);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(103, 23);
            this.btnParse.TabIndex = 1;
            this.btnParse.Text = "Parse HTML";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(694, 195);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "<div style=\"color:red;font-family:Verdana;font-size:30px\">\nBir iki üç dört beş al" +
                "tı yedi\n<span style=\"color:green;font-size:50px\">\n100 200 300 400\n</span>\nsekiz " +
                "dokuz on bin beş yüz seksen altı\n</div>";
            this.richTextBox.WordWrap = false;
            // 
            // htmlDocument
            // 
            this.htmlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlDocument.HTMLCode = null;
            this.htmlDocument.Location = new System.Drawing.Point(0, 0);
            this.htmlDocument.Name = "htmlDocument";
            this.htmlDocument.Size = new System.Drawing.Size(690, 237);
            this.htmlDocument.TabIndex = 0;
            // 
            // FormHTMLDeneme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 476);
            this.Controls.Add(this.splitContainer);
            this.Name = "FormHTMLDeneme";
            this.Text = "FormHTMLDeneme";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.RichTextBox richTextBox;
        private Cinar.HTMLParser.HTMLDocument htmlDocument;
    }
}