namespace FoM.Launcher
{
    partial class DevUI
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
            this.gbApplyPatch = new System.Windows.Forms.GroupBox();
            this.LocalFolderLabel = new System.Windows.Forms.Label();
            this.ManifestURLLabel = new System.Windows.Forms.Label();
            this.LocalFolder = new System.Windows.Forms.TextBox();
            this.ManifestURL = new System.Windows.Forms.TextBox();
            this.LocalFolderBrowse = new System.Windows.Forms.Button();
            this.InvokeButton = new System.Windows.Forms.Button();
            this.gbApplyPatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbApplyPatch
            // 
            this.gbApplyPatch.Controls.Add(this.InvokeButton);
            this.gbApplyPatch.Controls.Add(this.LocalFolderBrowse);
            this.gbApplyPatch.Controls.Add(this.ManifestURL);
            this.gbApplyPatch.Controls.Add(this.LocalFolder);
            this.gbApplyPatch.Controls.Add(this.ManifestURLLabel);
            this.gbApplyPatch.Controls.Add(this.LocalFolderLabel);
            this.gbApplyPatch.Location = new System.Drawing.Point(12, 12);
            this.gbApplyPatch.Name = "gbApplyPatch";
            this.gbApplyPatch.Size = new System.Drawing.Size(424, 118);
            this.gbApplyPatch.TabIndex = 0;
            this.gbApplyPatch.TabStop = false;
            this.gbApplyPatch.Text = "ApplyPatch()";
            // 
            // LocalFolderLabel
            // 
            this.LocalFolderLabel.AutoSize = true;
            this.LocalFolderLabel.Location = new System.Drawing.Point(6, 26);
            this.LocalFolderLabel.Name = "LocalFolderLabel";
            this.LocalFolderLabel.Size = new System.Drawing.Size(62, 13);
            this.LocalFolderLabel.TabIndex = 0;
            this.LocalFolderLabel.Text = "LocalFolder";
            // 
            // ManifestURLLabel
            // 
            this.ManifestURLLabel.AutoSize = true;
            this.ManifestURLLabel.Location = new System.Drawing.Point(6, 52);
            this.ManifestURLLabel.Name = "ManifestURLLabel";
            this.ManifestURLLabel.Size = new System.Drawing.Size(69, 13);
            this.ManifestURLLabel.TabIndex = 1;
            this.ManifestURLLabel.Text = "ManifestURL";
            // 
            // LocalFolder
            // 
            this.LocalFolder.Location = new System.Drawing.Point(81, 23);
            this.LocalFolder.Name = "LocalFolder";
            this.LocalFolder.Size = new System.Drawing.Size(289, 20);
            this.LocalFolder.TabIndex = 2;
            // 
            // ManifestURL
            // 
            this.ManifestURL.Location = new System.Drawing.Point(81, 49);
            this.ManifestURL.Name = "ManifestURL";
            this.ManifestURL.Size = new System.Drawing.Size(289, 20);
            this.ManifestURL.TabIndex = 4;
            // 
            // LocalFolderBrowse
            // 
            this.LocalFolderBrowse.Location = new System.Drawing.Point(376, 21);
            this.LocalFolderBrowse.Name = "LocalFolderBrowse";
            this.LocalFolderBrowse.Size = new System.Drawing.Size(26, 23);
            this.LocalFolderBrowse.TabIndex = 3;
            this.LocalFolderBrowse.Text = "...";
            this.LocalFolderBrowse.UseVisualStyleBackColor = true;
            this.LocalFolderBrowse.Click += new System.EventHandler(this.LocalFolderBrowse_Click);
            // 
            // InvokeButton
            // 
            this.InvokeButton.Location = new System.Drawing.Point(295, 75);
            this.InvokeButton.Name = "InvokeButton";
            this.InvokeButton.Size = new System.Drawing.Size(75, 23);
            this.InvokeButton.TabIndex = 5;
            this.InvokeButton.Text = "Invoke";
            this.InvokeButton.UseVisualStyleBackColor = true;
            this.InvokeButton.Click += new System.EventHandler(this.InvokeButton_Click);
            // 
            // DevUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 148);
            this.Controls.Add(this.gbApplyPatch);
            this.Name = "DevUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FoM Launcher - Development UI";
            this.gbApplyPatch.ResumeLayout(false);
            this.gbApplyPatch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbApplyPatch;
        private System.Windows.Forms.Label ManifestURLLabel;
        private System.Windows.Forms.Label LocalFolderLabel;
        private System.Windows.Forms.Button InvokeButton;
        private System.Windows.Forms.Button LocalFolderBrowse;
        private System.Windows.Forms.TextBox ManifestURL;
        private System.Windows.Forms.TextBox LocalFolder;
    }
}

