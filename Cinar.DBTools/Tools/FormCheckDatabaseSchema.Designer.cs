namespace Cinar.DBTools.Tools
{
    partial class FormCheckDatabaseSchema
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
            this.lbProblems = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFix = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbProblems
            // 
            this.lbProblems.FormattingEnabled = true;
            this.lbProblems.ItemHeight = 17;
            this.lbProblems.Location = new System.Drawing.Point(17, 37);
            this.lbProblems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbProblems.Name = "lbProblems";
            this.lbProblems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbProblems.Size = new System.Drawing.Size(622, 463);
            this.lbProblems.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Discovered Problems";
            // 
            // btnFix
            // 
            this.btnFix.Location = new System.Drawing.Point(350, 509);
            this.btnFix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(192, 30);
            this.btnFix.TabIndex = 2;
            this.btnFix.Text = "Fix Selected Problems";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(553, 509);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(17, 510);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FormCheckDatabaseSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 554);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbProblems);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCheckDatabaseSchema";
            this.Text = "Database Schema Notices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbProblems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
    }
}