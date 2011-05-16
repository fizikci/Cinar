namespace Cinar.DBTools.Controls
{
    partial class TemplateEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt = new Cinar.Scripting.CinarScriptEditor();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.IsReadOnly = false;
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Name = "txt";
            this.txt.ShowVRuler = false;
            this.txt.Size = new System.Drawing.Size(907, 469);
            this.txt.TabIndex = 0;
            // 
            // TemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txt);
            this.Name = "TemplateEditor";
            this.Size = new System.Drawing.Size(907, 469);
            this.ResumeLayout(false);

        }

        #endregion

        private Scripting.CinarScriptEditor txt;

    }
}
