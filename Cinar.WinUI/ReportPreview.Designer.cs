using DevExpress.XtraPrinting.Preview;

namespace Cinar.WinUI
{
    partial class ReportPreview
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
            this.printControl = new DevExpress.XtraPrinting.Control.PrintControl();
            this.printingSystem = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printControl
            // 
            this.printControl.BackColor = System.Drawing.Color.Empty;
            this.printControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printControl.ForeColor = System.Drawing.Color.Empty;
            this.printControl.IsMetric = false;
            this.printControl.Location = new System.Drawing.Point(2, 2);
            this.printControl.Name = "printControl";
            this.printControl.PrintingSystem = this.printingSystem;
            this.printControl.Size = new System.Drawing.Size(696, 392);
            this.printControl.TabIndex = 1;
            this.printControl.TabStop = false;
            this.printControl.TooltipFont = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.panelControl1.Controls.Add(this.printControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 396);
            this.panelControl1.TabIndex = 4;
            // 
            // ReportPreview
            // 
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportPreview";
            this.Size = new System.Drawing.Size(700, 396);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraPrinting.Control.PrintControl printControl;
        protected DevExpress.XtraPrinting.PrintingSystem printingSystem;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        protected PrintBarManager fPrintBarManager;

    }
}