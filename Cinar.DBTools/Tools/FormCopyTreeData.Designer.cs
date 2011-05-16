namespace Cinar.DBTools.Tools
{
    partial class FormCopyTreeData
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbSrcDb = new System.Windows.Forms.ComboBox();
            this.cbSrcTable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSrcColumn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSrcStringColumn = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDstDb = new System.Windows.Forms.ComboBox();
            this.cbDstTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDstColumn = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDstStringColumn = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tvSource = new System.Windows.Forms.TreeView();
            this.tvDest = new System.Windows.Forms.TreeView();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database";
            // 
            // cbSrcDb
            // 
            this.cbSrcDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcDb.FormattingEnabled = true;
            this.cbSrcDb.Location = new System.Drawing.Point(126, 21);
            this.cbSrcDb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSrcDb.Name = "cbSrcDb";
            this.cbSrcDb.Size = new System.Drawing.Size(163, 23);
            this.cbSrcDb.TabIndex = 1;
            this.cbSrcDb.SelectedIndexChanged += new System.EventHandler(this.cbSrcDb_SelectedIndexChanged);
            // 
            // cbSrcTable
            // 
            this.cbSrcTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcTable.FormattingEnabled = true;
            this.cbSrcTable.Location = new System.Drawing.Point(126, 52);
            this.cbSrcTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSrcTable.Name = "cbSrcTable";
            this.cbSrcTable.Size = new System.Drawing.Size(163, 23);
            this.cbSrcTable.TabIndex = 3;
            this.cbSrcTable.SelectedIndexChanged += new System.EventHandler(this.cbSrcTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Table";
            // 
            // cbSrcColumn
            // 
            this.cbSrcColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcColumn.FormattingEnabled = true;
            this.cbSrcColumn.Location = new System.Drawing.Point(126, 83);
            this.cbSrcColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSrcColumn.Name = "cbSrcColumn";
            this.cbSrcColumn.Size = new System.Drawing.Size(163, 23);
            this.cbSrcColumn.TabIndex = 5;
            this.cbSrcColumn.SelectedIndexChanged += new System.EventHandler(this.cbSrcColumn_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "ParentId Column";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(99, 649);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(147, 26);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Transfer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 344);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Select foreign tables to transfer";
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.ItemHeight = 15;
            this.lbTables.Location = new System.Drawing.Point(17, 364);
            this.lbTables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbTables.Name = "lbTables";
            this.lbTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTables.Size = new System.Drawing.Size(312, 274);
            this.lbTables.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Location = new System.Drawing.Point(352, 344);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(617, 329);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transfer Log";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLog.Location = new System.Drawing.Point(16, 22);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(583, 288);
            this.txtLog.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSrcStringColumn);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbSrcColumn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbSrcTable);
            this.groupBox1.Controls.Add(this.cbSrcDb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(316, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // cbSrcStringColumn
            // 
            this.cbSrcStringColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcStringColumn.FormattingEnabled = true;
            this.cbSrcStringColumn.Location = new System.Drawing.Point(126, 116);
            this.cbSrcStringColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSrcStringColumn.Name = "cbSrcStringColumn";
            this.cbSrcStringColumn.Size = new System.Drawing.Size(163, 23);
            this.cbSrcStringColumn.TabIndex = 7;
            this.cbSrcStringColumn.SelectedIndexChanged += new System.EventHandler(this.cbSrcColumn_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "String Column";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Table";
            // 
            // cbDstDb
            // 
            this.cbDstDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstDb.FormattingEnabled = true;
            this.cbDstDb.Location = new System.Drawing.Point(126, 26);
            this.cbDstDb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDstDb.Name = "cbDstDb";
            this.cbDstDb.Size = new System.Drawing.Size(163, 23);
            this.cbDstDb.TabIndex = 1;
            this.cbDstDb.SelectedIndexChanged += new System.EventHandler(this.cbDstDb_SelectedIndexChanged);
            // 
            // cbDstTable
            // 
            this.cbDstTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstTable.FormattingEnabled = true;
            this.cbDstTable.Location = new System.Drawing.Point(126, 56);
            this.cbDstTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDstTable.Name = "cbDstTable";
            this.cbDstTable.Size = new System.Drawing.Size(163, 23);
            this.cbDstTable.TabIndex = 3;
            this.cbDstTable.SelectedIndexChanged += new System.EventHandler(this.cbDstTable_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ParentId Column";
            // 
            // cbDstColumn
            // 
            this.cbDstColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstColumn.FormattingEnabled = true;
            this.cbDstColumn.Location = new System.Drawing.Point(126, 87);
            this.cbDstColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDstColumn.Name = "cbDstColumn";
            this.cbDstColumn.Size = new System.Drawing.Size(163, 23);
            this.cbDstColumn.TabIndex = 5;
            this.cbDstColumn.SelectedIndexChanged += new System.EventHandler(this.cbDstColumn_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDstStringColumn);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbDstColumn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbDstTable);
            this.groupBox2.Controls.Add(this.cbDstDb);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(14, 174);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(316, 153);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // cbDstStringColumn
            // 
            this.cbDstStringColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstStringColumn.FormattingEnabled = true;
            this.cbDstStringColumn.Location = new System.Drawing.Point(126, 117);
            this.cbDstStringColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDstStringColumn.Name = "cbDstStringColumn";
            this.cbDstStringColumn.Size = new System.Drawing.Size(163, 23);
            this.cbDstStringColumn.TabIndex = 7;
            this.cbDstStringColumn.SelectedIndexChanged += new System.EventHandler(this.cbDstColumn_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "String Column";
            // 
            // tvSource
            // 
            this.tvSource.HideSelection = false;
            this.tvSource.Location = new System.Drawing.Point(369, 34);
            this.tvSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvSource.Name = "tvSource";
            this.tvSource.Size = new System.Drawing.Size(286, 292);
            this.tvSource.TabIndex = 5;
            this.tvSource.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDest_BeforeExpand);
            // 
            // tvDest
            // 
            this.tvDest.HideSelection = false;
            this.tvDest.Location = new System.Drawing.Point(681, 34);
            this.tvDest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvDest.Name = "tvDest";
            this.tvDest.Size = new System.Drawing.Size(286, 292);
            this.tvDest.TabIndex = 7;
            this.tvDest.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDest_BeforeExpand);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(369, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "Select source node to transfer";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(678, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "Select destination node to transfer";
            // 
            // FormCopyTreeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 686);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tvDest);
            this.Controls.Add(this.tvSource);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbTables);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCopyTreeData";
            this.Text = "Copy Tree Data";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSrcDb;
        private System.Windows.Forms.ComboBox cbSrcTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSrcColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDstDb;
        private System.Windows.Forms.ComboBox cbDstTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDstColumn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbSrcStringColumn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbDstStringColumn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TreeView tvSource;
        private System.Windows.Forms.TreeView tvDest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}