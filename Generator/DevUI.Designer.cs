namespace FoM.Generator
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
            this.CreatePatchGroup = new System.Windows.Forms.GroupBox();
            this.CreatePatch = new System.Windows.Forms.Button();
            this.PatchFolderBrowse = new System.Windows.Forms.Button();
            this.LocalFolderBrowse = new System.Windows.Forms.Button();
            this.ManifestNameText = new System.Windows.Forms.TextBox();
            this.ManifestNameLabel = new System.Windows.Forms.Label();
            this.PatchFolderText = new System.Windows.Forms.TextBox();
            this.PatchFolderLabel = new System.Windows.Forms.Label();
            this.LocalFolderText = new System.Windows.Forms.TextBox();
            this.LocalFolderLabel = new System.Windows.Forms.Label();
            this.DistributionURLLabel = new System.Windows.Forms.Label();
            this.DistributionURLText = new System.Windows.Forms.TextBox();
            this.CreatePatchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreatePatchGroup
            // 
            this.CreatePatchGroup.Controls.Add(this.DistributionURLText);
            this.CreatePatchGroup.Controls.Add(this.DistributionURLLabel);
            this.CreatePatchGroup.Controls.Add(this.CreatePatch);
            this.CreatePatchGroup.Controls.Add(this.PatchFolderBrowse);
            this.CreatePatchGroup.Controls.Add(this.LocalFolderBrowse);
            this.CreatePatchGroup.Controls.Add(this.ManifestNameText);
            this.CreatePatchGroup.Controls.Add(this.ManifestNameLabel);
            this.CreatePatchGroup.Controls.Add(this.PatchFolderText);
            this.CreatePatchGroup.Controls.Add(this.PatchFolderLabel);
            this.CreatePatchGroup.Controls.Add(this.LocalFolderText);
            this.CreatePatchGroup.Controls.Add(this.LocalFolderLabel);
            this.CreatePatchGroup.Location = new System.Drawing.Point(12, 12);
            this.CreatePatchGroup.Name = "CreatePatchGroup";
            this.CreatePatchGroup.Size = new System.Drawing.Size(422, 159);
            this.CreatePatchGroup.TabIndex = 0;
            this.CreatePatchGroup.TabStop = false;
            this.CreatePatchGroup.Text = "CreatePatch()";
            // 
            // CreatePatch
            // 
            this.CreatePatch.Location = new System.Drawing.Point(303, 131);
            this.CreatePatch.Name = "CreatePatch";
            this.CreatePatch.Size = new System.Drawing.Size(75, 23);
            this.CreatePatch.TabIndex = 5;
            this.CreatePatch.Text = "CreatePatch()";
            this.CreatePatch.UseVisualStyleBackColor = true;
            this.CreatePatch.Click += new System.EventHandler(this.CreatePatch_Click);
            // 
            // PatchFolderBrowse
            // 
            this.PatchFolderBrowse.Location = new System.Drawing.Point(384, 51);
            this.PatchFolderBrowse.Name = "PatchFolderBrowse";
            this.PatchFolderBrowse.Size = new System.Drawing.Size(24, 23);
            this.PatchFolderBrowse.TabIndex = 3;
            this.PatchFolderBrowse.Text = "...";
            this.PatchFolderBrowse.UseVisualStyleBackColor = true;
            this.PatchFolderBrowse.Click += new System.EventHandler(this.PatchFolderBrowse_Click);
            // 
            // LocalFolderBrowse
            // 
            this.LocalFolderBrowse.Location = new System.Drawing.Point(384, 25);
            this.LocalFolderBrowse.Name = "LocalFolderBrowse";
            this.LocalFolderBrowse.Size = new System.Drawing.Size(24, 23);
            this.LocalFolderBrowse.TabIndex = 1;
            this.LocalFolderBrowse.Text = "...";
            this.LocalFolderBrowse.UseVisualStyleBackColor = true;
            this.LocalFolderBrowse.Click += new System.EventHandler(this.LocalFolderBrowse_Click);
            // 
            // ManifestNameText
            // 
            this.ManifestNameText.Location = new System.Drawing.Point(119, 79);
            this.ManifestNameText.Name = "ManifestNameText";
            this.ManifestNameText.Size = new System.Drawing.Size(259, 20);
            this.ManifestNameText.TabIndex = 4;
            this.ManifestNameText.Text = "Test";
            // 
            // ManifestNameLabel
            // 
            this.ManifestNameLabel.AutoSize = true;
            this.ManifestNameLabel.Location = new System.Drawing.Point(6, 82);
            this.ManifestNameLabel.Name = "ManifestNameLabel";
            this.ManifestNameLabel.Size = new System.Drawing.Size(75, 13);
            this.ManifestNameLabel.TabIndex = 4;
            this.ManifestNameLabel.Text = "ManifestName";
            // 
            // PatchFolderText
            // 
            this.PatchFolderText.Location = new System.Drawing.Point(119, 53);
            this.PatchFolderText.Name = "PatchFolderText";
            this.PatchFolderText.Size = new System.Drawing.Size(259, 20);
            this.PatchFolderText.TabIndex = 2;
            this.PatchFolderText.Text = "C:\\Code\\fom-launcher\\TestData\\Patch Stage";
            // 
            // PatchFolderLabel
            // 
            this.PatchFolderLabel.AutoSize = true;
            this.PatchFolderLabel.Location = new System.Drawing.Point(6, 56);
            this.PatchFolderLabel.Name = "PatchFolderLabel";
            this.PatchFolderLabel.Size = new System.Drawing.Size(64, 13);
            this.PatchFolderLabel.TabIndex = 2;
            this.PatchFolderLabel.Text = "PatchFolder";
            // 
            // LocalFolderText
            // 
            this.LocalFolderText.Location = new System.Drawing.Point(119, 27);
            this.LocalFolderText.Name = "LocalFolderText";
            this.LocalFolderText.Size = new System.Drawing.Size(259, 20);
            this.LocalFolderText.TabIndex = 0;
            this.LocalFolderText.Text = "C:\\Code\\fom-launcher\\TestData\\SourceImages";
            // 
            // LocalFolderLabel
            // 
            this.LocalFolderLabel.AutoSize = true;
            this.LocalFolderLabel.Location = new System.Drawing.Point(6, 30);
            this.LocalFolderLabel.Name = "LocalFolderLabel";
            this.LocalFolderLabel.Size = new System.Drawing.Size(62, 13);
            this.LocalFolderLabel.TabIndex = 0;
            this.LocalFolderLabel.Text = "LocalFolder";
            // 
            // DistributionURLLabel
            // 
            this.DistributionURLLabel.AutoSize = true;
            this.DistributionURLLabel.Location = new System.Drawing.Point(6, 108);
            this.DistributionURLLabel.Name = "DistributionURLLabel";
            this.DistributionURLLabel.Size = new System.Drawing.Size(81, 13);
            this.DistributionURLLabel.TabIndex = 6;
            this.DistributionURLLabel.Text = "DistributionURL";
            // 
            // DistributionURLText
            // 
            this.DistributionURLText.Location = new System.Drawing.Point(119, 105);
            this.DistributionURLText.Name = "DistributionURLText";
            this.DistributionURLText.Size = new System.Drawing.Size(259, 20);
            this.DistributionURLText.TabIndex = 7;
            this.DistributionURLText.Text = "http://patch.patrickshafer.com";
            // 
            // DevUI
            // 
            this.AcceptButton = this.CreatePatch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 178);
            this.Controls.Add(this.CreatePatchGroup);
            this.Name = "DevUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patch Generator";
            this.CreatePatchGroup.ResumeLayout(false);
            this.CreatePatchGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CreatePatchGroup;
        private System.Windows.Forms.TextBox ManifestNameText;
        private System.Windows.Forms.Label ManifestNameLabel;
        private System.Windows.Forms.TextBox PatchFolderText;
        private System.Windows.Forms.Label PatchFolderLabel;
        private System.Windows.Forms.TextBox LocalFolderText;
        private System.Windows.Forms.Label LocalFolderLabel;
        private System.Windows.Forms.Button PatchFolderBrowse;
        private System.Windows.Forms.Button LocalFolderBrowse;
        private System.Windows.Forms.Button CreatePatch;
        private System.Windows.Forms.TextBox DistributionURLText;
        private System.Windows.Forms.Label DistributionURLLabel;
    }
}

