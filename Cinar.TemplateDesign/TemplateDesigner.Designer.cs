namespace Cinar.TemplateDesign
{
    partial class TemplateDesigner
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
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.xtraScrollableControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.Gray;
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.Controls.Add(this.flowLayoutPanel);
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Margin = new System.Windows.Forms.Padding(0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(300, 300);
            this.xtraScrollableControl.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.Gray;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(300, 200);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Interval = 25;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // TemplateDesigner
            // 
            this.Controls.Add(this.xtraScrollableControl);
            this.Name = "TemplateDesigner";
            this.Size = new System.Drawing.Size(300, 300);
            this.xtraScrollableControl.ResumeLayout(false);
            this.xtraScrollableControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Timer timer;
    }
}
