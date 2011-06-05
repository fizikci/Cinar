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
            this.panelConnections = new System.Windows.Forms.Panel();
            this.labelConnections = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.menuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuConShowHiddenConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConEditConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConDeleteConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConHideConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConRefreshMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoreDatabaseOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConEmptyDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConTruncateDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConDropDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConCreateDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConBackupDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConTransferDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConExecuteSQLFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateSProc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBCreateFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConShowDatabaseERDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiagramDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTablesShowTableCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableCount = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableOpenWithFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenerateSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableGenSQLDump = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColumnDistinct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColumnMax = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColumnMin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColumnGroupedCounts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColumnDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowUIMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIndexCreateIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIndexEditIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIndexDropIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.tabControlEditors = new Cinar.DBTools.Controls.MyTabControl();
            this.menuStripEditorTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuTabClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTabCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTabCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuTabCompareWithOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuTabCopyFullPath = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTabOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.labelProperties = new System.Windows.Forms.Label();
            this.treeCodeGen = new System.Windows.Forms.TreeView();
            this.menuStripCodeGen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddExistingItems = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddExistingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGenerateCode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowGeneratedCode = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCodeGeneratorExplorer = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenConnectionsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveConnectionsAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuNewCinarSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenCinarSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveCinarSolution = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuBeautifySQL = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExecuteSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExecuteScript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsCodeGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsGenerateTablesFromReflectedMetadata = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuToolsQScriptForEachColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsQScriptCalculateOptDataLen = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpScriptingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.btnNewConnection = new System.Windows.Forms.ToolStripButton();
            this.btnEditConnection = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteConnection = new System.Windows.Forms.ToolStripButton();
            this.cbActiveConnection = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
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
            this.menuCopy2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCut2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectAll2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
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
            this.panelConnections.SuspendLayout();
            this.menuStripTree.SuspendLayout();
            this.menuStripEditorTabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelProperties.SuspendLayout();
            this.menuStripCodeGen.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
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
            this.toolStripContainer1.ContentPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStripContainer1.ContentPanel.BackgroundImage")));
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerProperties);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1030, 604);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1030, 676);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("statusStrip1.BackgroundImage")));
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText,
            this.toolStripStatusLabel2,
            this.statusExecTime,
            this.toolStripStatusLabel4,
            this.statusNumberOfRows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1030, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusText
            // 
            this.statusText.ForeColor = System.Drawing.Color.White;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(806, 17);
            this.statusText.Spring = true;
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(72, 17);
            this.toolStripStatusLabel2.Text = "  Exec. Time:";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusExecTime
            // 
            this.statusExecTime.ForeColor = System.Drawing.Color.White;
            this.statusExecTime.Name = "statusExecTime";
            this.statusExecTime.Size = new System.Drawing.Size(32, 17);
            this.statusExecTime.Text = "0 ms";
            this.statusExecTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel4.Text = "  Returned:";
            this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusNumberOfRows
            // 
            this.statusNumberOfRows.ForeColor = System.Drawing.Color.White;
            this.statusNumberOfRows.Name = "statusNumberOfRows";
            this.statusNumberOfRows.Size = new System.Drawing.Size(41, 17);
            this.statusNumberOfRows.Text = "0 rows";
            this.statusNumberOfRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainerProperties.BackgroundImage")));
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.Location = new System.Drawing.Point(4, 4);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.splitContainerMain);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerProperties.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerProperties.Panel2MinSize = 22;
            this.splitContainerProperties.Size = new System.Drawing.Size(1022, 596);
            this.splitContainerProperties.SplitterDistance = 813;
            this.splitContainerProperties.TabIndex = 1;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainerMain.BackgroundImage")));
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerMain.Panel1.Controls.Add(this.panelConnections);
            this.splitContainerMain.Panel1.Controls.Add(this.treeView);
            this.splitContainerMain.Panel1MinSize = 22;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainerMain.Panel2.BackgroundImage")));
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlEditors);
            this.splitContainerMain.Size = new System.Drawing.Size(813, 596);
            this.splitContainerMain.SplitterDistance = 199;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelConnections
            // 
            this.panelConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConnections.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelConnections.BackgroundImage")));
            this.panelConnections.Controls.Add(this.labelConnections);
            this.panelConnections.Location = new System.Drawing.Point(0, 0);
            this.panelConnections.Name = "panelConnections";
            this.panelConnections.Size = new System.Drawing.Size(199, 20);
            this.panelConnections.TabIndex = 2;
            // 
            // labelConnections
            // 
            this.labelConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelConnections.BackColor = System.Drawing.Color.Transparent;
            this.labelConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelConnections.Image = ((System.Drawing.Image)(resources.GetObject("labelConnections.Image")));
            this.labelConnections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelConnections.Location = new System.Drawing.Point(0, 0);
            this.labelConnections.Name = "labelConnections";
            this.labelConnections.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.labelConnections.Size = new System.Drawing.Size(199, 20);
            this.labelConnections.TabIndex = 1;
            this.labelConnections.Text = "      Connections";
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.ContextMenuStrip = this.menuStripTree;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTree;
            this.treeView.Indent = 18;
            this.treeView.Location = new System.Drawing.Point(0, 19);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(199, 579);
            this.treeView.TabIndex = 0;
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // menuStripTree
            // 
            this.menuStripTree.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConShowHiddenConnections,
            this.menuConNewConnection,
            this.menuConEditConnection,
            this.menuConDeleteConnection,
            this.menuConHideConnection,
            this.menuConRefresh,
            this.menuConRefreshMetadata,
            this.menuMoreDatabaseOperations,
            this.menuConCreate,
            this.menuConShowDatabaseERDiagram,
            this.menuDiagramNew,
            this.menuDiagramOpen,
            this.menuDiagramDelete,
            this.menuTablesShowTableCounts,
            this.menuTableCreate,
            this.menuTableDrop,
            this.menuTableCount,
            this.menuTableOpen,
            this.menuTableOpenWithFilter,
            this.menuTableAnalyze,
            this.menuTableGenerateSQL,
            this.menuColumnDistinct,
            this.menuColumnMax,
            this.menuColumnMin,
            this.menuColumnGroupedCounts,
            this.menuColumnDrop,
            this.menuShowUIMetadata,
            this.menuIndexCreateIndex,
            this.menuIndexEditIndex,
            this.menuIndexDropIndex});
            this.menuStripTree.Name = "contextMenuStrip1";
            this.menuStripTree.Size = new System.Drawing.Size(266, 664);
            // 
            // menuConShowHiddenConnections
            // 
            this.menuConShowHiddenConnections.Name = "menuConShowHiddenConnections";
            this.menuConShowHiddenConnections.Size = new System.Drawing.Size(265, 22);
            this.menuConShowHiddenConnections.Text = "Show Hidden Connections";
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
            // menuConHideConnection
            // 
            this.menuConHideConnection.Name = "menuConHideConnection";
            this.menuConHideConnection.Size = new System.Drawing.Size(265, 22);
            this.menuConHideConnection.Text = "Hide Connection";
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
            // menuMoreDatabaseOperations
            // 
            this.menuMoreDatabaseOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConEmptyDatabase,
            this.menuConTruncateDatabase,
            this.menuConDropDatabase,
            this.menuConCreateDatabase,
            this.menuConBackupDatabase,
            this.menuConTransferDatabase,
            this.menuConExecuteSQLFromFile});
            this.menuMoreDatabaseOperations.Name = "menuMoreDatabaseOperations";
            this.menuMoreDatabaseOperations.Size = new System.Drawing.Size(265, 22);
            this.menuMoreDatabaseOperations.Text = "More Database Operations";
            // 
            // menuConEmptyDatabase
            // 
            this.menuConEmptyDatabase.Name = "menuConEmptyDatabase";
            this.menuConEmptyDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConEmptyDatabase.Text = "Empty Database";
            // 
            // menuConTruncateDatabase
            // 
            this.menuConTruncateDatabase.Name = "menuConTruncateDatabase";
            this.menuConTruncateDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConTruncateDatabase.Text = "Truncate Database";
            // 
            // menuConDropDatabase
            // 
            this.menuConDropDatabase.Name = "menuConDropDatabase";
            this.menuConDropDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConDropDatabase.Text = "Drop Database";
            // 
            // menuConCreateDatabase
            // 
            this.menuConCreateDatabase.Name = "menuConCreateDatabase";
            this.menuConCreateDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConCreateDatabase.Text = "Create Database...";
            // 
            // menuConBackupDatabase
            // 
            this.menuConBackupDatabase.Name = "menuConBackupDatabase";
            this.menuConBackupDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConBackupDatabase.Text = "Backup Database...";
            // 
            // menuConTransferDatabase
            // 
            this.menuConTransferDatabase.Name = "menuConTransferDatabase";
            this.menuConTransferDatabase.Size = new System.Drawing.Size(213, 22);
            this.menuConTransferDatabase.Text = "Transfer Database...";
            // 
            // menuConExecuteSQLFromFile
            // 
            this.menuConExecuteSQLFromFile.Name = "menuConExecuteSQLFromFile";
            this.menuConExecuteSQLFromFile.Size = new System.Drawing.Size(213, 22);
            this.menuConExecuteSQLFromFile.Text = "Execute SQL From File...";
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
            // menuTableOpenWithFilter
            // 
            this.menuTableOpenWithFilter.Name = "menuTableOpenWithFilter";
            this.menuTableOpenWithFilter.Size = new System.Drawing.Size(265, 22);
            this.menuTableOpenWithFilter.Text = "Open Table with Filter...";
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
            this.menuTableGenSQLCreateTable.Text = "Create";
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
            // menuColumnDistinct
            // 
            this.menuColumnDistinct.Image = ((System.Drawing.Image)(resources.GetObject("menuColumnDistinct.Image")));
            this.menuColumnDistinct.Name = "menuColumnDistinct";
            this.menuColumnDistinct.Size = new System.Drawing.Size(265, 22);
            this.menuColumnDistinct.Text = "Distinct";
            // 
            // menuColumnMax
            // 
            this.menuColumnMax.Image = ((System.Drawing.Image)(resources.GetObject("menuColumnMax.Image")));
            this.menuColumnMax.Name = "menuColumnMax";
            this.menuColumnMax.Size = new System.Drawing.Size(265, 22);
            this.menuColumnMax.Text = "Max()";
            // 
            // menuColumnMin
            // 
            this.menuColumnMin.Image = ((System.Drawing.Image)(resources.GetObject("menuColumnMin.Image")));
            this.menuColumnMin.Name = "menuColumnMin";
            this.menuColumnMin.Size = new System.Drawing.Size(265, 22);
            this.menuColumnMin.Text = "Min()";
            // 
            // menuColumnGroupedCounts
            // 
            this.menuColumnGroupedCounts.Image = ((System.Drawing.Image)(resources.GetObject("menuColumnGroupedCounts.Image")));
            this.menuColumnGroupedCounts.Name = "menuColumnGroupedCounts";
            this.menuColumnGroupedCounts.Size = new System.Drawing.Size(265, 22);
            this.menuColumnGroupedCounts.Text = "Grouped Counts";
            // 
            // menuColumnDrop
            // 
            this.menuColumnDrop.Image = ((System.Drawing.Image)(resources.GetObject("menuColumnDrop.Image")));
            this.menuColumnDrop.Name = "menuColumnDrop";
            this.menuColumnDrop.Size = new System.Drawing.Size(265, 22);
            this.menuColumnDrop.Text = "Drop Column";
            // 
            // menuShowUIMetadata
            // 
            this.menuShowUIMetadata.Name = "menuShowUIMetadata";
            this.menuShowUIMetadata.Size = new System.Drawing.Size(265, 22);
            this.menuShowUIMetadata.Text = "Generate UI Metadata";
            // 
            // menuIndexCreateIndex
            // 
            this.menuIndexCreateIndex.Image = ((System.Drawing.Image)(resources.GetObject("menuIndexCreateIndex.Image")));
            this.menuIndexCreateIndex.Name = "menuIndexCreateIndex";
            this.menuIndexCreateIndex.Size = new System.Drawing.Size(265, 22);
            this.menuIndexCreateIndex.Text = "Create Index...";
            // 
            // menuIndexEditIndex
            // 
            this.menuIndexEditIndex.Image = ((System.Drawing.Image)(resources.GetObject("menuIndexEditIndex.Image")));
            this.menuIndexEditIndex.Name = "menuIndexEditIndex";
            this.menuIndexEditIndex.Size = new System.Drawing.Size(265, 22);
            this.menuIndexEditIndex.Text = "Edit Index...";
            // 
            // menuIndexDropIndex
            // 
            this.menuIndexDropIndex.Image = ((System.Drawing.Image)(resources.GetObject("menuIndexDropIndex.Image")));
            this.menuIndexDropIndex.Name = "menuIndexDropIndex";
            this.menuIndexDropIndex.Size = new System.Drawing.Size(265, 22);
            this.menuIndexDropIndex.Text = "Drop Index";
            // 
            // imageListTree
            // 
            this.imageListTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabControlEditors
            // 
            this.tabControlEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlEditors.ContextMenuStrip = this.menuStripEditorTabs;
            this.tabControlEditors.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlEditors.ImageList = this.imageListTabs;
            this.tabControlEditors.ItemSize = new System.Drawing.Size(100, 22);
            this.tabControlEditors.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditors.Name = "tabControlEditors";
            this.tabControlEditors.Padding = new System.Drawing.Point(0, 0);
            this.tabControlEditors.SelectedIndex = 0;
            this.tabControlEditors.ShowToolTips = true;
            this.tabControlEditors.Size = new System.Drawing.Size(612, 598);
            this.tabControlEditors.TabIndex = 1;
            // 
            // menuStripEditorTabs
            // 
            this.menuStripEditorTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTabSave,
            this.toolStripMenuItem7,
            this.menuTabClose,
            this.menuTabCloseAll,
            this.menuTabCloseAllButThis,
            this.toolStripMenuItem11,
            this.menuCut2,
            this.menuCopy2,
            this.menuPaste2,
            this.menuSelectAll2,
            this.toolStripMenuItem8,
            this.menuTabCompareWithOriginal,
            this.toolStripMenuItem9,
            this.menuTabCopyFullPath,
            this.menuTabOpenContainingFolder});
            this.menuStripEditorTabs.Name = "menuStripEditorTabs";
            this.menuStripEditorTabs.Size = new System.Drawing.Size(211, 270);
            // 
            // menuTabSave
            // 
            this.menuTabSave.Image = ((System.Drawing.Image)(resources.GetObject("menuTabSave.Image")));
            this.menuTabSave.Name = "menuTabSave";
            this.menuTabSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuTabSave.Size = new System.Drawing.Size(210, 22);
            this.menuTabSave.Text = "Save";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(207, 6);
            // 
            // menuTabClose
            // 
            this.menuTabClose.Name = "menuTabClose";
            this.menuTabClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.menuTabClose.Size = new System.Drawing.Size(210, 22);
            this.menuTabClose.Text = "Close";
            // 
            // menuTabCloseAll
            // 
            this.menuTabCloseAll.Name = "menuTabCloseAll";
            this.menuTabCloseAll.Size = new System.Drawing.Size(210, 22);
            this.menuTabCloseAll.Text = "Close All";
            // 
            // menuTabCloseAllButThis
            // 
            this.menuTabCloseAllButThis.Name = "menuTabCloseAllButThis";
            this.menuTabCloseAllButThis.Size = new System.Drawing.Size(210, 22);
            this.menuTabCloseAllButThis.Text = "Close All But This";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(207, 6);
            // 
            // menuTabCompareWithOriginal
            // 
            this.menuTabCompareWithOriginal.Image = ((System.Drawing.Image)(resources.GetObject("menuTabCompareWithOriginal.Image")));
            this.menuTabCompareWithOriginal.Name = "menuTabCompareWithOriginal";
            this.menuTabCompareWithOriginal.Size = new System.Drawing.Size(210, 22);
            this.menuTabCompareWithOriginal.Text = "Compare With Original...";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(207, 6);
            // 
            // menuTabCopyFullPath
            // 
            this.menuTabCopyFullPath.Name = "menuTabCopyFullPath";
            this.menuTabCopyFullPath.Size = new System.Drawing.Size(210, 22);
            this.menuTabCopyFullPath.Text = "Copy Full Path";
            // 
            // menuTabOpenContainingFolder
            // 
            this.menuTabOpenContainingFolder.Name = "menuTabOpenContainingFolder";
            this.menuTabOpenContainingFolder.Size = new System.Drawing.Size(210, 22);
            this.menuTabOpenContainingFolder.Text = "Open Containing Folder...";
            // 
            // imageListTabs
            // 
            this.imageListTabs.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListTabs.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid);
            this.splitContainer1.Panel1.Controls.Add(this.panelProperties);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeCodeGen);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(205, 596);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 5;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(202, 228);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            this.propertyGrid.SelectedObjectsChanged += new System.EventHandler(this.propertyGrid_SelectedObjectsChanged);
            // 
            // panelProperties
            // 
            this.panelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelProperties.BackgroundImage")));
            this.panelProperties.Controls.Add(this.labelProperties);
            this.panelProperties.Location = new System.Drawing.Point(0, 2);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(202, 20);
            this.panelProperties.TabIndex = 4;
            // 
            // labelProperties
            // 
            this.labelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProperties.BackColor = System.Drawing.Color.Transparent;
            this.labelProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelProperties.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProperties.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelProperties.Image = ((System.Drawing.Image)(resources.GetObject("labelProperties.Image")));
            this.labelProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelProperties.Location = new System.Drawing.Point(0, 0);
            this.labelProperties.Name = "labelProperties";
            this.labelProperties.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.labelProperties.Size = new System.Drawing.Size(205, 20);
            this.labelProperties.TabIndex = 2;
            this.labelProperties.Text = "      Properties";
            // 
            // treeCodeGen
            // 
            this.treeCodeGen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCodeGen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeCodeGen.ContextMenuStrip = this.menuStripCodeGen;
            this.treeCodeGen.HideSelection = false;
            this.treeCodeGen.ImageIndex = 0;
            this.treeCodeGen.ImageList = this.imageListTree;
            this.treeCodeGen.Indent = 18;
            this.treeCodeGen.Location = new System.Drawing.Point(0, 20);
            this.treeCodeGen.Name = "treeCodeGen";
            this.treeCodeGen.SelectedImageIndex = 0;
            this.treeCodeGen.ShowLines = false;
            this.treeCodeGen.Size = new System.Drawing.Size(202, 323);
            this.treeCodeGen.TabIndex = 7;
            this.treeCodeGen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeCodeGen_MouseDown);
            // 
            // menuStripCodeGen
            // 
            this.menuStripCodeGen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddNewItem,
            this.menuAddExistingItems,
            this.menuAddNewFolder,
            this.menuAddExistingFolder,
            this.menuOpenItem,
            this.menuDeleteItem,
            this.menuGenerateCode,
            this.menuShowGeneratedCode});
            this.menuStripCodeGen.Name = "menuStripCodeGen";
            this.menuStripCodeGen.Size = new System.Drawing.Size(201, 180);
            // 
            // menuAddNewItem
            // 
            this.menuAddNewItem.Name = "menuAddNewItem";
            this.menuAddNewItem.Size = new System.Drawing.Size(200, 22);
            this.menuAddNewItem.Text = "Add New Item...";
            // 
            // menuAddExistingItems
            // 
            this.menuAddExistingItems.Name = "menuAddExistingItems";
            this.menuAddExistingItems.Size = new System.Drawing.Size(200, 22);
            this.menuAddExistingItems.Text = "Add Existing Item(s)...";
            // 
            // menuAddNewFolder
            // 
            this.menuAddNewFolder.Name = "menuAddNewFolder";
            this.menuAddNewFolder.Size = new System.Drawing.Size(200, 22);
            this.menuAddNewFolder.Text = "Add New Folder";
            // 
            // menuAddExistingFolder
            // 
            this.menuAddExistingFolder.Name = "menuAddExistingFolder";
            this.menuAddExistingFolder.Size = new System.Drawing.Size(200, 22);
            this.menuAddExistingFolder.Text = "Add Existing Folder...";
            // 
            // menuOpenItem
            // 
            this.menuOpenItem.Name = "menuOpenItem";
            this.menuOpenItem.Size = new System.Drawing.Size(200, 22);
            this.menuOpenItem.Text = "Open";
            // 
            // menuDeleteItem
            // 
            this.menuDeleteItem.Name = "menuDeleteItem";
            this.menuDeleteItem.Size = new System.Drawing.Size(200, 22);
            this.menuDeleteItem.Text = "Delete";
            // 
            // menuGenerateCode
            // 
            this.menuGenerateCode.Image = ((System.Drawing.Image)(resources.GetObject("menuGenerateCode.Image")));
            this.menuGenerateCode.Name = "menuGenerateCode";
            this.menuGenerateCode.Size = new System.Drawing.Size(200, 22);
            this.menuGenerateCode.Text = "Generate Code...";
            // 
            // menuShowGeneratedCode
            // 
            this.menuShowGeneratedCode.Name = "menuShowGeneratedCode";
            this.menuShowGeneratedCode.Size = new System.Drawing.Size(200, 22);
            this.menuShowGeneratedCode.Text = "Show Generated Code...";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.labelCodeGeneratorExplorer);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 20);
            this.panel1.TabIndex = 6;
            // 
            // labelCodeGeneratorExplorer
            // 
            this.labelCodeGeneratorExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCodeGeneratorExplorer.BackColor = System.Drawing.Color.Transparent;
            this.labelCodeGeneratorExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCodeGeneratorExplorer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelCodeGeneratorExplorer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCodeGeneratorExplorer.Image = ((System.Drawing.Image)(resources.GetObject("labelCodeGeneratorExplorer.Image")));
            this.labelCodeGeneratorExplorer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCodeGeneratorExplorer.Location = new System.Drawing.Point(0, 0);
            this.labelCodeGeneratorExplorer.Name = "labelCodeGeneratorExplorer";
            this.labelCodeGeneratorExplorer.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.labelCodeGeneratorExplorer.Size = new System.Drawing.Size(202, 20);
            this.labelCodeGeneratorExplorer.TabIndex = 2;
            this.labelCodeGeneratorExplorer.Text = "      Code Generator Explorer";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip1.BackgroundImage")));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.queryToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1030, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewConnection,
            this.menuOpenConnectionsFile,
            this.menuSaveConnectionsAs,
            this.toolStripSeparator3,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripMenuItem6,
            this.menuNewCinarSolution,
            this.menuOpenCinarSolution,
            this.menuSaveCinarSolution,
            this.toolStripMenuItem3,
            this.menuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuNewConnection
            // 
            this.menuNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("menuNewConnection.Image")));
            this.menuNewConnection.Name = "menuNewConnection";
            this.menuNewConnection.Size = new System.Drawing.Size(215, 22);
            this.menuNewConnection.Text = "New Connection...";
            // 
            // menuOpenConnectionsFile
            // 
            this.menuOpenConnectionsFile.Name = "menuOpenConnectionsFile";
            this.menuOpenConnectionsFile.Size = new System.Drawing.Size(215, 22);
            this.menuOpenConnectionsFile.Text = "Open Connections File...";
            // 
            // menuSaveConnectionsAs
            // 
            this.menuSaveConnectionsAs.Name = "menuSaveConnectionsAs";
            this.menuSaveConnectionsAs.Size = new System.Drawing.Size(215, 22);
            this.menuSaveConnectionsAs.Text = "Save Connections As...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // menuOpen
            // 
            this.menuOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuOpen.Image")));
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(215, 22);
            this.menuOpen.Text = "Open File...";
            // 
            // menuSave
            // 
            this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(215, 22);
            this.menuSave.Text = "Save File";
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveAs.Image")));
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(215, 22);
            this.menuSaveAs.Text = "Save File As...";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(212, 6);
            // 
            // menuNewCinarSolution
            // 
            this.menuNewCinarSolution.Name = "menuNewCinarSolution";
            this.menuNewCinarSolution.Size = new System.Drawing.Size(215, 22);
            this.menuNewCinarSolution.Text = "New Cinar Solution...";
            // 
            // menuOpenCinarSolution
            // 
            this.menuOpenCinarSolution.Name = "menuOpenCinarSolution";
            this.menuOpenCinarSolution.Size = new System.Drawing.Size(215, 22);
            this.menuOpenCinarSolution.Text = "Open Cinar Solution...";
            // 
            // menuSaveCinarSolution
            // 
            this.menuSaveCinarSolution.Name = "menuSaveCinarSolution";
            this.menuSaveCinarSolution.Size = new System.Drawing.Size(215, 22);
            this.menuSaveCinarSolution.Text = "Save Cinar Solution";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(212, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(215, 22);
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
            this.menuReplace,
            this.toolStripMenuItem10,
            this.menuBeautifySQL});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuUndo
            // 
            this.menuUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuUndo.Image")));
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuUndo.Size = new System.Drawing.Size(192, 22);
            this.menuUndo.Text = "Undo";
            // 
            // menuRedo
            // 
            this.menuRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuRedo.Image")));
            this.menuRedo.Name = "menuRedo";
            this.menuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuRedo.Size = new System.Drawing.Size(192, 22);
            this.menuRedo.Text = "Redo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 6);
            // 
            // menuCut
            // 
            this.menuCut.Image = ((System.Drawing.Image)(resources.GetObject("menuCut.Image")));
            this.menuCut.Name = "menuCut";
            this.menuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuCut.Size = new System.Drawing.Size(192, 22);
            this.menuCut.Text = "Cut";
            // 
            // menuCopy
            // 
            this.menuCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuCopy.Image")));
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopy.Size = new System.Drawing.Size(192, 22);
            this.menuCopy.Text = "Copy";
            // 
            // menuPaste
            // 
            this.menuPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuPaste.Image")));
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuPaste.Size = new System.Drawing.Size(192, 22);
            this.menuPaste.Text = "Paste";
            // 
            // menuSelectAll
            // 
            this.menuSelectAll.Name = "menuSelectAll";
            this.menuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectAll.Size = new System.Drawing.Size(192, 22);
            this.menuSelectAll.Text = "Select All";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 6);
            // 
            // menuFind
            // 
            this.menuFind.Image = ((System.Drawing.Image)(resources.GetObject("menuFind.Image")));
            this.menuFind.Name = "menuFind";
            this.menuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuFind.Size = new System.Drawing.Size(192, 22);
            this.menuFind.Text = "Find...";
            // 
            // menuReplace
            // 
            this.menuReplace.Image = ((System.Drawing.Image)(resources.GetObject("menuReplace.Image")));
            this.menuReplace.Name = "menuReplace";
            this.menuReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuReplace.Size = new System.Drawing.Size(192, 22);
            this.menuReplace.Text = "Replace...";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(189, 6);
            // 
            // menuBeautifySQL
            // 
            this.menuBeautifySQL.Name = "menuBeautifySQL";
            this.menuBeautifySQL.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuBeautifySQL.Size = new System.Drawing.Size(192, 22);
            this.menuBeautifySQL.Text = "Beautify SQL";
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddEditor,
            this.menuExecuteSQL,
            this.menuExecuteScript});
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.queryToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.queryToolStripMenuItem.Text = "Query";
            // 
            // menuAddEditor
            // 
            this.menuAddEditor.Image = ((System.Drawing.Image)(resources.GetObject("menuAddEditor.Image")));
            this.menuAddEditor.Name = "menuAddEditor";
            this.menuAddEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuAddEditor.Size = new System.Drawing.Size(236, 22);
            this.menuAddEditor.Text = "New Query Editor";
            // 
            // menuExecuteSQL
            // 
            this.menuExecuteSQL.Image = ((System.Drawing.Image)(resources.GetObject("menuExecuteSQL.Image")));
            this.menuExecuteSQL.Name = "menuExecuteSQL";
            this.menuExecuteSQL.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.menuExecuteSQL.Size = new System.Drawing.Size(236, 22);
            this.menuExecuteSQL.Text = "Execute as SQL Query";
            // 
            // menuExecuteScript
            // 
            this.menuExecuteScript.Image = ((System.Drawing.Image)(resources.GetObject("menuExecuteScript.Image")));
            this.menuExecuteScript.Name = "menuExecuteScript";
            this.menuExecuteScript.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.menuExecuteScript.Size = new System.Drawing.Size(236, 22);
            this.menuExecuteScript.Text = "Execute as Cinar Script";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsCodeGenerator,
            this.menuToolsGenerateTablesFromReflectedMetadata,
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
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // menuToolsCodeGenerator
            // 
            this.menuToolsCodeGenerator.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsCodeGenerator.Image")));
            this.menuToolsCodeGenerator.Name = "menuToolsCodeGenerator";
            this.menuToolsCodeGenerator.Size = new System.Drawing.Size(332, 22);
            this.menuToolsCodeGenerator.Text = "Code Generator...";
            // 
            // menuToolsGenerateTablesFromReflectedMetadata
            // 
            this.menuToolsGenerateTablesFromReflectedMetadata.Name = "menuToolsGenerateTablesFromReflectedMetadata";
            this.menuToolsGenerateTablesFromReflectedMetadata.Size = new System.Drawing.Size(332, 22);
            this.menuToolsGenerateTablesFromReflectedMetadata.Text = "Generate Tables From Reflected Metadata...";
            // 
            // menuToolsCheckDatabaseSchema
            // 
            this.menuToolsCheckDatabaseSchema.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsCheckDatabaseSchema.Image")));
            this.menuToolsCheckDatabaseSchema.Name = "menuToolsCheckDatabaseSchema";
            this.menuToolsCheckDatabaseSchema.Size = new System.Drawing.Size(332, 22);
            this.menuToolsCheckDatabaseSchema.Text = "Check Database Schema...";
            // 
            // menuToolsDBTransfer
            // 
            this.menuToolsDBTransfer.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsDBTransfer.Image")));
            this.menuToolsDBTransfer.Name = "menuToolsDBTransfer";
            this.menuToolsDBTransfer.Size = new System.Drawing.Size(332, 22);
            this.menuToolsDBTransfer.Text = "Database Transfer...";
            // 
            // menuToolsViewERDiagram
            // 
            this.menuToolsViewERDiagram.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsViewERDiagram.Image")));
            this.menuToolsViewERDiagram.Name = "menuToolsViewERDiagram";
            this.menuToolsViewERDiagram.Size = new System.Drawing.Size(332, 22);
            this.menuToolsViewERDiagram.Text = "View ER Diagram...";
            // 
            // menuToolsSQLDump
            // 
            this.menuToolsSQLDump.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsSQLDump.Image")));
            this.menuToolsSQLDump.Name = "menuToolsSQLDump";
            this.menuToolsSQLDump.Size = new System.Drawing.Size(332, 22);
            this.menuToolsSQLDump.Text = "SQL Dump...";
            // 
            // menuToolsSimpleIntegrationService
            // 
            this.menuToolsSimpleIntegrationService.Image = ((System.Drawing.Image)(resources.GetObject("menuToolsSimpleIntegrationService.Image")));
            this.menuToolsSimpleIntegrationService.Name = "menuToolsSimpleIntegrationService";
            this.menuToolsSimpleIntegrationService.Size = new System.Drawing.Size(332, 22);
            this.menuToolsSimpleIntegrationService.Text = "Simple Integration Service...";
            // 
            // menuToolsCopyTreeData
            // 
            this.menuToolsCopyTreeData.Name = "menuToolsCopyTreeData";
            this.menuToolsCopyTreeData.Size = new System.Drawing.Size(332, 22);
            this.menuToolsCopyTreeData.Text = "Copy Tree Data...";
            // 
            // menuToolsCompareDatabases
            // 
            this.menuToolsCompareDatabases.Name = "menuToolsCompareDatabases";
            this.menuToolsCompareDatabases.Size = new System.Drawing.Size(332, 22);
            this.menuToolsCompareDatabases.Text = "Compare Databases...";
            // 
            // menuToolsCompareDirectories
            // 
            this.menuToolsCompareDirectories.Name = "menuToolsCompareDirectories";
            this.menuToolsCompareDirectories.Size = new System.Drawing.Size(332, 22);
            this.menuToolsCompareDirectories.Text = "Compare Directories...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(329, 6);
            // 
            // quickScriptToolStripMenuItem
            // 
            this.quickScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsQScriptDeleteFromTables,
            this.menuToolsQScriptSelectCountsFromTables,
            this.menuToolsQScriptForEachTable,
            this.menuToolsQScriptForEachColumn,
            this.menuToolsQScriptCalculateOptDataLen});
            this.quickScriptToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quickScriptToolStripMenuItem.Image")));
            this.quickScriptToolStripMenuItem.Name = "quickScriptToolStripMenuItem";
            this.quickScriptToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.quickScriptToolStripMenuItem.Text = "Quick Script";
            // 
            // menuToolsQScriptDeleteFromTables
            // 
            this.menuToolsQScriptDeleteFromTables.Name = "menuToolsQScriptDeleteFromTables";
            this.menuToolsQScriptDeleteFromTables.Size = new System.Drawing.Size(258, 22);
            this.menuToolsQScriptDeleteFromTables.Text = "Delete From Tables";
            // 
            // menuToolsQScriptSelectCountsFromTables
            // 
            this.menuToolsQScriptSelectCountsFromTables.Name = "menuToolsQScriptSelectCountsFromTables";
            this.menuToolsQScriptSelectCountsFromTables.Size = new System.Drawing.Size(258, 22);
            this.menuToolsQScriptSelectCountsFromTables.Text = "Select Count(*) From Tables";
            // 
            // menuToolsQScriptForEachTable
            // 
            this.menuToolsQScriptForEachTable.Name = "menuToolsQScriptForEachTable";
            this.menuToolsQScriptForEachTable.Size = new System.Drawing.Size(258, 22);
            this.menuToolsQScriptForEachTable.Text = "For Each Table";
            // 
            // menuToolsQScriptForEachColumn
            // 
            this.menuToolsQScriptForEachColumn.Name = "menuToolsQScriptForEachColumn";
            this.menuToolsQScriptForEachColumn.Size = new System.Drawing.Size(258, 22);
            this.menuToolsQScriptForEachColumn.Text = "For Each Column";
            // 
            // menuToolsQScriptCalculateOptDataLen
            // 
            this.menuToolsQScriptCalculateOptDataLen.Name = "menuToolsQScriptCalculateOptDataLen";
            this.menuToolsQScriptCalculateOptDataLen.Size = new System.Drawing.Size(258, 22);
            this.menuToolsQScriptCalculateOptDataLen.Text = "Calculate Optimal Data Lengths";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpScriptingTest,
            this.toolStripMenuItem1,
            this.menuHelpAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menuHelpScriptingTest
            // 
            this.menuHelpScriptingTest.Image = ((System.Drawing.Image)(resources.GetObject("menuHelpScriptingTest.Image")));
            this.menuHelpScriptingTest.Name = "menuHelpScriptingTest";
            this.menuHelpScriptingTest.Size = new System.Drawing.Size(230, 22);
            this.menuHelpScriptingTest.Text = "Cinar Script Test && Learn...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(227, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(230, 22);
            this.menuHelpAbout.Text = "About...";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.btnNewConnection,
            this.btnEditConnection,
            this.btnDeleteConnection,
            this.cbActiveConnection,
            this.toolStripSeparator1,
            this.toolStripLabel4,
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
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1030, 25);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 1;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(28, 22);
            this.toolStripLabel2.Text = "File:";
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
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel3.Text = "Connections:";
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
            // cbActiveConnection
            // 
            this.cbActiveConnection.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbActiveConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActiveConnection.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbActiveConnection.Name = "cbActiveConnection";
            this.cbActiveConnection.Size = new System.Drawing.Size(200, 25);
            this.cbActiveConnection.SelectedIndexChanged += new System.EventHandler(this.cbActiveConnection_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel4.Text = "Query:";
            // 
            // btnAddEditor
            // 
            this.btnAddEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEditor.Image")));
            this.btnAddEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEditor.Name = "btnAddEditor";
            this.btnAddEditor.Size = new System.Drawing.Size(23, 22);
            this.btnAddEditor.Text = "New Query Editor (Ctrl+N)";
            // 
            // btnExecuteSQL
            // 
            this.btnExecuteSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteSQL.Image")));
            this.btnExecuteSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteSQL.Name = "btnExecuteSQL";
            this.btnExecuteSQL.Size = new System.Drawing.Size(23, 22);
            this.btnExecuteSQL.Text = "Execute as SQL Query (F9)";
            // 
            // btnExecuteScript
            // 
            this.btnExecuteScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteScript.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteScript.Image")));
            this.btnExecuteScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteScript.Name = "btnExecuteScript";
            this.btnExecuteScript.Size = new System.Drawing.Size(23, 22);
            this.btnExecuteScript.Text = "Execute as Cinar Script (F10)";
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
            // menuCopy2
            // 
            this.menuCopy2.Name = "menuCopy2";
            this.menuCopy2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopy2.Size = new System.Drawing.Size(210, 22);
            this.menuCopy2.Text = "Copy";
            // 
            // menuCut2
            // 
            this.menuCut2.Name = "menuCut2";
            this.menuCut2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuCut2.Size = new System.Drawing.Size(210, 22);
            this.menuCut2.Text = "Cut";
            // 
            // menuPaste2
            // 
            this.menuPaste2.Name = "menuPaste2";
            this.menuPaste2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuPaste2.Size = new System.Drawing.Size(210, 22);
            this.menuPaste2.Text = "Paste";
            // 
            // menuSelectAll2
            // 
            this.menuSelectAll2.Name = "menuSelectAll2";
            this.menuSelectAll2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectAll2.Size = new System.Drawing.Size(210, 22);
            this.menuSelectAll2.Text = "Select All";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(207, 6);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 676);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "Cinar Database Tools";
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
            this.panelConnections.ResumeLayout(false);
            this.menuStripTree.ResumeLayout(false);
            this.menuStripEditorTabs.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelProperties.ResumeLayout(false);
            this.menuStripCodeGen.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewConnection;
        private System.Windows.Forms.ContextMenuStrip menuStripTree;
        private System.Windows.Forms.ToolStripMenuItem menuTableCount;
        private System.Windows.Forms.ToolStripMenuItem menuColumnDistinct;
        private System.Windows.Forms.ToolStripMenuItem menuTableOpen;
        private System.Windows.Forms.ToolStripMenuItem menuColumnMax;
        private System.Windows.Forms.ToolStripMenuItem menuColumnMin;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCodeGenerator;
        private System.Windows.Forms.ToolStripMenuItem menuToolsCheckDatabaseSchema;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNewConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExecuteSQL;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenerateSQL;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLSelect;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLInsert;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuConRefresh;
        private System.Windows.Forms.ToolStripMenuItem menuColumnGroupedCounts;
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
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptForEachColumn;
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
        private System.Windows.Forms.ToolStripMenuItem menuTableGenSQLDelete;
        private System.Windows.Forms.ToolStripMenuItem menuToolsQScriptCalculateOptDataLen;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelConnections;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private System.Windows.Forms.Label labelProperties;
        private System.Windows.Forms.ToolStripMenuItem menuIndexCreateIndex;
        private System.Windows.Forms.ToolStripMenuItem menuIndexEditIndex;
        private System.Windows.Forms.ToolStripMenuItem menuIndexDropIndex;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ImageList imageListTabs;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddEditor;
        private System.Windows.Forms.ToolStripMenuItem menuExecuteSQL;
        private System.Windows.Forms.ToolStripMenuItem menuExecuteScript;
        private System.Windows.Forms.ToolStripMenuItem menuMoreDatabaseOperations;
        private System.Windows.Forms.ToolStripMenuItem menuConShowHiddenConnections;
        private System.Windows.Forms.ToolStripMenuItem menuConHideConnection;
        private System.Windows.Forms.ToolStripMenuItem menuSaveConnectionsAs;
        private System.Windows.Forms.Panel panelConnections;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCodeGeneratorExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        internal System.Windows.Forms.TreeView treeView;
        internal Controls.MyTabControl tabControlEditors;
        internal System.Windows.Forms.PropertyGrid propertyGrid;
        internal System.Windows.Forms.TreeView treeCodeGen;
        internal System.Windows.Forms.ToolStripMenuItem menuOpenCinarSolution;
        internal System.Windows.Forms.ToolStripMenuItem menuSaveCinarSolution;
        internal System.Windows.Forms.ToolStripMenuItem menuNewCinarSolution;
        private System.Windows.Forms.ContextMenuStrip menuStripCodeGen;
        internal System.Windows.Forms.ToolStripMenuItem menuAddNewItem;
        internal System.Windows.Forms.ToolStripMenuItem menuAddExistingItems;
        internal System.Windows.Forms.ToolStripMenuItem menuAddNewFolder;
        internal System.Windows.Forms.ToolStripMenuItem menuDeleteItem;
        internal System.Windows.Forms.ToolStripMenuItem menuOpenItem;
        private System.Windows.Forms.ToolStripMenuItem menuColumnDrop;
        internal System.Windows.Forms.ToolStripMenuItem menuGenerateCode;
        private System.Windows.Forms.ContextMenuStrip menuStripEditorTabs;
        private System.Windows.Forms.ToolStripMenuItem menuTabSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem menuTabClose;
        private System.Windows.Forms.ToolStripMenuItem menuTabCloseAll;
        private System.Windows.Forms.ToolStripMenuItem menuTabCloseAllButThis;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem menuTabCompareWithOriginal;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem menuTabCopyFullPath;
        private System.Windows.Forms.ToolStripMenuItem menuTabOpenContainingFolder;
        internal System.Windows.Forms.ToolStripMenuItem menuShowGeneratedCode;
        internal System.Windows.Forms.ToolStripMenuItem menuAddExistingFolder;
        private System.Windows.Forms.ToolStripMenuItem menuToolsGenerateTablesFromReflectedMetadata;
        private System.Windows.Forms.ToolStripMenuItem menuTableOpenWithFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem menuBeautifySQL;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem menuCut2;
        private System.Windows.Forms.ToolStripMenuItem menuCopy2;
        private System.Windows.Forms.ToolStripMenuItem menuPaste2;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll2;

    }
}