namespace Cinar.DBTools
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusExecTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusNumberOfRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.menuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCount = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTop10 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDistinct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMax = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateSQLSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateSQLInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateSQLUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateSQLCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupedCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewConnectionContext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefreshMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowTableCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateUIMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtSQL = new System.Windows.Forms.RichTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpResults = new System.Windows.Forms.TabPage();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tpSQLLog = new System.Windows.Forms.TabPage();
            this.txtSQLLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCodeGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCheckDatabaseSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyTreeData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSimpleIntegrationService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompareDatabases = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.quickScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteFromTables = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectCountsFromTables = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScriptingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewConnection = new System.Windows.Forms.ToolStripButton();
            this.btnEditConnection = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExecuteSQL = new System.Windows.Forms.ToolStripButton();
            this.btnExecuteScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCodeGenerator = new System.Windows.Forms.ToolStripButton();
            this.btnCheckDatabaseSchema = new System.Windows.Forms.ToolStripButton();
            this.btnDatabaseTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTryAndSee = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tpTableAnalyze = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.menuAnalyzeTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStripTree.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpSQLLog.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpTableAnalyze.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(813, 539);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(813, 610);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText,
            this.toolStripStatusLabel2,
            this.statusExecTime,
            this.toolStripStatusLabel4,
            this.statusNumberOfRows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(813, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusText
            // 
            this.statusText.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusText.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(591, 17);
            this.statusText.Spring = true;
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabel2.Text = "  Exec. Time:";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusExecTime
            // 
            this.statusExecTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusExecTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusExecTime.Name = "statusExecTime";
            this.statusExecTime.Size = new System.Drawing.Size(33, 17);
            this.statusExecTime.Text = "0 ms";
            this.statusExecTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(62, 17);
            this.toolStripStatusLabel4.Text = "  Returned:";
            this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusNumberOfRows
            // 
            this.statusNumberOfRows.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusNumberOfRows.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusNumberOfRows.Name = "statusNumberOfRows";
            this.statusNumberOfRows.Size = new System.Drawing.Size(43, 17);
            this.statusNumberOfRows.Text = "0 rows";
            this.statusNumberOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(813, 539);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.menuStripTree;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTree;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(269, 539);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            // 
            // menuStripTree
            // 
            this.menuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCount,
            this.menuTop10,
            this.menuDistinct,
            this.menuMax,
            this.menuMin,
            this.menuGenerateSQL,
            this.menuRefresh,
            this.menuGroupedCounts,
            this.menuTableDrop,
            this.menuEditConnection,
            this.menuDeleteConnection,
            this.menuOpenERDiagram,
            this.menuNewERDiagram,
            this.menuNewConnectionContext,
            this.menuRefreshMetadata,
            this.menuDeleteERDiagram,
            this.menuShowTableCounts,
            this.menuGenerateUIMetadata,
            this.menuAnalyzeTable});
            this.menuStripTree.Name = "contextMenuStrip1";
            this.menuStripTree.Size = new System.Drawing.Size(198, 422);
            // 
            // menuCount
            // 
            this.menuCount.Name = "menuCount";
            this.menuCount.Size = new System.Drawing.Size(197, 22);
            this.menuCount.Text = "Count";
            // 
            // menuTop10
            // 
            this.menuTop10.Name = "menuTop10";
            this.menuTop10.Size = new System.Drawing.Size(197, 22);
            this.menuTop10.Text = "Top 1000";
            // 
            // menuDistinct
            // 
            this.menuDistinct.Name = "menuDistinct";
            this.menuDistinct.Size = new System.Drawing.Size(197, 22);
            this.menuDistinct.Text = "Distinct";
            // 
            // menuMax
            // 
            this.menuMax.Name = "menuMax";
            this.menuMax.Size = new System.Drawing.Size(197, 22);
            this.menuMax.Text = "Max()";
            // 
            // menuMin
            // 
            this.menuMin.Name = "menuMin";
            this.menuMin.Size = new System.Drawing.Size(197, 22);
            this.menuMin.Text = "Min()";
            // 
            // menuGenerateSQL
            // 
            this.menuGenerateSQL.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGenerateSQLSelect,
            this.menuGenerateSQLInsert,
            this.menuGenerateSQLUpdate,
            this.menuGenerateSQLCreateTable});
            this.menuGenerateSQL.Name = "menuGenerateSQL";
            this.menuGenerateSQL.Size = new System.Drawing.Size(197, 22);
            this.menuGenerateSQL.Text = "Generate SQL";
            // 
            // menuGenerateSQLSelect
            // 
            this.menuGenerateSQLSelect.Name = "menuGenerateSQLSelect";
            this.menuGenerateSQLSelect.Size = new System.Drawing.Size(147, 22);
            this.menuGenerateSQLSelect.Text = "Select";
            // 
            // menuGenerateSQLInsert
            // 
            this.menuGenerateSQLInsert.Name = "menuGenerateSQLInsert";
            this.menuGenerateSQLInsert.Size = new System.Drawing.Size(147, 22);
            this.menuGenerateSQLInsert.Text = "Insert";
            // 
            // menuGenerateSQLUpdate
            // 
            this.menuGenerateSQLUpdate.Name = "menuGenerateSQLUpdate";
            this.menuGenerateSQLUpdate.Size = new System.Drawing.Size(147, 22);
            this.menuGenerateSQLUpdate.Text = "Update";
            // 
            // menuGenerateSQLCreateTable
            // 
            this.menuGenerateSQLCreateTable.Name = "menuGenerateSQLCreateTable";
            this.menuGenerateSQLCreateTable.Size = new System.Drawing.Size(147, 22);
            this.menuGenerateSQLCreateTable.Text = "Create Table";
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(197, 22);
            this.menuRefresh.Text = "Refresh Nodes";
            // 
            // menuGroupedCounts
            // 
            this.menuGroupedCounts.Name = "menuGroupedCounts";
            this.menuGroupedCounts.Size = new System.Drawing.Size(197, 22);
            this.menuGroupedCounts.Text = "Grouped Counts";
            // 
            // menuTableDrop
            // 
            this.menuTableDrop.Name = "menuTableDrop";
            this.menuTableDrop.Size = new System.Drawing.Size(197, 22);
            this.menuTableDrop.Text = "Drop Table";
            // 
            // menuEditConnection
            // 
            this.menuEditConnection.Name = "menuEditConnection";
            this.menuEditConnection.Size = new System.Drawing.Size(197, 22);
            this.menuEditConnection.Text = "Edit Connection...";
            // 
            // menuDeleteConnection
            // 
            this.menuDeleteConnection.Name = "menuDeleteConnection";
            this.menuDeleteConnection.Size = new System.Drawing.Size(197, 22);
            this.menuDeleteConnection.Text = "Delete Connection";
            // 
            // menuOpenERDiagram
            // 
            this.menuOpenERDiagram.Name = "menuOpenERDiagram";
            this.menuOpenERDiagram.Size = new System.Drawing.Size(197, 22);
            this.menuOpenERDiagram.Text = "Open ER Diagram...";
            // 
            // menuNewERDiagram
            // 
            this.menuNewERDiagram.Name = "menuNewERDiagram";
            this.menuNewERDiagram.Size = new System.Drawing.Size(197, 22);
            this.menuNewERDiagram.Text = "New ER Diagram...";
            // 
            // menuNewConnectionContext
            // 
            this.menuNewConnectionContext.Name = "menuNewConnectionContext";
            this.menuNewConnectionContext.Size = new System.Drawing.Size(197, 22);
            this.menuNewConnectionContext.Text = "Add New Connection...";
            // 
            // menuRefreshMetadata
            // 
            this.menuRefreshMetadata.Name = "menuRefreshMetadata";
            this.menuRefreshMetadata.Size = new System.Drawing.Size(197, 22);
            this.menuRefreshMetadata.Text = "Refresh Metadata";
            // 
            // menuDeleteERDiagram
            // 
            this.menuDeleteERDiagram.Name = "menuDeleteERDiagram";
            this.menuDeleteERDiagram.Size = new System.Drawing.Size(197, 22);
            this.menuDeleteERDiagram.Text = "Delete ER Diagram";
            // 
            // menuShowTableCounts
            // 
            this.menuShowTableCounts.Name = "menuShowTableCounts";
            this.menuShowTableCounts.Size = new System.Drawing.Size(197, 22);
            this.menuShowTableCounts.Text = "Show Table Counts";
            // 
            // menuGenerateUIMetadata
            // 
            this.menuGenerateUIMetadata.Name = "menuGenerateUIMetadata";
            this.menuGenerateUIMetadata.Size = new System.Drawing.Size(197, 22);
            this.menuGenerateUIMetadata.Text = "Generate UI Metadata";
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Host");
            this.imageListTree.Images.SetKeyName(1, "Database");
            this.imageListTree.Images.SetKeyName(2, "Table");
            this.imageListTree.Images.SetKeyName(3, "Field");
            this.imageListTree.Images.SetKeyName(4, "Key");
            this.imageListTree.Images.SetKeyName(5, "View");
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
            this.splitContainer2.Size = new System.Drawing.Size(540, 539);
            this.splitContainer2.SplitterDistance = 167;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtSQL
            // 
            this.txtSQL.AcceptsTab = true;
            this.txtSQL.DetectUrls = false;
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(540, 167);
            this.txtSQL.TabIndex = 0;
            this.txtSQL.Text = "";
            this.txtSQL.WordWrap = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpResults);
            this.tabControl.Controls.Add(this.tpInfo);
            this.tabControl.Controls.Add(this.tpSQLLog);
            this.tabControl.Controls.Add(this.tpTableAnalyze);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(540, 368);
            this.tabControl.TabIndex = 0;
            // 
            // tpResults
            // 
            this.tpResults.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tpResults.Location = new System.Drawing.Point(4, 22);
            this.tpResults.Name = "tpResults";
            this.tpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpResults.Size = new System.Drawing.Size(532, 342);
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
            this.tpInfo.Size = new System.Drawing.Size(532, 342);
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
            this.txtInfo.Size = new System.Drawing.Size(526, 336);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.WordWrap = false;
            // 
            // tpSQLLog
            // 
            this.tpSQLLog.Controls.Add(this.txtSQLLog);
            this.tpSQLLog.Location = new System.Drawing.Point(4, 22);
            this.tpSQLLog.Name = "tpSQLLog";
            this.tpSQLLog.Size = new System.Drawing.Size(532, 342);
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
            this.txtSQLLog.Size = new System.Drawing.Size(532, 342);
            this.txtSQLLog.TabIndex = 1;
            this.txtSQLLog.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewConnection,
            this.toolStripSeparator3,
            this.menuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuNewConnection
            // 
            this.menuNewConnection.Name = "menuNewConnection";
            this.menuNewConnection.Size = new System.Drawing.Size(175, 22);
            this.menuNewConnection.Text = "New Connection...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(175, 22);
            this.menuExit.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCodeGenerator,
            this.menuCheckDatabaseSchema,
            this.menuDBTransfer,
            this.menuViewERDiagram,
            this.menuCopyTreeData,
            this.menuSimpleIntegrationService,
            this.menuCompareDatabases,
            this.toolStripMenuItem2,
            this.quickScriptToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // menuCodeGenerator
            // 
            this.menuCodeGenerator.Name = "menuCodeGenerator";
            this.menuCodeGenerator.Size = new System.Drawing.Size(222, 22);
            this.menuCodeGenerator.Text = "Code Generator...";
            // 
            // menuCheckDatabaseSchema
            // 
            this.menuCheckDatabaseSchema.Name = "menuCheckDatabaseSchema";
            this.menuCheckDatabaseSchema.Size = new System.Drawing.Size(222, 22);
            this.menuCheckDatabaseSchema.Text = "Check Database Schema...";
            // 
            // menuDBTransfer
            // 
            this.menuDBTransfer.Name = "menuDBTransfer";
            this.menuDBTransfer.Size = new System.Drawing.Size(222, 22);
            this.menuDBTransfer.Text = "Database Transfer...";
            // 
            // menuViewERDiagram
            // 
            this.menuViewERDiagram.Name = "menuViewERDiagram";
            this.menuViewERDiagram.Size = new System.Drawing.Size(222, 22);
            this.menuViewERDiagram.Text = "View ER Diagram...";
            // 
            // menuCopyTreeData
            // 
            this.menuCopyTreeData.Name = "menuCopyTreeData";
            this.menuCopyTreeData.Size = new System.Drawing.Size(222, 22);
            this.menuCopyTreeData.Text = "Copy Tree Data...";
            // 
            // menuSimpleIntegrationService
            // 
            this.menuSimpleIntegrationService.Name = "menuSimpleIntegrationService";
            this.menuSimpleIntegrationService.Size = new System.Drawing.Size(222, 22);
            this.menuSimpleIntegrationService.Text = "Simple Integration Service...";
            // 
            // menuCompareDatabases
            // 
            this.menuCompareDatabases.Name = "menuCompareDatabases";
            this.menuCompareDatabases.Size = new System.Drawing.Size(222, 22);
            this.menuCompareDatabases.Text = "Compare Databases...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(219, 6);
            // 
            // quickScriptToolStripMenuItem
            // 
            this.quickScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDeleteFromTables,
            this.menuSelectCountsFromTables});
            this.quickScriptToolStripMenuItem.Name = "quickScriptToolStripMenuItem";
            this.quickScriptToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.quickScriptToolStripMenuItem.Text = "Quick Script";
            // 
            // menuDeleteFromTables
            // 
            this.menuDeleteFromTables.Name = "menuDeleteFromTables";
            this.menuDeleteFromTables.Size = new System.Drawing.Size(221, 22);
            this.menuDeleteFromTables.Text = "Delete From Tables";
            // 
            // menuSelectCountsFromTables
            // 
            this.menuSelectCountsFromTables.Name = "menuSelectCountsFromTables";
            this.menuSelectCountsFromTables.Size = new System.Drawing.Size(221, 22);
            this.menuSelectCountsFromTables.Text = "Select Count(*) From Tables";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuScriptingTest,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menuScriptingTest
            // 
            this.menuScriptingTest.Name = "menuScriptingTest";
            this.menuScriptingTest.Size = new System.Drawing.Size(280, 22);
            this.menuScriptingTest.Text = "Çınar Scripting Test && Learning Center...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(277, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewConnection,
            this.btnEditConnection,
            this.btnDeleteConnection,
            this.toolStripSeparator1,
            this.btnExecuteSQL,
            this.btnExecuteScript,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.btnCodeGenerator,
            this.btnCheckDatabaseSchema,
            this.btnDatabaseTransfer,
            this.toolStripSeparator4,
            this.btnTryAndSee});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            // 
            // btnNewConnection
            // 
            this.btnNewConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("btnNewConnection.Image")));
            this.btnNewConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewConnection.Name = "btnNewConnection";
            this.btnNewConnection.Size = new System.Drawing.Size(23, 22);
            this.btnNewConnection.Text = "New Connection";
            // 
            // btnEditConnection
            // 
            this.btnEditConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditConnection.Image = ((System.Drawing.Image)(resources.GetObject("btnEditConnection.Image")));
            this.btnEditConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditConnection.Name = "btnEditConnection";
            this.btnEditConnection.Size = new System.Drawing.Size(23, 22);
            this.btnEditConnection.Text = "Edit Connection";
            // 
            // btnDeleteConnection
            // 
            this.btnDeleteConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteConnection.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteConnection.Image")));
            this.btnDeleteConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteConnection.Name = "btnDeleteConnection";
            this.btnDeleteConnection.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteConnection.Text = "Delete Connection";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExecuteSQL
            // 
            this.btnExecuteSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteSQL.Image")));
            this.btnExecuteSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteSQL.Name = "btnExecuteSQL";
            this.btnExecuteSQL.Size = new System.Drawing.Size(23, 22);
            this.btnExecuteSQL.Text = "Execute SQL";
            // 
            // btnExecuteScript
            // 
            this.btnExecuteScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteScript.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteScript.Image")));
            this.btnExecuteScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteScript.Name = "btnExecuteScript";
            this.btnExecuteScript.Size = new System.Drawing.Size(23, 22);
            this.btnExecuteScript.Text = "Execute Script";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Tools:";
            // 
            // btnCodeGenerator
            // 
            this.btnCodeGenerator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCodeGenerator.Image = ((System.Drawing.Image)(resources.GetObject("btnCodeGenerator.Image")));
            this.btnCodeGenerator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCodeGenerator.Name = "btnCodeGenerator";
            this.btnCodeGenerator.Size = new System.Drawing.Size(23, 22);
            this.btnCodeGenerator.Text = "Code Generator";
            // 
            // btnCheckDatabaseSchema
            // 
            this.btnCheckDatabaseSchema.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckDatabaseSchema.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckDatabaseSchema.Image")));
            this.btnCheckDatabaseSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckDatabaseSchema.Name = "btnCheckDatabaseSchema";
            this.btnCheckDatabaseSchema.Size = new System.Drawing.Size(23, 22);
            this.btnCheckDatabaseSchema.Text = "Check Database Schema";
            // 
            // btnDatabaseTransfer
            // 
            this.btnDatabaseTransfer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDatabaseTransfer.Image = ((System.Drawing.Image)(resources.GetObject("btnDatabaseTransfer.Image")));
            this.btnDatabaseTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDatabaseTransfer.Name = "btnDatabaseTransfer";
            this.btnDatabaseTransfer.Size = new System.Drawing.Size(23, 22);
            this.btnDatabaseTransfer.Text = "Database Transfer";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTryAndSee
            // 
            this.btnTryAndSee.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTryAndSee.Image = ((System.Drawing.Image)(resources.GetObject("btnTryAndSee.Image")));
            this.btnTryAndSee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTryAndSee.Name = "btnTryAndSee";
            this.btnTryAndSee.Size = new System.Drawing.Size(23, 22);
            this.btnTryAndSee.Text = "toolStripButton1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tpTableAnalyze
            // 
            this.tpTableAnalyze.Controls.Add(this.webBrowser);
            this.tpTableAnalyze.Location = new System.Drawing.Point(4, 22);
            this.tpTableAnalyze.Name = "tpTableAnalyze";
            this.tpTableAnalyze.Size = new System.Drawing.Size(532, 342);
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
            this.webBrowser.Size = new System.Drawing.Size(532, 342);
            this.webBrowser.TabIndex = 0;
            // 
            // menuAnalyzeTable
            // 
            this.menuAnalyzeTable.Name = "menuAnalyzeTable";
            this.menuAnalyzeTable.Size = new System.Drawing.Size(197, 22);
            this.menuAnalyzeTable.Text = "Analyze Table";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 610);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Çınar Database Tools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStripTree.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.tpSQLLog.ResumeLayout(false);
            this.tpSQLLog.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpTableAnalyze.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtSQL;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpResults;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewConnection;
        private System.Windows.Forms.ContextMenuStrip menuStripTree;
        private System.Windows.Forms.ToolStripMenuItem menuCount;
        private System.Windows.Forms.ToolStripMenuItem menuDistinct;
        private System.Windows.Forms.ToolStripMenuItem menuTop10;
        private System.Windows.Forms.ToolStripMenuItem menuMax;
        private System.Windows.Forms.ToolStripMenuItem menuMin;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuCodeGenerator;
        private System.Windows.Forms.ToolStripMenuItem menuCheckDatabaseSchema;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNewConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExecuteSQL;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateSQL;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateSQLSelect;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateSQLInsert;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateSQLUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateSQLCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.ToolStripMenuItem menuGroupedCounts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCodeGenerator;
        private System.Windows.Forms.ToolStripButton btnCheckDatabaseSchema;
        private System.Windows.Forms.ToolStripMenuItem menuDBTransfer;
        private System.Windows.Forms.ToolStripMenuItem menuTableDrop;
        private System.Windows.Forms.ToolStripButton btnExecuteScript;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnDatabaseTransfer;
        private System.Windows.Forms.ToolStripButton btnEditConnection;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnDeleteConnection;
        private System.Windows.Forms.ToolStripMenuItem menuEditConnection;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusExecTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statusNumberOfRows;
        private System.Windows.Forms.ToolStripMenuItem menuViewERDiagram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnTryAndSee;
        private System.Windows.Forms.ToolStripMenuItem menuOpenERDiagram;
        private System.Windows.Forms.ToolStripMenuItem menuNewERDiagram;
        private System.Windows.Forms.ToolStripMenuItem menuNewConnectionContext;
        private System.Windows.Forms.ToolStripMenuItem menuRefreshMetadata;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteERDiagram;
        private System.Windows.Forms.ToolStripMenuItem menuCopyTreeData;
        private System.Windows.Forms.ToolStripMenuItem menuShowTableCounts;
        private System.Windows.Forms.ToolStripMenuItem menuSimpleIntegrationService;
        private System.Windows.Forms.ToolStripMenuItem quickScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteFromTables;
        private System.Windows.Forms.ToolStripMenuItem menuScriptingTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuSelectCountsFromTables;
        private System.Windows.Forms.ToolStripMenuItem menuCompareDatabases;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.TabPage tpSQLLog;
        private System.Windows.Forms.TextBox txtSQLLog;
        private System.Windows.Forms.ToolStripMenuItem menuGenerateUIMetadata;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem menuAnalyzeTable;
        private System.Windows.Forms.TabPage tpTableAnalyze;
        private System.Windows.Forms.WebBrowser webBrowser;

    }
}