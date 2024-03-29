﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.SQLParser;
using Cinar.UICommands;
using Cinar.Database;
using Cinar.DBTools.Tools;
using System.IO;
using System.Collections;
using Cinar.Scripting;
using System.Diagnostics;
using Cinar.DBTools.Controls;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Constraint = Cinar.Database.Constraint;
using Cinar.DBTools.CodeGen;
using Cinar.WebServer;
using ColumnDef = Cinar.DBTools.Tools.ColumnDef;
using System.Text.RegularExpressions;

namespace Cinar.DBTools
{
    public partial class FormMain : Form
    {
        public CommandManager cmdMan = new CommandManager();
        TreeNode rootNode;
        int splitterMainLast = 0, splitterPropLast = 0;
        Image imgYellowBg = null;
        Controller codeGenController;

        public FormMain()
        {
            InitializeComponent();
            SetFont(this, Font);

            toolStrip.BackColor = Color.FromArgb(188, 199, 216);
            imgYellowBg = splitContainerMain.Panel1.BackgroundImage;

            imageListTree.Images.Add("Folder", FamFamFam.folder);
            imageListTree.Images.Add("File", FamFamFam.page);
            imageListTree.Images.Add("Table", FamFamFam.table);
            imageListTree.Images.Add("Column", FamFamFam.table_row_insert);
            imageListTree.Images.Add("Key", FamFamFam.key);
            imageListTree.Images.Add("Constraint", FamFamFam.database_link);
            imageListTree.Images.Add("Index", FamFamFam.database_key);
            imageListTree.Images.Add("View", FamFamFam.eye);
            imageListTree.Images.Add("Diagram", FamFamFam.chart_organisation);
            imageListTree.Images.Add("Database", FamFamFam.database);
            imageListTree.Images.Add("MySQL", FamFamFam.mysql);
            imageListTree.Images.Add("PostgreSQL", FamFamFam.postgresql);
            imageListTree.Images.Add("SQLServer", FamFamFam.sqlserver);
            imageListTree.Images.Add("Cinar", FamFamFam.Cinar);
            imageListTree.Images.Add("SQLite", FamFamFam.SQLite);
            imageListTree.Images.Add("Script", FamFamFam.script);

            imageListTabs.Images.Add("Diagram", FamFamFam.chart_organisation);
            imageListTabs.Images.Add("Query", FamFamFam.script);
            imageListTabs.Images.Add("File", FamFamFam.page);

            #region commands
            cmdMan.AfterCommandExecute = showSelectedObjectOnPropertyGrid;

            cmdMan.Commands = new CommandCollection(){
                    #region main menu
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnNewConnection},
                                         new CommandTrigger{ Control = menuNewConnection},
                                     },
                                     IsVisible = () => treeView.SelectedNode!=null && (treeView.SelectedNode==treeView.Nodes[0] || SelectedObject is ConnectionSettings)
                                 },
                     new Command {
                                     Execute = cmdOpenConnectionsFile,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuOpenConnectionsFile},
                                     }
                                 },
                     new Command {
                                     Execute = cmdSaveConnectionsAs,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuSaveConnectionsAs},
                                     }
                                 },
                     new Command {
                                     Execute = cmdOpen,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuOpen},
                                         new CommandTrigger{ Control = btnOpen},
                                     }
                                 },
                     new Command {
                                     Execute = cmdSave,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuSave},
                                         new CommandTrigger{ Control = btnSave},
                                         new CommandTrigger{ Control = menuTabSave},
                                     },
                                     IsEnabled = () => CurrEditor != null && CurrEditor.Modified
                                 },
                     new Command {
                                     Execute = cmdSaveAs,
                                     Trigger = new CommandTrigger{ Control = menuSaveAs}
                                 },
                     new Command {
                                     Execute = cmdExit,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuExit},
                                     }
                                 },
                     new Command {
                                     Execute = cmdEditorCommand,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuUndo, Argument = "Undo"},
                                         new CommandTrigger{ Control = menuRedo, Argument = "Redo"},
                                         new CommandTrigger{ Control = menuCut, Argument = "Cut"},
                                         new CommandTrigger{ Control = menuCut2, Argument = "Cut"},
                                         new CommandTrigger{ Control = menuCopy, Argument = "Copy"},
                                         new CommandTrigger{ Control = menuCopy2, Argument = "Copy"},
                                         new CommandTrigger{ Control = menuPaste, Argument = "Paste"},
                                         new CommandTrigger{ Control = menuPaste2, Argument = "Paste"},
                                         new CommandTrigger{ Control = menuSelectAll, Argument = "SelectAll"},
                                         new CommandTrigger{ Control = menuSelectAll2, Argument = "SelectAll"},
                                         new CommandTrigger{ Control = menuFind, Argument = "Find"},
                                         new CommandTrigger{ Control = menuReplace, Argument = "Replace"},
                                         new CommandTrigger{ Control = menuBeautifySQL, Argument = "Beautify"},
                                     }
                                 },
                     new Command {
                                     Execute = cmdAbout,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuHelpAbout},
                                     }
                                 },
                #endregion
                    #region toolBar & tools menu
                     new Command {
                                     Execute = (arg)=>{addSQLEditor("", "");},
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnAddEditor},
                                         new CommandTrigger{ Control = menuAddEditor}
                                     }
                                 },
                     new Command {
                                     Execute = (arg)=>{
                                        CancelEventArgs e = (CancelEventArgs) cmdMan.LastEventArgs;
                                        if (CurrEditor.Modified)
                                        {
                                            DialogResult dr = MessageBox.Show("Would you like to save?", "Cinar Database Tools", MessageBoxButtons.YesNoCancel);
                                            if (dr == DialogResult.Yes)
                                                CurrEditor.Save();
                
                                            if (dr == DialogResult.Cancel)
                                                e.Cancel = true;
                                        }
                                        if (!e.Cancel)
                                            CurrEditor.OnClose();
                                     },
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = tabControlEditors, Event="CloseTab"},
                                     },
                                },
                     new Command {
                                     Execute = cmdCloseEditor,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabClose},
                                     },
                                },
                     new Command {
                                     Execute = cmdCloseAll,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabCloseAll},
                                     },
                                },
                     new Command {
                                     Execute = cmdCloseAllButThis,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabCloseAllButThis},
                                     },
                                     IsEnabled = () => tabControlEditors.TabPages.Count > 1
                                },
                     new Command {
                                     Execute = (arg)=>{
                                         Provider.CompareCode(CurrEditor.FilePath, CurrEditor.Content);
                                     },
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabCompareWithOriginal},
                                     },
                                     IsEnabled = () => CurrEditor!=null && !String.IsNullOrEmpty(CurrEditor.FilePath)
                                },
                     new Command {
                                     Execute = (arg)=>{
                                         Clipboard.SetText(CurrEditor.FilePath);
                                     },
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabCopyFullPath},
                                     },
                                     IsEnabled = () => CurrEditor!=null && !String.IsNullOrEmpty(CurrEditor.FilePath)
                                },
                     new Command {
                                     Execute = (arg)=>{
                                         Process.Start("explorer.exe", "/select," + CurrEditor.FilePath);
                                         //Process.Start(Path.GetDirectoryName(CurrEditor.FilePath));
                                     },
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTabOpenContainingFolder},
                                     },
                                     IsEnabled = () => CurrEditor!=null && !String.IsNullOrEmpty(CurrEditor.FilePath)
                                },
                     new Command {
                                     Execute = cmdExecuteSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnExecuteSQL},
                                         new CommandTrigger{ Control = menuExecuteSQL}
                                     },
                                     IsEnabled = () => CurrSQLEditor!=null && !string.IsNullOrEmpty(CurrSQLEditor.SQLEditor.Text)
                                 },
                     new Command {
                                     Execute = cmdExecuteScript,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnExecuteScript},
                                         new CommandTrigger{ Control = menuExecuteScript}
                                     },
                                     IsEnabled = () => CurrSQLEditor!=null && !string.IsNullOrEmpty(CurrSQLEditor.SQLEditor.Text)
                                 },
                     new Command {
                                     Execute = cmdShowForm,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 
                                         new CommandTrigger{ Control = btnCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 

                                         new CommandTrigger{ Control = menuToolsGenerateTablesFromReflectedMetadata, Argument=typeof(FormGenerateTablesFromClasses).FullName}, 

                                         new CommandTrigger{ Control = menuToolsCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},
                                         new CommandTrigger{ Control = btnCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},

                                         new CommandTrigger{ Control = menuToolsDBTransfer, Argument=typeof(FormDBTransfer).FullName},
                                         new CommandTrigger{ Control = btnDatabaseTransfer, Argument=typeof(FormDBTransfer).FullName},

                                         new CommandTrigger{ Control = menuToolsSimpleIntegrationService, Argument=typeof(FormDBIntegration).FullName},
                                         new CommandTrigger{ Control = btnSimpleIntegrationService, Argument=typeof(FormDBIntegration).FullName},

                                         new CommandTrigger{ Control = menuToolsSQLDump, Argument=typeof(FormSQLDump).FullName},
                                         new CommandTrigger{ Control = btnSQLDump, Argument=typeof(FormSQLDump).FullName},

                                         new CommandTrigger{ Control = menuToolsCopyTreeData, Argument=typeof(FormCopyTreeData).FullName},
                                         new CommandTrigger{ Control = menuToolsCompareDatabases, Argument=typeof(FormCompareDatabases).FullName},
                                     },
                                     IsEnabled = ()=> Provider.Database != null
                                 },
                     new Command {
                                     Execute = cmdShowForm,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuHelpScriptingTest, Argument=typeof(FormScriptingTest).FullName},
                                         new CommandTrigger{ Control = menuToolsCompareDirectories, Argument=typeof(FormCompareDirectories).FullName},
                                     }
                                 },
                     new Command {
                                     Execute = cmdSearchTableNamesInFiles,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsSearchTableNamesInFiles},
                                     }
                                 },
                     new Command {
                                     Execute = cmdSearchSQLInProjectFiles,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuSearchSQLInProjectFiles},
                                     }
                                 },
                     new Command {
                                     Execute = cmdQuickScript,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsQScriptDeleteFromTables, Argument=SQLResources.SQLDeleteFromTables},
                                         new CommandTrigger{ Control = menuToolsQScriptSelectCountsFromTables, Argument=SQLResources.SQLSelectCountsFromTables},
                                         new CommandTrigger{ Control = menuToolsQScriptForEachTable, Argument=SQLResources.SQLForEachTable},
                                         new CommandTrigger{ Control = menuToolsQScriptForEachColumn, Argument=SQLResources.SQLForEachColumn},
                                         new CommandTrigger{ Control = menuToolsQScriptCalculateOptDataLen, Argument=SQLResources.SQLCalculateOptimalDataLength},
                                         new CommandTrigger{ Control = menuToolsQScriptSPHelpText, Argument=SQLResources.SQLSPHelpText},
                                         new CommandTrigger{ Control = menuToolsQScriptSearhAllStringFields, Argument=SQLResources.SQLSearhAllStringFields},
                                         new CommandTrigger{ Control = menuToolsQScriptSQLTransferTableSQLGenerator, Argument=SQLResources.SQLTransferTableSQLGenerator},
                                         new CommandTrigger{ Control = menuToolsQScriptDataTableToStringTable, Argument=SQLResources.SQLDataTableToStringTable},
                                         new CommandTrigger{ Control = menuToolsQScriptDropAllForeignKeys, Argument=SQLResources.SQLDropAllForeignKeys},
                                         new CommandTrigger{ Control = menuToolsQScriptFindIncorrectForeignKeys, Argument=SQLResources.SQLFindIncorrectForeignKeys},
                                         new CommandTrigger{ Control = menuToolsQScriptCompareDatabases, Argument=SQLResources.SQLCompareDatabases},
                                         new CommandTrigger{ Control = menuToolsQScriptChangeCollation, Argument=SQLResources.SQLChangeCollationSQLServer},
                                     }
                                 },
                    #endregion
                    #region tree menus
                     new Command {
                                     Execute = cmdToggleConnectionsPanel,
                                     Trigger = new CommandTrigger{ Control = labelConnections}
                                 },
                     new Command {
                                     Execute = cmdTogglePropertiesPanel,
                                     Trigger = new CommandTrigger{ Control = labelProperties}
                                 },
                     new Command {
                                     Execute = cmdDeleteSelectedObject,
                                     Trigger = new CommandTrigger{ Control = treeView, Event = "KeyUp", Predicate = (e)=>{return (e as KeyEventArgs).KeyCode == Keys.Delete;}}
                                 },
                     new Command {
                                     Execute = cmdShowHiddenConnections,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConShowHiddenConnections},
                                     },
                                     IsVisible = () => SelectedObject is List<ConnectionSettings>
                                 },
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConNewConnection},
                                     },
                                     IsVisible = () => SelectedObject is ConnectionSettings || SelectedObject is List<ConnectionSettings>
                                 },
                     new Command {
                                     Execute = cmdEditConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConEditConnection},
                                         new CommandTrigger{ Control = btnEditConnection},
                                     },
                                     IsEnabled = () => SelectedObject is ConnectionSettings,
                                     IsVisible = () => SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdDeleteConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConDeleteConnection},
                                         new CommandTrigger{ Control = btnDeleteConnection},
                                     },
                                     IsEnabled = () => SelectedObject is ConnectionSettings,
                                     IsVisible = () => SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdHideConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConHideConnection},
                                     },
                                     IsVisible = () => SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdRefresh,
                                     Trigger = new CommandTrigger{ Control = menuConRefresh},
                                     IsVisible = ()=> SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdRefreshMetadata,
                                     Trigger = new CommandTrigger{ Control = menuConRefreshMetadata},
                                     IsVisible = ()=> SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdDoDatabaseOperation,
                                     Triggers = new List<CommandTrigger>{
                                         new CommandTrigger{ Control = menuMoreDatabaseOperations, Argument="-"},
                                         new CommandTrigger{ Control = menuConCreateDatabase, Argument="Create"},
                                         new CommandTrigger{ Control = menuConDropDatabase, Argument="Drop"},
                                         new CommandTrigger{ Control = menuConTruncateDatabase, Argument="Truncate"},
                                         new CommandTrigger{ Control = menuConEmptyDatabase, Argument="Empty"},
                                         new CommandTrigger{ Control = menuConTransferDatabase, Argument="Transfer"},
                                         new CommandTrigger{ Control = menuConBackupDatabase, Argument="Backup"},
                                     },
                                     IsVisible = ()=> SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdCreateDBObject,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConCreate, Argument="-"},
                                         new CommandTrigger{ Control = menuDBCreateTable, Argument="Table"},
                                         new CommandTrigger{ Control = menuDBCreateView, Argument="View"},
                                         new CommandTrigger{ Control = menuDBCreateTrigger, Argument="Trigger"},
                                         new CommandTrigger{ Control = menuDBCreateSProc, Argument="SProc"},
                                         new CommandTrigger{ Control = menuDBCreateFunction, Argument="Function"},
                                     },
                                     IsVisible = ()=> SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdExecuteSQLFromFile,
                                     Trigger = new CommandTrigger{ Control = menuConExecuteSQLFromFile},
                                     IsVisible = ()=> SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdNewERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsViewERDiagram},
                                         new CommandTrigger{ Control = btnViewERDiagram},
                                     },
                                     IsEnabled = ()=> Provider.Database!=null,
                                 },
                     new Command {
                                     Execute = cmdNewERDiagram,
                                     Triggers = new List<CommandTrigger>{
                                         new CommandTrigger{ Control = menuDiagramNew},
                                         new CommandTrigger{ Control = menuConShowDatabaseERDiagram},
                                     },
                                     IsVisible = ()=> SelectedObject is List<Diagram> || SelectedObject is Diagram
                                 },
                     new Command {
                                     Execute = cmdOpenERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDiagramOpen},
                                     },
                                     IsVisible = ()=> SelectedObject is Diagram,
                                 },
                     new Command {
                                     Execute = cmdDeleteERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDiagramDelete},
                                     },
                                     IsVisible = ()=> SelectedObject is Diagram,
                                 },
                     new Command {
                                     Execute = cmdShowTableCounts,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTablesShowTableCounts},
                                     },
                                     IsVisible = () => SelectedObject is TableCollection
                                 },
                     new Command {
                                     Execute = cmdCreateTable,
                                     Trigger = new CommandTrigger{ Control = menuTableCreate},
                                     IsVisible = ()=> SelectedObject is TableCollection
                                 },
                     new Command {
                                     Execute = cmdAlterTable,
                                     Trigger = new CommandTrigger{ Control = menuTableAlter},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdTableDrop,
                                     Trigger = new CommandTrigger{ Control = menuTableDrop},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdTableTruncate,
                                     Trigger = new CommandTrigger{ Control = menuTableTruncate},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdTableOpen,
                                     Triggers = new List<CommandTrigger>{
                                         new CommandTrigger{ Control = menuTableOpen},
                                         new CommandTrigger{ Control = menuTableOpenWithFilter, Argument = "WithFilter"},
                                     },
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdTableCount,
                                     Trigger = new CommandTrigger{ Control = menuTableCount},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdAnalyzeTable,
                                     Trigger = new CommandTrigger{ Control = menuTableAnalyze},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdShowTableInDiagram,
                                     Trigger = new CommandTrigger{ Control = menuTableShowInDiagram},
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdShowUIMetadataPreview,
                                     Trigger = new CommandTrigger{ Control = menuTableShowUIPreview},
                                     IsVisible = ()=> SelectedObject is Table || SelectedObject is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdGenerateSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTableGenerateSQL, Argument="-"},
                                         new CommandTrigger{ Control = menuTableGenSQLCreateTable, Argument="CreateTable"},
                                         new CommandTrigger{ Control = menuTableGenSQLSelect, Argument="Select"},
                                     },
                                     IsVisible = ()=> SelectedObject is Table
                                 },
                     new Command {
                                     Execute = cmdGenerateSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTableGenSQLInsert, Argument="Insert"},
                                         new CommandTrigger{ Control = menuTableGenSQLUpdate, Argument="Update"},
                                         new CommandTrigger{ Control = menuTableGenSQLDump, Argument="Dump"},
                                     },
                                     IsVisible = ()=> SelectedObject is Table && !(SelectedObject as Table).IsView
                                 },
                     new Command {
                                     Execute = cmdColumnDistinct,
                                     Trigger = new CommandTrigger{ Control = menuColumnDistinct},
                                     IsVisible = ()=> SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdColumnMax,
                                     Trigger = new CommandTrigger{ Control = menuColumnMax},
                                     IsVisible = ()=> SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdColumnMin,
                                     Trigger = new CommandTrigger{ Control = menuColumnMin},
                                     IsVisible = ()=> SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdGroupedCounts,
                                     Trigger = new CommandTrigger{ Control = menuColumnGroupedCounts},
                                     IsVisible = ()=> SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdColumnDrop,
                                     Trigger = new CommandTrigger{ Control = menuColumnDrop},
                                     IsVisible = ()=> SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdGenerateUIMetadata,
                                     Trigger = new CommandTrigger{ Control = menuShowUIMetadata},
                                     IsVisible = ()=> SelectedObject is ConnectionSettings || SelectedObject is Table || SelectedObject is Column
                                 },
                     new Command {
                                     Execute = cmdCreateIndex,
                                     Trigger = new CommandTrigger{ Control = menuIndexCreateIndex},
                                     IsVisible = ()=> SelectedObject is IndexCollection || SelectedObject is BaseIndexConstraint
                                 },
                     new Command {
                                     Execute = cmdEditIndex,
                                     Trigger = new CommandTrigger{ Control = menuIndexEditIndex},
                                     IsVisible = ()=> SelectedObject is BaseIndexConstraint
                                 },
                     new Command {
                                     Execute = cmdDropIndex,
                                     Trigger = new CommandTrigger{ Control = menuIndexDropIndex},
                                     IsVisible = ()=> SelectedObject is BaseIndexConstraint
                                 },
                    #endregion
             };
            
            codeGenController = new Controller(this);

            cmdMan.SetCommandTriggers();
            #endregion

            showConnections(null);
        }

        public void SetFont(Control parent, Font font)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl is TextEditorControl || ctl is MenuStrip || ctl is ContextMenuStrip) continue;

                ctl.Font = font;
                if (ctl is DataGridView)
                    ((DataGridView)ctl).RowsDefaultCellStyle.Font = font;
                SetFont(ctl, font);
            }
        }

        private SQLEditorAndResults CurrSQLEditor
        {
            get
            {
                if (tabControlEditors.TabCount > 0)
                    return (tabControlEditors.SelectedTab.Controls[0] as SQLEditorAndResults);
                return null;
            }
        }

        internal IEditor CurrEditor
        {
            get
            {
                if (tabControlEditors.TabCount > 0)
                    return (tabControlEditors.SelectedTab.Controls[0] as IEditor);
                return null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            bool isAnyEditorOpened = false;
            string lastOpenedFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\lastopened.txt";
            if (File.Exists(lastOpenedFilePath))
            {
                string[] files = File.ReadAllText(lastOpenedFilePath).Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var file in files)
                    if (File.Exists(file))
                    {
                        addSQLEditor(file, "");
                        isAnyEditorOpened = true;
                    }
            }

            foreach (string tempFilePath in Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "tmpsvdfile_*.txt"))
            {
                addSQLEditor(tempFilePath, "", true);
                isAnyEditorOpened = true;
            }

            if(!isAnyEditorOpened)
                addSQLEditor("", "");

            timer.Enabled = true;
        }

        #region methods
        private void showConnections(string path)
        {
            treeView.Nodes.Clear();

            rootNode = treeView.Nodes.Add("Database Connections");
            rootNode.Tag = Provider.Connections;

            Provider.LoadConnectionsFromXML(path);
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                TreeNode node = createConnectionNode(cs);
                populateTreeNodesForDatabase(node);
            }
            //treeView.Sort();
            if (rootNode.Nodes.Count > 0 && rootNode.Nodes[0].Nodes.Count > 0)
            {
                rootNode.Nodes[0].Expand();
                treeView.SelectedNode = rootNode.Nodes[0];
            }
        }

        private TreeNode createConnectionNode(ConnectionSettings cs)
        {
            TreeNode node = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), cs.Provider.ToString(), cs.Provider.ToString());
            node.NodeFont = new Font(treeView.Font, FontStyle.Underline);
            node.Tag = cs;

            cbActiveConnection.Items.Add(cs);
            return node;
        }

        private void executeSQL(string sql, params object[] args)
        {
            if(args.Length>0)
                sql = String.Format(sql, args);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataSet ds = null;

            try
            {
                if (sql.Length > 500000) // massive SQL
                {
                    SQLParser.Tokenizer tokenizer = new SQLParser.Tokenizer(new StringReader(sql));
                    SQLParser.Token token = tokenizer.ReadNextToken();
                    List<string> sqlList = new List<string>();
                    string anSql = "";
                    while (token != null)
                    {
                        if (token.Value == ";")
                        {
                            sqlList.Add(anSql);
                            anSql = "";
                        }
                        else
                        {
                            anSql += " " + token.Value;
                        }
                        token = tokenizer.ReadNextToken();
                    }
                    if (anSql != "") sqlList.Add(anSql);

                    StringBuilder queriesWithError = new StringBuilder();

                    BackgroundWorkerDialog bwd = new BackgroundWorkerDialog();
                    bwd.MessageFormat = "Please wait while {0} query executed.";
                    bwd.DoWork = () => {
                        int sqlListLength = sqlList.Count;
                        for (int i = 0; i < sqlListLength && !bwd.Canceled; i++)
                        {
                            try
                            {
                                Provider.Database.ExecuteNonQuery(sqlList[i]);
                            }
                            catch (Exception ex) 
                            {
                                queriesWithError.AppendLine("/* " + ex.Message + " */");
                                queriesWithError.AppendLine(sqlList[i]);
                            }

                            int percent = Convert.ToInt32((i / (float)sqlListLength) * 100);
                            bwd.ReportProgress(percent, sqlList.Count-i);
                        }
                        return "";
                    };
                    bwd.ShowDialog();

                    watch.Stop();
                    SetStatusText("Queries executed succesfully.");

                    statusExecTime.Text = watch.ElapsedMilliseconds + " ms";
                    statusNumberOfRows.Text = "";

                    if (CurrSQLEditor == null) findOrCreateNewSQLEditor();
                    if (queriesWithError.Length > 0)
                        CurrSQLEditor.ShowInfoText("/* QUERIES WITH ERROR */\r\n\r\n" + queriesWithError);
                }
                else // smaller SQL
                {
                    ds = Provider.Database.GetDataSet(sql);
                    watch.Stop();

                    SetStatusText("Query executed succesfully.");

                    statusExecTime.Text = watch.ElapsedMilliseconds + " ms";
                    statusNumberOfRows.Text = (ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count) + " rows";
                    sql = sql.Replace("\n", " ").Replace("\r", "").Replace("\t", " ") + (sql.EndsWith(";") ? "" : ";");
                    while (sql.Contains("  ")) sql = sql.Replace("  ", " ");
                    if (sql.Length > 1000) sql = sql.Substring(0, 1000) + "...";

                    if (CurrSQLEditor == null) findOrCreateNewSQLEditor();

                    CurrSQLEditor.SQLLog.Text += string.Format(Environment.NewLine + "/*[{0} - {1,5} ms]*/ {2}", DateTime.Now.ToString("hh:mm:ss"), watch.ElapsedMilliseconds, sql);

                    if (ds.Tables.Count > 1)
                        CurrSQLEditor.BindGridResults(ds);
                    else if (ds.Tables.Count == 1)
                        CurrSQLEditor.BindGridResults(ds.Tables[0]);
                    else
                        CurrSQLEditor.BindGridResults(null);
                }
            }
            catch (Exception ex)
            {
                CurrSQLEditor.ShowInfoText(ex.Message + (ex.InnerException != null ? " (" + ex.InnerException.Message + ")" : ""));
            }
        }

        public void SetStatusText(string msg)
        {
            statusText.Text = msg;
        }
        private bool checkConnection()
        {
            if (Provider.Database == null)
            {
                MessageBox.Show("Please select a database first", "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        internal void populateTreeNodesForDatabase(TreeNode parentNode)
        {
            ConnectionSettings cs = parentNode.Tag as ConnectionSettings;
            if (cs == null)
                return; //***

            TreeNode schemasNode = parentNode.Nodes.Add("ER Diagrams", "ER Diagrams", "Folder", "Folder");
            schemasNode.Tag = cs.Schemas;

            TreeNode tablesNode = parentNode.Nodes.Add("Tables", "Tables", "Folder", "Folder");
            tablesNode.Tag = cs.Database.Tables;
            TreeNode viewsNode = parentNode.Nodes.Add("Views", "Views", "Folder", "Folder");
            viewsNode.Tag = cs.Database.Tables;

            foreach (Table tbl in cs.Database.Tables.OrderBy(t=>t.Name))
                populateTreeNodesFor(tbl.IsView ? viewsNode : tablesNode, tbl);

            foreach (Diagram schema in cs.Schemas.OrderBy(s => s.Name))
                populateTreeNodesFor(schemasNode, schema);
        }
        internal void populateTreeNodesFor(TreeNode parentNode, Diagram schema)
        {
            TreeNode tnSchema = parentNode.Nodes.Add(schema.Name, schema.Name, "Diagram", "Diagram");
            tnSchema.Tag = schema;
        }
        internal void populateTreeNodesFor(TreeNode parentNode, Table tbl)
        {
            if (parentNode == null)
                parentNode = findNode(tbl.Database.Tables);

            TreeNode tnTable = parentNode.Nodes.Add(tbl.Name, tbl.Name, tbl.IsView ? "View" : "Table", tbl.IsView ? "View" : "Table");
            tnTable.Tag = tbl;

            TreeNode columnsNode = tnTable.Nodes.Add("Columns", "Columns", "Folder", "Folder");
            columnsNode.Tag = tbl.Columns;
            foreach (Column column in tbl.Columns.OrderBy(c => c.Name))
                populateTreeNodesFor(columnsNode, column);

            TreeNode keysNode = tnTable.Nodes.Add("Constraints", "Constraints", "Folder", "Folder");
            keysNode.Tag = tbl.Constraints;
            if (tbl.Constraints != null)
                foreach (Constraint constraint in tbl.Constraints.OrderBy(t => t.Name))
                    populateTreeNodesFor(keysNode, constraint);

            TreeNode indexesNode = tnTable.Nodes.Add("Indexes", "Indexes", "Folder", "Folder");
            keysNode.Tag = tbl.Indices;
            if (tbl.Indices != null)
                foreach (Index index in tbl.Indices.OrderBy(t => t.Name))
                    populateTreeNodesFor(indexesNode, index);
        }
        internal void populateTreeNodesFor(TreeNode parentNode, BaseIndexConstraint index)
        {
            if(index is Index)
                populateTreeNodesFor(parentNode, index as Index);
            else
                populateTreeNodesFor(parentNode, index as Constraint);
        }
        internal void populateTreeNodesFor(TreeNode parentNode, Index index)
        {
            if (parentNode == null)
                parentNode = findNode(index.Table.Indices);

            TreeNode tnKey = parentNode.Nodes.Add(index.Name, index.Name + " (" + string.Join(", ", index.ColumnNames.ToArray()) + ")", "Index", "Index");
            tnKey.Tag = index;
        }
        internal void populateTreeNodesFor(TreeNode parentNode, Constraint index)
        {
            if (parentNode == null)
                parentNode = findNode(index.Table.Constraints);

            TreeNode tnKey = parentNode.Nodes.Add(index.Name, index.Name + " (" + string.Join(", ", index.ColumnNames.ToArray()) + ")", index is PrimaryKeyConstraint ? "Key" : "Constraint", index is PrimaryKeyConstraint ? "Key" : "Constraint");
            tnKey.Tag = index;
        }
        internal void populateTreeNodesFor(TreeNode parentNode, Column fld)
        {
            if (parentNode == null)
                parentNode = findNode(fld.Table.Columns);

            TreeNode tnColumn = parentNode.Nodes.Add(fld.Name, fld.Name + " (" + fld.ColumnType + ")", fld.IsPrimaryKey ? "Key" : "Column", fld.IsPrimaryKey ? "Key" : "Column");
            tnColumn.Tag = fld;
        }

        private ConnectionSettings findConnection(TreeNode treeNode)
        {
            if (treeNode == null)
                return null;

            while (treeNode.Parent != null)
            {
                if (treeNode.Tag is ConnectionSettings)
                    return (ConnectionSettings)treeNode.Tag;
                treeNode = treeNode.Parent;
            }
            return null;
        }
        private TreeNode findNode(TreeNodeCollection nodes, Predicate<TreeNode> predicate)
        {
            foreach (TreeNode node in nodes)
            {
                if (predicate(node))
                    return node;
                TreeNode n = findNode(node.Nodes, predicate);
                if (n != null)
                    return n;
            }
            return null;
        }
        internal TreeNode findNode(object tag) 
        {
            TreeNode node = findNode(treeView.Nodes, tn => tn.Tag == tag);
            if (node == null)
                node = findNode(treeCodeGen.Nodes, tn => tn.Tag == tag);
            return node;
        }
        private TreeNode findSelectedDBNode()
        {
            TreeNode treeNode = treeView.SelectedNode;
            while (treeNode != null)
            {
                if (treeNode.Tag is ConnectionSettings)
                    return treeNode;
                treeNode = treeNode.Parent;
            }
            return null;
        }
        private Table findSelectedTable()
        {
            if (SelectedObject is Table)
                return SelectedObject as Table;

            if (SelectedObject is ColumnCollection)
                return (SelectedObject as ColumnCollection).Table;

            if (SelectedObject is Column)
                return (SelectedObject as Column).Table;

            if (SelectedObject is IndexCollection)
                return (SelectedObject as IndexCollection).Table;

            if (SelectedObject is BaseIndexConstraint)
                return (SelectedObject as BaseIndexConstraint).Table;

            return null;
        }

        internal void addSQLEditor(string filePath, string sql, bool temp = false)
        {
            foreach (MyTabPage myTabPage in tabControlEditors.TabPages)
                if (!string.IsNullOrEmpty(filePath) && myTabPage.ToolTipText == filePath)
                {
                    tabControlEditors.SelectedTab = myTabPage;
                    return;
                }

            MyTabPage tp = new MyTabPage();
            //tp.ImageKey = "Query";
            tp.ImageIndex = 1;

            SQLEditorAndResults sqlEd = new SQLEditorAndResults(filePath, sql, temp);
            sqlEd.Dock = DockStyle.Fill;
            tp.Controls.Add(sqlEd);
            tp.ToolTipText = filePath;
            tp.Text = (temp || string.IsNullOrEmpty(filePath)) ? "Query" : Path.GetFileName(filePath);
            tabControlEditors.TabPages.Add(tp);
            tabControlEditors.SelectTab(tp);

            SetFont(tp, Font);
        }
        internal void addFileEditor(string filePath, string content="")
        {
            foreach (MyTabPage myTabPage in tabControlEditors.TabPages)
                if (!string.IsNullOrEmpty(filePath) && myTabPage.ToolTipText == filePath)
                {
                    tabControlEditors.SelectedTab = myTabPage;
                    return;
                }
            //if (!File.Exists(filePath))
            //    throw new Exception("File not found");

            MyTabPage tp = new MyTabPage();
            //tp.ImageKey = "File";
            tp.ImageIndex = 2;

            TemplateEditor ed = new TemplateEditor(filePath, content);
            ed.Dock = DockStyle.Fill;
            tp.Controls.Add(ed);
            tp.ToolTipText = filePath;
            if (File.Exists(filePath))
                tp.Text = Path.GetFileName(filePath);
            tabControlEditors.TabPages.Add(tp);
            tabControlEditors.SelectTab(tp);

            SetFont(tp, Font);
        }
        private DiagramEditor addDiagram(Diagram schema)
        {
            foreach (MyTabPage myTabPage in tabControlEditors.TabPages)
                if (myTabPage.Controls[0] is DiagramEditor && (myTabPage.Controls[0] as DiagramEditor).CurrentSchema==schema)
                {
                    tabControlEditors.SelectedTab = myTabPage;
                    return myTabPage.Controls[0] as DiagramEditor;
                }

            MyTabPage tp = new MyTabPage();
            //tp.ImageKey = "Diagram";
            tp.ImageIndex = 0;

            tp.Text = schema == null ? "New Diagram" : schema.Name;
            tp.BackColor = Color.FromKnownColor(KnownColor.Window);
            tp.AutoScroll = true;

            DiagramEditor d = new DiagramEditor();
            d.MainForm = this;
            if(schema!=null)
                d.CurrentSchema = schema;
            d.propertyGrid = this.propertyGrid;
            d.ContextMenuStrip = menuStripTree;
            tp.Controls.Add(d);
            tabControlEditors.TabPages.Add(tp);
            tabControlEditors.SelectTab(tp);

            d.Size = d.MinimumSize = tp.Size - new Size(tp.Padding.Left + tp.Padding.Right, tp.Padding.Top + tp.Padding.Bottom);
            SetFont(tp, Font);

            return d;
        }
        #endregion

        #region Commands
        private void cmdSave(string arg)
        {
            if (CurrEditor.Save())
                tabControlEditors.SelectedTab.Text = CurrEditor.GetName();
        }
        private void cmdSaveAs(string arg)
        {
            if(CurrSQLEditor.SaveAs())
                tabControlEditors.SelectedTab.Text = Path.GetFileName(CurrSQLEditor.FilePath);
        }
        private void cmdOpen(string arg)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Query Files|*.sql";
            if (ofd.ShowDialog() == DialogResult.OK)
                addSQLEditor(ofd.FileName, "");
        }

        bool cancel = false;
        private void cmdCloseEditor(string arg)
        {
            cancel = false;

            if (CurrEditor.Modified)
            {
                DialogResult dr = MessageBox.Show("Would you like to save?", "Cinar Database Tools", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                    CurrEditor.Save();
                
                if (dr == DialogResult.Cancel)
                    cancel = true;
            }
            if (!cancel)
            {
                CurrEditor.OnClose();
                tabControlEditors.TabPages.Remove(tabControlEditors.SelectedTab);
            }
        }

        private void cmdCloseAllButThis(string arg)
        {
            TabPage page = tabControlEditors.SelectedTab;
            while (tabControlEditors.TabPages.Count>1 && cancel==false)
            {
                if (page == tabControlEditors.SelectedTab) {
                    int i = tabControlEditors.TabPages.IndexOf(page);
                    tabControlEditors.SelectedIndex = (i == 0) ? tabControlEditors.TabPages.Count - 1 : 0;
                }
                cmdCloseEditor("");
            }
        }

        private void cmdCloseAll(string arg)
        {
            while (tabControlEditors.TabPages.Count > 0 && cancel == false)
            {
                cmdCloseEditor("");
            }
        }

        private void cmdEditorCommand(string arg)
        {
            Control c = ActiveControl;
            while (c is ContainerControl) c = (c as ContainerControl).ActiveControl;
            if (c == null) return;

            switch (arg)
            {
                case "Undo":
                    if (c is TextBoxBase) 
                        (c as TextBoxBase).Undo();
                    else if (c is TextArea)
                        new ICSharpCode.TextEditor.Actions.Undo().Execute(c as TextArea);
                    break;
                case "Redo":
                    if (c is TextBoxBase)
                        (c as TextBoxBase).Undo();
                    else if (CurrSQLEditor != null)
                        new ICSharpCode.TextEditor.Actions.Redo().Execute(c as TextArea);
                    break;
                case "Cut":
                    if (c is TextBoxBase)
                        (c as TextBoxBase).Cut();
                    else if (c is TextArea)
                        new ICSharpCode.TextEditor.Actions.Cut().Execute(c as TextArea);
                    break;
                case "Copy":
                    if (c is TextBoxBase)
                        (c as TextBoxBase).Copy();
                    else if (c is TextArea)
                        new ICSharpCode.TextEditor.Actions.Copy().Execute(c as TextArea);
                    break;
                case "Paste":
                    if (c is TextBoxBase)
                        (c as TextBoxBase).Paste();
                    else if (c is TextArea)
                        new ICSharpCode.TextEditor.Actions.Paste().Execute(c as TextArea);
                    break;
                case "SelectAll":
                    if (c is TextBoxBase)
                        (c as TextBoxBase).SelectAll();
                    else if (c is TextArea)
                        new ICSharpCode.TextEditor.Actions.SelectWholeDocument().Execute(c as TextArea);
                    break;
                case "Find":
                case "Replace":
                    if (c is TextArea && CurrSQLEditor != null)
                    {
                        FindDialog fd = new FindDialog(CurrSQLEditor.SQLEditor);
                        fd.Show();
                    }
                    break;
                case "Beautify":
                    string selectedText = null;
                    if (c is TextBoxBase)
                        selectedText = (c as TextBoxBase).SelectedText;
                    else if (c is TextArea)
                        selectedText = (c as TextArea).SelectionManager.SelectedText;

                    if (string.IsNullOrEmpty(selectedText))
                    {
                        MessageBox.Show("Select SQL statement to beautify", "Cinar Database Tools");
                        return;
                    }

                    SQLParser.Parser parser = new SQLParser.Parser(new StringReader(selectedText));
                    string beautifiedSQL = "";
                    try
                    {

                        SQLParser.Statement statement = parser.ReadNextStatement();
                        while (statement != null)
                        {
                            beautifiedSQL += statement.ToString();
                            statement = parser.ReadNextStatement();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL parse error:\n" + ex.Message, "Cinar Database Tools");
                        break;
                    }

                    if (c is TextBoxBase)
                    {
                        (c as TextBoxBase).SelectedText = beautifiedSQL;
                    }
                    else if (c is TextArea)
                    {
                        ISelection sel = (c as TextArea).SelectionManager.SelectionCollection[0];
                        (c as TextArea).Document.Replace(sel.Offset, sel.Length, beautifiedSQL);
                        (c as TextArea).SelectionManager.ClearSelection();
                    }
                    break;
            }
            c.Refresh();
        }

        private void cmdRefresh(string arg)
        {
            TreeNode dbNode = findSelectedDBNode();
            dbNode.Nodes.Clear();
            populateTreeNodesForDatabase(dbNode);
            //treeView.Sort();
        }
        private void cmdRefreshMetadata(string arg)
        {
            if (arg=="nowarn" || MessageBox.Show("Metada will be reread from database.\nThis may take a long time according to the number of tables.\n\nContinue?", "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.Database oldDb = Provider.Database;
                Provider.ActiveConnection.RefreshDatabaseSchema();

                foreach (Table oldTable in oldDb.Tables)
                {
                    Table newTable = Provider.Database.Tables[oldTable.Name];
                    if (newTable == null) continue;

                    foreach (Column oldColumn in oldTable.Columns)
                    {
                        Column newColumn = newTable.Columns[oldColumn.Name];
                        if (newColumn == null) continue;

                        newColumn.UIMetadata = oldColumn.UIMetadata;
                    }

                    newTable.UIMetadata = oldTable.UIMetadata;
                }
                
                TreeNode dbNode = findSelectedDBNode();
                dbNode.Nodes.Clear();
                populateTreeNodesForDatabase(dbNode);
                //treeView.Sort();
            }
        }

        private void cmdOpenConnectionsFile(string arg)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Path.GetDirectoryName(Provider.ConnectionsPath);
            ofd.Filter = "Connection Files|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
                showConnections(ofd.FileName);
        }
        private void cmdSaveConnectionsAs(string arg)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Path.GetDirectoryName(Provider.ConnectionsPath);
            sfd.FileName = Provider.ConnectionsPath;
            sfd.Filter = "Connection Files|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Provider.ConnectionsPath = sfd.FileName;
                Provider.SaveConnections();
            }
        }

        private void cmdShowHiddenConnections(string arg)
        {
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                TreeNode tn = findNode(treeView.Nodes, t => t.Tag == cs);
                if (tn == null)
                {
                    tn = createConnectionNode(cs);
                    populateTreeNodesForDatabase(tn);
                }
            }
        }
        private void cmdHideConnection(string arg)
        {
            ConnectionSettings cs = SelectedObject as ConnectionSettings;
            if (cs == null) return;

            treeView.SelectedNode.Remove();
            cbActiveConnection.Items.Remove(cs);
        }

        private void cmdNewConnection(string arg)
        {
            FormConnect f = new FormConnect();
            if (arg == "create")
                f.Text = "Create Database";
            if (SelectedObject is ConnectionSettings)
            {
                ConnectionSettings curr = SelectedObject as ConnectionSettings;
                f.txtDBName.Text = "";
                f.txtHost.Text = curr.Host;
                f.txtPassword.Text = curr.Password;
                f.cbProvider.SelectedItem = curr.Provider.ToString();
                f.txtUserName.Text = curr.UserName;
            }

            while (f.ShowDialog() == DialogResult.OK)
            {
                ConnectionSettings cs = new ConnectionSettings();
                cs.DbName = f.txtDBName.Text;
                cs.Host = f.txtHost.Text;
                cs.Password = f.txtPassword.Text;
                cs.Provider = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), f.cbProvider.SelectedItem.ToString());
                cs.UserName = f.txtUserName.Text;
                cs.CreateDatabaseIfNotExist = f.cbCreateDatabase.Checked;
                try
                {
                    cs.RefreshDatabaseSchema();
                    Provider.Connections.Add(cs);
                    TreeNode tn = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), cs.Provider.ToString(), cs.Provider.ToString());
                    tn.Tag = cs;
                    tn.NodeFont = new Font(this.Font, FontStyle.Underline);
                    cbActiveConnection.Items.Add(cs);
                    treeView.SelectedNode = tn;
                    tn.Expand();
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.DialogResult = DialogResult.None;
                }
            }
        }
        private void cmdEditConnection(string arg)
        {
            ConnectionSettings cs = (ConnectionSettings)SelectedObject;
            FormConnect f = new FormConnect();
            f.txtDBName.Text = cs.DbName;
            f.txtHost.Text = cs.Host;
            f.txtPassword.Text = cs.Password;
            f.cbProvider.SelectedItem = cs.Provider.ToString();
            f.txtUserName.Text = cs.UserName;
            while (f.ShowDialog() == DialogResult.OK)
            {
                cs.DbName = f.txtDBName.Text;
                cs.Host = f.txtHost.Text;
                cs.Password = f.txtPassword.Text;
                cs.Provider = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), f.cbProvider.SelectedItem.ToString());
                cs.UserName = f.txtUserName.Text;
                try
                {
                    cs.RefreshDatabaseSchema();
                    treeView.SelectedNode.Name = treeView.SelectedNode.Text = cs.ToString();
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.DialogResult = DialogResult.None;
                }
            }
        }
        private void cmdDeleteConnection(string arg)
        {
            ConnectionSettings cs = (ConnectionSettings)SelectedObject;
            if (MessageBox.Show("The connection \"" + cs + "\" will be deleted. It doesn't harm your data.\n\nContinue?", "Cinar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                TreeNode currNode = treeView.SelectedNode;
                if (currNode.PrevNode != null)
                    treeView.SelectedNode = currNode.PrevNode;
                else if (currNode.NextNode != null)
                    treeView.SelectedNode = currNode.NextNode;
                Provider.Connections.Remove(cs);
                currNode.Remove();

                cbActiveConnection.Items.Remove(cs);

                Provider.ConnectionsModified = true;

                SetStatusText("Connection deleted.");
            }
        }

        private void cmdShowTableInDiagram(string arg)
        {
            Table tbl = SelectedObject as Table;

            Diagram d = new Diagram();
            d.Name = tbl.Name + " diagram";
            d.conn = Provider.ActiveConnection;

            List<Table> list = new List<Table>();
            list.Add(tbl);

            List<Table> parents = tbl.ReferenceTables.Except(list).ToList();
            foreach (Table t in parents)
                if (!list.Contains(t))
                    list.Add(t);

            List<Table> children = tbl.ReferencedByTables.Except(list).ToList();
            foreach (Table t in children)
                if (!list.Contains(t))
                    list.Add(t);

            DiagramEditor de = addDiagram(d);
            de.AddTablesToSchema(list, true);

            for (int i = 0; i < parents.Count; i++)
                d.GetTableView(parents[i].Name).Position = new Point(de.Width / 4 - Diagram.Def_TableWidth/2, de.Height / (parents.Count + 1) * (i + 1));
            TableView tv = d.GetTableView(tbl.Name);
            tv.Position = new Point(de.Width / 2 - Diagram.Def_TableWidth / 2, (de.Height - tv.Size.Height) / 2);
            tv.ShowFull = true;
            for (int i = 0; i < children.Count; i++)
                d.GetTableView(children[i].Name).Position = new Point(de.Width / 4 * 3 - Diagram.Def_TableWidth / 2, de.Height / (children.Count + 1) * (i + 1));

            d.CorrectConnectionLinesPositions();
            de.Invalidate();
        }

        private void cmdShowUIMetadataPreview(string arg)
        {
            UIMetadataPreview p = new UIMetadataPreview(this.propertyGrid);
            p.Connection = Provider.ActiveConnection;

            foreach (MyTabPage myTabPage in tabControlEditors.TabPages)
                if (!string.IsNullOrEmpty(p.Name) && myTabPage.ToolTipText == p.GetName())
                {
                    tabControlEditors.SelectedTab = myTabPage;
                    return;
                }

            MyTabPage tp = new MyTabPage();
            //tp.ImageKey = "Query";
            tp.ImageIndex = 1;

            p.Dock = DockStyle.Fill;
            tp.Controls.Add(p);
            tp.ToolTipText = p.GetName();
            tp.Text = p.GetName();
            tabControlEditors.TabPages.Add(tp);
            tabControlEditors.SelectTab(tp);

            SetFont(tp, Font);

            p.Preview();
        }

        private void cmdDeleteSelectedObject(string arg)
        {
            if (SelectedObject is ConnectionSettings)
                cmdDeleteConnection("");
            else if (SelectedObject is Table)
                cmdTableDrop("");
            else if (SelectedObject is Column)
                cmdColumnDrop("");
            else if (SelectedObject is BaseIndexConstraint)
                cmdDropIndex("");
        }

        private void cmdTogglePropertiesPanel(string arg)
        {
            this.SuspendLayout();
            if (propertyGrid.Visible)
            {
                splitterPropLast = splitContainerProperties.SplitterDistance;
                splitContainerProperties.SplitterDistance = splitContainerProperties.Width - 30;
                splitContainerProperties.Panel2.BackgroundImage = null;
                propertyGrid.Visible = false;
            }
            else
            {
                splitContainerProperties.SplitterDistance = splitterPropLast;
                splitContainerProperties.Panel2.BackgroundImage = imgYellowBg;
                propertyGrid.Visible = true;
            }
            this.PerformLayout();
        }
        private void cmdToggleConnectionsPanel(string arg)
        {
            this.SuspendLayout();
            if (treeView.Visible)
            {
                splitterMainLast = splitContainerMain.SplitterDistance;
                splitContainerMain.SplitterDistance = 22;
                splitContainerMain.Panel1.BackgroundImage = null;
                treeView.Visible = false;
            }
            else
            {
                splitContainerMain.Panel1.BackgroundImage = imgYellowBg;
                splitContainerMain.SplitterDistance = splitterMainLast;
                treeView.Visible = true;
            }
            this.PerformLayout();
        }

        private void cmdShowTableCounts(string arg)
        {
            foreach (TreeNode node in findSelectedDBNode().Nodes["Tables"].Nodes)
            { 
                Table tbl = node.Tag as Table;
                string count = "?";
                try { count = Provider.Database.GetString("select count(*) from [" + tbl + "]"); }
                catch { }
                node.Text = string.Format("{0} ({1})", tbl.Name, count);
            }
        }

        private void cmdDoDatabaseOperation(string arg)
        {
            switch (arg)
            {
                case "Drop":
                    if (MessageBox.Show("Database will be dropped and ALL DATA LOST.\n\nContinue?", "Cinar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Provider.Database.ExecuteNonQuery("drop database " + Provider.Database.Name);
                        TreeNode tn = findSelectedDBNode();
                        Provider.Connections.Remove(Provider.ActiveConnection);
                        tn.Remove();
                        if (ObjectRemoved != null)
                            ObjectRemoved(this, new DbObjectRemovedArgs { Object = tn.Tag });
                        CurrSQLEditor.ShowInfoText(string.Format("Database {0} dropped successfully.", tn.Text));
                    }
                    break;
                case "Create":
                    cmdNewConnection("create");
                    break;
                case "Truncate":
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (Table t in Provider.Database.Tables)
                            sb.AppendLine("truncate table [" + t.Name + "];");
                        SQLInputDialog sid = new SQLInputDialog(sb.ToString(), false);
                        if (sid.ShowDialog() == DialogResult.OK)
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                        break;
                    }
                case "Empty":
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (Table t in Provider.Database.Tables)
                            foreach (Constraint c in t.Constraints)
                                if (c is Cinar.Database.ForeignKeyConstraint)
                                    sb.AppendLine(Provider.Database.GetSQLConstraintRemove(c) + ";");
                        foreach (Table t in Provider.Database.Tables)
                            sb.AppendLine(Provider.Database.GetSQLTableDrop(t, true) + ";");
                        SQLInputDialog sid = new SQLInputDialog(sb.ToString(), false);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                            cmdRefreshMetadata("nowarn");
                        }
                        break;
                    }
                case "Transfer":
                    cmdShowForm(typeof(FormDBTransfer).FullName);
                    break;
                case "Backup":
                    cmdShowForm(typeof(FormSQLDump).FullName);
                    break;
            }
        }
        private void cmdCreateDBObject(string arg)
        {
            if (arg == "-")
                return;

            if (arg == "Table")
            {
                cmdCreateTable("");
                return;
            }

            string key = "SQLCreate"+arg+Provider.Database.Provider;
            string sql = SQLResources.ResourceManager.GetString(key);
            addSQLEditor("", sql);
            //TODO: add postgre & MS SQL implementations to resources.
        }
        private void cmdExecuteSQLFromFile(string arg)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Query Files|*.sql";
            if (ofd.ShowDialog() == DialogResult.OK)
                executeSQL(File.ReadAllText(ofd.FileName));
        }

        private void cmdExecuteSQL(string arg)
        {
            if(!checkConnection()) return;

            string sel = CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (string.IsNullOrEmpty(sel))
                sel = CurrSQLEditor.SQLEditor.Text;

            //Interpreter engine = new Interpreter(sel, null);
            //engine.SetAttribute("db", Provider.Database);
            //engine.SetAttribute("util", new Util());
            //engine.SetAttribute("table", findSelectedTable());
            //engine.SetAttribute("tree", treeView);
            //engine.Parse();
            //engine.Execute();
            string sql = sel; //engine.Output;

            executeSQL(sql);
        }
        private void cmdExecuteScript(string arg)
        {
            Interpreter engine = new Interpreter(CurrSQLEditor.SQLEditor.Text, new List<string> { "Cinar.DBTools" });
            engine.SetAttribute("db", Provider.Database);
            engine.SetAttribute("util", new Util());
            engine.SetAttribute("table", findSelectedTable());
            engine.SetAttribute("tree", treeView);
            engine.Parse();
            engine.Execute();

            CurrSQLEditor.ShowInfoText(engine.Output);

            SetStatusText("Script executed succesfully.");
        }

        internal void cmdSetActiveConnection(string arg)
        {
            TreeNode tnOld = findNode(treeView.Nodes, n => n.BackColor == Color.LightBlue);
            if (tnOld != null)
                tnOld.BackColor = Color.White; 

            if (SelectedObject is ConnectionSettings)
            {
                Provider.ActiveConnection = (ConnectionSettings)SelectedObject;

                if (treeView.SelectedNode.Nodes.Count == 0)
                    populateTreeNodesForDatabase(treeView.SelectedNode);
            }
            else
            {
                Provider.ActiveConnection = findConnection(treeView.SelectedNode);
            }

            TreeNode tnNew = findSelectedDBNode();
            if (tnNew != null)
                tnNew.BackColor = Color.LightBlue;

            cbActiveConnection.SelectedItem = Provider.ActiveConnection;

            SetStatusText("Active connection: " + Provider.ActiveConnection ?? "None");
        }

        private void cmdTableOpen(string arg)
        {
            Table tbl = findSelectedTable();
            if (tbl == null)
                return;

            FilterExpression fExp = new FilterExpression();
            if (arg == "WithFilter")
            {
                FilterExpressionDialog fed = new FilterExpressionDialog(tbl);
                if (fed.ShowDialog() == DialogResult.OK)
                    fExp = fed.FilterExpression;
                else
                    return;
            }

            if (CurrSQLEditor != null)
            {
                CurrSQLEditor.ShowTableData(tbl, fExp);
            }
            else
            {
                findOrCreateNewSQLEditor();
                CurrSQLEditor.ShowTableData(tbl, fExp);
            }
        }

        private void findOrCreateNewSQLEditor()
        {
            TabPage tabPage = tabControlEditors.TabPages.OfType<TabPage>().FirstOrDefault(tp => tp.Controls[0] is SQLEditorAndResults);
            if (tabPage != null)
                tabControlEditors.SelectedTab = tabPage;
            else
                addSQLEditor("", "");
        }
        internal void cmdTableDrop(string arg)
        {
            if (!checkConnection()) return;
            Table table = SelectedObject as Table;
            SQLInputDialog sid = new SQLInputDialog(Provider.Database.GetSQLTableDrop(table, true), true, string.Format("Table \"{0}\" will be dropped and ALL DATA LOST.\n\nContinue?", table.Name));
            if(sid.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    int i = Provider.Database.ExecuteNonQuery(sid.SQL);
                    Provider.Database.Tables.Remove(table);
                    findNode(table).Remove();
                    if (ObjectRemoved != null)
                        ObjectRemoved(this, new DbObjectRemovedArgs { Object = table });
                    if (CurrSQLEditor != null)
                        CurrSQLEditor.ShowInfoText("Table " + table.Name + " dropped succesfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        internal void cmdTableTruncate(string arg)
        {
            if (!checkConnection()) return;
            Table table = SelectedObject as Table;
            SQLInputDialog sid = new SQLInputDialog(Provider.Database.GetSQLTableTruncate(table, true), true, string.Format("Table \"{0}\" will be truncated and ALL DATA LOST.\n\nContinue?", table.Name));
            if (sid.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int i = Provider.Database.ExecuteNonQuery(sid.SQL);
                    if (CurrSQLEditor != null)
                        CurrSQLEditor.ShowInfoText("Table " + table.Name + " truncated succesfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdTableCount(string arg)
        {
            if (!checkConnection()) return;
            Table table = SelectedObject as Table;
            if(table==null)
            {
                MessageBox.Show("Please select table first", "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tableName = table.Name;
            executeSQL("select count(*) AS number_of_rows from [" + tableName + "]");
        }
        CinarWebServer server = null;
        private void cmdAnalyzeTable(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            
            int count = Provider.Database.GetInt("select count(*) from [" + tableName + "]");

            DataTable dtMaxMin = null;
            if (count > 0)
            {
                List<string> list = new List<string>();
                foreach (Column f in Provider.Database.Tables[tableName].Columns)
                    list.Add("select '" + f.Name + "' as [Column Name], min([" + f.Name + "]) as [Min. Value], max([" + f.Name + "]) as [Max. Value] from [" + tableName + "]");
                string sql = string.Join("\nUNION\n", list.ToArray());
                dtMaxMin = Provider.Database.GetDataTable(sql);
            }

            if (server == null)
            {
                server = new CinarWebServer(3000);
                server.Start();
                server.ProcessRequest = (Request req, Response resp) =>
                {
                    resp.WriteLine("Method: {0}<br/>Url: {1}<br/>Protocol: {2}", req.Method, req.Url, req.ProtocolVersion);
                    resp.WriteLine("<hr/><pre>{0}</pre>", req.Headers.ToJSON());
                };
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
                <html>
                <head>
                    <style>
                    body, td, th {{font-family:Microsoft Sans Serif; font-size:12px}}
					div.header {{background:navy; color:white; font-size:16px}}
                    </style>
                </head>
                <body>
                    <div class=""header"">{0}</div>
<a href=""http://localhost:3000/dnm?x=2"">Click me..</a>
                    <b>Number of Rows:</b> {1}<br/>
                    <br/>
                    {2}
                </body>
                </html>", tableName, count, dtMaxMin.ToHtmlTable());

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\table_analyze.html";
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            CurrSQLEditor.Navigate(path);
        }
        private void cmdCreateTable(string arg)
        {
            CreateTable();
        }
        public Table CreateTable()
        {
            FormCreateTable fct = new FormCreateTable();
            Table tbl = null;
            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    tbl = fct.GetCreatedTable();
                    try
                    {
                        if (string.IsNullOrEmpty(tbl.Name))
                            throw new Exception("Enter a valid name for the table.");
                        if (tbl.Columns == null || tbl.Columns.Count == 0)
                            throw new Exception("Add minimum one column to the table.");
                        if (Provider.Database.Tables[tbl.Name] != null)
                            throw new Exception("There is already a table named " + tbl.Name);

                        Provider.Database.Tables.Add(tbl);
                        if (!CreateTable(Provider.Database, tbl))
                            Provider.Database.Tables.Remove(tbl);
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
            return tbl;
        }
        public bool CreateTable(Database.Database db, Table tbl)
        {
            string sql = tbl.ToDDL();
            SQLInputDialog sid = new SQLInputDialog(sql, false);
            if (sid.ShowDialog() == DialogResult.OK)
            {
                db.ExecuteNonQuery(sid.SQL);
                try
                {
                    populateTreeNodesFor(null, tbl);
                    if (ObjectAdded != null)
                        ObjectAdded(this, new DbObjectAddedArgs { Object = tbl });
                }
                catch { }
                return true;
            }
            return false;
        }
        
        private void cmdAlterTable(string arg)
        {
            FormCreateTable fct = new FormCreateTable();
            Table tbl = SelectedObject as Table;
            if (tbl == null) {
                MessageBox.Show("Select table first", "Cinar Database Tools");
                return;
            }
            fct.SetTable(tbl);
            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    TableDef tblNew = fct.GetAlteredTable();
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (ColumnDef colNew in tblNew.Columns)
                        {
                            // field yeni eklenmişse
                            if (colNew.OriginalColumn == null) 
                            { 
                                Column f = new Column()
                                {
                                    Name = colNew.Name,
                                    ColumnTypeOriginal = colNew.ColumnType,
                                    ColumnType = Provider.Database.StringToDbType(colNew.ColumnType),
                                    Length = colNew.Length,
                                    DefaultValue = colNew.DefaultValue,
                                    IsNullable = colNew.IsNullable,
                                    IsAutoIncrement = colNew.IsAutoIncrement
                                };
                                tbl.Columns.Add(f);
                                sb.AppendLine(Provider.Database.GetSQLColumnAdd(tbl.Name, f) + ";");

                                if (colNew.IsPrimaryKey)
                                {
                                    PrimaryKeyConstraint k = new PrimaryKeyConstraint();
                                    tbl.Constraints.Add(k);
                                    k.ColumnNames.Add(f.Name);
                                    k.Name = "PK_" + tbl.Name;
                                    sb.AppendLine(Provider.Database.GetSQLConstraintAdd(k) + ";");
                                }

                                continue;
                            }
                            // field'ın adı değiştirilmişse
                            if (colNew.OriginalColumn.Name != colNew.Name)
                            {
                                string oldName = colNew.OriginalColumn.Name;
                                colNew.OriginalColumn.Name = colNew.Name;
                                sb.AppendLine(Provider.Database.GetSQLColumnRename(oldName, colNew.OriginalColumn) + ";");
                            }
                            // default value'su değiştirilmişse
                            if (colNew.OriginalColumn.DefaultValue != colNew.DefaultValue)
                            {
                                colNew.OriginalColumn.DefaultValue = colNew.DefaultValue;
                                sb.AppendLine(Provider.Database.GetSQLColumnChangeDefault(colNew.OriginalColumn) + ";");
                            }
                            // field type, length veya nullable değiştirilmişse
                            if (colNew.OriginalColumn.ColumnTypeOriginal != colNew.ColumnType || colNew.OriginalColumn.Length != colNew.Length || colNew.OriginalColumn.IsNullable != colNew.IsNullable)
                            {
                                var dependentConstraints = colNew.OriginalColumn.Table.Constraints.Where(cons => cons.ColumnNames.Contains(colNew.OriginalColumn.Name)).ToList();
                                foreach(var cons in dependentConstraints)
                                    sb.AppendLine(Provider.Database.GetSQLConstraintRemove(cons) + ";");

                                colNew.OriginalColumn.ColumnTypeOriginal = colNew.ColumnType;
                                colNew.OriginalColumn.ColumnType = Provider.Database.StringToDbType(colNew.ColumnType);
                                colNew.OriginalColumn.Length = colNew.Length;
                                colNew.OriginalColumn.IsNullable = colNew.IsNullable;

                                sb.AppendLine(Provider.Database.GetSQLColumnChangeDataType(colNew.OriginalColumn) + ";");

                                foreach (var cons in dependentConstraints)
                                    sb.AppendLine(Provider.Database.GetSQLConstraintAdd(cons) + ";");
                            }
                            // primary key kaldırılmışsa
                            if (colNew.OriginalColumn.IsPrimaryKey == true && colNew.IsPrimaryKey == false)
                            {
                                var k = colNew.OriginalColumn.Table.Constraints.FirstOrDefault(cons => cons is PrimaryKeyConstraint);
                                if (k != null)
                                {
                                    colNew.OriginalColumn.Table.Constraints.Remove(k);
                                    sb.AppendLine(Provider.Database.GetSQLConstraintRemove(k) + ";");
                        }
                            }
                            // primary key yapılmışsa
                            if (colNew.OriginalColumn.IsPrimaryKey == false && colNew.IsPrimaryKey == true)
                            {
                                PrimaryKeyConstraint k = new PrimaryKeyConstraint();
                                tbl.Constraints.Add(k);
                                k.ColumnNames.Add(colNew.Name);
                                k.Name = "PK_" + tbl.Name;
                                sb.AppendLine(Provider.Database.GetSQLConstraintAdd(k) + ";");
                            }
                        }
                        List<Column> deletedColumns = new List<Column>();
                        foreach (Column c in tbl.Columns)
                            if (!tblNew.Columns.Any(nc => nc.Name == c.Name))
                                deletedColumns.Add(c);
                        foreach (Column c in deletedColumns)
                            sb.AppendLine(Provider.Database.GetSQLColumnRemove(c) + ";");

                        if (tbl.Name != tblNew.Name)
                        {
                            sb.AppendLine(Provider.Database.GetSQLTableRename(tbl.Name, tblNew.Name) + ";");
                            tbl.Name = tblNew.Name;
                        }


                        string sql = sb.ToString();
                        SQLInputDialog sid = new SQLInputDialog(sql, false);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                            try
                            {
                                findNode(tbl).Remove();
                                populateTreeNodesFor(null, tbl);
                                if (ObjectChanged != null)
                                    ObjectChanged(this, new DbObjectChangedArgs { Object = tbl });
                            }
                            catch { }
                        }
                        else 
                        {
                            tblNew.UndoChanges();
                        }

                        break;
                    }
                    catch (Exception ex)
                    {
                        tblNew.UndoChanges();
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
        }

        private void cmdGenerateSQL(string arg)
        {
            Table table = (Table)SelectedObject;
            StringBuilder sb = new StringBuilder();
            switch (arg)
            {
                case "CreateTable":
                    if (table.IsView)
                        sb.Append(Provider.Database.GetSQLViewCreate(table));
                    else
                        sb.Append(table.ToDDL());
                    break;
                case "Insert":
                    sb.AppendLine("insert into " + table.Name + "(");
                    foreach (Column column in table.Columns)
                        if (!column.IsPrimaryKey)
                            sb.AppendLine("\t" + column.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(") values (");
                    foreach (Column column in table.Columns)
                        if (!column.IsPrimaryKey)
                            sb.AppendLine("\t@" + column.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(")");
                    break;
                case "Update":
                    sb.AppendLine("update " + table.Name + " set");
                    foreach (Column column in table.Columns)
                        if (!column.IsPrimaryKey)
                            sb.AppendLine("\t" + column.Name + " = @" + column.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine("where");
                    if (table.PrimaryColumn != null)
                        sb.AppendLine("\t@" + table.PrimaryColumn.Name + " = @" + table.PrimaryColumn.Name);
                    else
                        sb.AppendLine("\t1 = 2");
                    break;
                case "Select":
                    sb.AppendLine("select ");
                    foreach (Column column in table.Columns)
                        sb.AppendLine("\t" + column.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine("from");
                    sb.AppendLine("\t" + table.Name);
                    break;
                case "Dump":
                    sb.AppendLine(table.ToDDL());
                    sb.AppendLine(table.Dump(table.Database.Provider));
                    break;
                case "-":
                    return;
                default:
                    sb.AppendLine("Not available.");
                    break;
            }
            SetStatusText("SQL generated.");
            addSQLEditor("", sb.ToString());
        }

        private void cmdGenerateUIMetadata(string arg)
        {
            if (SelectedObject is ConnectionSettings)
            {
                ConnectionSettings cs = (ConnectionSettings)SelectedObject;
                if (MessageBox.Show("UI metadata will be generated for database " + cs.DbName + ". Continue?", "Cinar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                cs.Database.GenerateUIMetadata();
            }
            else if (SelectedObject is Table)
            {
                Table tbl = (Table)SelectedObject;
                if (MessageBox.Show("UI metadata will be generated for table " + tbl.Name + ". Continue?", "Cinar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                tbl.GenerateUIMetadata();
            }
            else if (SelectedObject is Column)
            {
                Column column = (Column)SelectedObject;
                if (MessageBox.Show("UI metadata will be generated for column " + column.Name + ". Continue?", "Cinar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                column.GenerateUIMetadata();
            }
        }

        private void cmdColumnDistinct(string arg)
        {
            if (!checkConnection() || !(SelectedObject is Column)) return;
            Column column = (Column)SelectedObject;
            executeSQL(@"
                select distinct top 1000 
                    [{0}] 
                from 
                    [{1}]", 
                          column.Name, 
                          column.Table.Name);
        }
        private void cmdColumnMax(string arg)
        {
            if (!checkConnection()) return;
            Column column = (Column)SelectedObject;
            executeSQL(@"
                select 
                    max([{0}]) AS {0}_MAX_value 
                from 
                    [{1}]",
                          column.Name,
                          column.Table.Name);
        }
        private void cmdColumnMin(string arg)
        {
            if (!checkConnection()) return;
            Column column = (Column)SelectedObject;
            executeSQL(@"
                select 
                    min([{0}]) AS {0}_MIN_value 
                from 
                    [{1}]", 
                          column.Name, 
                          column.Table.Name);
        }
        private void cmdColumnDrop(string arg)
        {
            Column column = SelectedObject as Column;
            SQLInputDialog sid = new SQLInputDialog(Provider.Database.GetSQLColumnRemove(column), true, string.Format("Column \"{0}.{1}\" will be dropped and ALL DATA LOST.\n\nContinue?", column.Table.Name, column.Name));
            if (sid.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int i = Provider.Database.ExecuteNonQuery(sid.SQL);
                    column.Table.Columns.Remove(column);
                    findNode(column).Remove();
                    if (ObjectRemoved != null)
                        ObjectRemoved(this, new DbObjectRemovedArgs { Object = column });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cmdGroupedCounts(string arg)
        {
            if (!checkConnection()) return;
            Column column = (Column)SelectedObject;
            showGroupedCounts(column);

        }

        private void showGroupedCounts(Column column)
        {
            string columnName = "[" + column.Name + "]";
            string columnNameAs = column.Name;

            if (column.SimpleColumnType == SimpleDbType.DateTime)
            {
                var interval = Provider.Database.GetDictionary<DateTime, DateTime>("select min(" + columnName+"), max("+columnName+") from ["+column.Table.Name+"]").First();

                if((interval.Value-interval.Key).Days<=2)
                    columnName = Provider.Database.GetSQLDateYearMonthDayHourPart(column.Name);
                else if ((interval.Value - interval.Key).Days < 90)
                    columnName = Provider.Database.GetSQLDateYearMonthDayPart(column.Name);
                else
                    columnName = Provider.Database.GetSQLDateYearMonthPart(column.Name);

                //columnName = Provider.Database.GetSQLDateYearMonthPart(column.Name);
                //columnNameAs = column.Name + "YearMonth";
            }

            if (column.ReferenceColumn == null || column.ReferenceColumn.Table.StringColumn == null)
            {
                executeSQL(@"
                    select top 1000 
                        {0} as [{1}], 
                        count(*) as [# of {2} records] 
                    from 
                        [{2}] 
                    group by {0} 
                    order by count(*) desc",
                                              columnName,
                                              columnNameAs,
                                              column.Table.Name);
            }
            else
            {
                executeSQL(@"
                    select top 1000 
                        t.[{0}], 
                        tRef.[{4}],
                        count(*) as [# of {1} records]
                    from 
                        [{1}] t
                        left join [{3}] tRef on t.[{0}] = tRef.[{2}]
                    group by t.[{0}], tRef.[{4}]
                    order by count(*) desc",
                                              column.Name,
                                              column.Table.Name,
                                              column.ReferenceColumn.Name,
                                              column.ReferenceColumn.Table.Name,
                                              column.ReferenceColumn.Table.StringColumn.Name);
            }

            MyDataGrid grid = CurrSQLEditor.Grid;
            grid.DoubleClick += delegate
            {
                if (grid.SelectedRows.Count <= 0)
                    return;
                if (columnNameAs.EndsWith("YearMonth"))
                    return;

                DataRow dr = (grid.SelectedRows[0].DataBoundItem as DataRowView).Row;
                object keyVal = dr[column.Name];
                string from = Provider.Database.GetFromWithJoin(column.Table);
                executeSQL(@"
                    select top 1000 
                        {4} 
                    from {1} 
                    where [{3}].[{0}] = '{2}'",
                                              column.Name,
                                              from,
                                              keyVal == null ? "" : keyVal.ToString().Replace("\\", "\\\\").Replace("'", "\\'"),
                                              column.Table.Name,
                                              Provider.Database.GetFieldNamesWithJoin(column.Table));
            };
        }

        private void cmdCreateIndex(string arg)
        {
            //TreeNode tn = findSelectedTableNode();
            Table table = findSelectedTable();
            if (table == null) return;

            FormCreateIndex fct = new FormCreateIndex(table);
            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    BaseIndexConstraint index = fct.GetCreatedKey();
                    try
                    {
                        if(index is Index)
                            table.Indices.Add((Index)index);
                        else
                            table.Constraints.Add((Constraint)index);

                        if (string.IsNullOrEmpty(index.Name))
                            throw new Exception("Enter a valid name.");
                        if (index.Columns == null || index.Columns.Count == 0)
                            throw new Exception("Select minimum one column.");

                        string sql = Provider.Database.GetSQLBaseIndexConstraintAdd(index);
                        SQLInputDialog sid = new SQLInputDialog(sql, false);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                            populateTreeNodesFor(null, index);
                            if (ObjectAdded != null) 
                                ObjectAdded(this, new DbObjectAddedArgs { Object = index });
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        if (index is Index)
                            table.Indices.Remove((Index)index);
                        else
                            table.Constraints.Remove((Constraint)index);
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
        }

        private void cmdEditIndex(string arg)
        {
            Table table = findSelectedTable();
            if (table == null) return;

            FormCreateIndex fct = new FormCreateIndex(table);
            BaseIndexConstraint oldIndex = SelectedObject as BaseIndexConstraint;
            fct.SetKey(oldIndex);

            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    BaseIndexConstraint index = fct.GetCreatedKey();
                    try
                    {
                        if (index is Index)
                            table.Indices.Add((Index)index);
                        else
                            table.Constraints.Add((Constraint)index);

                        if (string.IsNullOrEmpty(index.Name))
                            throw new Exception("Enter a valid name.");
                        if (index.ColumnNames == null || index.ColumnNames.Count == 0)
                            throw new Exception("Select minimum one column.");

                        string sql = Provider.Database.GetSQLBaseIndexConstraintRemove(oldIndex) + ";" + Environment.NewLine;
                        sql += Provider.Database.GetSQLBaseIndexConstraintAdd(index);
                        SQLInputDialog sid = new SQLInputDialog(sql, false);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                            if (oldIndex is Index)
                                table.Indices.Remove((Index)oldIndex);
                            else
                                table.Constraints.Remove((Constraint)oldIndex);
                            findNode(oldIndex).Remove();
                            if (ObjectRemoved != null)
                                ObjectRemoved(this, new DbObjectRemovedArgs { Object = oldIndex });
                            populateTreeNodesFor(null, index);
                            if (ObjectAdded != null)
                                ObjectAdded(this, new DbObjectAddedArgs { Object = index });
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        if (index is Index)
                            table.Indices.Remove((Index)index);
                        else
                            table.Constraints.Remove((Constraint)index);
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
        }
        private void cmdDropIndex(string arg)
        {
            if (!checkConnection()) return;
            Table table = findSelectedTable();
            BaseIndexConstraint index = SelectedObject as BaseIndexConstraint;

            try
            {
                string sql = Provider.Database.GetSQLBaseIndexConstraintRemove(index);
                SQLInputDialog sid = new SQLInputDialog(sql, false, string.Format("Index \"{0}\" on \"{1}\" will be dropped.\n\nContinue?", index.Name, table.Name));
                if (sid.ShowDialog() == DialogResult.OK)
                {
                    Provider.Database.ExecuteNonQuery(sid.SQL);
                    if (index is Index)
                        table.Indices.Remove((Index)index);
                    else
                        table.Constraints.Remove((Constraint)index);
                    findNode(index).Remove();
                    if (ObjectRemoved != null)
                        ObjectRemoved(this, new DbObjectRemovedArgs { Object = index });
                    CurrSQLEditor.ShowInfoText(string.Format("Index {0} on {1} dropped successfully.", index.Name, table.Name));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar Database Tools");
                if (index is Index)
                    table.Indices.Add((Index)index);
                else
                    table.Constraints.Add((Constraint)index);
            }
        }

        private void cmdNewERDiagram(string arg)
        {
            addDiagram(null);
        }
        private void cmdOpenERDiagram(string arg)
        {
            Diagram schema = SelectedObject as Diagram;
            if (schema == null) return;
            schema.conn = Provider.ActiveConnection;
            addDiagram(schema);
        }
        private void cmdDeleteERDiagram(string arg)
        {
            Diagram schema = SelectedObject as Diagram;
            if (MessageBox.Show(string.Format("Diagram \"{0}\" will be deleted.\n\nContinue?", schema.Name), "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Provider.ActiveConnection.Schemas.Remove(schema);
                findNode(schema).Remove();
            }
        }

        private void cmdShowForm(string arg)
        {
            try
            {
                var form = (IDBToolsForm)Activator.CreateInstance(Type.GetType(arg));
                form.MainForm = this;
                (form as Form).Icon = this.Icon;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar");
            }
        }



        private void cmdSearchTableNamesInFiles(string arg)
        {
            var f = new FormSearchProjectFolder();

            if (f.ShowDialog() == DialogResult.OK)
            {
                var allFiles = new List<string>();
                foreach (string ext in f.SearchExtensions)
                    allFiles.AddRange(Directory.EnumerateFiles(f.ProjectFolder, "*" + ext, SearchOption.AllDirectories));

                string[] keyWords;
                
                string strAllFileContent = null;

                if (f.WhatToSearch == "Image names not used in Project Folder")
                {
                    var imageNames = new List<string>();
                    foreach (string ext in new[] { ".jpg", ".png", ".gif" })
                        imageNames.AddRange(Directory.EnumerateFiles(f.ProjectFolder, "*" + ext,
                                                                     SearchOption.AllDirectories));
                    keyWords = imageNames.ToArray();

                    var allFilesContent = new StringBuilder();
                    foreach (var file in allFiles)
                        allFilesContent.AppendLine(File.ReadAllText(file).ToUpper());
                    strAllFileContent = allFilesContent.ToString();
                    allFilesContent.Clear();
                    allFilesContent = null;
                }
                else if (f.WhatToSearch == "Javascript files not used in Project Folder")
                {
                    var imageNames = new List<string>();
                    imageNames.AddRange(Directory.EnumerateFiles(f.ProjectFolder, "*.JS", SearchOption.AllDirectories));
                    keyWords = imageNames.ToArray();

                    var allFilesContent = new StringBuilder();
                    foreach (var file in allFiles)
                        allFilesContent.AppendLine(File.ReadAllText(file).ToUpper());
                    strAllFileContent = allFilesContent.ToString();
                    allFilesContent.Clear();
                    allFilesContent = null;
                }
                else if (f.WhatToSearch == "CSS files not used in Project Folder")
                {
                    var imageNames = new List<string>();
                    imageNames.AddRange(Directory.EnumerateFiles(f.ProjectFolder, "*.CSS", SearchOption.AllDirectories));
                    keyWords = imageNames.ToArray();

                    StringBuilder allFilesContent = new StringBuilder();
                    foreach (var file in allFiles)
                        allFilesContent.AppendLine(File.ReadAllText(file).ToUpper());
                    strAllFileContent = allFilesContent.ToString();
                    allFilesContent.Clear();
                    allFilesContent = null;
                }
                else if (f.WhatToSearch == "ASPX files not used in Project Folder")
                {
                    var imageNames = new List<string>();
                    imageNames.AddRange(Directory.EnumerateFiles(f.ProjectFolder, "*.ASPX", SearchOption.AllDirectories));
                    keyWords = imageNames.ToArray();

                    StringBuilder allFilesContent = new StringBuilder();
                    foreach (var file in allFiles)
                        allFilesContent.AppendLine(File.ReadAllText(file).ToUpper());
                    strAllFileContent = allFilesContent.ToString();
                    allFilesContent.Clear();
                    allFilesContent = null;
                }
                else
                {
                    if (f.WhatToSearch.StartsWith("Table"))
                        keyWords = Provider.Database.Tables.Where(t => !t.IsView).Select(t => t.Name).ToArray();
                    else
                        keyWords = Provider.Database.Tables.Where(t => t.IsView).Select(t => t.Name).ToArray();
                }

                BackgroundWorkerDialog bwd = new BackgroundWorkerDialog();
                bwd.MessageFormat = "Please wait while " + keyWords.Length + " keywords being searched. ({0})";
                bwd.DoWork = () =>
                    {
                        StringBuilder sb = new StringBuilder();

                        if (f.WhatToSearch != "Image names not used in project folder" && f.WhatToSearch != "Javascript files not used in Project Folder" && f.WhatToSearch != "CSS files not used in Project Folder" && f.WhatToSearch != "ASPX files not used in Project Folder")
                            sb.AppendLine("create table search_results (keyword varchar(255), object_name varchar(255), content text);");

                        for (int i = 0; i < keyWords.Length && !bwd.Canceled; i++)
                        {
                            string kw = keyWords[i];

                            if (f.WhatToSearch == "Image names not used in project folder" || f.WhatToSearch == "Javascript files not used in Project Folder" || f.WhatToSearch == "CSS files not used in Project Folder" || f.WhatToSearch == "ASPX files not used in Project Folder")
                            {
                                if (strAllFileContent.IndexOf(Path.GetFileName(kw).ToUpper()) == -1)
                                    sb.AppendLine(kw);
                            }
                            else
                            {
                                var reg = new Regex("\\b" + kw + "\\b", RegexOptions.IgnoreCase);
                                var allMatches = allFiles
                                                     .SelectMany(fn => File.ReadLines(fn), (fn, line) => new {fn, line})
                                                     .Where(@t => reg.IsMatch(@t.line) &&
                                                                //@t.line.ToUpper().Contains(kw.ToUpper()) &&
                                                                  (@t.line.ToUpper().Contains("CREATEOBJECTSET") ||
                                                                   @t.line.ToUpper().Contains("ENTİTY") ||
                                                                   @t.line.ToUpper().Contains("ENTITY") ||
                                                                   @t.line.ToUpper().Contains("CLASS") ||
                                                                   @t.line.ToUpper().Contains("OBJDB") ||
                                                                   @t.line.ToUpper().Contains("SELECT ") ||
                                                                   @t.line.ToUpper().Contains("İNSERT ") ||
                                                                   @t.line.ToUpper().Contains("INSERT ") ||
                                                                   @t.line.ToUpper().Contains("UPDATE ") ||
                                                                   @t.line.ToUpper().Contains("DELETE ")))
                                                     .Select(@t => new
                                                         {
                                                             Keyword = kw,
                                                             File = @t.fn.Replace(f.ProjectFolder + "\\", ""),
                                                             Line = @t.line.Trim(),
                                                         });

                                foreach (var matchInfo in allMatches)
                                    sb.AppendFormat("insert into search_results values('{0}', '{1}', '{2}');\r\n",
                                                    matchInfo.Keyword,
                                                    matchInfo.File.Replace("\\", "\\\\").Replace("'", "\\'"),
                                                    matchInfo.Line.Replace("\\", "\\\\").Replace("'", "\\'"));
                            }

                            int percent = Convert.ToInt32((i/(float) keyWords.Length)*100);
                            bwd.ReportProgress(percent, Path.GetFileName(kw));
                        }

                        return sb.ToString();
                    };
                bwd.Completed = () => addFileEditor("", bwd.Result);
                bwd.Show();
            }
        }

        private void cmdSearchSQLInProjectFiles(string arg)
        {
            var f = new FormSearchProjectFolder();

            if (f.ShowDialog() == DialogResult.OK)
            {
                var fProjectFolder = @"C:\Users\buk\Desktop\_delete";

                var allFiles = new List<string>();
                foreach (string ext in f.SearchExtensions /*new string[] { ".cs"}*/)
                    allFiles.AddRange(Directory.EnumerateFiles(f.ProjectFolder/*fProjectFolder*/, "*" + ext, SearchOption.AllDirectories));


                BackgroundWorkerDialog bwd = new BackgroundWorkerDialog();
                bwd.MessageFormat = "Please wait while " + allFiles.Count + " files being searched. ({0})";
                bwd.DoWork = () =>
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("drop table DOCS_SQLUsage;");
                    sb.AppendLine("create table DOCS_SQLUsage (FilePath varchar(max), LineNumber int, SQLType varchar(10), SQLCode varchar(max), FormattedSQLCode varchar(max), MainTable varchar(max), Fields varchar(max), RelatedTables varchar(max));");

                    for (int i = 0; i < allFiles.Count && !bwd.Canceled; i++)
                    {
                        var filePath = allFiles[i];
                        var sqls = GetStrings(filePath).Where(s => isSQL(s.SqlCode.Trim().ToUpper())).ToList();

                        if (sqls.Count > 0)
                            sqls.ForEach(s => s.FilePath = s.FilePath.Replace(f.ProjectFolder/*fProjectFolder*/, "").Replace("\\", "/"));
                        else
                            continue;

                        sqls.ForEach(sql => {
                            try
                            {
                                if (sql.SqlCode.Trim().ToUpper().StartsWith("SELECT"))
                                {
                                    sql.SqlType = "SELECT";
                                    var s = (new SQLParser.Parser(new StringReader(sql.SqlCode), false).ReadNextStatement() as SQLParser.SelectStatement)?.SelectExpression;
                                    if (s == null) { sql.SqlType = "SKIP"; return; }
                                    sql.MainTable = s.From?.FirstOrDefault()?.TableName;
                                    sql.RelatedTables = s.From?.Skip(1).Select(t => t.GetTableReference(s.From)).StringJoin(", ");
                                    sql.Fields = s.Select?.Select(t => t.Field).StringJoin(", ");
                                    sql.FormattedSQLCode = s.ToString();
                                }
                                else if (sql.SqlCode.Trim().ToUpper().StartsWith("UPDATE"))
                                {
                                    sql.SqlType = "UPDATE";
                                    var s = new SQLParser.Parser(new StringReader(sql.SqlCode), false).ReadNextStatement() as SQLParser.UpdateStatement;
                                    if (s == null) { sql.SqlType = "SKIP"; return; }
                                    sql.MainTable = s.TableName;
                                    sql.Fields = s.Set?.Select(t => t.Key).StringJoin(", ");
                                    sql.FormattedSQLCode = s.ToString();
                                }
                                else if (sql.SqlCode.Trim().ToUpper().StartsWith("DELETE"))
                                {
                                    sql.SqlType = "DELETE";
                                    var s = new SQLParser.Parser(new StringReader(sql.SqlCode), false).ReadNextStatement() as SQLParser.DeleteStatement;
                                    if (s == null) { sql.SqlType = "SKIP"; return; }
                                    sql.MainTable = s.TableName;
                                    sql.Fields = "";
                                    sql.FormattedSQLCode = s.ToString();
                                }
                                else if (sql.SqlCode.Trim().ToUpper().StartsWith("INSERT"))
                                {
                                    sql.SqlType = "INSERT";
                                    var s = new SQLParser.Parser(new StringReader(sql.SqlCode), false).ReadNextStatement() as SQLParser.InsertStatement;
                                    if (s == null) { sql.SqlType = "SKIP"; return; }
                                    sql.MainTable = s.TableName;
                                    sql.Fields = s.Fields?.StringJoin(", ");
                                    sql.FormattedSQLCode = s.ToString();
                                }
                            }
                            catch { 
                            }
                        });


                        foreach (var sql in sqls.Where(s => s.SqlType != "SKIP"))
                        {
                            /*
                            sb.AppendFormat(@"
-----------------------------------------------------------------
-- {2}, {5}, {7}
-----------------------------------------------------------------
{3}
-----------------------------------------------------------------
{4}
",
                                            sql.FilePath,
                                            sql.LineNumber,
                                            sql.SqlType,
                                            sql.SqlCode,
                                            sql.FormattedSQLCode,
                                            sql.MainTable,
                                            sql.Fields,
                                            sql.RelatedTables);
                            */
                            
                            sb.AppendFormat("insert into DOCS_SQLUsage values('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');\r\n",
                                            sql.FilePath?.Replace("\\", "\\\\").Replace("'", "''"),
                                            sql.LineNumber,
                                            sql.SqlType,
                                            sql.SqlCode?.Replace("\\", "\\\\").Replace("'", "''"),
                                            sql.FormattedSQLCode?.Replace("\\", "\\\\").Replace("'", "''"),
                                            sql.MainTable?.Replace("\\", "\\\\").Replace("'", "''"),
                                            sql.Fields?.Replace("\\", "\\\\").Replace("'", "''"),
                                            sql.RelatedTables?.Replace("\\", "\\\\").Replace("'", "''"));
                            
                        }

                        int percent = Convert.ToInt32((i / (float)allFiles.Count) * 100);
                        bwd.ReportProgress(percent, Path.GetFileName(filePath));
                    }

                    return sb.ToString();
                };
                bwd.Completed = () => addFileEditor("", bwd.Result);
                bwd.Show();
            }
        }

        private bool isSQL(string s)
        {
            return
                (s.StartsWith("SELECT") || s.StartsWith("UPDATE") || s.StartsWith("INSERT") || s.StartsWith("DELETE"))
                && s.Length > 10;
        }

        private List<CodeLocation> GetStrings(string filePath)
        {
            var content = File.ReadLines(filePath).Select(l => l.Trim().StartsWith("//") ? "" : l).StringJoin("\r\n");
            var escapes = new Dictionary<char, string>() { { 'n', "\n" }, { 'r', "\r" }, { '\'', "\'" }, { '\"', "\"" }, { 't', "\t" } };
            var res = new List<CodeLocation>();

            bool reading = false;
            bool statementEnded = true;
            int lineNumber = 0;
            int plusFound = 0;

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '\n')
                    lineNumber++;

                if (reading == false && content[i] == '"')
                {
                    reading = true;
                    if (statementEnded == true)
                    {
                        res.Add(new CodeLocation { LineNumber = lineNumber, FilePath = filePath });
                        statementEnded = false;
                    }
                    continue;
                }

                if (reading)
                {
                    if(plusFound > 1)
                    {
                        res[res.Count - 1].SqlCode += " {0} ";
                        plusFound = 0;
                    }

                    if (content[i] == '\\')
                    {
                        i++;
                        res[res.Count - 1].SqlCode += escapes.ContainsKey(content[i]) ? escapes[content[i]] : "";
                    }
                    else if (content[i] == '"')
                    {
                        reading = false;
                        res[res.Count - 1].SqlCode += " ";
                    }
                    else
                        res[res.Count - 1].SqlCode += content[i];
                }
                else
                {
                    if (content[i] == ';')
                        statementEnded = true;

                    if (!statementEnded && content[i] == '+')
                        plusFound++;
                }
            }

            return res;
        }
        public class CodeLocation
        {
            public string FilePath { get; set; }
            public int LineNumber { get; set; }
            public string SqlType { get; set; }
            public string SqlCode { get; set; } = "";
            public string MainTable { get; set; }
            public string Fields { get; set; }
            public string RelatedTables { get; set; }
            public string FormattedSQLCode { get; set; }
        }

        private void cmdQuickScript(string arg)
        {
            CurrSQLEditor.SQLEditor.Text = arg;
            CurrSQLEditor.Refresh();
        }

        private void cmdExit(string arg)
        {
            Close();
        }
        private void cmdAbout(string arg)
        {
            new FormAbout().ShowDialog();
        }
        private void cmdTryAndSee(string arg)
        {
            //MessageBox.Show("TryAndSee");
            new FormContentExtractor().Show();
        }
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            bool cancel = false;

            // save modified query editors and add them to "lastopened" log.
            string openedFiles = "";
            //File.WriteAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\lastopened.txt", openedFiles, Encoding.UTF8);

            for (int i = 0; i < tabControlEditors.TabPages.Count && !cancel; i++)
            {
                tabControlEditors.SelectedIndex = i;

                if (CurrEditor.Modified)
                {
                    DialogResult dr = MessageBox.Show("Would you like to save " + CurrEditor.GetName() + "?", "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (!CurrEditor.Save())
                            cancel = true;
                    }
                    else if (dr == DialogResult.Cancel)
                        cancel = true;
                }

                if (CurrEditor is SQLEditorAndResults && !string.IsNullOrEmpty((CurrEditor as SQLEditorAndResults).FilePath))
                    openedFiles += (CurrEditor as SQLEditorAndResults).FilePath + Environment.NewLine;
            }
            if (!cancel && openedFiles.Length > 0)
                File.WriteAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\lastopened.txt", openedFiles, Encoding.UTF8);

            // save connections
            if (Provider.ConnectionsModified)
            {
                DialogResult dr2 = MessageBox.Show("Would you like to save current connections & metadata to the file: \n\n" + Provider.ConnectionsPath, "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr2 == DialogResult.Yes)
                    Provider.SaveConnections();
                else if (dr2 == DialogResult.Cancel)
                    cancel = true;
            }

            // save codeGen project
            if (codeGenController.Solution!=null && codeGenController.Solution.Modified)
            {
                DialogResult dr2 = MessageBox.Show("Would you like to save code generation project: \n\n" + codeGenController.Solution.FullPath, "Cinar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr2 == DialogResult.Yes)
                    codeGenController.Solution.Save();
                else if (dr2 == DialogResult.Cancel)
                    cancel = true;
            }

            e.Cancel = cancel;
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Diagram)
                cmdOpenERDiagram(null);
            else if (e.Node.Tag is ConnectionSettings)
                cmdEditConnection(null);
            else if (e.Node.Tag is Table)
                cmdTableOpen(null);
            else if (e.Node.Tag is Column)
                cmdColumnDistinct(null);
        }
        private void showSelectedObjectOnPropertyGrid()
        {
            object o = SelectedObject;
            if (o is IList)
                o = null;

            propertyGrid.SelectedObject = null;
            propertyGrid.SelectedObject = o;
        }

        private void cbActiveConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = findNode(treeView.Nodes, n => n.Tag == cbActiveConnection.SelectedItem);
            TreeNode node2 = findSelectedDBNode();
            if (node != node2) treeView.SelectedNode = node;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            if (DateTime.Now.Second % 10 == 0 && CurrSQLEditor != null)
                CurrSQLEditor.SaveTemp();
        }

        private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
        {
            if (propertyGrid.SelectedObject != null)
                labelProperties.Text = "      " + propertyGrid.SelectedObject.GetType().Name + " Properties";
            else
                labelProperties.Text = "      Properties";
        }

        public event EventHandler<DbObjectChangedArgs> ObjectChanged;
        public event EventHandler<DbObjectRemovedArgs> ObjectRemoved;
        public event EventHandler<DbObjectAddedArgs> ObjectAdded;

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            bool cancel = true;
            string sql = null, message = "";
            Action afterExecute = null, afterError = null;

            try
            {
                if (propertyGrid.SelectedObject is IMetadata)
                {
                    if (propertyGrid.SelectedObject is Column)
                    {
                        Column column = propertyGrid.SelectedObject as Column;
                        switch (e.ChangedItem.Label)
                        {
                            case "Name":
                                sql = Provider.Database.GetSQLColumnRename((string)e.OldValue, column);
                                message = "Do you really want to rename this column?";
                                afterExecute = () => { findNode(column).Text = column.Name + " (" + column.ColumnType + ")"; };
                                break;
                            case "ColumnType":
                            case "Length":
                                string oldOriginalColumnType = column.ColumnTypeOriginal;
                                column.ColumnTypeOriginal = Provider.Database.DbTypeToString(column.ColumnType);
                                sql = Provider.Database.GetSQLColumnChangeDataType(column);
                                message = "Do you really want to change the type of this column?";
                                afterError = () => { column.ColumnTypeOriginal = oldOriginalColumnType; };
                                break;
                            case "IsNullable":
                                sql = column.IsNullable ?
                                    Provider.Database.GetSQLColumnRemoveNotNull(column) : Provider.Database.GetSQLColumnAddNotNull(column);
                                message = "Do you really want to change nullability of this column?";
                                break;
                            case "DefaultValue":
                                sql = Provider.Database.GetSQLColumnChangeDefault(column);
                                message = "Do you really want to change default value of this column?";
                                break;
                            case "IsAutoIncrement":
                                sql = column.IsAutoIncrement ?
                                    Provider.Database.GetSQLColumnSetAutoIncrement(column) : Provider.Database.GetSQLColumnRemoveAutoIncrement(column);
                                message = "Do you really want to change auto incrementing of this column?";
                                break;
                        }
                    }
                    if (propertyGrid.SelectedObject is Table)
                    {
                        Table table = propertyGrid.SelectedObject as Table;
                        switch (e.ChangedItem.Label)
                        {
                            case "Name":
                                sql = Provider.Database.GetSQLTableRename((string)e.OldValue, table.Name);
                                message = "Do you really want to rename this table?";
                                afterExecute = () => { findNode(table).Text = table.Name; };
                                break;
                        }
                    }

                    if (sql != null)
                    {
                        SQLInputDialog sid = new SQLInputDialog(sql, true, message);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            Provider.Database.ExecuteNonQuery(sid.SQL);
                            if (afterExecute != null) afterExecute();
                            if (ObjectChanged != null)
                                ObjectChanged(this, new DbObjectChangedArgs() { Object = propertyGrid.SelectedObject, NewValue = e.ChangedItem.Value, OldValue = e.OldValue, PropertyName = e.ChangedItem.Label });
                            cancel = false;
                        }
                    }
                }

                if (propertyGrid.SelectedObject is Item || propertyGrid.SelectedObject is Diagram)
                {
                    cancel = false;
                    switch (e.ChangedItem.Label)
                    {
                        case "Name":
                            findNode(propertyGrid.SelectedObject).Text = e.ChangedItem.Value.ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (afterError != null) afterError();
                cancel = true;
            }

            try
            {
                if (cancel)
                    e.ChangedItem.PropertyDescriptor.SetValue(propertyGrid.SelectedObject, e.OldValue);
            }
            catch { }
        }

        public object SelectedObject;

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode tn = e.Item as TreeNode;
            if (tn.Tag is Table || tn.Tag is Column)
                DoDragDrop(tn.Tag, DragDropEffects.Copy);
        }

        private void treeCodeGen_MouseDown(object sender, MouseEventArgs e)
        {
            treeCodeGen.SelectedNode = treeCodeGen.GetNodeAt(e.X, e.Y);
            SelectedObject = treeCodeGen.SelectedNode == null ? null : treeCodeGen.SelectedNode.Tag;
            cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            showSelectedObjectOnPropertyGrid();
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
            if (treeView.SelectedNode == null) return;

            SelectedObject = treeView.SelectedNode.Tag;
            cmdSetActiveConnection("");
            cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            showSelectedObjectOnPropertyGrid();

            if (CurrSQLEditor != null)
            {
                Table tn = findSelectedTable();
                if (tn != null)
                    CurrSQLEditor.ShowTableDataIfTableTabActive(tn, new FilterExpression());
            }
        }

        private void menuTableShowChildrenRecords_DropDownOpening(object sender, EventArgs e)
        {
            Table tbl = SelectedObject as Table;
            if (tbl == null) return;

            menuTableShowChildrenRecords.DropDownItems.Clear();
            foreach (Table t in tbl.Database.Tables)
                foreach (Cinar.Database.ForeignKeyConstraint fk in t.Constraints.Where(c => c is Cinar.Database.ForeignKeyConstraint))
                    if (fk.RefTableName == tbl.Name)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(t.Name, FamFamFam.table);
                        item.Click += (sndr, args) => { showGroupedCounts(fk.Columns[0]); };
                        menuTableShowChildrenRecords.DropDownItems.Add(item);
                    }
        }

        private void menuTableShowChildrenRecords_DropDownClosed(object sender, EventArgs e)
        {
            menuTableShowChildrenRecords.DropDownItems.Clear();
            menuTableShowChildrenRecords.DropDownItems.Add("empty");
        }
    }

    public class DbObjectChangedArgs : EventArgs
    {
        public object Object { get; set; }
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
    public class DbObjectRemovedArgs : EventArgs
    {
        public object Object { get; set; }
    }
    public class DbObjectAddedArgs : EventArgs
    {
        public object Object { get; set; }
    }
}