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
            this.LocalFolderLabel = new System.Windows.Forms.Label();
            this.LocalFolderText = new System.Windows.Forms.TextBox();
            this.PatchFolderLabel = new System.Windows.Forms.Label();
            this.PatchFolderText = new System.Windows.Forms.TextBox();
            this.ManifestNameLabel = new System.Windows.Forms.Label();
            this.ManifestNameText = new System.Windows.Forms.TextBox();
            this.CreatePatchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreatePatchGroup
            // 
            this.CreatePatchGroup.Controls.Add(this.ManifestNameText);
            this.CreatePatchGroup.Controls.Add(this.ManifestNameLabel);
            this.CreatePatchGroup.Controls.Add(this.PatchFolderText);
            this.CreatePatchGroup.Controls.Add(this.PatchFolderLabel);
            this.CreatePatchGroup.Controls.Add(this.LocalFolderText);
            this.CreatePatchGroup.Controls.Add(this.LocalFolderLabel);
            this.CreatePatchGroup.Location = new System.Drawing.Point(12, 12);
            this.CreatePatchGroup.Name = "CreatePatchGroup";
            this.CreatePatchGroup.Size = new System.Drawing.Size(392, 116);
            this.CreatePatchGroup.TabIndex = 0;
            this.CreatePatchGroup.TabStop = false;
            this.CreatePatchGroup.Text = "CreatePatch()";
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
            // LocalFolderText
            // 
            this.LocalFolderText.Location = new System.Drawing.Point(119, 27);
            this.LocalFolderText.Name = "LocalFolderText";
            this.LocalFolderText.Size = new System.Drawing.Size(259, 20);
            this.LocalFolderText.TabIndex = 1;
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
            // PatchFolderText
            // 
            this.PatchFolderText.Location = new System.Drawing.Point(119, 53);
            this.PatchFolderText.Name = "PatchFolderText";
            this.PatchFolderText.Size = new System.Drawing.Size(259, 20);
            this.PatchFolderText.TabIndex = 3;
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
            // ManifestNameText
            // 
            this.ManifestNameText.Location = new System.Drawing.Point(119, 79);
            this.ManifestNameText.Name = "ManifestNameText";
            this.ManifestNameText.Size = new System.Drawing.Size(259, 20);
            this.ManifestNameText.TabIndex = 5;
            // 
            // DevUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 150);
            this.Controls.Add(this.CreatePatchGroup);
            this.Name = "DevUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
    }
}

