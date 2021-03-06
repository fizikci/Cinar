﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using System.IO;
using Cinar.DBTools.Tools;

namespace Cinar.DBTools.Controls
{
    public partial class SQLEditorAndResults : UserControl, IEditor
    {
        public string filePath;
        public string FilePath { get { return filePath; } }
        public bool Modified { get { return txtSQL.Text!=InitialText; } }

        private string tempFilePath = "";

        public SQLEditorAndResults(string filePath, string sql, bool temp)
        {
            InitializeComponent();

            btnSave.Text = "";

            imageListTabs.Images.Add("Results", FamFamFam.application_split);
            imageListTabs.Images.Add("Output", FamFamFam.application_xp_terminal);
            imageListTabs.Images.Add("SQLLog", FamFamFam.clock);
            imageListTabs.Images.Add("Info", FamFamFam.information);
            imageListTabs.Images.Add("TableData", FamFamFam.table);

            tpResults.ImageKey = "Results";
            tpOutput.ImageKey = "Output";
            tpSQLLog.ImageKey = "SQLLog";
            tpInfo.ImageKey = "Info";
            tpTableData.ImageKey = "TableData";

            this.filePath = temp ? "" : filePath;
            this.tempFilePath = temp ? filePath : "";

            if (!string.IsNullOrEmpty(filePath))
            {
                txtSQL.LoadFile(filePath);
                InitialText = txtSQL.Text;
            }
            else if (!string.IsNullOrEmpty(tempFilePath))
            {
                txtSQL.LoadFile(tempFilePath);
                InitialText = "this is not a saved file. just a temp file. this sentence is not for warning, just to be sure that InitialText is not equal to txtSQL.Text. (to keep the editor in Modified state)";
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

            gridShowTable.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(gridShowTable_ColumnHeaderMouseClick);
            gridShowTable.CellValueChanged += delegate { btnSave.Text = "Click to save!"; };
            gridShowTable.UserAddedRow += delegate { btnSave.Text = "Click to save!"; };
            btnNextPage.Click += new EventHandler(btnNextPage_Click);
            btnPrevPage.Click += new EventHandler(btnPrevPage_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnRefresh.Click += delegate { bindTableData(); };
            btnDeleteSelectedRows.Click += delegate { deleteSelectedRows(); };
            btnFilter.Click += new EventHandler(btnFilter_Click);

            fExp = new FilterExpression();
        }

        void btnFilter_Click(object sender, EventArgs e)
        {
            FilterExpressionDialog fed = new FilterExpressionDialog(ShowTable);
            fed.FilterExpression = fExp;
            if (fed.ShowDialog() == DialogResult.OK)
            {
                fExp = fed.FilterExpression;
                txtPageNo.Text = "1";
                bindTableData();
            }
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
            tabControl.SelectedTab = tpOutput;
            txtInfo.Text = txt;
        }

        public void Navigate(string url)
        {
            webBrowser.Navigate(url);
            tabControl.SelectedTab = tpInfo;
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
            sfd.Filter = "Query Files|*.sql";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                InitialText = txtSQL.Text;
                txtSQL.SaveFile(filePath);
                return true;
            }
            return false;
        }

        internal void SaveTemp()
        {
            if(txtSQL.Text.IsEmpty())
                return;

            if (tempFilePath.IsEmpty())
                tempFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\tmpsvdfile_"+DateTime.Now.ToString("yyyyMMddHHmmss")+".txt";

            txtSQL.SaveFile(tempFilePath);

        }

        FilterExpression fExp;
        public Table ShowTable
        {
            get;
            set;
        }

        public void ShowTableData(Table table, FilterExpression filter)
        {
            tableDataBound = false;
            this.fExp = filter ?? new FilterExpression();

            this.ShowTable = table;
            tabControl.SelectedTab = tpTableData;
            if (!tableDataBound)
                bindTableData();
        }
        public void ShowTableDataIfTableTabActive(Table table, FilterExpression filter)
        {
            tableDataBound = false;
            this.fExp = filter ?? new FilterExpression();
            if (this.ShowTable != table && tabControl.SelectedTab == tpTableData)
            {
                this.ShowTable = table;
                if (!tableDataBound)
                    bindTableData();
            }
            this.ShowTable = table;
        }

        private void clearPagingSortingMetadata()
        {
            txtPageNo.Text = "1";
            FilterExpression exp = new FilterExpression();
            exp.Criterias = fExp.Criterias;
            fExp = exp;
            btnSave.Text = "";
        }

        bool tableDataBound = false;
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab==tpTableData && ShowTable != null && ShowTable != gridShowTable.Tag)
                bindTableData();
        }

