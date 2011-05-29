namespace Cinar.DBTools.Tools
{
    partial class FilterExpressionDialog
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.colColumnName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCriteriaType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(399, 260);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(306, 260);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 26);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColumnName,
            this.colCriteriaType,
            this.colValue});
            this.grid.DataSource = this.criteriaBindingSource;
            this.grid.Location = new System.Drawing.Point(13, 13);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(473, 240);
            this.grid.TabIndex = 6;
            this.grid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grid_DataError);
            // 
            // colColumnName
            // 
            this.colColumnName.DataPropertyName = "ColumnName";
            this.colColumnName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colColumnName.HeaderText = "Column Name";
            this.colColumnName.Name = "colColumnName";
            this.colColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colColumnName.Width = 150;
            // 
            // colCriteriaType
            // 
            this.colCriteriaType.DataPropertyName = "CriteriaType";
            this.colCriteriaType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colCriteriaType.HeaderText = "";
            this.colCriteriaType.Name = "colCriteriaType";
            this.colCriteriaType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCriteriaType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.DataPropertyName = "ColumnValue";
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataSource = typeof(Cinar.Database.Criteria);
            // 
            // FilterExpressionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 296);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FilterExpressionDialog";
            this.Text = " Filter Expression";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn colColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCriteriaType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;

    }
}