namespace Cinar.DBTools.Tools
{
    partial class FormScriptingTest
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.btnEditSelectedTask = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewTask = new System.Windows.Forms.LinkLabel();
            this.btnDeleteSelectedTask = new System.Windows.Forms.LinkLabel();
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRunTests = new System.Windows.Forms.LinkLabel();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(12, 55);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(482, 344);
            this.grid.TabIndex = 0;
            // 
            // btnEditSelectedTask
            // 
            this.btnEditSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSelectedTask.AutoSize = true;
            this.btnEditSelectedTask.Location = new System.Drawing.Point(282, 39);
            this.btnEditSelectedTask.Name = "btnEditSelectedTask";
            this.btnEditSelectedTask.Size = new System.Drawing.Size(94, 13);
            this.btnEditSelectedTask.TabIndex = 14;
            this.btnEditSelectedTask.TabStop = true;
            this.btnEditSelectedTask.Text = "Edit Selected Test";
            this.btnEditSelectedTask.Click += new System.EventHandler(this.btnEditSelectedTask_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Scripting Tests :";
            // 
            // btnAddNewTask
            // 
            this.btnAddNewTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewTask.AutoSize = true;
            this.btnAddNewTask.Location = new System.Drawing.Point(200, 39);
            this.btnAddNewTask.Name = "btnAddNewTask";
            this.btnAddNewTask.Size = new System.Drawing.Size(75, 13);
            this.btnAddNewTask.TabIndex = 12;
            this.btnAddNewTask.TabStop = true;
            this.btnAddNewTask.Text = "Add New Test";
            this.btnAddNewTask.Click += new System.EventHandler(this.btnAddNewTask_Click);
            // 
            // btnDeleteSelectedTask
            // 
            this.btnDeleteSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelectedTask.AutoSize = true;
            this.btnDeleteSelectedTask.Location = new System.Drawing.Point(386, 39);
            this.btnDeleteSelectedTask.Name = "btnDeleteSelectedTask";
            this.btnDeleteSelectedTask.Size = new System.Drawing.Size(107, 13);
            this.btnDeleteSelectedTask.TabIndex = 13;
            this.btnDeleteSelectedTask.TabStop = true;
            this.btnDeleteSelectedTask.Text = "Delete Selected Test";
            this.btnDeleteSelectedTask.Click += new System.EventHandler(this.btnDeleteSelectedTask_Click);
            // 
            // cbCategories
            // 
            this.cbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(100, 6);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(155, 21);
            this.cbCategories.TabIndex = 44;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Select Subject :";
            // 
            // btnRunTests
            // 
            this.btnRunTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunTests.AutoSize = true;
            this.btnRunTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRunTests.Location = new System.Drawing.Point(261, 9);
            this.btnRunTests.Name = "btnRunTests";
            this.btnRunTests.Size = new System.Drawing.Size(65, 13);
            this.btnRunTests.TabIndex = 45;
            this.btnRunTests.TabStop = true;
            this.btnRunTests.Text = "Run Tests";
            this.btnRunTests.Click += new System.EventHandler(this.btnRunTests_Click);
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormScriptingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 411);
            this.Controls.Add(this.btnRunTests);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEditSelectedTask);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNewTask);
            this.Controls.Add(this.btnDeleteSelectedTask);
            this.Controls.Add(this.grid);
            this.Name = "FormScriptingTest";
            this.Text = "Çınar Scripting Testing & Learning Center";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.LinkLabel btnEditSelectedTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel btnAddNewTask;
        private System.Windows.Forms.LinkLabel btnDeleteSelectedTask;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel btnRunTests;
        private System.Windows.Forms.ImageList ımageList1;
    }
}