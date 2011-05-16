using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using System.Collections;

namespace Cinar.DBTools.Tools
{
    public partial class FormCopyTreeData : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormCopyTreeData()
        {
            InitializeComponent();

            BindDatabases();
        }

        private void BindDatabases()
        {
            cbDstDb.Items.Clear();
            cbSrcDb.Items.Clear();
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                cbDstDb.Items.Add(cs);
                cbSrcDb.Items.Add(cs);
            }
            cbDstDb.SelectedIndex = 0;
            cbSrcDb.SelectedIndex = 0;
        }
        private void BindSrcColumns()
        {
            cbSrcColumn.Items.Clear();
            foreach (Column column in (cbSrcTable.SelectedItem as Table).Columns)
                cbSrcColumn.Items.Add(column);

            cbSrcStringColumn.Items.Clear();
            foreach (Column column in (cbSrcTable.SelectedItem as Table).Columns)
                cbSrcStringColumn.Items.Add(column);
        }
        private void BindDstColumns()
        {
            cbDstColumn.Items.Clear();
            foreach (Column fld in (cbDstTable.SelectedItem as Table).Columns)
                cbDstColumn.Items.Add(fld);

            cbDstStringColumn.Items.Clear();
            foreach (Column fld in (cbDstTable.SelectedItem as Table).Columns)
                cbDstStringColumn.Items.Add(fld);
        }
        private void BindSrcTables()
        {
            cbSrcTable.Items.Clear();
            foreach (Table tbl in ((ConnectionSettings)cbSrcDb.SelectedItem).Database.Tables)
                cbSrcTable.Items.Add(tbl);
            cbSrcTable.SelectedIndex = 0;
        }
        private void BindDstTables()
        {
            cbDstTable.Items.Clear();
            foreach (Table tbl in ((ConnectionSettings)cbDstDb.SelectedItem).Database.Tables)
                cbDstTable.Items.Add(tbl);
            cbDstTable.SelectedIndex = 0;
        }

        private void cbSrcDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSrcTables();
            BindSrcColumns();
        }
        private void cbSrcTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSrcColumns();

            lbTables.Items.Clear();
            foreach (Table tbl in (cbSrcTable.SelectedItem as Table).ReferencedByTables)
                if (!lbTables.Items.Contains(tbl))
                    lbTables.Items.Add(tbl);
        }
        private void cbDstDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDstTables();
            BindDstColumns();
        }
        private void cbDstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDstColumns();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tvSource.SelectedNode!=null && tvDest.SelectedNode!=null &&
                MessageBox.Show(
                    string.Format("\"{0}\" düğümünün altındaki tüm kayıtlar \"{1}\" düğümünün altına kopyalanacak. Devam etmek istiyor musunuz?", 
                        tvSource.SelectedNode.Text, 
                        tvDest.SelectedNode.Text), 
                    "Cinar Database Tools", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Information) 
                        == DialogResult.No)
                return;

            btnStart.Enabled = false;

            Hashtable ht = new Hashtable();
            ht["dbSrc"] = (cbSrcDb.SelectedItem as ConnectionSettings).Database;
            ht["dbDst"] = (cbDstDb.SelectedItem as ConnectionSettings).Database;
            ht["idSrc"] = tvSource.SelectedNode.Tag;
            ht["idDst"] = tvDest.SelectedNode.Tag;
            ht["tblSrc"] = cbSrcTable.SelectedItem;
            ht["tblDst"] = cbDstTable.SelectedItem;
            ht["fldSrc"] = cbSrcColumn.SelectedItem;
            ht["fldDst"] = cbDstColumn.SelectedItem;

            backgroundWorker.RunWorkerAsync(ht);
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Hashtable ht = (Hashtable)e.Argument;
            Database.Database dbSrc = ht["dbSrc"] as Database.Database;
            Database.Database dbDst = ht["dbDst"] as Database.Database;
            int idSrc = (int)ht["idSrc"];
            int idDst = (int)ht["idDst"];
            Table tblSrc = ht["tblSrc"] as Table;
            Table tblDst = ht["tblDst"] as Table;
            Column fldSrc = ht["fldSrc"] as Column;
            Column fldDst = ht["fldDst"] as Column;

            backgroundWorker.ReportProgress(100, "Copying tree data");

            copyRecords(dbSrc, tblSrc, fldSrc, idSrc, dbDst, tblDst, fldDst, idDst);

            backgroundWorker.ReportProgress(100, Environment.NewLine + "Transfer completed.");
        }

        private void copyRecords(Cinar.Database.Database dbSrc, Table tblSrc, Column fldSrc, int parentIdSrc, Cinar.Database.Database dbDst, Table tblDst, Column fldDst, int parentIdDst)
        {
            string sql = string.Format("select * from [{0}] where [{1}] = {{0}}", tblSrc.Name, fldSrc.Name);
            DataTable dt = dbSrc.GetDataTable(sql, parentIdSrc);

            if (dt == null || dt.Rows.Count == 0)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                Hashtable ht = dbSrc.DataRowToHashtable(dr);
                ht.Remove(tblSrc.PrimaryColumn.Name);
                ht[tblDst.PrimaryColumn.Name] = 0;
                ht.Remove(fldSrc.Name);
                ht[fldDst.Name] = parentIdDst;
                dbDst.Insert(tblDst.Name, ht);

                backgroundWorker.ReportProgress(0, ".");

                int newId = dbDst.GetInt("select max(" + tblDst.PrimaryColumn.Name + ") from [" + tblDst.Name + "]");

                copyRecords(dbSrc, tblSrc, fldSrc, (int)dr[tblSrc.PrimaryColumn.Name], dbDst, tblDst, fldDst, newId);
                copyRelatedRecords(dbSrc, tblSrc, (int)dr[tblSrc.PrimaryColumn.Name], dbDst, tblDst, newId);
            }
        }

        private void copyRelatedRecords(Cinar.Database.Database dbSrc, Table tblSrc, int idSrc, Cinar.Database.Database dbDst, Table tblDst, int idDst)
        {
            foreach (Table tbl in tblSrc.ReferencedByTables)
            {
                Column fld = tbl.FindColumnWhichRefersTo(tblSrc);
                string sql = string.Format("select * from [{0}] where [{1}] = {{0}}", tbl.Name, fld.Name);
                DataTable dt = dbSrc.GetDataTable(sql, idSrc);

                if (dt == null || dt.Rows.Count == 0 || dbDst.Tables[tbl.Name] == null)
                    continue;

                foreach (DataRow dr in dt.Rows)
                {
                    Hashtable ht = dbSrc.DataRowToHashtable(dr);
                    ht.Remove(tbl.PrimaryColumn.Name);
                    ht[tbl.PrimaryColumn.Name] = 0;
                    ht.Remove(fld.Name);
                    ht[fld.Name] = idDst;
                    dbDst.Insert(tbl.Name, ht);
                }

                backgroundWorker.ReportProgress(100, Environment.NewLine + string.Format("{0} records copied from {1}.", dt.Rows.Count, tbl.Name));
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;

            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.Text += e.UserState.ToString();
        }

        private void cbSrcColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSrcColumn.SelectedItem != null && cbSrcStringColumn.SelectedItem != null)
            {
                tvSource.Nodes.Clear();
                TreeNode nodeRoot = tvSource.Nodes.Add("Kök");
                nodeRoot.Tag = 1;

                nodeRoot.Nodes.Add("---null---");

                tvSource.CollapseAll();
            }
        }
        private void cbDstColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDstColumn.SelectedItem != null && cbDstStringColumn.SelectedItem != null)
            {
                tvDest.Nodes.Clear();
                TreeNode nodeRoot2 = tvDest.Nodes.Add("Kök");
                nodeRoot2.Tag = 1;

                nodeRoot2.Nodes.Add("---null---");

                tvDest.CollapseAll();
            }
        }

        private void tvDest_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text != "---null---")
                return;

            e.Node.Nodes.Clear();

            Database.Database db = (sender == tvDest) ? (cbDstDb.SelectedItem as ConnectionSettings).Database : (cbSrcDb.SelectedItem as ConnectionSettings).Database;
            string parentIdColumnName = (sender == tvDest) ? (cbDstColumn.SelectedItem as Column).Name : (cbSrcColumn.SelectedItem as Column).Name;
            string stringColumnName = (sender == tvDest) ? (cbDstStringColumn.SelectedItem as Column).Name : (cbSrcStringColumn.SelectedItem as Column).Name;
            Table table = (sender == tvDest) ? (cbDstTable.SelectedItem as Table) : (cbSrcTable.SelectedItem as Table);

            int parentId = (int)e.Node.Tag;

            DataTable dt = db.GetDataTable("select [" + table.PrimaryColumn.Name + "], [" + stringColumnName + "], [" + parentIdColumnName + "] from [" + table.Name + "] where [" + parentIdColumnName + "]={0}", parentId);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = e.Node.Nodes.Add(dr[stringColumnName].ToString());
                tn.Tag = dr[table.PrimaryColumn.Name];
                tn.Nodes.Add("---null---");
                tn.Collapse();
            }

        }
    }
}
