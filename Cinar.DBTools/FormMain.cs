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

namespace Cinar.DBTools
{
    public partial class FormMain : Form
    {
        CommandManager cmdMan = new CommandManager();
        TreeNode rootNode;

        public FormMain()
        {
            InitializeComponent();

            cmdMan.AfterCommandExecute = () => {
                cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            };

            cmdMan.Commands = new CommandCollection(){
                     new Command {
                                     Execute = cmdNewConnection,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuNewConnection},
                                         new CommandTrigger{ Control = btnNewConnection},
                                     }
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
                                     Execute = cmdGenerateCode,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuCodeGenerator}, 
                                         new CommandTrigger{ Control = btnCodeGenerator}, 
                                     },
                                     IsEnabled = ()=> Provider.Database != null
                                 },
                     new Command {
                                     Execute = cmdCheckDatabaseSchema,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuCheckDatabaseSchema},
                                         new CommandTrigger{ Control = btnCheckDatabaseSchema},
                                     }
                                 },
                     new Command {
                                     Execute = cmdDBTransfer,
                                     Triggers = new List<CommandTrigger>(){
                                         new CommandTrigger{ Control = menuDBTransfer},
                                         new CommandTrigger{ Control = btnDatabaseTransfer},
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
                                     Execute = cmdRefresh,
                                     Trigger = new CommandTrigger{ Control = menuRefresh},
                                     IsVisible = ()=> treeView.SelectedNode!=null && treeView.SelectedNode.Tag is ConnectionSettings
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
             };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            rootNode = treeView.Nodes.Add("Database Connections");
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";
            if (File.Exists(path))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
                using (StreamReader sr = new StreamReader(path))
                {
                    Provider.Connections = (List<ConnectionSettings>)ser.Deserialize(sr);
                    foreach (ConnectionSettings cs in Provider.Connections)
                    {
                        if (cs.Database == null)
                            cs.RefreshDatabaseSchema();
                        cs.Database.SetCollectionParents();
                        cs.Database.SetConnectionString(cs.Provider, cs.Host, cs.DbName, cs.UserName, cs.Password, 1000);
                        cs.Database.CreateDbProvider();

                        TreeNode node = rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database");
                        node.Tag = cs;
                        populateTreeNodesForDatabase(node);
                    }
                }
            }
            treeView.Sort();
        }

        private void saveConnections()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";
            XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
            using (StreamWriter sr = new StreamWriter(path))
            {
                ser.Serialize(sr, Provider.Connections);
            }
            statusText.Text = "Connections saved.";
        }
        private void cmdRefresh(string arg)
        {
            Provider.ActiveConnection.RefreshDatabaseSchema();
            TreeNode dbNode = findSelectedDBNode();
            dbNode.Nodes.Clear();
            populateTreeNodesForDatabase(dbNode);
            saveConnections();
            treeView.Sort();
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
                saveConnections();
                rootNode.Nodes.Add(cs.ToString(), cs.ToString(), "Database", "Database").Tag = cs;
            }
        }
        private void cmdExit(string arg)
        {
            Close();
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
                saveConnections();
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
                saveConnections();
                currNode.Remove();

                statusText.Text = "Connection deleted.";
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
                    if(table.PrimaryField!=null)
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
        private void cmdExecuteSQL(string arg)
        {
            if(!checkConnection()) return;
            executeSQL(txtSQL.Text);
        }

        private void executeSQL(string sql)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataTable dt = Provider.Database.GetDataTable(sql);
            watch.Stop();
            statusExecTime.Text = watch.ElapsedMilliseconds + " ms";
            statusNumberOfRows.Text = (dt==null ? 0 : dt.Rows.Count) + " rows";
            statusText.Text = "Query executed succesfully.";
            bindGridResults(dt);
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

            statusText.Text = "Active connection: " + Provider.ActiveConnection;
        }

        private void populateTreeNodesForDatabase(TreeNode parentNode)
        {
            ConnectionSettings cs = parentNode.Tag as ConnectionSettings;
            if (cs == null)
                return; //***

            foreach (Table tbl in cs.Database.Tables)
            {
                TreeNode tnTable = parentNode.Nodes.Add(tbl.Name, tbl.Name, "Table", "Table");
                tnTable.Tag = tbl;
                foreach (Field fld in tbl.Fields)
                {
                    TreeNode tnField = tnTable.Nodes.Add(fld.Name, fld.Name + " (" + fld.FieldType + ")", fld.IsPrimaryKey ? "Key" : "Field", fld.IsPrimaryKey ? "Key" : "Field");
                    tnField.Tag = fld;
                }
            }
        }

        private ConnectionSettings findConnection(TreeNode treeNode)
        {
            while (treeNode.Parent!=null)
            {
                if(treeNode.Tag is ConnectionSettings)
                    return (ConnectionSettings)treeNode.Tag;
                treeNode = treeNode.Parent;
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

        private void cmdTableTop10(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            executeSQL("select top 10 * from " + tableName);
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

        private void bindGridResults(DataTable dt)
        {
            tabControl.SelectedTab = tpResults;
            gridResults.DataSource = dt;
        }

        private void showInfoText(string txt)
        {
            tabControl.SelectedTab = tpInfo;
            txtInfo.Text = txt;
        }

        private void cmdTableCount(string arg)
        {
            if (!checkConnection()) return;
            string tableName = treeView.SelectedNode.Name;
            executeSQL("select count(*) AS number_of_rows from " + tableName);
        }

        private void cmdFieldDistinct(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL("select distinct top 100 " + field.Name + " from " + field.Table.Name);
        }

        private void cmdFieldMax(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL("select max(" + field.Name + ") AS " + field.Name + "_MAX_value from " + field.Table.Name);
        }

        private void cmdFieldMin(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL("select min(" + field.Name + ") AS " + field.Name + "_MIN_value from " + field.Table.Name);
        }

        private void cmdGroupedCounts(string arg)
        {
            if (!checkConnection()) return;
            Field field = (Field)treeView.SelectedNode.Tag;
            executeSQL("select top 100 " + field.Name + ", count(*) as RecordCount from " + field.Table.Name + " group by "+field.Name+" order by RecordCount desc");
        }

        private void cmdGenerateCode(string arg)
        {
            FormCodeGenerator form = new FormCodeGenerator();
            form.Show();
        }

        private void cmdCheckDatabaseSchema(string arg)
        {
            FormCheckDatabaseSchema form = new FormCheckDatabaseSchema(saveConnections);
            form.Show();
        }

        private void cmdDBTransfer(string arg)
        {
            FormDBTransfer form = new FormDBTransfer();
            form.Show();
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                if (e.Node.Tag is Table)
                    (e.Node.Tag as Table).Name = e.Label;
                else if (e.Node.Tag is Field)
                    (e.Node.Tag as Field).Name = e.Label;
            }
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
    }
    public class ConnectionSettings
    {
        public DatabaseProvider Provider { get; set; }
        public string Host { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Database.Database Database;

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
                toStr += Host;
            toStr += ")";

            return toStr;
        }
    }
}