        void gridShowTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string sortColumn = fExp.Orders.Count == 1 ? fExp.Orders[0].ColumnName : "";
            bool sortAsc = fExp.Orders.Count == 1 ? fExp.Orders[0].Ascending : true;
            fExp.Orders = new OrderList();
            string newSortColumn = gridShowTable.Columns[e.ColumnIndex].Name;

            if (newSortColumn == sortColumn)
                sortAsc = !sortAsc;
            else
            {
                sortColumn = newSortColumn;
                sortAsc = true;
            }

            fExp.Orders = new OrderList();
            fExp.Orders.Add(new Order() { Ascending = sortAsc, ColumnName = sortColumn });

            bindTableData();
        }

        private void bindTableData()
        {
            int pageSize = int.Parse(txtPageSize.Text);
            int pageNo = int.Parse(txtPageNo.Text) - 1;

            fExp.PageNo = pageNo;
            fExp.PageSize = pageSize;

            try
            {
                gridShowTable.RowNumberOffset = pageNo * pageSize;
                DataTable dt = Provider.Database.GetDataTableFor(ShowTable.Name, fExp);
                gridShowTable.DataSource = dt;

                for (int i = 0; i < gridShowTable.Columns.Count; i++)
                    gridShowTable.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;

                gridShowTable.Tag = ShowTable;

                if (fExp.Orders.Count > 0)
                    gridShowTable.Sort(gridShowTable.Columns[fExp.Orders[0].ColumnName], fExp.Orders[0].Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

                btnPrevPage.Enabled = pageNo > 0;
                btnNextPage.Enabled = gridShowTable.DataSource is DataTable && (gridShowTable.DataSource as DataTable).Rows.Count == int.Parse(txtPageSize.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + Environment.NewLine + "Try refreshing metadata.", "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(fExp.Orders.Count>0)
                gridShowTable.Sort(gridShowTable.Columns[fExp.Orders[0].ColumnName], fExp.Orders[0].Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

            btnPrevPage.Enabled = pageNo >= 1;
            btnNextPage.Enabled = gridShowTable.DataSource is DataTable && (gridShowTable.DataSource as DataTable).Rows.Count == int.Parse(txtPageSize.Text);

            tableDataBound = true;
        }

        void btnPrevPage_Click(object sender, EventArgs e)
        {
            int pageNo = int.Parse(txtPageNo.Text);
            if (pageNo == 1) return;
            
            pageNo--;

            txtPageNo.Text = pageNo.ToString();
            bindTableData();
        }

        void btnNextPage_Click(object sender, EventArgs e)
        {
            int pageNo = int.Parse(txtPageNo.Text);
            pageNo++;

            txtPageNo.Text = pageNo.ToString();
            bindTableData();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(btnSave.Text))
            {
                try
                {
                    DataTable dt = gridShowTable.DataSource as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        switch (dr.RowState)
                        {
                            case DataRowState.Detached:
                                break;
                            case DataRowState.Unchanged:
                                break;
                            case DataRowState.Added:
                                Provider.Database.Insert(ShowTable.Name, dr);
                                break;
                            case DataRowState.Deleted:
                                break;
                            case DataRowState.Modified:
                                Provider.Database.Update(ShowTable.Name, dr);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    btnSave.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void deleteSelectedRows()
        {
            if (gridShowTable.SelectedRows.Count > 0 && MessageBox.Show("Are you sure to delete " + gridShowTable.SelectedRows.Count + " rows?", "Cinar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                foreach (DataGridViewRow row in gridShowTable.SelectedRows)
                {
                    DataRow dr = ((DataRowView)row.DataBoundItem).Row;
                    Provider.Database.Delete(ShowTable.Name, dr);
                }
                bindTableData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + (ex.InnerException!=null ? "\n" + ex.InnerException.Message : ""), "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string GetName()
        {
            return Path.GetFileName(this.FilePath);
        }


        public void OnClose()
        {
            if (this.FilePath.IsEmpty() && !this.tempFilePath.IsEmpty())
                File.Delete(tempFilePath);
        }


        public string Content
        {
            get { return SQLEditor.Text; }
            set { SQLEditor.Text = value; }
        }
    }
}
