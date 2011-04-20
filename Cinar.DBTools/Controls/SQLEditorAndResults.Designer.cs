namespace Cinar.DBTools.Controls
{
    partial class SQLEditorAndResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLEditorAndResults));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtSQL = new Cinar.DBTools.CinarSQLEditor();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpResults = new System.Windows.Forms.TabPage();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tpSQLLog = new System.Windows.Forms.TabPage();
            this.txtSQLLog = new Cinar.DBTools.CinarSQLEditor();
            this.tpTableInfo = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tpTableData = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.myDataGrid1 = new Cinar.DBTools.Controls.MyDataGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExportAs = new System.Windows.Forms.ToolStripButton();
            this.btnCopyData = new System.Windows.Forms.ToolStripSplitButton();
            this.menuCopySelectedCell = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopySelectedRows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyAllRows = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResetFilter = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteSelectedRows = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtLimit = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtOffset = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpSQLLog.SuspendLayout();
            this.tpTableInfo.SuspendLayout();
            this.tpTableData.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGrid1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtSQL);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl);
            this.splitContainer2.Size = new System.Drawing.Size(638, 665);
            this.splitContainer2.SplitterDistance = 204;
            this.splitContainer2.TabIndex = 1;
            // 
            // txtSQL
            // 
            this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.IsReadOnly = false;
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(638, 204);
            this.txtSQL.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpResults);
            this.tabControl.Controls.Add(this.tpInfo);
            this.tabControl.Controls.Add(this.tpSQLLog);
            this.tabControl.Controls.Add(this.tpTableInfo);
            this.tabControl.Controls.Add(this.tpTableData);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabControl.ItemSize = new System.Drawing.Size(100, 24);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(638, 457);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tpResults
            // 
            this.tpResults.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tpResults.Location = new System.Drawing.Point(4, 28);
            this.tpResults.Name = "tpResults";
            this.tpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpResults.Size = new System.Drawing.Size(630, 425);
            this.tpResults.TabIndex = 0;
            this.tpResults.Text = "Results";
            this.tpResults.UseVisualStyleBackColor = true;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.txtInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 28);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(630, 425);
            this.tpInfo.TabIndex = 1;
            this.tpInfo.Text = "Output";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.AcceptsReturn = true;
            this.txtInfo.AcceptsTab = true;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtInfo.Location = new System.Drawing.Point(3, 3);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(624, 419);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.WordWrap = false;
            // 
            // tpSQLLog
            // 
            this.tpSQLLog.Controls.Add(this.txtSQLLog);
            this.tpSQLLog.Location = new System.Drawing.Point(4, 28);
            this.tpSQLLog.Name = "tpSQLLog";
            this.tpSQLLog.Size = new System.Drawing.Size(630, 425);
            this.tpSQLLog.TabIndex = 2;
            this.tpSQLLog.Text = "SQL Log";
            this.tpSQLLog.UseVisualStyleBackColor = true;
            // 
            // txtSQLLog
            // 
            this.txtSQLLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLLog.IsReadOnly = false;
            this.txtSQLLog.Location = new System.Drawing.Point(0, 0);
            this.txtSQLLog.Name = "txtSQLLog";
            this.txtSQLLog.Size = new System.Drawing.Size(630, 425);
            this.txtSQLLog.TabIndex = 1;
            // 
            // tpTableInfo
            // 
            this.tpTableInfo.Controls.Add(this.webBrowser);
            this.tpTableInfo.Location = new System.Drawing.Point(4, 28);
            this.tpTableInfo.Name = "tpTableInfo";
            this.tpTableInfo.Size = new System.Drawing.Size(630, 425);
            this.tpTableInfo.TabIndex = 3;
            this.tpTableInfo.Text = "Info";
            this.tpTableInfo.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(630, 425);
            this.webBrowser.TabIndex = 0;
            // 
            // tpTableData
            // 
            this.tpTableData.Controls.Add(this.panel1);
            this.tpTableData.Location = new System.Drawing.Point(4, 28);
            this.tpTableData.Name = "tpTableData";
            this.tpTableData.Size = new System.Drawing.Size(630, 425);
            this.tpTableData.TabIndex = 4;
            this.tpTableData.Text = "Table Data";
            this.tpTableData.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStripContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 425);
            this.panel1.TabIndex = 2;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.myDataGrid1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(630, 400);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(630, 425);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // myDataGrid1
            // 
            this.myDataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.myDataGrid1.Name = "myDataGrid1";
            this.myDataGrid1.Size = new System.Drawing.Size(630, 400);
            this.myDataGrid1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportAs,
            this.btnCopyData,
            this.btnResetFilter,
            this.btnDeleteSelectedRows,
            this.toolStripLabel1,
            this.txtLimit,
            this.toolStripLabel2,
            this.txtOffset});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(281, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // btnExportAs
            // 
            this.btnExportAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportAs.Image = ((System.Drawing.Image)(resources.GetObject("btnExportAs.Image")));
            this.btnExportAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportAs.Name = "btnExportAs";
            this.btnExportAs.Size = new System.Drawing.Size(23, 22);
            this.btnExportAs.Text = "Export As...";
            // 
            // btnCopyData
            // 
            this.btnCopyData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopyData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopySelectedCell,
            this.menuCopySelectedRows,
            this.menuCopyAllRows});
            this.btnCopyData.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyData.Image")));
            this.btnCopyData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyData.Name = "btnCopyData";
            this.btnCopyData.Size = new System.Drawing.Size(32, 22);
            this.btnCopyData.Text = "Copy Data";
            // 
            // menuCopySelectedCell
            // 
            this.menuCopySelectedCell.Name = "menuCopySelectedCell";
            this.menuCopySelectedCell.Size = new System.Drawing.Size(180, 22);
            this.menuCopySelectedCell.Text = "Copy Selected Cell";
            // 
            // menuCopySelectedRows
            // 
            this.menuCopySelectedRows.Name = "menuCopySelectedRows";
            this.menuCopySelectedRows.Size = new System.Drawing.Size(180, 22);
            this.menuCopySelectedRows.Text = "Copy Selected Rows";
            // 
            // menuCopyAllRows
            // 
            this.menuCopyAllRows.Name = "menuCopyAllRows";
            this.menuCopyAllRows.Size = new System.Drawing.Size(180, 22);
            this.menuCopyAllRows.Text = "Copy All Rows";
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResetFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnResetFilter.Image")));
            this.btnResetFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(23, 22);
            this.btnResetFilter.Text = "Reset Filter";
            // 
            // btnDeleteSelectedRows
            // 
            this.btnDeleteSelectedRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteSelectedRows.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteSelectedRows.Image")));
            this.btnDeleteSelectedRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteSelectedRows.Name = "btnDeleteSelectedRows";
            this.btnDeleteSelectedRows.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteSelectedRows.Text = "Delete Selected Rows";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel1.Text = "Limit :";
            // 
            // txtLimit
            // 
            this.txtLimit.AutoSize = false;
            this.txtLimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLimit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(30, 15);
            this.txtLimit.Text = "100";
            this.txtLimit.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel2.Text = "Offset:";
            // 
            // txtOffset
            // 
            this.txtOffset.AutoSize = false;
            this.txtOffset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOffset.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(30, 15);
            this.txtOffset.Text = "0";
            this.txtOffset.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SQLEditorAndResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "SQLEditorAndResults";
            this.Size = new System.Drawing.Size(638, 665);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.tpSQLLog.ResumeLayout(false);
            this.tpTableInfo.ResumeLayout(false);
            this.tpTableData.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGrid1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private CinarSQLEditor txtSQL;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpResults;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TabPage tpSQLLog;
        private System.Windows.Forms.TabPage tpTableInfo;
        private System.Windows.Forms.WebBrowser webBrowser;
        private CinarSQLEditor txtSQLLog;
        private System.Windows.Forms.TabPage tpTableData;
        private MyDataGrid myDataGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExportAs;
        private System.Windows.Forms.ToolStripSplitButton btnCopyData;
        private System.Windows.Forms.ToolStripMenuItem menuCopySelectedCell;
        private System.Windows.Forms.ToolStripMenuItem menuCopySelectedRows;
        private System.Windows.Forms.ToolStripMenuItem menuCopyAllRows;
        private System.Windows.Forms.ToolStripButton btnResetFilter;
        private System.Windows.Forms.ToolStripButton btnDeleteSelectedRows;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtLimit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtOffset;
    }
}
