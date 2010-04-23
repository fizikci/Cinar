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

namespace Cinar.DBTools
{
    public partial class FormMain : Form
    {
        CommandManager cmdMan = new CommandManager();
        TreeNode rootNode;

        public FormMain()
        {
            InitializeComponent();

            cmdMan.AfterCommandExecute = () =>
            {
                cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            };

            cmdMan.Commands = new CommandCollection(){
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuNewConnection},
                                         new CommandTrigger{ Control = btnNewConnection},
                                         new CommandTrigger{ Control = menuNewConnectionContext},
                                     },
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode==treeView.Nodes[0]
                                 },
                     new Command {
                                     Execute = cmdExit,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuExit},
                                     }
                                 },
                     new Command {
                                     Execute = cmdEditConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuEditConnection},
                                         new CommandTrigger{ Control = btnEditConnection},
                                     },
                                     IsEnabled = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings,
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdDeleteConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDeleteConnection},
                                         new CommandTrigger{ Control = btnDeleteConnection},
                                     },
                                     IsEnabled = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings,
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdShowTableCounts,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuShowTableCounts},
                                     },
                                     IsEnabled = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings,
                                     IsVisible = () => treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdExecuteSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = btnExecuteSQL},
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
                                         new CommandTrigger{ Control = menuCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 
                                         new CommandTrigger{ Control = btnCodeGenerator, Argument=typeof(FormCodeGenerator).FullName}, 
                                         new CommandTrigger{ Control = menuCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},
                                         new CommandTrigger{ Control = btnCheckDatabaseSchema, Argument=typeof(FormCheckDatabaseSchema).FullName},
                                         new CommandTrigger{ Control = menuDBTransfer, Argument=typeof(FormDBTransfer).FullName},
                                         new CommandTrigger{ Control = btnDatabaseTransfer, Argument=typeof(FormDBTransfer).FullName},
                                         new CommandTrigger{ Control = menuCopyTreeData, Argument=typeof(FormCopyTreeData).FullName},
                                         new CommandTrigger{ Control = menuSimpleIntegrationService, Argument=typeof(FormDBIntegration).FullName},
                                         new CommandTrigger{ Control = menuScriptingTest, Argument=typeof(FormScriptingTest).FullName},
                                         new CommandTrigger{ Control = menuCompareDatabases, Argument=typeof(FormCompareDatabases).FullName},
                                     },
                                     IsEnabled = ()=> Provider.Database != null
                                 },
                     new Command {
                                     Execute = cmdQuickScript,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDeleteFromTables, Argument=@"$
foreach(table in db.Tables)
    echo('delete from ' + table.Name + ';\r\n');
$"},
                                         new CommandTrigger{ Control = menuSelectCountsFromTables, Argument=@"$
foreach(table in db.Tables)
    echo(""select '"" + table.Name + ""', count(*) from "" + table.Name + "" UNION \r\n"");
$"},
                                     }
                                 },
                     new Command {
                                     Execute = cmdSetActiveConnection,
                                     Trigger = new CommandTrigger{ Control = treeView, Event = "AfterSelect"}
                                 },
                     new Command {
                                     Execute = cmdTableCount,
                                     Trigger = new CommandTrigger{ Control = menuCount},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdTableTop10,
                                     Trigger = new CommandTrigger{ Control = menuTop10},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdTableDrop,
                                     Trigger = new CommandTrigger{ Control = menuTableDrop},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdFieldDistinct,
                                     Trigger = new CommandTrigger{ Control = menuDistinct},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdFieldMax,
                                     Trigger = new CommandTrigger{ Control = menuMax},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdFieldMin,
                                     Trigger = new CommandTrigger{ Control = menuMin},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdGroupedCounts,
                                     Trigger = new CommandTrigger{ Control = menuGroupedCounts},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Field
                                 },
                     new Command {
                                     Execute = cmdAnalyzeTable,
                                     Trigger = new CommandTrigger{ Control = menuAnalyzeTable},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdRefresh,
                                     Trigger = new CommandTrigger{ Control = menuRefresh},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdRefreshMetadata,
                                     Trigger = new CommandTrigger{ Control = menuRefreshMetadata},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
                                 },
                     new Command {
                                     Execute = cmdOpenERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuOpenERDiagram},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Schema,
                                 },
                     new Command {
                                     Execute = cmdDeleteERDiagram,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDeleteERDiagram},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Schema,
                                 },
                     new Command {
                                     Execute = cmdNewERDiagram,
                                     Trigger = new CommandTrigger{ Control = menuNewERDiagram},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is List<Schema>
                                 },
                     new Command {
                                     Execute = cmdTryAndSee,
                                     Trigger = new CommandTrigger{ Control = btnTryAndSee}
                                 },
                     new Command {
                                     Execute = cmdGenerateSQL,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuGenerateSQL, Argument="-"},
                                         new CommandTrigger{ Control = menuGenerateSQLCreateTable, Argument="CreateTable"},
                                         new CommandTrigger{ Control = menuGenerateSQLInsert, Argument="Insert"},
                                         new CommandTrigger{ Control = menuGenerateSQLSelect, Argument="Select"},
                                         new CommandTrigger{ Control = menuGenerateSQLUpdate, Argument="Update"},
                                     },
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is Table
                                 },
                     new Command {
                                     Execute = cmdGenerateUIMetadata,
                                     Trigger = new CommandTrigger{ Control = menuGenerateUIMetadata},
                                     IsVisible = ()=> treeView.SelectedNode!=null && (treeView.SelectedNode.Tag is ConnectionSettings || treeView.SelectedNode.Tag is Table || treeView.SelectedNode.Tag is Field)
                                 },
             };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            rootNode = treeView.Nodes.Add("Database Connections");
            Provider.LoadConnectionsFromXML();
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                TreeNode node = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database");
                node.Tag = cs;

                populateTreeNodesForDatabase(node);
            }
            treeView.Sort();
            if (rootNode.Nodes.Count > 0 && rootNode.Nodes[0].Nodes.Count > 0)
            {
                rootNode.Nodes[0].Expand();
                treeView.SelectedNode = rootNode.Nodes[0];
            }
        }

        #region methods
        public void SaveConnections()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";
            XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
            using (StreamWriter sr = new StreamWriter(path))
            {
                ser.Serialize(sr, Provider.Connections);
            }
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
            txtSQLLog.Text += Environment.NewLine + sql + (sql.EndsWith(";") ? "" : ";");

            if (ds.Tables.Count > 1)
                bindGridResults(ds);
            else if (ds.Tables.Count == 1)
                bindGridResults(ds.Tables[0]);
            else
                bindGridResults(null);
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

            TreeNode schemasNode = parentNode.Nodes.Add("ER Diagrams", "ER Diagrams", "Diagram", "Diagram");
            schemasNode.Tag = cs.Schemas;

            TreeNode tablesNode = parentNode.Nodes.Add("Tables", "Tables", "Table", "Table");
            tablesNode.Tag = cs.Database.Tables;
            TreeNode viewsNode = parentNode.Nodes.Add("Views", "Views", "View", "View");
            viewsNode.Tag = cs.Database.Tables;

            foreach (Table tbl in cs.Database.Tables)
            {
                TreeNode tnTable = (tbl.IsView ? viewsNode : tablesNode).Nodes.Add(tbl.Name, tbl.Name, tbl.IsView ? "View" : "Table", tbl.IsView ? "View" : "Table");
                tnTable.Tag = tbl;
                foreach (Field fld in tbl.Fields)
                {
                    TreeNode tnField = tnTable.Nodes.Add(fld.Name, fld.Name + " (" + fld.FieldType + ")", fld.IsPrimaryKey ? "Key" : "Field", fld.IsPrimaryKey ? "Key" : "Field");
                    tnField.Tag = fld;
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
        private void bindGridResults(object data)
        {
            tabControl.SelectedTab = tpResults;
            tpResults.Controls.Clear();

            if (data is DataSet)
            {
                DataSet ds = data as DataSet;
                if (ds == null || ds.Tables.Count == 0)
                    return;

                if (ds.Tables.Count == 1)
                {
                    MyDataGrid grid = createNewGrid();
                    grid.DataSource = ds.Tables[0];
                    tpResults.Controls.Add(grid);
                    return;
                }

                Control currContainer = tpResults;
                for (int i = 0; i < ds.Tables.Count - 1; i++)
                {
                    SplitContainer sc = new SplitContainer();
                    sc.Dock = DockStyle.Fill;
                    sc.Orientation = Orientation.Horizontal;
                    MyDataGrid grid = createNewGrid();
                    grid.DataSource = ds.Tables[i];
                    sc.Panel1.Controls.Add(grid);
                    if (i == ds.Tables.Count - 2)
                    {
                        MyDataGrid grid2 = createNewGrid();
                        grid2.DataSource = ds.Tables[i + 1];
                        sc.Panel2.Controls.Add(grid2);
                    }
                    currContainer.Controls.Add(sc);
                    currContainer = sc.Panel2;
                }
            }
            else
            {
                MyDataGrid gridResults = createNewGrid();
                gridResults.DataSource = data;
                tpResults.Controls.Add(gridResults);
            }
        }

        private MyDataGrid createNewGrid()
        {
            MyDataGrid grid = new MyDataGrid();
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = true;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            return grid;
        }

        private void showInfoText(string txt)
        {
            tabControl.SelectedTab = tpInfo;
            txtInfo.Text = txt;
        }
        #endregion

        #region Commands
        private void cmdRefresh(string arg)
        {
            TreeNode dbNode = findSelectedDBNode();
            dbNode.Nodes.Clear();
            populateTreeNodesForDatabase(dbNode);
            treeView.Sort();
        }
        private void cmdRefreshMetadata(string arg)
        {
            if (MessageBox.Show("Metada will be reread from database and all changes made to metadata will be lost. Continue?", "Çınar Database Tools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Provider.ActiveConnection.RefreshDatabaseSchema();
                TreeNode dbNode = findSelectedDBNode();
                dbNode.Nodes.Clear();
                populateTreeNodesForDatabase(dbNode);
                SaveConnections();
                treeView.Sort();
            }
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
                rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database").Tag = cs;
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
            Database.Database db = (treeView.SelectedNode.Tag as ConnectionSettings).Database;
            foreach (TreeNode node in treeView.SelectedNode.Nodes["Tables"].Nodes)
            { 
                Table tbl = node.Tag as Table;
                node.Text = string.Format("{0} ({1})", tbl.Name, db.GetInt("select count(*) from [" + tbl + "]"));
            }
        }

        private void cmdExecuteSQL(string arg)
        {
            if(!checkConnection()) return;

            Interpreter engine = new Interpreter(txtSQL.Text, null);
            engine.SetAttribute("db", Provider.Database);
            engine.SetAttribute("util", new Util());
            engine.Parse();
            engine.Execute();
            string sql = engine.Output;

            executeSQL(sql);
        }
        private void cmdExecuteScript(string arg)
        {
            Interpreter engine = new Interpreter(txtSQL.Text, null);
            engine.SetAttribute("db", Provider.Database);
            engine.SetAttribute("util", new Util());
            engine.Parse();
            engine.Execute();

            showInfoText(engine.Output);

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

            statusText.Text = "Active connection: " + Provider.ActiveConnection;
        }

        private void cmdTableTop10(string arg)
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
                showInfoText("Table " + tableName + " dropped succesfully.");
            }
            cmdRefresh(null);
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

            webBrowser.Navigate(path);
            tabControl.SelectedTab = tpTableAnalyze;
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
                case "-":
                    return;
                default:
                    sb.AppendLine("Not available.");
                    break;
            }
            statusText.Text = "SQL generated.";
            showInfoText(sb.ToString());
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
                executeSQL(@"
                    select top 100 
                        {0}, 
                        count(*) as RecordCount 
                    from 
                        [{1}] 
                    group by {0} 
                    order by RecordCount desc", 
                                              field.Name, 
                                              field.Table.Name);
            else
                executeSQL(@"
                    select top 100 
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
            txtSQL.Text = arg;
        }

        private void cmdExit(string arg)
        {
            Close();
        }
        private void cmdTryAndSee(string arg)
        {
            MessageBox.Show("TryAndSee");
        }
        #endregion

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
            }
        }
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Schema)
                cmdOpenERDiagram(null);
            else if (e.Node.Tag is ConnectionSettings)
                cmdEditConnection(null);
            else if (e.Node.Tag is Table)
                cmdTableTop10(null);
            else if (e.Node.Tag is Field)
                cmdFieldDistinct(null);
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

        public static void LoadConnectionsFromXML()
        {
            if (connectionsLooaded)
                return;

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";
            if (File.Exists(path))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
                using (StreamReader sr = new StreamReader(path))
                {
                    Connections = (List<ConnectionSettings>)ser.Deserialize(sr);
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
            if (Database == null)
                Database = new Database.Database(Provider, Host, DbName, UserName, Password, 1000);
            else
                Database.Refresh();
        }

        public override string ToString()
        {
            string toStr = DbName;
            toStr += " (" + Provider;
            if (Host.Contains('.'))
            {
                string[] parts = Host.Split('.');
                toStr += " @ " + parts[parts.Length - 2]  + "." + parts[parts.Length - 1];
            }
            else
                toStr += " @ " + Host;
            toStr += ")";

            return toStr;
        }
    }
}