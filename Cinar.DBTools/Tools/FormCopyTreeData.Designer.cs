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
            this.cbSrcField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSrcStringField = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDstDb = new System.Windows.Forms.ComboBox();
            this.cbDstTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDstField = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDstStringField = new System.Windows.Forms.ComboBox();
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
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Database";
            // 
            // cbSrcDb
            // 
            this.cbSrcDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcDb.FormattingEnabled = true;
            this.cbSrcDb.Location = new System.Drawing.Point(108, 18);
            this.cbSrcDb.Name = "cbSrcDb";
            this.cbSrcDb.Size = new System.Drawing.Size(140, 21);
            this.cbSrcDb.TabIndex = 22;
            this.cbSrcDb.SelectedIndexChanged += new System.EventHandler(this.cbSrcDb_SelectedIndexChanged);
            // 
            // cbSrcTable
            // 
            this.cbSrcTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcTable.FormattingEnabled = true;
            this.cbSrcTable.Location = new System.Drawing.Point(108, 45);
            this.cbSrcTable.Name = "cbSrcTable";
            this.cbSrcTable.Size = new System.Drawing.Size(140, 21);
            this.cbSrcTable.TabIndex = 26;
            this.cbSrcTable.SelectedIndexChanged += new System.EventHandler(this.cbSrcTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Table";
            // 
            // cbSrcField
            // 
            this.cbSrcField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcField.FormattingEnabled = true;
            this.cbSrcField.Location = new System.Drawing.Point(108, 72);
            this.cbSrcField.Name = "cbSrcField";
            this.cbSrcField.Size = new System.Drawing.Size(140, 21);
            this.cbSrcField.TabIndex = 28;
            this.cbSrcField.SelectedIndexChanged += new System.EventHandler(this.cbSrcField_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "ParentId Field";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(85, 562);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 23);
            this.btnStart.TabIndex = 31;
            this.btnStart.Text = "Start Transfer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Select foreign tables to transfer";
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.Location = new System.Drawing.Point(15, 315);
            this.lbTables.Name = "lbTables";
            this.lbTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTables.Size = new System.Drawing.Size(268, 238);
            this.lbTables.TabIndex = 29;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Location = new System.Drawing.Point(302, 298);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(529, 285);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transfer Log";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLog.Location = new System.Drawing.Point(14, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(500, 250);
            this.txtLog.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSrcStringField);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbSrcField);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbSrcTable);
            this.groupBox1.Controls.Add(this.cbSrcDb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 133);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // cbSrcStringField
            // 
            this.cbSrcStringField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrcStringField.FormattingEnabled = true;
            this.cbSrcStringField.Location = new System.Drawing.Point(108, 100);
            this.cbSrcStringField.Name = "cbSrcStringField";
            this.cbSrcStringField.Size = new System.Drawing.Size(140, 21);
            this.cbSrcStringField.TabIndex = 30;
            this.cbSrcStringField.SelectedIndexChanged += new System.EventHandler(this.cbSrcField_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "String Field";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Table";
            // 
            // cbDstDb
            // 
            this.cbDstDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstDb.FormattingEnabled = true;
            this.cbDstDb.Location = new System.Drawing.Point(108, 22);
            this.cbDstDb.Name = "cbDstDb";
            this.cbDstDb.Size = new System.Drawing.Size(140, 21);
            this.cbDstDb.TabIndex = 22;
            this.cbDstDb.SelectedIndexChanged += new System.EventHandler(this.cbDstDb_SelectedIndexChanged);
            // 
            // cbDstTable
            // 
            this.cbDstTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstTable.FormattingEnabled = true;
            this.cbDstTable.Location = new System.Drawing.Point(108, 49);
            this.cbDstTable.Name = "cbDstTable";
            this.cbDstTable.Size = new System.Drawing.Size(140, 21);
            this.cbDstTable.TabIndex = 26;
            this.cbDstTable.SelectedIndexChanged += new System.EventHandler(this.cbDstTable_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "ParentId Field";
            // 
            // cbDstField
            // 
            this.cbDstField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstField.FormattingEnabled = true;
            this.cbDstField.Location = new System.Drawing.Point(108, 76);
            this.cbDstField.Name = "cbDstField";
            this.cbDstField.Size = new System.Drawing.Size(140, 21);
            this.cbDstField.TabIndex = 28;
            this.cbDstField.SelectedIndexChanged += new System.EventHandler(this.cbDstField_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDstStringField);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbDstField);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbDstTable);
            this.groupBox2.Controls.Add(this.cbDstDb);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 132);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // cbDstStringField
            // 
            this.cbDstStringField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDstStringField.FormattingEnabled = true;
            this.cbDstStringField.Location = new System.Drawing.Point(108, 102);
            this.cbDstStringField.Name = "cbDstStringField";
            this.cbDstStringField.Size = new System.Drawing.Size(140, 21);
            this.cbDstStringField.TabIndex = 32;
            this.cbDstStringField.SelectedIndexChanged += new System.EventHandler(this.cbDstField_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "String Field";
            // 
            // tvSource
            // 
            this.tvSource.HideSelection = false;
            this.tvSource.Location = new System.Drawing.Point(316, 30);
            this.tvSource.Name = "tvSource";
            this.tvSource.Size = new System.Drawing.Size(246, 253);
            this.tvSource.TabIndex = 33;
            this.tvSource.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDest_BeforeExpand);
            // 
            // tvDest
            // 
            this.tvDest.HideSelection = false;
            this.tvDest.Location = new System.Drawing.Point(584, 30);
            this.tvDest.Name = "tvDest";
            this.tvDest.Size = new System.Drawing.Size(246, 253);
            this.tvDest.TabIndex = 34;
            this.tvDest.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDest_BeforeExpand);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(316, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Select source node to transfer";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(581, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Select destination node to transfer";
            // 
            // FormCopyTreeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 595);
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
        private System.Windows.Forms.ComboBox cbSrcField;
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
        private System.Windows.Forms.ComboBox cbDstField;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbSrcStringField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbDstStringField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TreeView tvSource;
        private System.Windows.Forms.TreeView tvDest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}