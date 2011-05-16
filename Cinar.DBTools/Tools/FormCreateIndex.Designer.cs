namespace Cinar.DBTools.Tools
{
    partial class FormCreateIndex
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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtKeyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keyColumnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLocal = new System.Windows.Forms.TabPage();
            this.rbIndex = new System.Windows.Forms.RadioButton();
            this.rbUnique = new System.Windows.Forms.RadioButton();
            this.rbPrimaryKey = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.selectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageFK = new System.Windows.Forms.TabPage();
            this.pnlForeignTableColumns = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboForeignTableKeys = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.keyColumnBindingSource)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.tabPageFK.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(518, 400);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 26);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(423, 400);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 26);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtKeyName
            // 
            this.txtKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyName.Location = new System.Drawing.Point(63, 14);
            this.txtKeyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKeyName.Name = "txtKeyName";
            this.txtKeyName.Size = new System.Drawing.Size(541, 23);
            this.txtKeyName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name :";
            // 
            // keyColumnBindingSource
            // 
            this.keyColumnBindingSource.DataSource = typeof(Cinar.DBTools.Tools.KeyColumn);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageLocal);
            this.tabControl.Controls.Add(this.tabPageFK);
            this.tabControl.Location = new System.Drawing.Point(15, 48);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(591, 339);
            this.tabControl.TabIndex = 10;
            // 
            // tabPageLocal
            // 
            this.tabPageLocal.Controls.Add(this.rbIndex);
            this.tabPageLocal.Controls.Add(this.rbUnique);
            this.tabPageLocal.Controls.Add(this.rbPrimaryKey);
            this.tabPageLocal.Controls.Add(this.label2);
            this.tabPageLocal.Controls.Add(this.grid);
            this.tabPageLocal.Location = new System.Drawing.Point(4, 24);
            this.tabPageLocal.Name = "tabPageLocal";
            this.tabPageLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocal.Size = new System.Drawing.Size(583, 311);
            this.tabPageLocal.TabIndex = 0;
            this.tabPageLocal.Text = "Local Key";
            this.tabPageLocal.UseVisualStyleBackColor = true;
            // 
            // rbIndex
            // 
            this.rbIndex.AutoSize = true;
            this.rbIndex.Location = new System.Drawing.Point(401, 23);
            this.rbIndex.Name = "rbIndex";
            this.rbIndex.Size = new System.Drawing.Size(53, 19);
            this.rbIndex.TabIndex = 23;
            this.rbIndex.Text = "Index";
            this.rbIndex.UseVisualStyleBackColor = true;
            // 
            // rbUnique
            // 
            this.rbUnique.AutoSize = true;
            this.rbUnique.Location = new System.Drawing.Point(255, 23);
            this.rbUnique.Name = "rbUnique";
            this.rbUnique.Size = new System.Drawing.Size(121, 19);
            this.rbUnique.TabIndex = 21;
            this.rbUnique.Text = "Unique Constraint";
            this.rbUnique.UseVisualStyleBackColor = true;
            // 
            // rbPrimaryKey
            // 
            this.rbPrimaryKey.AutoSize = true;
            this.rbPrimaryKey.Checked = true;
            this.rbPrimaryKey.Location = new System.Drawing.Point(89, 23);
            this.rbPrimaryKey.Name = "rbPrimaryKey";
            this.rbPrimaryKey.Size = new System.Drawing.Size(146, 19);
            this.rbPrimaryKey.TabIndex = 20;
            this.rbPrimaryKey.TabStop = true;
            this.rbPrimaryKey.Text = "Primary Key Constraint";
            this.rbPrimaryKey.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Index Type :";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectedDataGridViewCheckBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.grid.DataSource = this.keyColumnBindingSource;
            this.grid.Location = new System.Drawing.Point(10, 62);
            this.grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(558, 230);
            this.grid.TabIndex = 18;
            // 
            // selectedDataGridViewCheckBoxColumn
            // 
            this.selectedDataGridViewCheckBoxColumn.DataPropertyName = "Selected";
            this.selectedDataGridViewCheckBoxColumn.HeaderText = "";
            this.selectedDataGridViewCheckBoxColumn.Name = "selectedDataGridViewCheckBoxColumn";
            this.selectedDataGridViewCheckBoxColumn.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 220;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ColumnType";
            this.dataGridViewTextBoxColumn2.HeaderText = "ColumnType";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Length";
            this.dataGridViewTextBoxColumn3.HeaderText = "Length";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // tabPageFK
            // 
            this.tabPageFK.Controls.Add(this.pnlForeignTableColumns);
            this.tabPageFK.Controls.Add(this.label3);
            this.tabPageFK.Controls.Add(this.comboForeignTableKeys);
            this.tabPageFK.Location = new System.Drawing.Point(4, 24);
            this.tabPageFK.Name = "tabPageFK";
            this.tabPageFK.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFK.Size = new System.Drawing.Size(583, 311);
            this.tabPageFK.TabIndex = 1;
            this.tabPageFK.Text = "Foreign Key";
            this.tabPageFK.UseVisualStyleBackColor = true;
            // 
            // pnlForeignTableColumns
            // 
            this.pnlForeignTableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlForeignTableColumns.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlForeignTableColumns.Location = new System.Drawing.Point(23, 61);
            this.pnlForeignTableColumns.Name = "pnlForeignTableColumns";
            this.pnlForeignTableColumns.Size = new System.Drawing.Size(535, 226);
            this.pnlForeignTableColumns.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Foreign Table Key :";
            // 
            // comboForeignTableKeys
            // 
            this.comboForeignTableKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboForeignTableKeys.FormattingEnabled = true;
            this.comboForeignTableKeys.Location = new System.Drawing.Point(133, 19);
            this.comboForeignTableKeys.Name = "comboForeignTableKeys";
            this.comboForeignTableKeys.Size = new System.Drawing.Size(425, 23);
            this.comboForeignTableKeys.TabIndex = 25;
            // 
            // FormCreateIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 441);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtKeyName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCreateIndex";
            this.Text = "Create Index / Constraint";
            ((System.ComponentModel.ISupportInitialize)(this.keyColumnBindingSource)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageLocal.ResumeLayout(false);
            this.tabPageLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.tabPageFK.ResumeLayout(false);
            this.tabPageFK.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtKeyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource keyColumnBindingSource;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLocal;
        private System.Windows.Forms.RadioButton rbIndex;
        private System.Windows.Forms.RadioButton rbUnique;
        private System.Windows.Forms.RadioButton rbPrimaryKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TabPage tabPageFK;
        private System.Windows.Forms.FlowLayoutPanel pnlForeignTableColumns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboForeignTableKeys;

    }
}