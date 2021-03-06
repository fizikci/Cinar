﻿namespace Cinar.DBTools.Controls
{
    partial class DiagramEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramEditor));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuArrangeTables = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddTables = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetAsDisplayColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetAsPrimaryKey = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArrangeTables,
            this.menuAddTables,
            this.menuCreateTable,
            this.menuRemove,
            this.menuSetAsDisplayColumn,
            this.menuSetAsPrimaryKey});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(205, 158);
            // 
            // menuArrangeTables
            // 
            this.menuArrangeTables.Name = "menuArrangeTables";
            this.menuArrangeTables.Size = new System.Drawing.Size(204, 22);
            this.menuArrangeTables.Text = "Arrange Tables";
            // 
            // menuAddTables
            // 
            this.menuAddTables.Name = "menuAddTables";
            this.menuAddTables.Size = new System.Drawing.Size(204, 22);
            this.menuAddTables.Text = "Add Tables to Diagram...";
            // 
            // menuCreateTable
            // 
            this.menuCreateTable.Name = "menuCreateTable";
            this.menuCreateTable.Size = new System.Drawing.Size(204, 22);
            this.menuCreateTable.Text = "Create Table...";
            // 
            // menuRemove
            // 
            this.menuRemove.Name = "menuRemove";
            this.menuRemove.Size = new System.Drawing.Size(204, 22);
            this.menuRemove.Text = "Remove from Diagram";
            // 
            // menuSetAsDisplayColumn
            // 
            this.menuSetAsDisplayColumn.Name = "menuSetAsDisplayColumn";
            this.menuSetAsDisplayColumn.Size = new System.Drawing.Size(204, 22);
            this.menuSetAsDisplayColumn.Text = "Set As Display Column";
            // 
            // menuSetAsPrimaryKey
            // 
            this.menuSetAsPrimaryKey.Name = "menuSetAsPrimaryKey";
            this.menuSetAsPrimaryKey.Size = new System.Drawing.Size(204, 22);
            this.menuSetAsPrimaryKey.Text = "Set As Primary Index";
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Host");
            this.imageListTree.Images.SetKeyName(1, "Database");
            this.imageListTree.Images.SetKeyName(2, "Table");
            this.imageListTree.Images.SetKeyName(3, "Column");
            this.imageListTree.Images.SetKeyName(4, "Key");
            // 
            // DiagramEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenu;
            this.Name = "DiagramEditor";
            this.Size = new System.Drawing.Size(1168, 731);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuArrangeTables;
        private System.Windows.Forms.ToolStripMenuItem menuAddTables;
        private System.Windows.Forms.ToolStripMenuItem menuCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuRemove;
        private System.Windows.Forms.ToolStripMenuItem menuSetAsDisplayColumn;
        private System.Windows.Forms.ToolStripMenuItem menuSetAsPrimaryKey;
        private System.Windows.Forms.ImageList imageListTree;
    }
}
