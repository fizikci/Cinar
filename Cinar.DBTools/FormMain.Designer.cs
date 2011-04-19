using System;
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
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.labelConnections = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.menuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuConNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConEditConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConDeleteConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConRefreshMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConCreateDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConDropDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConTruncateDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConEmptyDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateSProc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConBackupDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConTransferDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConExecuteSQLFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConShowDatabaseERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTablesShowTableCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableAlter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableCount = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenerateSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLDump = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFieldDistinct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFieldMax = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFieldMin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFieldGroupedCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowUIMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.btnCloseSQLEditor = new System.Windows.Forms.PictureBox();
            this.tabControlEditors = new System.Windows.Forms.TabControl();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.labelProperties = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenConnectionsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCodeGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCheckDatabaseSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsDBTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsViewERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSQLDump = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSimpleIntegrationService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCopyTreeData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCompareDatabases = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCompareDirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.quickScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptDeleteFromTables = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptSelectCountsFromTables = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptForEachTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptForEachField = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptCalculateOptDataLen = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpScriptingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewConnection = new System.Windows.Forms.ToolStripButton();
            this.btnEditConnection = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cbActiveConnection = new System.Windows.Forms.ToolStripComboBox();
            this.btnAddEditor = new System.Windows.Forms.ToolStripButton();
            this.btnExecuteSQL = new System.Windows.Forms.ToolStripButton();
            this.btnExecuteScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCodeGenerator = new System.Windows.Forms.ToolStripButton();
            this.btnCheckDatabaseSchema = new System.Windows.Forms.ToolStripButton();
            this.btnDatabaseTransfer = new System.Windows.Forms.ToolStripButton();
            this.btnViewERDiagram = new System.Windows.Forms.ToolStripButton();
            this.btnSQLDump = new System.Windows.Forms.ToolStripButton();
            this.btnSimpleIntegrationService = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.menuStripTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCloseSQLEditor)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerProperties);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1109, 606);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1109, 682);
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
            this.statusStrip1.Size = new System.Drawing.Size(1109, 24);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusText
            // 
            this.statusText.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusText.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(877, 19);
            this.statusText.Spring = true;
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(72, 19);
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
            this.statusExecTime.Size = new System.Drawing.Size(36, 19);
            this.statusExecTime.Text = "0 ms";
            this.statusExecTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(64, 19);
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
            this.statusNumberOfRows.Size = new System.Drawing.Size(45, 19);
            this.statusNumberOfRows.Text = "0 rows";
            this.statusNumberOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.splitContainerMain);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainerProperties.Panel2.Controls.Add(this.labelProperties);
            this.splitContainerProperties.Panel2MinSize = 22;
            this.splitContainerProperties.Size = new System.Drawing.Size(1109, 606);
            this.splitContainerProperties.SplitterDistance = 893;
            this.splitContainerProperties.TabIndex = 1;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.labelConnections);
            this.splitContainerMain.Panel1.Controls.Add(this.treeView);
            this.splitContainerMain.Panel1MinSize = 22;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.btnCloseSQLEditor);
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlEditors);
            this.splitContainerMain.Size = new System.Drawing.Size(893, 606);
            this.splitContainerMain.SplitterDistance = 165;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 0;
            // 
            // labelConnections
            // 
            this.labelConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelConnections.BackColor = System.Drawing.Color.LightBlue;
            this.labelConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelConnections.Image = ((System.Drawing.Image)(resources.GetObject("labelConnections.Image")));
            this.labelConnections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelConnections.Location = new System.Drawing.Point(0, 0);
            this.labelConnections.Name = "labelConnections";
            this.labelConnections.Size = new System.Drawing.Size(165, 22);
            this.labelConnections.TabIndex = 1;
            this.labelConnections.Text = "     Connections";
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.ContextMenuStrip = this.menuStripTree;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTree;
            this.treeView.LabelEdit = true;
            this.treeView.Location = new System.Drawing.Point(0, 23);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(165, 583);
            this.treeView.TabIndex = 0;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // menuStripTree
            // 
            this.menuStripTree.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConNewConnection,
            this.menuConEditConnection,
            this.menuConDeleteConnection,
            this.menuConRefresh,
            this.menuConRefreshMetadata,
            this.menuConCreateDatabase,
            this.menuConDropDatabase,
            this.menuConTruncateDatabase,
            this.menuConEmptyDatabase,
            this.menuConCreate,
            this.menuConBackupDatabase,
            this.menuConTransferDatabase,
            this.menuConExecuteSQLFromFile,
            this.menuConShowDatabaseERDiagram,
            this.menuDiagramNew,
            this.menuDiagramOpen,
            this.menuDiagramDelete,
            this.menuTablesShowTableCounts,
            this.menuTableCreate,
            this.menuTableAlter,
            this.menuTableDrop,
            this.menuTableCount,
            this.menuTableOpen,
            this.menuTableAnalyze,
            this.menuTableGenerateSQL,
            this.menuFieldDistinct,
            this.menuFieldMax,
            this.menuFieldMin,
            this.menuFieldGroupedCounts,
            this.menuShowUIMetadata});
            this.menuStripTree.Name = "contextMenuStrip1";
            this.menuStripTree.Size = new System.Drawing.Size(266, 664);
            // 
            // menuConNewConnection
            // 
            this.menuConNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("menuConNewConnection.Image")));
            this.menuConNewConnection.Name = "menuConNewConnection";
            this.menuConNewConnection.Size = new System.Drawing.Size(265, 22);
            this.menuConNewConnection.Text = "Add New Connection...";
            // 
            // menuConEditConnection
            // 
            this.menuConEditConnection.Image = ((System.Drawing.Image)(resources.GetObject("menuConEditConnection.Image")));
            this.menuConEditConnection.Name = "menuConEditConnection";
            this.menuConEditConnection.Size = new System.Drawing.Size(265, 22);
            this.menuConEditConnection.Text = "Edit Connection...";
            // 
            // menuConDeleteConnection
            // 
            this.menuConDeleteConnection.Image = ((System.Drawing.Image)(resources.GetObject("menuConDeleteConnection.Image")));
            this.menuConDeleteConnection.Name = "menuConDeleteConnection";
            this.menuConDeleteConnection.Size = new System.Drawing.Size(265, 22);
            this.menuConDeleteConnection.Text = "Delete Connection";
            // 
            // menuConRefresh
            // 
            this.menuConRefresh.Image = ((System.Drawing.Image)(resources.GetObject("menuConRefresh.Image")));
            this.menuConRefresh.Name = "menuConRefresh";
            this.menuConRefresh.Size = new System.Drawing.Size(265, 22);
            this.menuConRefresh.Text = "Refresh Nodes";
            // 
            // menuConRefreshMetadata
            // 
            this.menuConRefreshMetadata.Image = ((System.Drawing.Image)(resources.GetObject("menuConRefreshMetadata.Image")));
            this.menuConRefreshMetadata.Name = "menuConRefreshMetadata";
            this.menuConRefreshMetadata.Size = new System.Drawing.Size(265, 22);
            this.menuConRefreshMetadata.Text = "Refresh Metadata";
            // 
            // menuConCreateDatabase
            // 
            this.menuConCreateDatabase.Name = "menuConCreateDatabase";
            this.menuConCreateDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConCreateDatabase.Text = "Create Database...";
            // 
            // menuConDropDatabase
            // 
            this.menuConDropDatabase.Name = "menuConDropDatabase";
            this.menuConDropDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConDropDatabase.Text = "Drop Database";
            // 
            // menuConTruncateDatabase
            // 
            this.menuConTruncateDatabase.Name = "menuConTruncateDatabase";
            this.menuConTruncateDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConTruncateDatabase.Text = "Truncate Database";
            // 
            // menuConEmptyDatabase
            // 
            this.menuConEmptyDatabase.Name = "menuConEmptyDatabase";
            this.menuConEmptyDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConEmptyDatabase.Text = "Empty Database";
            // 
            // menuConCreate
            // 
            this.menuConCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDBCreateTable,
            this.menuDBCreateView,
            this.menuDBCreateTrigger,
            this.menuDBCreateSProc,
            this.menuDBCreateFunction});
            this.menuConCreate.Name = "menuConCreate";
            this.menuConCreate.Size = new System.Drawing.Size(265, 22);
            this.menuConCreate.Text = "Create";
            // 
            // menuDBCreateTable
            // 
            this.menuDBCreateTable.Name = "menuDBCreateTable";
            this.menuDBCreateTable.Size = new System.Drawing.Size(188, 22);
            this.menuDBCreateTable.Text = "Table...";
            // 
            // menuDBCreateView
            // 
            this.menuDBCreateView.Name = "menuDBCreateView";
            this.menuDBCreateView.Size = new System.Drawing.Size(188, 22);
            this.menuDBCreateView.Text = "View...";
            // 
            // menuDBCreateTrigger
            // 
            this.menuDBCreateTrigger.Name = "menuDBCreateTrigger";
            this.menuDBCreateTrigger.Size = new System.Drawing.Size(188, 22);
            this.menuDBCreateTrigger.Text = "Trigger...";
            // 
            // menuDBCreateSProc
            // 
            this.menuDBCreateSProc.Name = "menuDBCreateSProc";
            this.menuDBCreateSProc.Size = new System.Drawing.Size(188, 22);
            this.menuDBCreateSProc.Text = "Stored Procedure...";
            // 
            // menuDBCreateFunction
            // 
            this.menuDBCreateFunction.Name = "menuDBCreateFunction";
            this.menuDBCreateFunction.Size = new System.Drawing.Size(188, 22);
            this.menuDBCreateFunction.Text = "Function...";
            // 
            // menuConBackupDatabase
            // 
            this.menuConBackupDatabase.Name = "menuConBackupDatabase";
            this.menuConBackupDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConBackupDatabase.Text = "Backup Database...";
            // 
            // menuConTransferDatabase
            // 
            this.menuConTransferDatabase.Name = "menuConTransferDatabase";
            this.menuConTransferDatabase.Size = new System.Drawing.Size(265, 22);
            this.menuConTransferDatabase.Text = "Transfer Database...";
            // 
            // menuConExecuteSQLFromFile
            // 
            this.menuConExecuteSQLFromFile.Name = "menuConExecuteSQLFromFile";
            this.menuConExecuteSQLFromFile.Size = new System.Drawing.Size(265, 22);
            this.menuConExecuteSQLFromFile.Text = "Execute SQL From File...";
            // 
            // menuConShowDatabaseERDiagram
            // 
            this.menuConShowDatabaseERDiagram.Name = "menuConShowDatabaseERDiagram";
            this.menuConShowDatabaseERDiagram.Size = new System.Drawing.Size(265, 22);
            this.menuConShowDatabaseERDiagram.Text = "Show Database ER Diagram";
            // 
            // menuDiagramNew
            // 
            this.menuDiagramNew.Image = ((System.Drawing.Image)(resources.GetObject("menuDiagramNew.Image")));
            this.menuDiagramNew.Name = "menuDiagramNew";
            this.menuDiagramNew.Size = new System.Drawing.Size(265, 22);
            this.menuDiagramNew.Text = "New ER Diagram...";
            // 
            // menuDiagramOpen
            // 
            this.menuDiagramOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuDiagramOpen.Image")));
            this.menuDiagramOpen.Name = "menuDiagramOpen";
            this.menuDiagramOpen.Size = new System.Drawing.Size(265, 22);
            this.menuDiagramOpen.Text = "Open ER Diagram...";
            // 
            // menuDiagramDelete
            // 
            this.menuDiagramDelete.Image = ((System.Drawing.Image)(resources.GetObject("menuDiagramDelete.Image")));
            this.menuDiagramDelete.Name = "menuDiagramDelete";
            this.menuDiagramDelete.Size = new System.Drawing.Size(265, 22);
            this.menuDiagramDelete.Text = "Delete ER Diagram";
            // 
            // menuTablesShowTableCounts
            // 
            this.menuTablesShowTableCounts.Name = "menuTablesShowTableCounts";
            this.menuTablesShowTableCounts.Size = new System.Drawing.Size(265, 22);
            this.menuTablesShowTableCounts.Text = "Show Table Row Counts on Tree";
            // 
            // menuTableCreate
            // 
            this.menuTableCreate.Image = ((System.Drawing.Image)(resources.GetObject("menuTableCreate.Image")));
            this.menuTableCreate.Name = "menuTableCreate";
            this.menuTableCreate.Size = new System.Drawing.Size(265, 22);
            this.menuTableCreate.Text = "Create Table...";
            // 
            // menuTableAlter
            // 
            this.menuTableAlter.Image = ((System.Drawing.Image)(resources.GetObject("menuTableAlter.Image")));
            this.menuTableAlter.Name = "menuTableAlter";
            this.menuTableAlter.Size = new System.Drawing.Size(265, 22);
            this.menuTableAlter.Text = "Alter Table...";
            // 
            // menuTableDrop
            // 
            this.menuTableDrop.Image = ((System.Drawing.Image)(resources.GetObject("menuTableDrop.Image")));
            this.menuTableDrop.Name = "menuTableDrop";
            this.menuTableDrop.Size = new System.Drawing.Size(265, 22);
            this.menuTableDrop.Text = "Drop Table";
            // 
            // menuTableCount
            // 
            this.menuTableCount.Image = ((System.Drawing.Image)(resources.GetObject("menuTableCount.Image")));
            this.menuTableCount.Name = "menuTableCount";
            this.menuTableCount.Size = new System.Drawing.Size(265, 22);
            this.menuTableCount.Text = "Count";
            // 
            // menuTableOpen
            // 
            this.menuTableOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuTableOpen.Image")));
            this.menuTableOpen.Name = "menuTableOpen";
            this.menuTableOpen.Size = new System.Drawing.Size(265, 22);
            this.menuTableOpen.Text = "Open Table";
            // 
            // menuTableAnalyze
            // 
            this.menuTableAnalyze.Name = "menuTableAnalyze";
            this.menuTableAnalyze.Size = new System.Drawing.Size(265, 22);
            this.menuTableAnalyze.Text = "Analyze Table";
            // 
            // menuTableGenerateSQL
            // 
            this.menuTableGenerateSQL.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTableGenSQLSelect,
            this.menuTableGenSQLInsert,
            this.menuTableGenSQLUpdate,
            this.menuTableGenSQLCreateTable,
            this.menuTableGenSQLDelete,
            this.menuTableGenSQLDump});
            this.menuTableGenerateSQL.Name = "menuTableGenerateSQL";
            this.menuTableGenerateSQL.Size = new System.Drawing.Size(265, 22);
            this.menuTableGenerateSQL.Text = "Generate SQL";
            // 
            // menuTableGenSQLSelect
            // 
            this.menuTableGenSQLSelect.Name = "menuTableGenSQLSelect";
            this.menuTableGenSQLSelect.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLSelect.Text = "Select";
            // 
            // menuTableGenSQLInsert
            // 
            this.menuTableGenSQLInsert.Name = "menuTableGenSQLInsert";
            this.menuTableGenSQLInsert.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLInsert.Text = "Insert";
            // 
            // menuTableGenSQLUpdate
            // 
            this.menuTableGenSQLUpdate.Name = "menuTableGenSQLUpdate";
            this.menuTableGenSQLUpdate.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLUpdate.Text = "Update";
            // 
            // menuTableGenSQLCreateTable
            // 
            this.menuTableGenSQLCreateTable.Name = "menuTableGenSQLCreateTable";
            this.menuTableGenSQLCreateTable.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLCreateTable.Text = "Create Table";
            // 
            // menuTableGenSQLDelete
            // 
            this.menuTableGenSQLDelete.Name = "menuTableGenSQLDelete";
            this.menuTableGenSQLDelete.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLDelete.Text = "Delete";
            // 
            // menuTableGenSQLDump
            // 
            this.menuTableGenSQLDump.Name = "menuTableGenSQLDump";
            this.menuTableGenSQLDump.Size = new System.Drawing.Size(234, 22);
            this.menuTableGenSQLDump.Text = "Dump Schema && Metadata";
            // 
            // menuFieldDistinct
            // 
            this.menuFieldDistinct.Image = ((System.Drawing.Image)(resources.GetObject("menuFieldDistinct.Image")));
            this.menuFieldDistinct.Name = "menuFieldDistinct";
            this.menuFieldDistinct.Size = new System.Drawing.Size(265, 22);
            this.menuFieldDistinct.Text = "Distinct";
            // 
            // menuFieldMax
            // 
            this.menuFieldMax.Image = ((System.Drawing.Image)(resources.GetObject("menuFieldMax.Image")));
            this.menuFieldMax.Name = "menuFieldMax";
            this.menuFieldMax.Size = new System.Drawing.Size(265, 22);
            this.menuFieldMax.Text = "Max()";
            // 
            // menuFieldMin
            // 
            this.menuFieldMin.Image = ((System.Drawing.Image)(resources.GetObject("menuFieldMin.Image")));
            this.menuFieldMin.Name = "menuFieldMin";
            this.menuFieldMin.Size = new System.Drawing.Size(265, 22);
            this.menuFieldMin.Text = "Min()";
            // 
            // menuFieldGroupedCounts
            // 
            this.menuFieldGroupedCounts.Image = ((System.Drawing.Image)(resources.GetObject("menuFieldGroupedCounts.Image")));
            this.menuFieldGroupedCounts.Name = "menuFieldGroupedCounts";
            this.menuFieldGroupedCounts.Size = new System.Drawing.Size(265, 22);
            this.menuFieldGroupedCounts.Text = "Grouped Counts";
            // 
            // menuShowUIMetadata
            // 
            this.menuShowUIMetadata.Name = "menuShowUIMetadata";
            this.menuShowUIMetadata.Size = new System.Drawing.Size(265, 22);
            this.menuShowUIMetadata.Text = "Show/Edit UI Metadata";
            // 
            // imageListTree
            // 
            this.imageListTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnCloseSQLEditor
            // 
            this.btnCloseSQLEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseSQLEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseSQLEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseSQLEditor.Image")));
            this.btnCloseSQLEditor.Location = new System.Drawing.Point(696, 3);
            this.btnCloseSQLEditor.Name = "btnCloseSQLEditor";
            this.btnCloseSQLEditor.Size = new System.Drawing.Size(19, 20);
            this.btnCloseSQLEditor.TabIndex = 1;
            this.btnCloseSQLEditor.TabStop = false;
            // 
            // tabControlEditors
            // 
            this.tabControlEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEditors.ImageList = this.imageListTree;
            this.tabControlEditors.ItemSize = new System.Drawing.Size(100, 24);
            this.tabControlEditors.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditors.Name = "tabControlEditors";
            this.tabControlEditors.SelectedIndex = 0;
            this.tabControlEditors.Size = new System.Drawing.Size(723, 606);
            this.tabControlEditors.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlEditors.TabIndex = 1;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(3, 28);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(206, 575);
            this.propertyGrid.TabIndex = 3;
            // 
            // labelProperties
            // 
            this.labelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProperties.BackColor = System.Drawing.Color.LightBlue;
            this.labelProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProperties.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelProperties.Image = ((System.Drawing.Image)(resources.GetObject("labelProperties.Image")));
            this.labelProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelProperties.Location = new System.Drawing.Point(2, 3);
            this.labelProperties.Name = "labelProperties";
            this.labelProperties.Size = new System.Drawing.Size(207, 22);
            this.labelProperties.TabIndex = 2;
            this.labelProperties.Text = "     Properties";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1109, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewConnection,
            this.menuOpenConnectionsFile,
            this.toolStripSeparator3,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripMenuItem3,
            this.menuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuNewConnection
            // 
            this.menuNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("menuNewConnection.Image")));
            this.menuNewConnection.Name = "menuNewConnection";
            this.menuNewConnection.Size = new System.Drawing.Size(225, 24);
            this.menuNewConnection.Text = "New Connection...";
            // 
            // menuOpenConnectionsFile
            // 
            this.menuOpenConnectionsFile.Name = "menuOpenConnectionsFile";
            this.menuOpenConnectionsFile.Size = new System.Drawing.Size(225, 24);
            this.menuOpenConnectionsFile.Text = "Open Connections File...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // menuOpen
            // 
            this.menuOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuOpen.Image")));
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(225, 24);
            this.menuOpen.Text = "Open...";
            // 
            // menuSave
            // 
            this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(225, 24);
            this.menuSave.Text = "Save";
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveAs.Image")));
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(225, 24);
            this.menuSaveAs.Text = "Save As...";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(222, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(225, 24);
            this.menuExit.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUndo,
            this.menuRedo,
            this.toolStripMenuItem4,
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.menuSelectAll,
            this.toolStripMenuItem5,
            this.menuFind,
            this.menuReplace});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuUndo
            // 
            this.menuUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuUndo.Image")));
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuUndo.Size = new System.Drawing.Size(184, 24);
            this.menuUndo.Text = "Undo";
            // 
            // menuRedo
            // 
            this.menuRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuRedo.Image")));
            this.menuRedo.Name = "menuRedo";
            this.menuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuRedo.Size = new System.Drawing.Size(184, 24);
            this.menuRedo.Text = "Redo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(181, 6);
            // 
            // menuCut
            // 
            this.menuCut.Image = ((System.Drawing.Image)(resources.GetObject("menuCut.Image")));
            this.menuCut.Name = "menuCut";
            this.menuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuCut.Size = new System.Drawing.Size(184, 24);
            this.menuCut.Text = "Cut";
            // 
            // menuCopy
            // 
            this.menuCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuCopy.Image")));
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopy.Size = new System.Drawing.Size(184, 24);
            this.menuCopy.Text = "Copy";
            // 
            // menuPaste
            // 
            this.menuPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuPaste.Image")));
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuPaste.Size = new System.Drawing.Size(184, 24);
            this.menuPaste.Text = "Paste";
            // 
            // menuSelectAll
            // 
            this.menuSelectAll.Name = "menuSelectAll";
            this.menuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectAll.Size = new System.Drawing.Size(184, 24);
            this.menuSelectAll.Text = "Select All";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(181, 6);
            // 
            // menuFind
            // 
            this.menuFind.Image = ((System.Drawing.Image)(resources.GetObject("menuFind.Image")));
            this.menuFind.Name = "menuFind";
            this.menuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuFind.Size = new System.Drawing.Size(184, 24);
            this.menuFind.Text = "Find...";
            // 
            // menuReplace
            // 
            this.menuReplace.Image = ((System.Drawing.Image)(resources.GetObject("menuReplace.Image")));
            this.menuReplace.Name = "menuReplace";
            this.menuReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuReplace.Size = new System.Drawing.Size(184, 24);
            this.menuReplace.Text = "Replace...";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsCodeGenerator,
            this.menuToolsCheckDatabaseSchema,
            this.menuToolsDBTransfer,
            this.menuToolsViewERDiagram,
            this.menuToolsSQLDump,
            this.menuToolsSimpleIntegrationService,
            this.menuToolsCopyTreeData,
            this.menuToolsCompareDatabases,
            this.menuToolsCompareDirectories,
            this.toolStripMenuItem2,
            this.quickScriptToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // menuToolsCodeGenerator
            // 
            this.menuToolsCodeGenerator.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsCodeGenerator.Image")));
            this.menuToolsCodeGenerator.Name = "menuToolsCodeGenerator";
            this.menuToolsCodeGenerator.Size = new System.Drawing.Size(245, 24);
            this.menuToolsCodeGenerator.Text = "Code Generator...";
            // 
            // menuToolsCheckDatabaseSchema
            // 
            this.menuToolsCheckDatabaseSchema.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsCheckDatabaseSchema.Image")));
            this.menuToolsCheckDatabaseSchema.Name = "menuToolsCheckDatabaseSchema";
            this.menuToolsCheckDatabaseSchema.Size = new System.Drawing.Size(245, 24);
            this.menuToolsCheckDatabaseSchema.Text = "Check Database Schema...";
            // 
            // menuToolsDBTransfer
            // 
            this.menuToolsDBTransfer.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsDBTransfer.Image")));
            this.menuToolsDBTransfer.Name = "menuToolsDBTransfer";
            this.menuToolsDBTransfer.Size = new System.Drawing.Size(245, 24);
            this.menuToolsDBTransfer.Text = "Database Transfer...";
            // 
            // menuToolsViewERDiagram
            // 
            this.menuToolsViewERDiagram.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsViewERDiagram.Image")));
            this.menuToolsViewERDiagram.Name = "menuToolsViewERDiagram";
            this.menuToolsViewERDiagram.Size = new System.Drawing.Size(245, 24);
            this.menuToolsViewERDiagram.Text = "View ER Diagram...";
            // 
            // menuToolsSQLDump
            // 
            this.menuToolsSQLDump.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsSQLDump.Image")));
            this.menuToolsSQLDump.Name = "menuToolsSQLDump";
            this.menuToolsSQLDump.Size = new System.Drawing.Size(245, 24);
            this.menuToolsSQLDump.Text = "SQL Dump...";
            // 
            // menuToolsSimpleIntegrationService
            // 
            this.menuToolsSimpleIntegrationService.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsSimpleIntegrationService.Image")));
            this.menuToolsSimpleIntegrationService.Name = "menuToolsSimpleIntegrationService";
            this.menuToolsSimpleIntegrationService.Size = new System.Drawing.Size(245, 24);
            this.menuToolsSimpleIntegrationService.Text = "Simple Integration Service...";
            // 
            // menuToolsCopyTreeData
            // 
            this.menuToolsCopyTreeData.Name = "menuToolsCopyTreeData";
            this.menuToolsCopyTreeData.Size = new System.Drawing.Size(245, 24);
            this.menuToolsCopyTreeData.Text = "Copy Tree Data...";
            // 
            // menuToolsCompareDatabases
            // 
            this.menuToolsCompareDatabases.Name = "menuToolsCompareDatabases";
            this.menuToolsCompareDatabases.Size = new System.Drawing.Size(245, 24);
            this.menuToolsCompareDatabases.Text = "Compare Databases...";
            // 
            // menuToolsCompareDirectories
            // 
            this.menuToolsCompareDirectories.Name = "menuToolsCompareDirectories";
            this.menuToolsCompareDirectories.Size = new System.Drawing.Size(245, 24);
            this.menuToolsCompareDirectories.Text = "Compare Directories...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(242, 6);
            // 
            // quickScriptToolStripMenuItem
            // 
            this.quickScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsQScriptDeleteFromTables,
            this.menuToolsQScriptSelectCountsFromTables,
            this.menuToolsQScriptForEachTable,
            this.menuToolsQScriptForEachField,
            this.menuToolsQScriptCalculateOptDataLen});
            this.quickScriptToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quickScriptToolStripMenuItem.Image")));
            this.quickScriptToolStripMenuItem.Name = "quickScriptToolStripMenuItem";
            this.quickScriptToolStripMenuItem.Size = new System.Drawing.Size(245, 24);
            this.quickScriptToolStripMenuItem.Text = "Quick Script";
            // 
            // menuToolsQScriptDeleteFromTables
            // 
            this.menuToolsQScriptDeleteFromTables.Name = "menuToolsQScriptDeleteFromTables";
            this.menuToolsQScriptDeleteFromTables.Size = new System.Drawing.Size(272, 24);
            this.menuToolsQScriptDeleteFromTables.Text = "Delete From Tables";
            // 
            // menuToolsQScriptSelectCountsFromTables
            // 
            this.menuToolsQScriptSelectCountsFromTables.Name = "menuToolsQScriptSelectCountsFromTables";
            this.menuToolsQScriptSelectCountsFromTables.Size = new System.Drawing.Size(272, 24);
            this.menuToolsQScriptSelectCountsFromTables.Text = "Select Count(*) From Tables";
            // 
            // menuToolsQScriptForEachTable
            // 
            this.menuToolsQScriptForEachTable.Name = "menuToolsQScriptForEachTable";
            this.menuToolsQScriptForEachTable.Size = new System.Drawing.Size(272, 24);
            this.menuToolsQScriptForEachTable.Text = "For Each Table";
            // 
            // menuToolsQScriptForEachField
            // 
            this.menuToolsQScriptForEachField.Name = "menuToolsQScriptForEachField";
            this.menuToolsQScriptForEachField.Size = new System.Drawing.Size(272, 24);
            this.menuToolsQScriptForEachField.Text = "For Each Field";
            // 
            // menuToolsQScriptCalculateOptDataLen
            // 
            this.menuToolsQScriptCalculateOptDataLen.Name = "menuToolsQScriptCalculateOptDataLen";
            this.menuToolsQScriptCalculateOptDataLen.Size = new System.Drawing.Size(272, 24);
            this.menuToolsQScriptCalculateOptDataLen.Text = "Calculate Optimal Data Lengths";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpScriptingTest,
            this.toolStripMenuItem1,
            this.menuHelpAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menuHelpScriptingTest
            // 
            this.menuHelpScriptingTest.Image = ((System.Drawing.Image)(resources.GetObject("menuHelpScriptingTest.Image")));
            this.menuHelpScriptingTest.Name = "menuHelpScriptingTest";
            this.menuHelpScriptingTest.Size = new System.Drawing.Size(322, 24);
            this.menuHelpScriptingTest.Text = "Çınar Scripting Test && Learning Center...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(319, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(322, 24);
            this.menuHelpAbout.Text = "About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator5,
            this.btnNewConnection,
            this.btnEditConnection,
            this.btnDeleteConnection,
            this.toolStripSeparator1,
            this.cbActiveConnection,
            this.btnAddEditor,
            this.btnExecuteSQL,
            this.btnExecuteScript,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.btnCodeGenerator,
            this.btnCheckDatabaseSchema,
            this.btnDatabaseTransfer,
            this.btnViewERDiagram,
            this.btnSQLDump,
            this.btnSimpleIntegrationService});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1109, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // cbActiveConnection
            // 
            this.cbActiveConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActiveConnection.Name = "cbActiveConnection";
            this.cbActiveConnection.Size = new System.Drawing.Size(200, 25);
            this.cbActiveConnection.SelectedIndexChanged += new System.EventHandler(this.cbActiveConnection_SelectedIndexChanged);
            // 
            // btnAddEditor
            // 
            this.btnAddEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEditor.Image")));
            this.btnAddEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEditor.Name = "btnAddEditor";
            this.btnAddEditor.Size = new System.Drawing.Size(23, 22);
            this.btnAddEditor.Text = "Add New SQL Editor";
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
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
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
            // btnViewERDiagram
            // 
            this.btnViewERDiagram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewERDiagram.Image = ((System.Drawing.Image)(resources.GetObject("btnViewERDiagram.Image")));
            this.btnViewERDiagram.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewERDiagram.Name = "btnViewERDiagram";
            this.btnViewERDiagram.Size = new System.Drawing.Size(23, 22);
            this.btnViewERDiagram.Text = "View ER Diagram";
            // 
            // btnSQLDump
            // 
            this.btnSQLDump.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSQLDump.Image = ((System.Drawing.Image)(resources.GetObject("btnSQLDump.Image")));
            this.btnSQLDump.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSQLDump.Name = "btnSQLDump";
            this.btnSQLDump.Size = new System.Drawing.Size(23, 22);
            this.btnSQLDump.Text = "SQL Dump";
            // 
            // btnSimpleIntegrationService
            // 
            this.btnSimpleIntegrationService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSimpleIntegrationService.Image = ((System.Drawing.Image)(resources.GetObject("btnSimpleIntegrationService.Image")));
            this.btnSimpleIntegrationService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSimpleIntegrationService.Name = "btnSimpleIntegrationService";
            this.btnSimpleIntegrationService.Size = new System.Drawing.Size(23, 22);
            this.btnSimpleIntegrationService.Text = "Simple Integration Service";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 682);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
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
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
            this.splitContainerProperties.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.menuStripTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCloseSQLEditor)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewConnection;
        private System.Windows.Forms.ContextMenuStrip menuStripTree;
        private System.Windows.Forms.ToolStripMenuItem menuTableCount;
        private System.Windows.Forms.ToolStripMenuItem menuFieldDistinct;
        private System.Windows.Forms.ToolStripMenuItem menuTableOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFieldMax;
        private System.Windows.Forms.ToolStripMenuItem menuFieldMin;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCodeGenerator;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCheckDatabaseSchema;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNewConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExecuteSQL;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenerateSQL;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLSelect;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLInsert;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuConRefresh;
        private System.Windows.Forms.ToolStripMenuItem menuFieldGroupedCounts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCodeGenerator;
        private System.Windows.Forms.ToolStripButton btnCheckDatabaseSchema;
        private System.Windows.Forms.ToolStripMenuItem menuToolsDBTransfer;
        private System.Windows.Forms.ToolStripMenuItem menuTableDrop;
        private System.Windows.Forms.ToolStripButton btnExecuteScript;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnDatabaseTransfer;
        private System.Windows.Forms.ToolStripButton btnEditConnection;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripButton btnDeleteConnection;
        private System.Windows.Forms.ToolStripMenuItem menuConEditConnection;
        private System.Windows.Forms.ToolStripMenuItem menuConDeleteConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusExecTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statusNumberOfRows;
        private System.Windows.Forms.ToolStripMenuItem menuToolsViewERDiagram;
        private System.Windows.Forms.ToolStripMenuItem menuDiagramOpen;
        private System.Windows.Forms.ToolStripMenuItem menuDiagramNew;
        private System.Windows.Forms.ToolStripMenuItem menuConNewConnection;
        private System.Windows.Forms.ToolStripMenuItem menuConRefreshMetadata;
        private System.Windows.Forms.ToolStripMenuItem menuDiagramDelete;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCopyTreeData;
        private System.Windows.Forms.ToolStripMenuItem menuTablesShowTableCounts;
        private System.Windows.Forms.ToolStripMenuItem menuToolsSimpleIntegrationService;
        private System.Windows.Forms.ToolStripMenuItem quickScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptDeleteFromTables;
        private System.Windows.Forms.ToolStripMenuItem menuHelpScriptingTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptSelectCountsFromTables;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCompareDatabases;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuShowUIMetadata;
        private System.Windows.Forms.ToolStripMenuItem menuTableAnalyze;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLDump;
        private System.Windows.Forms.ToolStripMenuItem menuOpenConnectionsFile;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptForEachTable;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptForEachField;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCompareDirectories;
        private System.Windows.Forms.ToolStripMenuItem menuTableCreate;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripComboBox cbActiveConnection;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuUndo;
        private System.Windows.Forms.ToolStripMenuItem menuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuCut;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuPaste;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem menuFind;
        private System.Windows.Forms.ToolStripMenuItem menuReplace;
        private System.Windows.Forms.ToolStripButton btnViewERDiagram;
        private System.Windows.Forms.ToolStripButton btnSimpleIntegrationService;
        private System.Windows.Forms.ToolStripMenuItem menuToolsSQLDump;
        private System.Windows.Forms.ToolStripButton btnSQLDump;
        private System.Windows.Forms.PictureBox btnCloseSQLEditor;
        private System.Windows.Forms.TabControl tabControlEditors;
        private System.Windows.Forms.ToolStripButton btnAddEditor;
        private System.Windows.Forms.ToolStripMenuItem menuConCreateDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConDropDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConTruncateDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConEmptyDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConCreate;
        private System.Windows.Forms.ToolStripMenuItem menuDBCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuDBCreateView;
        private System.Windows.Forms.ToolStripMenuItem menuDBCreateTrigger;
        private System.Windows.Forms.ToolStripMenuItem menuDBCreateSProc;
        private System.Windows.Forms.ToolStripMenuItem menuDBCreateFunction;
        private System.Windows.Forms.ToolStripMenuItem menuConBackupDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConTransferDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuConExecuteSQLFromFile;
        private System.Windows.Forms.ToolStripMenuItem menuConShowDatabaseERDiagram;
        private System.Windows.Forms.ToolStripMenuItem menuTableAlter;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLDelete;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptCalculateOptDataLen;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelConnections;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Label labelProperties;

    }
}