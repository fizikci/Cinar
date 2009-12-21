namespace Cinar.TemplateDesign
{
    partial class PageDesigner
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
            this.handle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // handle
            // 
            this.handle.BackColor = System.Drawing.Color.White;
            this.handle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handle.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.handle.Location = new System.Drawing.Point(367, 280);
            this.handle.Name = "handle";
            this.handle.Size = new System.Drawing.Size(7, 7);
            this.handle.TabIndex = 3;
            this.handle.Visible = false;
            this.handle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.handle_MouseMove);
            this.handle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.handle_MouseDown);
            this.handle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.handle_MouseUp);
            // 
            // PageDesigner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.handle);
            this.Name = "PageDesigner";
            this.Size = new System.Drawing.Size(570, 570);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PageDesigner_MouseMove);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PageDesigner_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PageDesigner_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PageDesigner_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label handle;




    }
}
