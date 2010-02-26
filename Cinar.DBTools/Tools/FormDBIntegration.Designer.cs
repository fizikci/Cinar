namespace Cinar.DBTools.Tools
{
    partial class FormDBIntegration
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
            this.lbTasks = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.LinkLabel();
            this.btnStop = new System.Windows.Forms.LinkLabel();
            this.btnAddNewTask = new System.Windows.Forms.LinkLabel();
            this.btnDeleteSelectedTask = new System.Windows.Forms.LinkLabel();
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnEditSelectedTask = new System.Windows.Forms.LinkLabel();
            this.btnScriptInclude = new System.Windows.Forms.LinkLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTasks
            // 
            this.lbTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbTasks.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbTasks.FormattingEnabled = true;
            this.lbTasks.IntegralHeight = false;
            this.lbTasks.ItemHeight = 12;
            this.lbTasks.Location = new System.Drawing.Point(6, 23);
            this.lbTasks.Name = "lbTasks";
            this.lbTasks.Size = new System.Drawing.Size(560, 127);
            this.lbTasks.TabIndex = 0;
            this.lbTasks.DoubleClick += new System.EventHandler(this.lbTasks_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Integration Tasks :";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLog.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.IntegralHeight = false;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(5, 24);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(562, 377);
            this.lbLog.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Integration Log :";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.AutoSize = true;
            this.btnStart.Location = new System.Drawing.Point(504, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(29, 13);
            this.btnStart.TabIndex = 6;
            this.btnStart.TabStop = true;
            this.btnStart.Text = "Start";
            this.btnStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnStart_LinkClicked);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.AutoSize = true;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(538, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(29, 13);
            this.btnStop.TabIndex = 7;
            this.btnStop.TabStop = true;
            this.btnStop.Text = "Stop";
            this.btnStop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnStop_LinkClicked);
            // 
            // btnAddNewTask
            // 
            this.btnAddNewTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewTask.AutoSize = true;
            this.btnAddNewTask.Location = new System.Drawing.Point(270, 7);
            this.btnAddNewTask.Name = "btnAddNewTask";
            this.btnAddNewTask.Size = new System.Drawing.Size(78, 13);
            this.btnAddNewTask.TabIndex = 8;
            this.btnAddNewTask.TabStop = true;
            this.btnAddNewTask.Text = "Add New Task";
            this.btnAddNewTask.Click += new System.EventHandler(this.btnAddNewTask_Click);
            // 
            // btnDeleteSelectedTask
            // 
            this.btnDeleteSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelectedTask.AutoSize = true;
            this.btnDeleteSelectedTask.Location = new System.Drawing.Point(456, 7);
            this.btnDeleteSelectedTask.Name = "btnDeleteSelectedTask";
            this.btnDeleteSelectedTask.Size = new System.Drawing.Size(110, 13);
            this.btnDeleteSelectedTask.TabIndex = 9;
            this.btnDeleteSelectedTask.TabStop = true;
            this.btnDeleteSelectedTask.Text = "Delete Selected Task";
            this.btnDeleteSelectedTask.Click += new System.EventHandler(this.btnDeleteSelectedTask_Click);
            // 
            // cbCategories
            // 
            this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(151, 10);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(155, 21);
            this.cbCategories.TabIndex = 42;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Select Integration Project :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(11, 48);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnEditSelectedTask);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbTasks);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddNewTask);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteSelectedTask);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lbLog);
            this.splitContainer1.Panel2.Controls.Add(this.btnStart);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Size = new System.Drawing.Size(577, 574);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 43;
            // 
            // btnEditSelectedTask
            // 
            this.btnEditSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSelectedTask.AutoSize = true;
            this.btnEditSelectedTask.Location = new System.Drawing.Point(352, 7);
            this.btnEditSelectedTask.Name = "btnEditSelectedTask";
            this.btnEditSelectedTask.Size = new System.Drawing.Size(97, 13);
            this.btnEditSelectedTask.TabIndex = 10;
            this.btnEditSelectedTask.TabStop = true;
            this.btnEditSelectedTask.Text = "Edit Selected Task";
            this.btnEditSelectedTask.Click += new System.EventHandler(this.btnEditSelectedTask_Click);
            // 
            // btnScriptInclude
            // 
            this.btnScriptInclude.AutoSize = true;
            this.btnScriptInclude.Location = new System.Drawing.Point(515, 17);
            this.btnScriptInclude.Name = "btnScriptInclude";
            this.btnScriptInclude.Size = new System.Drawing.Size(72, 13);
            this.btnScriptInclude.TabIndex = 44;
            this.btnScriptInclude.TabStop = true;
            this.btnScriptInclude.Text = "Script Include";
            this.btnScriptInclude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnScriptInclude_LinkClicked);
            // 
            // FormDBIntegration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 634);
            this.Controls.Add(this.btnScriptInclude);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.label2);
            this.Name = "FormDBIntegration";
            this.Text = "Çınar Simple Database Integration";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbTasks;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel btnStart;
        private System.Windows.Forms.LinkLabel btnStop;
        private System.Windows.Forms.LinkLabel btnAddNewTask;
        private System.Windows.Forms.LinkLabel btnDeleteSelectedTask;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel btnEditSelectedTask;
        private System.Windows.Forms.LinkLabel btnScriptInclude;
    }
}