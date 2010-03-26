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

        private void BindSrcFields()
        {
            cbSrcField.Items.Clear();
            foreach (Field fld in (cbSrcTable.SelectedItem as Table).Fields)
                cbSrcField.Items.Add(fld);

            cbSrcStringField.Items.Clear();
            foreach (Field fld in (cbSrcTable.SelectedItem as Table).Fields)
                cbSrcStringField.Items.Add(fld);
        }

        private void BindDstFields()
        {
            cbDstField.Items.Clear();
            foreach (Field fld in (cbDstTable.SelectedItem as Table).Fields)
                cbDstField.Items.Add(fld);

            cbDstStringField.Items.Clear();
            foreach (Field fld in (cbDstTable.SelectedItem as Table).Fields)
                cbDstStringField.Items.Add(fld);
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
            BindSrcFields();
        }

        private void cbSrcTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSrcFields();

            lbTables.Items.Clear();
            foreach (Table tbl in (cbSrcTable.SelectedItem as Table).ReferencedByTables)
                if (!lbTables.Items.Contains(tbl))
                    lbTables.Items.Add(tbl);
        }

        private void cbDstDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDstTables();
            BindDstFields();
        }

        private void cbDstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDstFields();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tvSource.SelectedNode!=null && tvDest.SelectedNode!=null &&
                MessageBox.Show(
                    string.Format("\"{0}\" düğümünün altındaki tüm kayıtlar \"{1}\" düğümünün altına kopyalanacak. Devam etmek istiyor musunuz?", 
                        tvSource.SelectedNode.Text, 
                        tvDest.SelectedNode.Text), 
                    "Çınar Database Tools", 
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
            ht["fldSrc"] = cbSrcField.SelectedItem;
            ht["fldDst"] = cbDstField.SelectedItem;

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
            Field fldSrc = ht["fldSrc"] as Field;
            Field fldDst = ht["fldDst"] as Field;

            backgroundWorker.ReportProgress(100, "Copying tree data");

            copyRecords(dbSrc, tblSrc, fldSrc, idSrc, dbDst, tblDst, fldDst, idDst);

            backgroundWorker.ReportProgress(100, Environment.NewLine + "Transfer completed.");
        }

        private void copyRecords(Cinar.Database.Database dbSrc, Table tblSrc, Field fldSrc, int parentIdSrc, Cinar.Database.Database dbDst, Table tblDst, Field fldDst, int parentIdDst)
        {
            string sql = string.Format("select * from [{0}] where [{1}] = {{0}}", tblSrc.Name, fldSrc.Name);
            DataTable dt = dbSrc.GetDataTable(sql, parentIdSrc);

            if (dt == null || dt.Rows.Count == 0)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                Hashtable ht = dbSrc.DataRowToHashtable(dr);
                ht.Remove(tblSrc.PrimaryField.Name);
                ht[tblDst.PrimaryField.Name] = 0;
                ht.Remove(fldSrc.Name);
                ht[fldDst.Name] = parentIdDst;
                dbDst.Insert(tblDst.Name, ht);

                backgroundWorker.ReportProgress(0, ".");

                int newId = dbDst.GetInt("select max(" + tblDst.PrimaryField.Name + ") from [" + tblDst.Name + "]");

                copyRecords(dbSrc, tblSrc, fldSrc, (int)dr[tblSrc.PrimaryField.Name], dbDst, tblDst, fldDst, newId);
                copyRelatedRecords(dbSrc, tblSrc, (int)dr[tblSrc.PrimaryField.Name], dbDst, tblDst, newId);
            }
        }

        private void copyRelatedRecords(Cinar.Database.Database dbSrc, Table tblSrc, int idSrc, Cinar.Database.Database dbDst, Table tblDst, int idDst)
        {
            foreach (Table tbl in tblSrc.ReferencedByTables)
            {
                Field fld = tbl.FindFieldWhichRefersTo(tblSrc);
                string sql = string.Format("select * from [{0}] where [{1}] = {{0}}", tbl.Name, fld.Name);
                DataTable dt = dbSrc.GetDataTable(sql, idSrc);

                if (dt == null || dt.Rows.Count == 0 || dbDst.Tables[tbl.Name] == null)
                    continue;

                foreach (DataRow dr in dt.Rows)
                {
                    Hashtable ht = dbSrc.DataRowToHashtable(dr);
                    ht.Remove(tbl.PrimaryField.Name);
                    ht[tbl.PrimaryField.Name] = 0;
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
                MessageBox.Show(e.Error.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.Text += e.UserState.ToString();
        }

        private void cbSrcField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSrcField.SelectedItem != null && cbSrcStringField.SelectedItem != null)
            {
                tvSource.Nodes.Clear();
                TreeNode nodeRoot = tvSource.Nodes.Add("Kök");
                nodeRoot.Tag = 1;

                nodeRoot.Nodes.Add("---null---");

                tvSource.CollapseAll();
            }
        }
        private void cbDstField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDstField.SelectedItem != null && cbDstStringField.SelectedItem != null)
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
            string parentIdFieldName = (sender == tvDest) ? (cbDstField.SelectedItem as Field).Name : (cbSrcField.SelectedItem as Field).Name;
            string stringFieldName = (sender == tvDest) ? (cbDstStringField.SelectedItem as Field).Name : (cbSrcStringField.SelectedItem as Field).Name;
            Table table = (sender == tvDest) ? (cbDstTable.SelectedItem as Table) : (cbSrcTable.SelectedItem as Table);

            int parentId = (int)e.Node.Tag;

            DataTable dt = db.GetDataTable("select [" + table.PrimaryField.Name + "], [" + stringFieldName + "], [" + parentIdFieldName + "] from [" + table.Name + "] where [" + parentIdFieldName + "]={0}", parentId);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = e.Node.Nodes.Add(dr[stringFieldName].ToString());
                tn.Tag = dr[table.PrimaryField.Name];
                tn.Nodes.Add("---null---");
                tn.Collapse();
            }

        }
    }
}
