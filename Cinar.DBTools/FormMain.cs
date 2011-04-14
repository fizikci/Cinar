using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using Cinar.Database;
using Cinar.DBTools.Tools;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using Cinar.Scripting;
using System.Diagnostics;
using Cinar.DBTools.Controls;
using ICSharpCode.TextEditor.Document;

namespace Cinar.DBTools
{
    public partial class FormMain : Form
    {
        CommandManager cmdMan = new CommandManager();
        TreeNode rootNode;

        public FormMain()
        {
            InitializeComponent();
            SetFont(this, Font);

            #region commands
            cmdMan.AfterCommandExecute = () =>
            {
                cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            };

            cmdMan.Commands = new CommandCollection(){
                    #region main menu
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnNewConnection},
                                         new CommandTrigger{ Control = menuNewConnection},
                                     },
                                     IsVisible = () => treeView.SelectedNode!=null && (treeView.SelectedNode==treeView.Nodes[0] || treeView.SelectedNode.Tag is ConnectionSettings)
                                 },
                     new Command {
                                     Execute = cmdOpenConnectionsFile,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuOpenConnectionsFile},
                                     }
                                 },
                     new Command {
                                     Execute = cmdOpen,
                                     Trigger = new CommandTrigger{ Control = menuOpen}
                                 },
                     new Command {
                                     Execute = cmdSave,
                                     Trigger = new CommandTrigger{ Control = menuSave}
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
                                         new CommandTrigger{ Control = menuCopy, Argument = "Copy"},
                                         new CommandTrigger{ Control = menuPaste, Argument = "Paste"},
                                         new CommandTrigger{ Control = menuSelectAll, Argument = "SelectAll"},
                                         new CommandTrigger{ Control = menuFind, Argument = "Find"},
                                         new CommandTrigger{ Control = menuReplace, Argument = "Replace"},
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
                                     Execute = (s)=>{addSQLEditor("", "");},
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnAddEditor},
                                     }
                                 },
                     new Command {
                                     Execute = cmdCloseSQLEditor,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnCloseSQLEditor},
                                     },
                                     IsVisible = ()=>tabControlEditors.TabCount>1
                                },
                     new Command {
                                     Execute = cmdExecuteSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnExecuteSQL},
                                         //new CommandTrigger{ Control = CurrSQLEditor.SQLEditor, Event = "KeyUp", Predicate = (e)=>{KeyEventArgs k = (KeyEventArgs)e; return k.KeyCode==Keys.F5 || k.KeyCode==Keys.F9;}}
                                     }
                                 },
                     new Command {
                                     Execute = cmdExecuteScript,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnExecuteScript},
                                     }
                                 },
                     new Command {
                                     Execute = cmdShowForm,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 
                                         new CommandTrigger{ Control = btnCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 
                                         new CommandTrigger{ Control = menuToolsCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},
                                         new CommandTrigger{ Control = btnCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},
                                         new CommandTrigger{ Control = menuToolsDBTransfer, Argument=typeof(FormDBTransfer).FullName},
                                         new CommandTrigger{ Control = btnDatabaseTransfer, Argument=typeof(FormDBTransfer).FullName},
                                         new CommandTrigger{ Control = menuToolsCopyTreeData, Argument=typeof(FormCopyTreeData).FullName},
                                         new CommandTrigger{ Control = menuToolsSimpleIntegrationService, Argument=typeof(FormDBIntegration).FullName},
                                         new CommandTrigger{ Control = btnSimpleIntegrationService, Argument=typeof(FormDBIntegration).FullName},
                                         new CommandTrigger{ Control = menuToolsSQLDump, Argument=typeof(FormSQLDump).FullName},
                                         new CommandTrigger{ Control = btnSQLDump, Argument=typeof(FormSQLDump).FullName},
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
                                     Execute = cmdQuickScript,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuToolsQScriptDeleteFromTables, Argument=@"$
foreach(table in db.Tables)
    echo('truncate table ' + table.Name + ';\r\n');
$"},
                                         new CommandTrigger{ Control = menuToolsQScriptSelectCountsFromTables, Argument=@"$
for(int i=0; i<db.Tables.Count; i++)
{
	var table = db.Tables[i];
    echo(""select '"" + table.Name + ""', count(*) from "" + table.Name);
    if(i<db.Tables.Count-1) echo("" UNION \r\n"");
}
$"},
                                         new CommandTrigger{ Control = menuToolsQScriptForEachTable, Argument=@"$
foreach(table in db.Tables)
    echo(table.Name + ""\r\n"");
$"},
                                         new CommandTrigger{ Control = menuToolsQScriptForEachField, Argument=@"$
foreach(field in db.Tables[""TABLE_NAME""].Fields)
    echo(field.Name + ""\r\n"");
$"},
                                     }
                                 },
                     new Command {
                                     Execute = cmdTryAndSee,
                                     Trigger = new CommandTrigger{ Control = btnTryAndSee}
                                 },
                    #endregion
                    #region tree menus
                     new Command {
                                     Execute = cmdSetActiveConnection,
                                     Trigger = new CommandTrigger{ Control = treeView, Event = "AfterSelect"}
                                 },
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConNewConnection},
                                     },
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdEditConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConEditConnection},
                                         new CommandTrigger{ Control = btnEditConnection},
                                     },
                                     IsEnabled = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings,
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdDeleteConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuConDeleteConnection},
                                         new CommandTrigger{ Control = btnDeleteConnection},
                                     },
                                     IsEnabled = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings,
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdRefresh,
                                     Trigger = new CommandTrigger{ Control = menuConRefresh},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdRefreshMetadata,
                                     Trigger = new CommandTrigger{ Control = menuConRefreshMetadata},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdDoDatabaseOperation,
                                     Triggers = new List<CommandTrigger>{
                                         new CommandTrigger{ Control = menuConCreateDatabase, Argument="Create"},
                                         new CommandTrigger{ Control = menuConDropDatabase, Argument="Drop"},
                                         new CommandTrigger{ Control = menuConTruncateDatabase, Argument="Truncate"},
                                         new CommandTrigger{ Control = menuConEmptyDatabase, Argument="Empty"},
                                         new CommandTrigger{ Control = menuConBackupDatabase, Argument="Backup"},
                                         new CommandTrigger{ Control = menuConTransferDatabase, Argument="Transfer"},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
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
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdExecuteSQLFromFile,
                                     Trigger = new CommandTrigger{ Control = menuConExecuteSQLFromFile},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
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
                                     IsVisible = ()=> treeView.SelectedNode!=null && (treeView.SelectedNode.Tag is List<Schema> || treeView.SelectedNode.Tag is Schema)
                                 },
                     new Command {
                                     Execute = cmdOpenERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDiagramOpen},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Schema,
                                 },
                     new Command {
                                     Execute = cmdDeleteERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDiagramDelete},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Schema,
                                 },
                     new Command {
                                     Execute = cmdShowTableCounts,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTablesShowTableCounts},
                                     },
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is TableCollection
                                 },
                     new Command {
                                     Execute = cmdCreateTable,
                                     Trigger = new CommandTrigger{ Control = menuTableCreate},
                                     IsVisible = ()=> treeView.SelectedNode!=null && (treeView.SelectedNode.Tag is TableCollection || treeView.SelectedNode.Tag is Table)
                                 },
                     new Command {
                                     Execute = cmdAlterTable,
                                     Trigger = new CommandTrigger{ Control = menuTableAlter},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdTableDrop,
                                     Trigger = new CommandTrigger{ Control = menuTableDrop},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdTableOpen,
                                     Trigger = new CommandTrigger{ Control = menuTableOpen},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdTableCount,
                                     Trigger = new CommandTrigger{ Control = menuTableCount},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdAnalyzeTable,
                                     Trigger = new CommandTrigger{ Control = menuTableAnalyze},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdGenerateSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuTableGenerateSQL, Argument="-"},
                                         new CommandTrigger{ Control = menuTableGenSQLCreateTable, Argument="CreateTable"},
                                         new CommandTrigger{ Control = menuTableGenSQLInsert, Argument="Insert"},
                                         new CommandTrigger{ Control = menuTableGenSQLSelect, Argument="Select"},
                                         new CommandTrigger{ Control = menuTableGenSQLUpdate, Argument="Update"},
                                         new CommandTrigger{ Control = menuTableGenSQLDump, Argument="Dump"},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdFieldDistinct,
                                     Trigger = new CommandTrigger{ Control = menuFieldDistinct},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdFieldMax,
                                     Trigger = new CommandTrigger{ Control = menuFieldMax},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdFieldMin,
                                     Trigger = new CommandTrigger{ Control = menuFieldMin},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdGroupedCounts,
                                     Trigger = new CommandTrigger{ Control = menuFieldGroupedCounts},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdGenerateUIMetadata,
                                     Trigger = new CommandTrigger{ Control = menuShowUIMetadata},
                                     IsVisible = ()=> treeView.SelectedNode!=null && (treeView.SelectedNode.Tag is ConnectionSettings || treeView.SelectedNode.Tag is Table || treeView.SelectedNode.Tag is Field)
                                 },
                    #endregion
             };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();
            #endregion

            showConnections(null);
        }

        public void SetFont(Control parent, Font font)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl is CinarSQLEditor) continue;

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

        protected override void OnLoad(EventArgs e)
        {
            addSQLEditor("", "");
        }


        #region methods
        private void showConnections(string path)
        {
            treeView.Nodes.Clear();

            rootNode = treeView.Nodes.Add("Database Connections");

            Provider.LoadConnectionsFromXML(path);
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                TreeNode node = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database");
                node.NodeFont = new System.Drawing.Font(treeView.Font, FontStyle.Bold);
                node.Tag = cs;

                cbActiveConnection.Items.Add(cs);
                populateTreeNodesForDatabase(node);
            }
            treeView.Sort();
            if (rootNode.Nodes.Count > 0 && rootNode.Nodes[0].Nodes.Count > 0)
            {
                rootNode.Nodes[0].Expand();
                treeView.SelectedNode = rootNode.Nodes[0];
            }
        }

        public void SaveConnections()
        {
            Provider.SaveConnections();
            statusText.Text = "Connections saved.";
        }
        private void executeSQL(string sql, params object[] args)
        {
            sql = String.Format(sql, args);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataSet ds = Provider.Database.GetDataSet(sql);
            
            watch.Stop();
            statusExecTime.Text = watch.ElapsedMilliseconds + " ms";
            statusNumberOfRows.Text = (ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count) + " rows";
            statusText.Text = "Query executed succesfully.";
            sql = sql.Replace("\n", " ").Replace("\r", "").Replace("\t"," ") + (sql.EndsWith(";") ? "" : ";");
            while (sql.Contains("  ")) sql = sql.Replace("  "," ");
            if (sql.Length > 1000) sql = sql.Substring(0, 1000) + "...";
            CurrSQLEditor.SQLLog.Text += string.Format(Environment.NewLine + "/*[{0} - {1,5} ms]*/ {2}", DateTime.Now.ToString("hh:mm:ss"), watch.ElapsedMilliseconds, sql);

            if (ds.Tables.Count > 1)
                CurrSQLEditor.BindGridResults(ds);
            else if (ds.Tables.Count == 1)
                CurrSQLEditor.BindGridResults(ds.Tables[0]);
            else
                CurrSQLEditor.BindGridResults(null);
        }
        private bool checkConnection()
        {
            if (Provider.Database == null)
            {
                MessageBox.Show("Please select a database first", "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void populateTreeNodesForDatabase(TreeNode parentNode)
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

            foreach (Table tbl in cs.Database.Tables)
            {
                TreeNode tnTable = (tbl.IsView ? viewsNode : tablesNode).Nodes.Add(tbl.Name, tbl.Name, tbl.IsView ? "View" : "Table", tbl.IsView ? "View" : "Table");
                tnTable.Tag = tbl;
                TreeNode fieldsNode = tnTable.Nodes.Add("Fields", "Fields", "Folder", "Folder");
                fieldsNode.Tag = tbl.Fields;
                foreach (Field fld in tbl.Fields)
                {
                    TreeNode tnField = fieldsNode.Nodes.Add(fld.Name, fld.Name + " (" + fld.FieldType + ")", fld.IsPrimaryKey ? "Key" : "Field", fld.IsPrimaryKey ? "Key" : "Field");
                    tnField.Tag = fld;
                }
                TreeNode keysNode = tnTable.Nodes.Add("Indexes", "Indexes", "Folder", "Folder");
                keysNode.Tag = tbl.Keys;
                foreach (var key in tbl.Keys)
                {
                    TreeNode tnKey = keysNode.Nodes.Add(key.Name, key.Name + " (" + string.Join(", ",key.FieldNames.ToArray()) + ")", key.IsPrimary ? "Key" : "Field", key.IsPrimary ? "Key" : "Field");
                    tnKey.Tag = key;
                }
            }

            foreach (Schema schema in cs.Schemas)
            {
                TreeNode tnSchema = schemasNode.Nodes.Add(schema.Name, schema.Name, "Diagram", "Diagram");
                tnSchema.Tag = schema;
            }
        }
        private ConnectionSettings findConnection(TreeNode treeNode)
        {
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

        private void addSQLEditor(string filePath, string sql) {
            TabPage tp = new TabPage();
            SQLEditorAndResults sqlEd = new SQLEditorAndResults(filePath, sql);
            sqlEd.Dock = DockStyle.Fill;
            tp.Controls.Add(sqlEd);
            tp.Text = string.IsNullOrEmpty(filePath) ? "Query" : Path.GetFileName(filePath);
            tabControlEditors.Controls.Add(tp);
            tabControlEditors.SelectTab(tp);

            SetFont(tp, Font);
        }
        #endregion

        #region Commands
        private void cmdSave(string arg)
        {
            if (CurrSQLEditor.Save())
                tabControlEditors.SelectedTab.Text = Path.GetFileName(CurrSQLEditor.FilePath);
        }
        private void cmdSaveAs(string arg)
        {
            if(CurrSQLEditor.SaveAs())
                tabControlEditors.SelectedTab.Text = Path.GetFileName(CurrSQLEditor.FilePath);
        }
        private void cmdOpen(string arg)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                addSQLEditor(ofd.FileName, "");
        }
        private void cmdCloseSQLEditor(string arg)
        {
            if (CurrSQLEditor.Modified && MessageBox.Show("Would you like to save?", "Çınar DB Tools", MessageBoxButtons.YesNo) == DialogResult.Yes)
                cmdSave("");
            tabControlEditors.TabPages.Remove(tabControlEditors.SelectedTab);
        }

        private void cmdEditorCommand(string arg)
        {
            switch (arg)
            {
                case "Undo":
                    CurrSQLEditor.SQLEditor.Undo();
                    break;
                case "Redo":
                    CurrSQLEditor.SQLEditor.Redo();
                    break;
                case "Cut":
                    if (CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
                    {
                        ISelection sel = CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                        Clipboard.SetData(DataFormats.Text, sel.SelectedText);
                        CurrSQLEditor.SQLEditor.Document.Remove(sel.Offset, sel.Length);
                        CurrSQLEditor.SQLEditor.ActiveTextAreaControl.Caret.Position = sel.StartPosition;
                        CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.ClearSelection();
                    }
                    break;
                case "Copy":
                    if (CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
                    {
                        ISelection sel = CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                        Clipboard.SetData(DataFormats.Text, sel.SelectedText);
                    }
                    break;
                case "Paste":
                    string copiedText = Clipboard.GetText();
                    if (CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
                    {
                        ISelection sel = CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                        CurrSQLEditor.SQLEditor.Document.Replace(sel.Offset, sel.Length, copiedText);
                        CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.ClearSelection();
                        CurrSQLEditor.SQLEditor.ActiveTextAreaControl.Caret.Position = CurrSQLEditor.SQLEditor.Document.OffsetToPosition(sel.Offset + copiedText.Length);
                    }
                    else
                    {
                        CurrSQLEditor.SQLEditor.Document.Insert(CurrSQLEditor.SQLEditor.ActiveTextAreaControl.Caret.Offset, copiedText);
                        CurrSQLEditor.SQLEditor.ActiveTextAreaControl.Caret.Position = CurrSQLEditor.SQLEditor.Document.OffsetToPosition(CurrSQLEditor.SQLEditor.ActiveTextAreaControl.Caret.Offset + copiedText.Length);
                    }
                    break;
                case "SelectAll":
                    CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SetSelection(new ICSharpCode.TextEditor.TextLocation(0, 0), CurrSQLEditor.SQLEditor.Document.OffsetToPosition(CurrSQLEditor.SQLEditor.Text.Length));
                    break;
                case "Find":
                case "Replace":
                    FindDialog fd = new FindDialog(CurrSQLEditor.SQLEditor);
                    fd.Show();
                    break;
            }
        }

        private void cmdRefresh(string arg)
        {
            TreeNode dbNode = findSelectedDBNode();
            dbNode.Nodes.Clear();
            populateTreeNodesForDatabase(dbNode);
            treeView.Sort();
        }
        private void cmdRefreshMetadata(string arg)
        {
            if (MessageBox.Show("Metada will be reread from database.\n This may result in loss of some of your later added metadata.\n (such as UI metadata, and foreign relationships)\n\n Continue?", "Çınar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.Database oldDb = Provider.Database;
                Provider.ActiveConnection.RefreshDatabaseSchema();

                foreach (Table oldTable in oldDb.Tables)
                {
                    Table newTable = Provider.Database.Tables[oldTable.Name];
                    if (newTable == null) continue;

                    foreach (Field oldField in oldTable.Fields)
                    {
                        Field newField = newTable.Fields[oldField.Name];
                        if (newField == null) continue;

                        if (oldField.ReferenceField != null)
                        {
                            var isOk = newField.ReferenceField != null && oldField.ReferenceField.Table.Name == newField.ReferenceField.Table.Name && oldField.ReferenceField.Name == newField.ReferenceField.Name;
                            if (!isOk)
                            {
                                newField.ReferenceField = getField(oldField.ReferenceField.Table.Name, oldField.ReferenceField.Name);
                            }
                        }

                        newField.UIMetadata = oldField.UIMetadata;
                    }

                    newTable.UIMetadata = oldTable.UIMetadata;
                }
                
                TreeNode dbNode = findSelectedDBNode();
                dbNode.Nodes.Clear();
                populateTreeNodesForDatabase(dbNode);
                SaveConnections();
                treeView.Sort();
            }
        }
        private Field getField(string tableName, string fieldName)
        {
            if (Provider.Database.Tables[tableName] != null)
                return Provider.Database.Tables[tableName].Fields[fieldName];
            return null;
        }

        private void cmdOpenConnectionsFile(string arg)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Connection Files|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
                showConnections(ofd.FileName);
        }

        private void cmdNewConnection(string arg)
        {
            FormConnect f = new FormConnect();
            if (treeView.SelectedNode != null && treeView.SelectedNode.Tag is ConnectionSettings)
            {
                ConnectionSettings curr = treeView.SelectedNode.Tag as ConnectionSettings;
                f.txtDBName.Text = "";
                f.txtHost.Text = curr.Host;
                f.txtPassword.Text = curr.Password;
                f.cbProvider.SelectedItem = curr.Provider.ToString();
                f.txtUserName.Text = curr.UserName;
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                ConnectionSettings cs = new ConnectionSettings();
                cs.DbName = f.txtDBName.Text;
                cs.Host = f.txtHost.Text;
                cs.Password = f.txtPassword.Text;
                cs.Provider = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), f.cbProvider.SelectedItem.ToString());
                cs.UserName = f.txtUserName.Text;
                cs.RefreshDatabaseSchema();
                Provider.Connections.Add(cs);
                SaveConnections();
                TreeNode tn = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database");
                tn.Tag = cs;
                tn.NodeFont = new Font(tn.NodeFont, FontStyle.Bold);
                cbActiveConnection.Items.Add(cs);
            }
        }
        private void cmdEditConnection(string arg)
        {
            ConnectionSettings cs = (ConnectionSettings)treeView.SelectedNode.Tag;
            FormConnect f = new FormConnect();
            f.txtDBName.Text = cs.DbName;
            f.txtHost.Text = cs.Host;
            f.txtPassword.Text = cs.Password;
            f.cbProvider.SelectedItem = cs.Provider.ToString();
            f.txtUserName.Text = cs.UserName;
            if (f.ShowDialog() == DialogResult.OK)
            {
                cs.DbName = f.txtDBName.Text;
                cs.Host = f.txtHost.Text;
                cs.Password = f.txtPassword.Text;
                cs.Provider = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), f.cbProvider.SelectedItem.ToString());
                cs.UserName = f.txtUserName.Text;
                SaveConnections();
                treeView.SelectedNode.Name = treeView.SelectedNode.Text = cs.ToString();
                cs.RefreshDatabaseSchema();
            }
        }
        private void cmdDeleteConnection(string arg)
        {
            ConnectionSettings cs = (ConnectionSettings)treeView.SelectedNode.Tag;
            if (MessageBox.Show("Are you sure to delete connection \""+cs+"\"?", "Çınar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                TreeNode currNode = treeView.SelectedNode;
                if (currNode.PrevNode != null)
                    treeView.SelectedNode = currNode.PrevNode;
                else if (currNode.NextNode != null)
                    treeView.SelectedNode = currNode.NextNode;
                Provider.Connections.Remove(cs);
                SaveConnections();
                currNode.Remove();

                statusText.Text = "Connection deleted.";
            }
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
            throw new NotImplementedException();
        }
        private void cmdCreateDBObject(string arg)
        {
            throw new NotImplementedException();
        }
        private void cmdExecuteSQLFromFile(string arg)
        {
            throw new NotImplementedException();
        }

        private void cmdExecuteSQL(string arg)
        {
            if(!checkConnection()) return;

            string sel = CurrSQLEditor.SQLEditor.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (string.IsNullOrEmpty(sel))
                sel = CurrSQLEditor.SQLEditor.Text;

            Interpreter engine = new Interpreter(sel, null);
            engine.SetAttribute("db", Provider.Database);
            engine.SetAttribute("util", new Util());
            engine.Parse();
            engine.Execute();
            string sql = engine.Output;

            executeSQL(sql);
        }
        private void cmdExecuteScript(string arg)
        {
            Interpreter engine = new Interpreter(CurrSQLEditor.SQLEditor.Text, null);
            engine.SetAttribute("db", Provider.Database);
            engine.SetAttribute("util", new Util());
            engine.Parse();
            engine.Execute();

            CurrSQLEditor.ShowInfoText(engine.Output);

            statusText.Text = "Script executed succesfully.";
        }

        private void cmdSetActiveConnection(string arg)
        {
            TreeNode tnOld = findNode(treeView.Nodes, n => n.ForeColor == Color.White);
            if (tnOld != null)
            { 
                tnOld.ForeColor = Color.Black; 
                tnOld.BackColor = Color.White; 
            }

            if (treeView.SelectedNode.Tag is ConnectionSettings)
            {
                Provider.ActiveConnection = (ConnectionSettings)treeView.SelectedNode.Tag;

                if (treeView.SelectedNode.Nodes.Count == 0)
                    populateTreeNodesForDatabase(treeView.SelectedNode);
            }
            else
            {
                Provider.ActiveConnection = findConnection(treeView.SelectedNode);
            }

            TreeNode tnNew = findSelectedDBNode();
            if (tnNew != null)
            {
                tnNew.ForeColor = Color.White;
                tnNew.BackColor = Color.Green;
            }

            cbActiveConnection.SelectedItem = Provider.ActiveConnection;

            statusText.Text = "Active connection: " + Provider.ActiveConnection;
        }

        private void cmdTableOpen(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            executeSQL("select top 1000 * from [" + tableName + "]");
        }
        private void cmdTableDrop(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            if (MessageBox.Show("Are you sure to drop table " + tableName + "? All data will be lost!", "Çınar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int i = Provider.Database.ExecuteNonQuery("drop table " + tableName);
                Provider.Database.Tables.Remove(Provider.Database.Tables[tableName]);
                cmdRefresh(null);
                CurrSQLEditor.ShowInfoText("Table " + tableName + " dropped succesfully.");
            }
        }
        private void cmdTableCount(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            executeSQL("select count(*) AS number_of_rows from [" + tableName + "]");
        }
        private void cmdAnalyzeTable(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            
            int count = Provider.Database.GetInt("select count(*) from " + tableName);

            DataTable dtMaxMin = null;
            if (count > 0)
            {
                List<string> list = new List<string>();
                foreach (Field f in Provider.Database.Tables[tableName].Fields)
                    list.Add("select '" + f.Name + "' as [Field Name], min(" + f.Name + ") as [Min. Value], max(" + f.Name + ") as [Max. Value] from " + tableName);
                string sql = string.Join("\nUNION\n", list.ToArray());
                dtMaxMin = Provider.Database.GetDataTable(sql);
            }

            //Cinar.WebServer.WebServer server = new Cinar.WebServer.WebServer(3000);
            //server.ProcessRequest = (string req) => {
            //    MessageBox.Show(req);
            //};
            //server.Start();

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
            FormCreateTable fct = new FormCreateTable();
            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    Table tbl = fct.GetTable();
                    try
                    {
                        Provider.Database.Tables.Add(tbl);

                        if (string.IsNullOrEmpty(tbl.Name))
                            throw new Exception("Enter a valid name for the table.");
                        if (tbl.Fields == null || tbl.Fields.Count == 0)
                            throw new Exception("Add minimum one field to the table.");

                        string sql = tbl.ToDDL();
                        Provider.Database.ExecuteNonQuery(sql);
                        cmdRefresh("");
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Çınar DBTools");
                        Provider.Database.Tables.Remove(tbl);
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
        }
        private void cmdAlterTable(string arg)
        {
            FormCreateTable fct = new FormCreateTable();
            Table oldTable = treeView.SelectedNode.Tag as Table;
            fct.SetTable(oldTable);

            while (true)
            {
                if (fct.ShowDialog() == DialogResult.OK)
                {
                    Table newTable = fct.GetTable();
                    try
                    {
                        if (string.IsNullOrEmpty(newTable.Name))
                            throw new Exception("Enter a valid name for the table.");
                        if (newTable.Fields == null || newTable.Fields.Count == 0)
                            throw new Exception("Add minimum one field to the table.");

                        //string sql = tbl.ToDDL();
                        //Provider.Database.ExecuteNonQuery(sql);
                        //TODO: compare oldTable and newTable, and alter table here
                        cmdRefresh("");
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Çınar DBTools");
                        fct.DialogResult = DialogResult.None;
                    }
                }
                else
                    break;
            }
        }

        private void cmdGenerateSQL(string arg)
        {
            Table table = (Table)treeView.SelectedNode.Tag;
            StringBuilder sb = new StringBuilder();
            switch (arg)
            {
                case "CreateTable":
                    sb.Append(table.ToDDL());
                    break;
                case "Insert":
                    sb.AppendLine("insert into " + table.Name + "(");
                    foreach (Field field in table.Fields)
                        if (!field.IsPrimaryKey)
                            sb.AppendLine("\t" + field.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(") values (");
                    foreach (Field field in table.Fields)
                        if (!field.IsPrimaryKey)
                            sb.AppendLine("\t@" + field.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(")");
                    break;
                case "Update":
                    sb.AppendLine("update " + table.Name + " set");
                    foreach (Field field in table.Fields)
                        if (!field.IsPrimaryKey)
                            sb.AppendLine("\t" + field.Name + " = @" + field.Name + ",");
                    sb = sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine("where");
                    if (table.PrimaryField != null)
                        sb.AppendLine("\t@" + table.PrimaryField.Name + " = @" + table.PrimaryField.Name);
                    else
                        sb.AppendLine("\t1 = 2");
                    break;
                case "Select":
                    sb.AppendLine("select ");
                    foreach (Field field in table.Fields)
                        sb.AppendLine("\t" + field.Name + ",");
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
            statusText.Text = "SQL generated.";
            addSQLEditor("", sb.ToString());
        }

        private void cmdGenerateUIMetadata(string arg)
        {
            if (treeView.SelectedNode.Tag is ConnectionSettings)
            {
                ConnectionSettings cs = (ConnectionSettings)treeView.SelectedNode.Tag;
                if (MessageBox.Show("UI metadata will be generated for database " + cs.DbName + ". Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                cs.Database.GenerateUIMetadata();
            }
            else if (treeView.SelectedNode.Tag is Table)
            {
                Table tbl = (Table)treeView.SelectedNode.Tag;
                if (MessageBox.Show("UI metadata will be generated for table " + tbl.Name + ". Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                tbl.GenerateUIMetadata();
            }
            else if (treeView.SelectedNode.Tag is Field)
            {
                Field fld = (Field)treeView.SelectedNode.Tag;
                if (MessageBox.Show("UI metadata will be generated for field " + fld.Name + ". Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                fld.GenerateUIMetadata();
            }
        }

        private void cmdFieldDistinct(string arg)
        {
            if (!checkConnection() || !(treeView.SelectedNode.Tag is Field)) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL(@"
                select distinct top 1000 
                    {0} 
                from 
                    [{1}]", 
                          field.Name, 
                          field.Table.Name);
        }
        private void cmdFieldMax(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL(@"
                select 
                    max({0}) AS {0}_MAX_value 
                from 
                    [{1}]",
                          field.Name,
                          field.Table.Name);
        }
        private void cmdFieldMin(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL(@"
                select 
                    min({0}) AS {0}_MIN_value 
                from 
                    [{1}]", 
                          field.Name, 
                          field.Table.Name);
        }

        private void cmdGroupedCounts(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            if (field.ReferenceField == null || field.ReferenceField.Table.StringField == null)
            {
                executeSQL(@"
                    select top 1000 
                        {0}, 
                        count(*) as RecordCount 
                    from 
                        [{1}] 
                    group by {0} 
                    order by RecordCount desc",
                                              field.Name,
                                              field.Table.Name);
            }
            else
            {
                executeSQL(@"
                    select top 1000 
                        t.{0}, 
                        tRef.{4},
                        count(*) as RecordCount 
                    from 
                        {1} t
                        left join {3} tRef on t.{0} = tRef.{2}
                    group by t.{0}, tRef.{4}
                    order by RecordCount desc",
                                              field.Name,
                                              field.Table.Name,
                                              field.ReferenceField.Name,
                                              field.ReferenceField.Table.Name,
                                              field.ReferenceField.Table.StringField.Name);
            }

            MyDataGrid grid = CurrSQLEditor.Grid;
            grid.DoubleClick += delegate {
                if (grid.SelectedRows.Count <= 0)
                    return;
                DataRow dr = (grid.SelectedRows[0].DataBoundItem as DataRowView).Row;
                object keyVal = dr[field.Name];
                string from = Provider.Database.GetFromWithJoin(field.Table);
                executeSQL(@"
                    select top 1000 
                        * 
                    from {1} 
                    where [{3}].[{0}] = '{2}'",
                                              field.Name,
                                              from,
                                              keyVal,
                                              field.Table.Name);
            };
            
        }

        private void cmdNewERDiagram(string arg)
        {
            FormERDiagram form = new FormERDiagram();
            form.MainForm = this;
            form.Show();
        }
        private void cmdOpenERDiagram(string arg)
        {
            Schema schema = treeView.SelectedNode.Tag as Schema;
            schema.conn = Provider.ActiveConnection;
            FormERDiagram form = new FormERDiagram();
            form.MainForm = this;
            form.CurrentSchema = schema;
            form.Show();
        }
        private void cmdDeleteERDiagram(string arg)
        {
            Schema schema = treeView.SelectedNode.Tag as Schema;
            Provider.ActiveConnection.Schemas.Remove(schema);
            SaveConnections();
            cmdRefresh(null);
        }

        private void cmdShowForm(string arg)
        {
            IDBToolsForm form = (IDBToolsForm)Activator.CreateInstance(Type.GetType(arg));
            form.MainForm = this;
            form.Show();
        }

        private void cmdQuickScript(string arg)
        {
            CurrSQLEditor.SQLEditor.Text = arg;
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
            while (CurrSQLEditor != null)
                cmdCloseSQLEditor("");
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                if (e.Node.Tag is Table)
                    (e.Node.Tag as Table).Name = e.Label;
                else if (e.Node.Tag is Field)
                    (e.Node.Tag as Field).Name = e.Label;
                else if (e.Node.Tag is ConnectionSettings)
                    (e.Node.Tag as ConnectionSettings).Database.Name = e.Label;
                else if (e.Node.Tag is Schema)
                    (e.Node.Tag as Schema).Name = e.Label;
            }
        }
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Schema)
                cmdOpenERDiagram(null);
            else if (e.Node.Tag is ConnectionSettings)
                cmdEditConnection(null);
            else if (e.Node.Tag is Table)
                cmdTableOpen(null);
            else if (e.Node.Tag is Field)
                cmdFieldDistinct(null);
        }
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView.SelectedNode = e.Node;
        }

        private void cbActiveConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = findNode(treeView.Nodes, n => n.Tag == cbActiveConnection.SelectedItem);
            TreeNode node2 = findSelectedDBNode();
            if (node != node2) treeView.SelectedNode = node;
        }
    }

    public class Provider
    {
        public static List<ConnectionSettings> Connections = new List<ConnectionSettings>();
        public static ConnectionSettings ActiveConnection;
        public static Database.Database Database
        {
            get
            {
                if (ActiveConnection == null)
                    return null;

                return ActiveConnection.Database;
            }
        }

        public static ConnectionSettings GetConnection(string connectionName)
        {
            foreach (ConnectionSettings cs in Connections)
                if (connectionName == cs.ToString())
                    return cs;
            return null;
        }

        private static bool connectionsLooaded = false;

        public static string ConnectionsPath;
        public static void LoadConnectionsFromXML(string path)
        {
            if (path==ConnectionsPath && connectionsLooaded)
                return;

            if(string.IsNullOrEmpty(path))
                ConnectionsPath = path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";

            if (File.Exists(path))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
                using (StreamReader sr = new StreamReader(path))
                {
                    try
                    {
                        Connections = (List<ConnectionSettings>)ser.Deserialize(sr);
                    }
                    catch {
                        MessageBox.Show("This is not a valid connection file", "Çınar");
                    }
                    foreach (ConnectionSettings cs in Connections)
                    {
                        if (cs.Database == null)
                            cs.RefreshDatabaseSchema();
                        cs.Database.SetCollectionParents();
                        cs.Database.SetConnectionString(cs.Provider, cs.Host, cs.DbName, cs.UserName, cs.Password, 1000);
                        cs.Database.CreateDbProvider(false);
                    }
                }
            }
            connectionsLooaded = true;
        }
        public static void SaveConnections()
        {
            if (string.IsNullOrEmpty(ConnectionsPath))
                ConnectionsPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";
            XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
            using (StreamWriter sr = new StreamWriter(ConnectionsPath))
            {
                ser.Serialize(sr, Provider.Connections);
            }
        }
    }
    public class ConnectionSettings
    {
        public DatabaseProvider Provider { get; set; }
        public string Host { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Database.Database Database;
        public List<Schema> Schemas = new List<Schema>();

        public void RefreshDatabaseSchema()
        {
            //if (Database == null)
                Database = new Database.Database(Provider, Host, DbName, UserName, Password, 30);
            //else
            //    Database.Refresh();
        }

        public override string ToString()
        {
            string toStr = DbName + " @ ";
            if (Host.Contains('.'))
            {
                string[] parts = Host.Split('.');
                toStr += parts[parts.Length - 2]  + "." + parts[parts.Length - 1];
            }
            else
                toStr += Host;
            toStr += " (" + Provider + ")";

            return toStr;
        }
    }
}