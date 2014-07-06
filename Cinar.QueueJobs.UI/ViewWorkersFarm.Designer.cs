namespace Cinar.QueueJobs.UI
{
    partial class ViewWorkersFarm
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
            this.components = new System.ComponentModel.Container();
            this.panelWorkers = new System.Windows.Forms.FlowLayoutPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelWorkers
            // 
            this.panelWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWorkers.AutoScroll = true;
            this.panelWorkers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelWorkers.Location = new System.Drawing.Point(3, 3);
            this.panelWorkers.Name = "panelWorkers";
            this.panelWorkers.Size = new System.Drawing.Size(600, 437);
            this.panelWorkers.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(3, 445);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(600, 18);
            this.lblStatus.TabIndex = 2;
            // 
            // ViewWorkersFarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelWorkers);
            this.Name = "ViewWorkersFarm";
            this.Size = new System.Drawing.Size(606, 468);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelWorkers;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.Label lblStatus;
    }
}
