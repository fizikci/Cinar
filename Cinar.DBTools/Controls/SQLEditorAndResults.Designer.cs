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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtSQL = new Cinar.DBTools.CinarSQLEditor();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpResults = new System.Windows.Forms.TabPage();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tpSQLLog = new System.Windows.Forms.TabPage();
            this.txtSQLLog = new System.Windows.Forms.TextBox();
            this.tpTableAnalyze = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpSQLLog.SuspendLayout();
            this.tpTableAnalyze.SuspendLayout();
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
            this.tabControl.Controls.Add(this.tpTableAnalyze);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(80, 18);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(638, 457);
            this.tabControl.TabIndex = 0;
            // 
            // tpResults
            // 
            this.tpResults.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tpResults.Location = new System.Drawing.Point(4, 22);
            this.tpResults.Name = "tpResults";
            this.tpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpResults.Size = new System.Drawing.Size(630, 431);
            this.tpResults.TabIndex = 0;
            this.tpResults.Text = "Results";
            this.tpResults.UseVisualStyleBackColor = true;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.txtInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(359, 181);
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
            this.txtInfo.Size = new System.Drawing.Size(353, 175);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.WordWrap = false;
            // 
            // tpSQLLog
            // 
            this.tpSQLLog.Controls.Add(this.txtSQLLog);
            this.tpSQLLog.Location = new System.Drawing.Point(4, 22);
            this.tpSQLLog.Name = "tpSQLLog";
            this.tpSQLLog.Size = new System.Drawing.Size(630, 431);
            this.tpSQLLog.TabIndex = 2;
            this.tpSQLLog.Text = "SQL Log";
            this.tpSQLLog.UseVisualStyleBackColor = true;
            // 
            // txtSQLLog
            // 
            this.txtSQLLog.AcceptsReturn = true;
            this.txtSQLLog.AcceptsTab = true;
            this.txtSQLLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSQLLog.Location = new System.Drawing.Point(0, 0);
            this.txtSQLLog.Multiline = true;
            this.txtSQLLog.Name = "txtSQLLog";
            this.txtSQLLog.ReadOnly = true;
            this.txtSQLLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQLLog.Size = new System.Drawing.Size(630, 431);
            this.txtSQLLog.TabIndex = 1;
            this.txtSQLLog.WordWrap = false;
            // 
            // tpTableAnalyze
            // 
            this.tpTableAnalyze.Controls.Add(this.webBrowser);
            this.tpTableAnalyze.Location = new System.Drawing.Point(4, 22);
            this.tpTableAnalyze.Name = "tpTableAnalyze";
            this.tpTableAnalyze.Size = new System.Drawing.Size(359, 181);
            this.tpTableAnalyze.TabIndex = 3;
            this.tpTableAnalyze.Text = "Table Analyze";
            this.tpTableAnalyze.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(359, 181);
            this.webBrowser.TabIndex = 0;
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
            this.splitContainer2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.tpSQLLog.ResumeLayout(false);
            this.tpSQLLog.PerformLayout();
            this.tpTableAnalyze.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtSQLLog;
        private System.Windows.Forms.TabPage tpTableAnalyze;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}
