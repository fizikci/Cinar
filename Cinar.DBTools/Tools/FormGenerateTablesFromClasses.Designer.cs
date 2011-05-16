namespace Cinar.DBTools.Tools
{
    partial class FormGenerateTablesFromClasses
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
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.btnOpenAssembly = new System.Windows.Forms.Button();
            this.tree = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.editor = new Cinar.DBTools.CinarSQLEditor();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Assembly:";
            // 
            // txtAssembly
            // 
            this.txtAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAssembly.Location = new System.Drawing.Point(70, 10);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.ReadOnly = true;
            this.txtAssembly.Size = new System.Drawing.Size(491, 20);
            this.txtAssembly.TabIndex = 1;
            // 
            // btnOpenAssembly
            // 
            this.btnOpenAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenAssembly.Location = new System.Drawing.Point(559, 9);
            this.btnOpenAssembly.Name = "btnOpenAssembly";
            this.btnOpenAssembly.Size = new System.Drawing.Size(68, 22);
            this.btnOpenAssembly.TabIndex = 2;
            this.btnOpenAssembly.Text = "Browse...";
            this.btnOpenAssembly.UseVisualStyleBackColor = true;
            this.btnOpenAssembly.Click += new System.EventHandler(this.btnOpenAssembly_Click);
            // 
            // tree
            // 
            this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tree.CheckBoxes = true;
            this.tree.Location = new System.Drawing.Point(16, 44);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(220, 421);
            this.tree.TabIndex = 3;
            this.tree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(444, 480);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(539, 480);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 26);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editor.IsReadOnly = false;
            this.editor.Location = new System.Drawing.Point(243, 44);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(383, 421);
            this.editor.TabIndex = 4;
            // 
            // FormGenerateTablesFromClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 519);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.tree);
            this.Controls.Add(this.btnOpenAssembly);
            this.Controls.Add(this.txtAssembly);
            this.Controls.Add(this.label1);
            this.Name = "FormGenerateTablesFromClasses";
            this.Text = "Generate Tables From Reflected Metadata";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAssembly;
        private System.Windows.Forms.Button btnOpenAssembly;
        private System.Windows.Forms.TreeView tree;
        private CinarSQLEditor editor;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}