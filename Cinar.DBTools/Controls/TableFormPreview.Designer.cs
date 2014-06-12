namespace Cinar.DBTools.Controls
{
    partial class TableFormPreview
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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangeControl = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHide,
            this.menuChangeControl});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(159, 70);
            // 
            // menuHide
            // 
            this.menuHide.Name = "menuHide";
            this.menuHide.Size = new System.Drawing.Size(158, 22);
            this.menuHide.Text = "Hide";
            this.menuHide.Click += new System.EventHandler(this.menuHide_Click);
            // 
            // menuChangeControl
            // 
            this.menuChangeControl.Name = "menuChangeControl";
            this.menuChangeControl.Size = new System.Drawing.Size(158, 22);
            this.menuChangeControl.Text = "Change Control";
            // 
            // TableFormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TableFormPreview";
            this.Size = new System.Drawing.Size(840, 662);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuHide;
        private System.Windows.Forms.ToolStripMenuItem menuChangeControl;
    }
}
