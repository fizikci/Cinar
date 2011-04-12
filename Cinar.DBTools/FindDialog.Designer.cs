﻿namespace Cinar.DBTools
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
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what:";
            // 
            // txtFind
            // 
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFind.Location = new System.Drawing.Point(7, 28);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(237, 20);
            this.txtFind.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUseRegEx);
            this.groupBox1.Controls.Add(this.cbMatchWholeWord);
            this.groupBox1.Controls.Add(this.cbMatchCase);
            this.groupBox1.Location = new System.Drawing.Point(9, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cbUseRegEx
            // 
            this.cbUseRegEx.AutoSize = true;
            this.cbUseRegEx.Location = new System.Drawing.Point(17, 65);
            this.cbUseRegEx.Name = "cbUseRegEx";
            this.cbUseRegEx.Size = new System.Drawing.Size(144, 17);
            this.cbUseRegEx.TabIndex = 2;
            this.cbUseRegEx.Text = "Use Regular Expressions";
            this.cbUseRegEx.UseVisualStyleBackColor = true;
            // 
            // cbMatchWholeWord
            // 
            this.cbMatchWholeWord.AutoSize = true;
            this.cbMatchWholeWord.Location = new System.Drawing.Point(17, 42);
            this.cbMatchWholeWord.Name = "cbMatchWholeWord";
            this.cbMatchWholeWord.Size = new System.Drawing.Size(113, 17);
            this.cbMatchWholeWord.TabIndex = 1;
            this.cbMatchWholeWord.Text = "Match whole word";
            this.cbMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(17, 19);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(82, 17);
            this.cbMatchCase.TabIndex = 0;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(90, 208);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(75, 23);
            this.btnFindNext.TabIndex = 3;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(171, 208);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(171, 237);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btnReplaceAll.TabIndex = 5;
            this.btnReplaceAll.Text = "Replace All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // txtReplace
            // 
            this.txtReplace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtReplace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReplace.Location = new System.Drawing.Point(8, 74);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(237, 20);
            this.txtReplace.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Replace with:";
            // 
            // FindDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 269);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
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