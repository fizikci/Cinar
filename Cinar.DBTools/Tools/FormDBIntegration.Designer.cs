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
            this.btnToggleSelectedTask = new System.Windows.Forms.LinkLabel();
            this.btnEditSelectedTask = new System.Windows.Forms.LinkLabel();
            this.btnShowLog = new System.Windows.Forms.LinkLabel();
            this.btnScriptInclude = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            this.lbTasks.Location = new System.Drawing.Point(7, 26);
            this.lbTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbTasks.Name = "lbTasks";
            this.lbTasks.Size = new System.Drawing.Size(639, 95);
            this.lbTasks.TabIndex = 5;
            this.lbTasks.DoubleClick += new System.EventHandler(this.lbTasks_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 0;
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
            this.lbLog.Location = new System.Drawing.Point(6, 27);
            this.lbLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(641, 291);
            this.lbLog.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Integration Log :";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.AutoSize = true;
            this.btnStart.Location = new System.Drawing.Point(574, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(31, 15);
            this.btnStart.TabIndex = 1;
            this.btnStart.TabStop = true;
            this.btnStart.Text = "Start";
            this.btnStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnStart_LinkClicked);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.AutoSize = true;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(613, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(31, 15);
            this.btnStop.TabIndex = 2;
            this.btnStop.TabStop = true;
            this.btnStop.Text = "Stop";
            this.btnStop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnStop_LinkClicked);
            // 
            // btnAddNewTask
            // 
            this.btnAddNewTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewTask.AutoSize = true;
            this.btnAddNewTask.Location = new System.Drawing.Point(156, 8);
            this.btnAddNewTask.Name = "btnAddNewTask";
            this.btnAddNewTask.Size = new System.Drawing.Size(83, 15);
            this.btnAddNewTask.TabIndex = 1;
            this.btnAddNewTask.TabStop = true;
            this.btnAddNewTask.Text = "Add New Task";
            this.btnAddNewTask.Click += new System.EventHandler(this.btnAddNewTask_Click);
            // 
            // btnDeleteSelectedTask
            // 
            this.btnDeleteSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelectedTask.AutoSize = true;
            this.btnDeleteSelectedTask.Location = new System.Drawing.Point(518, 8);
            this.btnDeleteSelectedTask.Name = "btnDeleteSelectedTask";
            this.btnDeleteSelectedTask.Size = new System.Drawing.Size(114, 15);
            this.btnDeleteSelectedTask.TabIndex = 4;
            this.btnDeleteSelectedTask.TabStop = true;
            this.btnDeleteSelectedTask.Text = "Delete Selected Task";
            this.btnDeleteSelectedTask.Click += new System.EventHandler(this.btnDeleteSelectedTask_Click);
            // 
            // cbCategories
            // 
            this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(176, 11);
            this.cbCategories.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(180, 23);
            this.cbCategories.TabIndex = 1;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Integration Project :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(13, 56);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnToggleSelectedTask);
            this.splitContainer1.Panel1.Controls.Add(this.btnEditSelectedTask);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbTasks);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddNewTask);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteSelectedTask);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnShowLog);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lbLog);
            this.splitContainer1.Panel2.Controls.Add(this.btnStart);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Size = new System.Drawing.Size(658, 481);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnToggleSelectedTask
            // 
            this.btnToggleSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleSelectedTask.AutoSize = true;
            this.btnToggleSelectedTask.Location = new System.Drawing.Point(382, 8);
            this.btnToggleSelectedTask.Name = "btnToggleSelectedTask";
            this.btnToggleSelectedTask.Size = new System.Drawing.Size(118, 15);
            this.btnToggleSelectedTask.TabIndex = 3;
            this.btnToggleSelectedTask.TabStop = true;
            this.btnToggleSelectedTask.Text = "Toggle Selected Task";
            this.btnToggleSelectedTask.Click += new System.EventHandler(this.btnToggleSelectedTask_Click);
            // 
            // btnEditSelectedTask
            // 
            this.btnEditSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSelectedTask.AutoSize = true;
            this.btnEditSelectedTask.Location = new System.Drawing.Point(261, 8);
            this.btnEditSelectedTask.Name = "btnEditSelectedTask";
            this.btnEditSelectedTask.Size = new System.Drawing.Size(101, 15);
            this.btnEditSelectedTask.TabIndex = 2;
            this.btnEditSelectedTask.TabStop = true;
            this.btnEditSelectedTask.Text = "Edit Selected Task";
            this.btnEditSelectedTask.Click += new System.EventHandler(this.btnEditSelectedTask_Click);
            // 
            // btnShowLog
            // 
            this.btnShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowLog.AutoSize = true;
            this.btnShowLog.Location = new System.Drawing.Point(561, 322);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(81, 15);
            this.btnShowLog.TabIndex = 4;
            this.btnShowLog.TabStop = true;
            this.btnShowLog.Text = "Show Full Log";
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // btnScriptInclude
            // 
            this.btnScriptInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScriptInclude.AutoSize = true;
            this.btnScriptInclude.Location = new System.Drawing.Point(589, 19);
            this.btnScriptInclude.Name = "btnScriptInclude";
            this.btnScriptInclude.Size = new System.Drawing.Size(79, 15);
            this.btnScriptInclude.TabIndex = 2;
            this.btnScriptInclude.TabStop = true;
            this.btnScriptInclude.Text = "Script Include";
            this.btnScriptInclude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnScriptInclude_LinkClicked);
            // 
            // FormDBIntegration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 551);
            this.Controls.Add(this.btnScriptInclude);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDBIntegration";
            this.Text = "Cinar Simple Database Integration";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
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
        private System.Windows.Forms.LinkLabel btnToggleSelectedTask;
        private System.Windows.Forms.LinkLabel btnShowLog;
    }
}