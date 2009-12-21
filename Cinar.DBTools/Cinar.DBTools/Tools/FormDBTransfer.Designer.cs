namespace Cinar.DBTools.Tools
{
    partial class FormDBTransfer
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
            this.cbDbSrc = new System.Windows.Forms.ComboBox();
            this.cbDbDest = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editPageSize = new System.Windows.Forms.NumericUpDown();
            this.editLimit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.editPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLimit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source DB";
            // 
            // cbDbSrc
            // 
            this.cbDbSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbSrc.FormattingEnabled = true;
            this.cbDbSrc.Location = new System.Drawing.Point(17, 42);
            this.cbDbSrc.Name = "cbDbSrc";
            this.cbDbSrc.Size = new System.Drawing.Size(140, 21);
            this.cbDbSrc.TabIndex = 1;
            this.cbDbSrc.SelectedIndexChanged += new System.EventHandler(this.cbDbSrc_SelectedIndexChanged);
            // 
            // cbDbDest
            // 
            this.cbDbDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbDest.FormattingEnabled = true;
            this.cbDbDest.Location = new System.Drawing.Point(17, 93);
            this.cbDbDest.Name = "cbDbDest";
            this.cbDbDest.Size = new System.Drawing.Size(140, 21);
            this.cbDbDest.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination DB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Page Size";
            // 
            // editPageSize
            // 
            this.editPageSize.Location = new System.Drawing.Point(171, 43);
            this.editPageSize.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.editPageSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.editPageSize.Name = "editPageSize";
            this.editPageSize.Size = new System.Drawing.Size(92, 20);
            this.editPageSize.TabIndex = 5;
            this.editPageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // editLimit
            // 
            this.editLimit.Location = new System.Drawing.Point(171, 93);
            this.editLimit.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.editLimit.Name = "editLimit";
            this.editLimit.Size = new System.Drawing.Size(92, 20);
            this.editLimit.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Row Transfer Limit";
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.Location = new System.Drawing.Point(17, 186);
            this.lbTables.Name = "lbTables";
            this.lbTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTables.Size = new System.Drawing.Size(246, 264);
            this.lbTables.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Select tables to transfer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrefix);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.editLimit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.editPageSize);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbDbSrc);
            this.groupBox1.Controls.Add(this.lbTables);
            this.groupBox1.Controls.Add(this.cbDbDest);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 498);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer Options";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(171, 144);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(91, 20);
            this.txtPrefix.TabIndex = 15;
            this.txtPrefix.Text = "new_";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Table Name Prefix";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(76, 461);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 23);
            this.btnStart.TabIndex = 13;
            this.btnStart.Text = "Start Transfer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Location = new System.Drawing.Point(308, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 498);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transfer Log";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(204, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel Transfer";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLog.Location = new System.Drawing.Point(14, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(500, 431);
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
            // FormDBTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 519);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormDBTransfer";
            this.Text = "Database Transfer";
            ((System.ComponentModel.ISupportInitialize)(this.editPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLimit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDbSrc;
        private System.Windows.Forms.ComboBox cbDbDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown editPageSize;
        private System.Windows.Forms.NumericUpDown editLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnCancel;
    }
}