namespace FoM.Launcher
{
    partial class InternalUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternalUI));
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.PatchProgress = new System.Windows.Forms.ProgressBar();
            this.DevUIButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputBox.Location = new System.Drawing.Point(12, 12);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox.Size = new System.Drawing.Size(560, 259);
            this.OutputBox.TabIndex = 0;
            // 
            // PatchProgress
            // 
            this.PatchProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PatchProgress.Location = new System.Drawing.Point(12, 277);
            this.PatchProgress.Name = "PatchProgress";
            this.PatchProgress.Size = new System.Drawing.Size(487, 23);
            this.PatchProgress.TabIndex = 1;
            // 
            // DevUIButton
            // 
            this.DevUIButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DevUIButton.Location = new System.Drawing.Point(505, 277);
            this.DevUIButton.Name = "DevUIButton";
            this.DevUIButton.Size = new System.Drawing.Size(67, 23);
            this.DevUIButton.TabIndex = 2;
            this.DevUIButton.Text = "DevUI";
            this.DevUIButton.UseVisualStyleBackColor = true;
            this.DevUIButton.Click += new System.EventHandler(this.DevUIButton_Click);
            // 
            // InternalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 312);
            this.Controls.Add(this.DevUIButton);
            this.Controls.Add(this.PatchProgress);
            this.Controls.Add(this.OutputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InternalUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fall of Dominion - Internal Launcher";
            this.Load += new System.EventHandler(this.InternalUI_Load);
            this.Shown += new System.EventHandler(this.InternalUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.ProgressBar PatchProgress;
        private System.Windows.Forms.Button DevUIButton;
    }
}