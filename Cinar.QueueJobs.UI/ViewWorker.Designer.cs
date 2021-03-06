﻿namespace Cinar.QueueJobs.UI
{
    partial class ViewWorker
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblActiveSince = new System.Windows.Forms.Label();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblOKCount = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(172, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Waiting";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblActiveSince
            // 
            this.lblActiveSince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActiveSince.Location = new System.Drawing.Point(13, 32);
            this.lblActiveSince.Name = "lblActiveSince";
            this.lblActiveSince.Size = new System.Drawing.Size(163, 18);
            this.lblActiveSince.TabIndex = 2;
            this.lblActiveSince.Text = "Active Since";
            this.lblActiveSince.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCommand
            // 
            this.lblCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommand.Location = new System.Drawing.Point(9, 49);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(167, 19);
            this.lblCommand.TabIndex = 4;
            this.lblCommand.Text = "Command";
            this.lblCommand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCommand.Click += new System.EventHandler(this.lblLastCommand_Click);
            // 
            // lblOKCount
            // 
            this.lblOKCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOKCount.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblOKCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOKCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOKCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOKCount.Location = new System.Drawing.Point(118, 8);
            this.lblOKCount.Name = "lblOKCount";
            this.lblOKCount.Size = new System.Drawing.Size(55, 15);
            this.lblOKCount.TabIndex = 5;
            this.lblOKCount.Text = "0";
            this.lblOKCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.Location = new System.Drawing.Point(9, 68);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(167, 19);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(167, 17);
            this.progressBar1.TabIndex = 7;
            // 
            // ViewWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblOKCount);
            this.Controls.Add(this.lblCommand);
            this.Controls.Add(this.lblActiveSince);
            this.Controls.Add(this.lblTitle);
            this.Name = "ViewWorker";
            this.Size = new System.Drawing.Size(179, 113);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCommand;
        public System.Windows.Forms.Label lblActiveSince;
        public System.Windows.Forms.Label lblOKCount;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
