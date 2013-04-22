namespace Cinar.DBTools.Tools
{
    partial class FormCheckDatabaseSchema
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
            this.btnFix = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApplyToMetadata = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFix
            // 
            this.btnFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFix.Location = new System.Drawing.Point(365, 446);
            this.btnFix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(145, 26);
            this.btnFix.TabIndex = 2;
            this.btnFix.Text = "Generate SQL Script";
            this.btnFix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFix.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(521, 446);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPanel.Location = new System.Drawing.Point(12, 12);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(596, 422);
            this.flowPanel.TabIndex = 5;
            // 
            // btnApplyToMetadata
            // 
            this.btnApplyToMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyToMetadata.Location = new System.Drawing.Point(203, 446);
            this.btnApplyToMetadata.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnApplyToMetadata.Name = "btnApplyToMetadata";
            this.btnApplyToMetadata.Size = new System.Drawing.Size(145, 26);
            this.btnApplyToMetadata.TabIndex = 6;
            this.btnApplyToMetadata.Text = "Apply to Metadata";
            this.btnApplyToMetadata.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApplyToMetadata.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApplyToMetadata.UseVisualStyleBackColor = true;
            this.btnApplyToMetadata.Click += new System.EventHandler(this.btnApplyToMetadata_Click);
            // 
            // FormCheckDatabaseSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 483);
            this.Controls.Add(this.btnApplyToMetadata);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFix);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCheckDatabaseSchema";
            this.Text = "Database Schema Notices";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Button btnApplyToMetadata;
    }
}