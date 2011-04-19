using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Controls
{
    public partial class SQLEditorAndResults : UserControl
    {
        public string filePath;
        public string FilePath { get { return filePath; } }
        public bool Modified { get { return txtSQL.Text!=InitialText; } }

        public SQLEditorAndResults(string filePath, string sql)
        {
            InitializeComponent();

            this.filePath = filePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                txtSQL.LoadFile(filePath);
                InitialText = txtSQL.Text;
            }
            else if (!string.IsNullOrEmpty(sql))
            {
                txtSQL.Text = sql;
                InitialText = txtSQL.Text;
            }
            else
            {
                InitialText = txtSQL.Text = "";
            }

            txtSQLLog.Document.ReadOnly = true;
        }

        public string InitialText;

        public CinarSQLEditor SQLEditor {
            get {
                return txtSQL;
            }
        }

        public CinarSQLEditor SQLLog
        {
            get
            {
                return txtSQLLog;
            }
        }

        public MyDataGrid Grid {
            get {
                return (MyDataGrid)tpResults.Controls[0];
            }
        }

        public void BindGridResults(object data)
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


        public void ShowInfoText(string txt)
        {
            tabControl.SelectedTab = tpInfo;
            txtInfo.Text = txt;
        }

        public void Navigate(string url)
        {
            webBrowser.Navigate(url);
            tabControl.SelectedTab = tpTableAnalyze;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(filePath))
                return SaveAs();

            InitialText = txtSQL.Text;
            txtSQL.SaveFile(filePath);
            return true;
        }

        public bool SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                InitialText = txtSQL.Text;
                txtSQL.SaveFile(filePath);
                return true;
            }
            return false;
        }
    }
}
