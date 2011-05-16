namespace Cinar.DBTools.Tools
{
    partial class FormCreateTable
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColumnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsAutoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnCollectionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colColumnType,
            this.colLength,
            this.colDefaultValue,
            this.colIsNullable,
            this.colIsPrimaryKey,
            this.colIsAutoIncrement});
            this.dataGridView1.DataSource = this.columnCollectionBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(14, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(902, 344);
            this.dataGridView1.TabIndex = 2;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 150;
            // 
            // colColumnType
            // 
            this.colColumnType.DataPropertyName = "ColumnType";
            this.colColumnType.HeaderText = "ColumnType";
            this.colColumnType.Name = "colColumnType";
            this.colColumnType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColumnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colColumnType.Width = 120;
            // 
            // colLength
            // 
            this.colLength.DataPropertyName = "Length";
            this.colLength.HeaderText = "Length";
            this.colLength.Name = "colLength";
            this.colLength.Width = 50;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.DataPropertyName = "DefaultValue";
            this.colDefaultValue.HeaderText = "DefaultValue";
            this.colDefaultValue.Name = "colDefaultValue";
            // 
            // colIsNullable
            // 
            this.colIsNullable.DataPropertyName = "IsNullable";
            this.colIsNullable.HeaderText = "IsNullable";
            this.colIsNullable.Name = "colIsNullable";
            // 
            // colIsPrimaryKey
            // 
            this.colIsPrimaryKey.DataPropertyName = "IsPrimaryKey";
            this.colIsPrimaryKey.HeaderText = "IsPrimaryKey";
            this.colIsPrimaryKey.Name = "colIsPrimaryKey";
            // 
            // colIsAutoIncrement
            // 
            this.colIsAutoIncrement.DataPropertyName = "IsAutoIncrement";
            this.colIsAutoIncrement.HeaderText = "IsAutoIncrement";
            this.colIsAutoIncrement.Name = "colIsAutoIncrement";
            // 
            // columnCollectionBindingSource
            // 
            this.columnCollectionBindingSource.DataSource = typeof(System.Collections.Generic.List<Cinar.DBTools.Tools.ColumnDef>);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table Name :";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(120, 9);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(238, 23);
            this.txtTableName.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(824, 401);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(729, 401);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 26);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FormCreateTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 435);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCreateTable";
            this.Text = "Create / Alter Table";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource columnCollectionBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsNullable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsAutoIncrement;
    }
}