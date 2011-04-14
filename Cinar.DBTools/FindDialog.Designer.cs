namespace Cinar.DBTools
{
    partial class FindDialog
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
            this.txtFind = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUseRegEx = new System.Windows.Forms.CheckBox();
            this.cbMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what:";
            // 
            // txtFind
            // 
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFind.Location = new System.Drawing.Point(8, 37);
            this.txtFind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(276, 25);
            this.txtFind.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUseRegEx);
            this.groupBox1.Controls.Add(this.cbMatchWholeWord);
            this.groupBox1.Controls.Add(this.cbMatchCase);
            this.groupBox1.Location = new System.Drawing.Point(10, 136);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(276, 116);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbUseRegEx
            // 
            this.cbUseRegEx.AutoSize = true;
            this.cbUseRegEx.Location = new System.Drawing.Point(20, 85);
            this.cbUseRegEx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbUseRegEx.Name = "cbUseRegEx";
            this.cbUseRegEx.Size = new System.Drawing.Size(171, 21);
            this.cbUseRegEx.TabIndex = 2;
            this.cbUseRegEx.Text = "Use Regular Expressions";
            this.cbUseRegEx.UseVisualStyleBackColor = true;
            // 
            // cbMatchWholeWord
            // 
            this.cbMatchWholeWord.AutoSize = true;
            this.cbMatchWholeWord.Location = new System.Drawing.Point(20, 55);
            this.cbMatchWholeWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMatchWholeWord.Name = "cbMatchWholeWord";
            this.cbMatchWholeWord.Size = new System.Drawing.Size(135, 21);
            this.cbMatchWholeWord.TabIndex = 1;
            this.cbMatchWholeWord.Text = "Match whole word";
            this.cbMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(20, 25);
            this.cbMatchCase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(93, 21);
            this.cbMatchCase.TabIndex = 0;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(105, 272);
            this.btnFindNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(87, 30);
            this.btnFindNext.TabIndex = 3;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(199, 272);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(87, 30);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(199, 310);
            this.btnReplaceAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(87, 30);
            this.btnReplaceAll.TabIndex = 5;
            this.btnReplaceAll.Text = "Replace All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // txtReplace
            // 
            this.txtReplace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtReplace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReplace.Location = new System.Drawing.Point(9, 97);
            this.txtReplace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(276, 25);
            this.txtReplace.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Replace with:";
            // 
            // FindDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 352);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FindDialog";
            this.ShowInTaskbar = false;
            this.Text = " Find and Replace";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbUseRegEx;
        private System.Windows.Forms.CheckBox cbMatchWholeWord;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label label2;
    }
}