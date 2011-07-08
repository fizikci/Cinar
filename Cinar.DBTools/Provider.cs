using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Menees.DiffUtils;
using Menees.DiffUtils.Controls;

namespace Cinar.DBTools
{
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

        public static bool ConnectionsModified { get; set; }

        public static ConnectionSettings GetConnection(string connectionName)
        {
            foreach (ConnectionSettings cs in Connections)
                if (connectionName == cs.ToString())
                    return cs;
            return null;
        }

        private static bool connectionsLoaded = false;

        public static string ConnectionsPath;
        public static void LoadConnectionsFromXML(string path)
        {
            if (path == Provider.ConnectionsPath && connectionsLoaded)
                return;

            if(string.IsNullOrEmpty(path))
                path = Path.GetDirectoryName(Application.ExecutablePath) + "\\constr.xml";

            Provider.ConnectionsPath = path;

            if (File.Exists(path))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ConnectionSettings>));
                using (StreamReader sr = new StreamReader(path))
                {
                    try
                    {
                        Provider.Connections = (List<ConnectionSettings>)ser.Deserialize(sr);
                    }
                    catch {
                        MessageBox.Show("This is not a valid connection file", "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    foreach (ConnectionSettings cs in Connections)
                    {
                        cs.InitializeDatabase();
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found:\n" + path, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connectionsLoaded = true;
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

        public static void CompareCode(string filePath, string modifiedContent)
        {
            Form f = new Form();
            f.WindowState = FormWindowState.Maximized;
            f.Text = "[Original] vs. [Modified]";
            //ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            //f.Icon = (Icon)(resources.GetObject("$this.Icon"));

            DiffControl diffControl = new DiffControl();
            diffControl.Dock = DockStyle.Fill;
            f.Controls.Add(diffControl);

            bool chkIgnoreCase = false;
            bool chkIgnoreWhitespace = true;
            bool chkSupportChangeEditType = false;
            bool chkXML = false;

            string strA = filePath;

            try
            {
                TextDiff diff = new TextDiff(HashType.CRC32, chkIgnoreCase, chkIgnoreWhitespace, 0, chkSupportChangeEditType);

                IList<string> code1, code2;
                if (chkXML)
                {
                    code1 = Functions.GetXMLTextLines(strA, WhitespaceHandling.All);
                    code2 = modifiedContent.Replace("\r", "").Split('\n');
                }
                else
                {
                    code1 = Functions.GetFileTextLines(strA);
                    code2 = modifiedContent.Replace("\r", "").Split('\n');
                }

                EditScript script = diff.Execute(code1, code2);

                diffControl.SetData(code1, code2, script);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            f.Show();
        }

    }
}