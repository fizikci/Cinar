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
            this.menuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRenameGroupName = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRename,
            this.menuRenameGroupName,
            this.menuUp,
            this.menuDown,
            this.menuRefresh,
            this.toolStripMenuItem1});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(194, 142);
            // 
            // menuRename
            // 
            this.menuRename.Name = "menuRename";
            this.menuRename.Size = new System.Drawing.Size(193, 22);
            this.menuRename.Text = "Rename Display Name";
            this.menuRename.Click += new System.EventHandler(this.menuRename_Click);
            // 
            // menuUp
            // 
            this.menuUp.Name = "menuUp";
            this.menuUp.Size = new System.Drawing.Size(193, 22);
            this.menuUp.Text = "Up";
            this.menuUp.Click += new System.EventHandler(this.menuUp_Click);
            // 
            // menuDown
            // 
            this.menuDown.Name = "menuDown";
            this.menuDown.Size = new System.Drawing.Size(193, 22);
            this.menuDown.Text = "Down";
            this.menuDown.Click += new System.EventHandler(this.menuDown_Click);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(193, 22);
            this.menuRefresh.Text = "Refresh";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // menuRenameGroupName
            // 
            this.menuRenameGroupName.Name = "menuRenameGroupName";
            this.menuRenameGroupName.Size = new System.Drawing.Size(193, 22);
            this.menuRenameGroupName.Text = "Rename Group Name";
            this.menuRenameGroupName.Click += new System.EventHandler(this.menuRenameGroupName_Click);
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
        private System.Windows.Forms.ToolStripMenuItem menuRename;
        private System.Windows.Forms.ToolStripMenuItem menuUp;
        private System.Windows.Forms.ToolStripMenuItem menuDown;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuRenameGroupName;
    }
}
