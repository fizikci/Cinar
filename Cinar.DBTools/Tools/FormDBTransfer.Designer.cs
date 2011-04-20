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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDBTransfer));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupTransferData = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.editPageSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editLimit = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDbSrc = new System.Windows.Forms.ComboBox();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.cbDbDest = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTransferData = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupTransferData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Location = new System.Drawing.Point(359, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(617, 562);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transfer Log";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(236, 527);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel Transfer";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLog.Location = new System.Drawing.Point(16, 22);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(583, 494);
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
            // groupTransferData
            // 
            this.groupTransferData.Controls.Add(this.label3);
            this.groupTransferData.Controls.Add(this.txtPrefix);
            this.groupTransferData.Controls.Add(this.editPageSize);
            this.groupTransferData.Controls.Add(this.label6);
            this.groupTransferData.Controls.Add(this.label4);
            this.groupTransferData.Controls.Add(this.editLimit);
            this.groupTransferData.Enabled = false;
            this.groupTransferData.Location = new System.Drawing.Point(8, 90);
            this.groupTransferData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupTransferData.Name = "groupTransferData";
            this.groupTransferData.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupTransferData.Size = new System.Drawing.Size(313, 79);
            this.groupTransferData.TabIndex = 5;
            this.groupTransferData.TabStop = false;
            this.groupTransferData.Text = "     ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Page Size";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(233, 45);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(66, 23);
            this.txtPrefix.TabIndex = 5;
            this.txtPrefix.Text = "new_";
            // 
            // editPageSize
            // 
            this.editPageSize.Location = new System.Drawing.Point(13, 45);
            this.editPageSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.editPageSize.Size = new System.Drawing.Size(87, 23);
            this.editPageSize.TabIndex = 1;
            this.editPageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Table Prefix";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Row Transfer Limit";
            // 
            // editLimit
            // 
            this.editLimit.Location = new System.Drawing.Point(111, 45);
            this.editLimit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editLimit.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.editLimit.Name = "editLimit";
            this.editLimit.Size = new System.Drawing.Size(107, 23);
            this.editLimit.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(90, 541);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(147, 26);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Transfer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source DB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Select tables to transfer";
            // 
            // cbDbSrc
            // 
            this.cbDbSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbSrc.FormattingEnabled = true;
            this.cbDbSrc.Location = new System.Drawing.Point(119, 12);
            this.cbDbSrc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDbSrc.Name = "cbDbSrc";
            this.cbDbSrc.Size = new System.Drawing.Size(201, 23);
            this.cbDbSrc.TabIndex = 1;
            this.cbDbSrc.SelectedIndexChanged += new System.EventHandler(this.cbDbSrc_SelectedIndexChanged);
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.ItemHeight = 15;
            this.lbTables.Location = new System.Drawing.Point(8, 196);
            this.lbTables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbTables.Name = "lbTables";
            this.lbTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTables.Size = new System.Drawing.Size(312, 334);
            this.lbTables.TabIndex = 7;
            // 
            // cbDbDest
            // 
            this.cbDbDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbDest.FormattingEnabled = true;
            this.cbDbDest.Location = new System.Drawing.Point(119, 49);
            this.cbDbDest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDbDest.Name = "cbDbDest";
            this.cbDbDest.Size = new System.Drawing.Size(201, 23);
            this.cbDbDest.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination DB";
            // 
            // cbTransferData
            // 
            this.cbTransferData.AutoSize = true;
            this.cbTransferData.Location = new System.Drawing.Point(24, 87);
            this.cbTransferData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbTransferData.Name = "cbTransferData";
            this.cbTransferData.Size = new System.Drawing.Size(96, 19);
            this.cbTransferData.TabIndex = 4;
            this.cbTransferData.Text = "Transfer Data";
            this.cbTransferData.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbTransferData.UseVisualStyleBackColor = true;
            this.cbTransferData.CheckedChanged += new System.EventHandler(this.cbTransferData_CheckedChanged);
            // 
            // FormDBTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 589);
            this.Controls.Add(this.cbTransferData);
            this.Controls.Add(this.groupTransferData);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDbSrc);
            this.Controls.Add(this.lbTables);
            this.Controls.Add(this.cbDbDest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDBTransfer";
            this.Text = "Database Transfer";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupTransferData.ResumeLayout(false);
            this.groupTransferData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupTransferData;
        private System.Windows.Forms.CheckBox cbTransferData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.NumericUpDown editPageSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown editLimit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDbSrc;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.ComboBox cbDbDest;
        private System.Windows.Forms.Label label2;
    }
}