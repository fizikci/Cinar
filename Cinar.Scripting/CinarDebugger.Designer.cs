namespace Cinar.Scripting
{
    partial class CinarDebugger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CinarDebugger));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tree = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnStepOver = new System.Windows.Forms.ToolStripButton();
            this.btnRunToCursor = new System.Windows.Forms.ToolStripButton();
            this.btnContinue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editor = new Cinar.Scripting.CinarScriptEditor();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabWatch = new System.Windows.Forms.TabPage();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabWatch.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(788, 468);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(788, 515);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(788, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(82, 17);
            this.statusBarLabel.Text = "Cinar Debugger";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(788, 468);
            this.splitContainer1.SplitterDistance = 562;
            this.splitContainer1.TabIndex = 1;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(3, 3);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(208, 436);
            this.tree.TabIndex = 0;
            this.tree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tree_BeforeExpand);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStepOver,
            this.btnRunToCursor,
            this.btnContinue,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(201, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnStepOver
            // 
            this.btnStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStepOver.Image = ((System.Drawing.Image)(resources.GetObject("btnStepOver.Image")));
            this.btnStepOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStepOver.Name = "btnStepOver";
            this.btnStepOver.Size = new System.Drawing.Size(60, 22);
            this.btnStepOver.Text = "Step Over";
            this.btnStepOver.Click += new System.EventHandler(this.btnStepOver_Click);
            // 
            // btnRunToCursor
            // 
            this.btnRunToCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRunToCursor.Image = ((System.Drawing.Image)(resources.GetObject("btnRunToCursor.Image")));
            this.btnRunToCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunToCursor.Name = "btnRunToCursor";
            this.btnRunToCursor.Size = new System.Drawing.Size(78, 22);
            this.btnRunToCursor.Text = "Run to Cursor";
            this.btnRunToCursor.Click += new System.EventHandler(this.btnRunToCursor_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnContinue.Image = ((System.Drawing.Image)(resources.GetObject("btnContinue.Image")));
            this.btnContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(54, 22);
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // editor
            // 
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.IsReadOnly = false;
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(562, 468);
            this.editor.TabIndex = 0;
            this.editor.Text = "editor";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabWatch);
            this.tabControl.Controls.Add(this.tabOutput);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(222, 468);
            this.tabControl.TabIndex = 1;
            // 
            // tabWatch
            // 
            this.tabWatch.Controls.Add(this.tree);
            this.tabWatch.Location = new System.Drawing.Point(4, 22);
            this.tabWatch.Name = "tabWatch";
            this.tabWatch.Padding = new System.Windows.Forms.Padding(3);
            this.tabWatch.Size = new System.Drawing.Size(214, 442);
            this.tabWatch.TabIndex = 0;
            this.tabWatch.Text = "Watch";
            this.tabWatch.UseVisualStyleBackColor = true;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.txtOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(214, 442);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.AcceptsTab = true;
            this.txtOutput.DetectUrls = false;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(208, 436);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.Text = "";
            this.txtOutput.WordWrap = false;
            // 
            // CinarDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 515);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "CinarDebugger";
            this.Text = "Cinar Debugger";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabWatch.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CinarScriptEditor editor;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnStepOver;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.ToolStripButton btnRunToCursor;
        private System.Windows.Forms.ToolStripButton btnContinue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabWatch;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.RichTextBox txtOutput;


    }
}