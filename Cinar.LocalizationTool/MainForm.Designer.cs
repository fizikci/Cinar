namespace Cinar.LocalizationTool
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.yerelleştirmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dizinSeçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.başlaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kaydetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDizin = new System.Windows.Forms.Label();
            this.labelLed = new System.Windows.Forms.Label();
            this.listLog = new System.Windows.Forms.ListBox();
            this.progressFiles = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridTurkce = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridEnglish = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTurkce)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEnglish)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yerelleştirmeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // yerelleştirmeToolStripMenuItem
            // 
            this.yerelleştirmeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dizinSeçToolStripMenuItem,
            this.başlaToolStripMenuItem,
            this.kaydetToolStripMenuItem});
            this.yerelleştirmeToolStripMenuItem.Name = "yerelleştirmeToolStripMenuItem";
            this.yerelleştirmeToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.yerelleştirmeToolStripMenuItem.Text = "Yerelleştirme";
            // 
            // dizinSeçToolStripMenuItem
            // 
            this.dizinSeçToolStripMenuItem.Name = "dizinSeçToolStripMenuItem";
            this.dizinSeçToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.dizinSeçToolStripMenuItem.Text = "Dizin Seç...";
            this.dizinSeçToolStripMenuItem.Click += new System.EventHandler(this.dizinSeçToolStripMenuItem_Click);
            // 
            // başlaToolStripMenuItem
            // 
            this.başlaToolStripMenuItem.Name = "başlaToolStripMenuItem";
            this.başlaToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.başlaToolStripMenuItem.Text = "Başla";
            this.başlaToolStripMenuItem.Click += new System.EventHandler(this.başlaToolStripMenuItem_Click);
            // 
            // kaydetToolStripMenuItem
            // 
            this.kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
            this.kaydetToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kaydetToolStripMenuItem.Text = "Kaydet";
            this.kaydetToolStripMenuItem.Click += new System.EventHandler(this.kaydetToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dizin:";
            // 
            // labelDizin
            // 
            this.labelDizin.AutoSize = true;
            this.labelDizin.Location = new System.Drawing.Point(97, 41);
            this.labelDizin.Name = "labelDizin";
            this.labelDizin.Size = new System.Drawing.Size(134, 13);
            this.labelDizin.TabIndex = 2;
            this.labelDizin.Text = "C:\\ydaglar\\Projects\\finsfax";
            // 
            // labelLed
            // 
            this.labelLed.BackColor = System.Drawing.Color.DarkGray;
            this.labelLed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLed.Location = new System.Drawing.Point(15, 44);
            this.labelLed.Name = "labelLed";
            this.labelLed.Size = new System.Drawing.Size(30, 10);
            this.labelLed.TabIndex = 3;
            // 
            // listLog
            // 
            this.listLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listLog.FormattingEnabled = true;
            this.listLog.IntegralHeight = false;
            this.listLog.ItemHeight = 16;
            this.listLog.Location = new System.Drawing.Point(2, 35);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(666, 281);
            this.listLog.TabIndex = 4;
            // 
            // progressFiles
            // 
            this.progressFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressFiles.Location = new System.Drawing.Point(1, 6);
            this.progressFiles.Name = "progressFiles";
            this.progressFiles.Size = new System.Drawing.Size(667, 23);
            this.progressFiles.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 343);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listLog);
            this.tabPage1.Controls.Add(this.progressFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "İşlem";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridTurkce);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Türkçe";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridTurkce
            // 
            this.gridTurkce.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTurkce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTurkce.Location = new System.Drawing.Point(3, 3);
            this.gridTurkce.Name = "gridTurkce";
            this.gridTurkce.Size = new System.Drawing.Size(664, 311);
            this.gridTurkce.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridEnglish);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(670, 317);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "English";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridEnglish
            // 
            this.gridEnglish.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEnglish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEnglish.Location = new System.Drawing.Point(3, 3);
            this.gridEnglish.Name = "gridEnglish";
            this.gridEnglish.Size = new System.Drawing.Size(664, 311);
            this.gridEnglish.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 424);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelLed);
            this.Controls.Add(this.labelDizin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Çınar Yerelleştirme Uygulaması";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTurkce)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEnglish)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem yerelleştirmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dizinSeçToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem başlaToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDizin;
        private System.Windows.Forms.Label labelLed;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.ProgressBar progressFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridTurkce;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gridEnglish;
        private System.Windows.Forms.ToolStripMenuItem kaydetToolStripMenuItem;
    }
}

