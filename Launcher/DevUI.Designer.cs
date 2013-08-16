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
            this.UpdateCheckBox = new System.Windows.Forms.GroupBox();
            this.UpdateCheckInvoke = new System.Windows.Forms.Button();
            this.LocalFolderBrowse = new System.Windows.Forms.Button();
            this.ManifestURL = new System.Windows.Forms.TextBox();
            this.LocalFolder = new System.Windows.Forms.TextBox();
            this.ManifestURLLabel = new System.Windows.Forms.Label();
            this.LocalFolderLabel = new System.Windows.Forms.Label();
            this.ApplyUpdateBox = new System.Windows.Forms.GroupBox();
            this.ManifestLabel = new System.Windows.Forms.Label();
            this.ManifestInput = new System.Windows.Forms.Label();
            this.ApplyUpdateInvoke = new System.Windows.Forms.Button();
            this.UpdateCheckBox.SuspendLayout();
            this.ApplyUpdateBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateCheckBox
            // 
            this.UpdateCheckBox.Controls.Add(this.UpdateCheckInvoke);
            this.UpdateCheckBox.Controls.Add(this.LocalFolderBrowse);
            this.UpdateCheckBox.Controls.Add(this.ManifestURL);
            this.UpdateCheckBox.Controls.Add(this.LocalFolder);
            this.UpdateCheckBox.Controls.Add(this.ManifestURLLabel);
            this.UpdateCheckBox.Controls.Add(this.LocalFolderLabel);
            this.UpdateCheckBox.Location = new System.Drawing.Point(12, 12);
            this.UpdateCheckBox.Name = "UpdateCheckBox";
            this.UpdateCheckBox.Size = new System.Drawing.Size(424, 118);
            this.UpdateCheckBox.TabIndex = 0;
            this.UpdateCheckBox.TabStop = false;
            this.UpdateCheckBox.Text = "UpdateCheck()";
            // 
            // UpdateCheckInvoke
            // 
            this.UpdateCheckInvoke.Location = new System.Drawing.Point(295, 75);
            this.UpdateCheckInvoke.Name = "UpdateCheckInvoke";
            this.UpdateCheckInvoke.Size = new System.Drawing.Size(75, 23);
            this.UpdateCheckInvoke.TabIndex = 3;
            this.UpdateCheckInvoke.Text = "Invoke";
            this.UpdateCheckInvoke.UseVisualStyleBackColor = true;
            this.UpdateCheckInvoke.Click += new System.EventHandler(this.UpdateCheckInvoke_Click);
            // 
            // LocalFolderBrowse
            // 
            this.LocalFolderBrowse.Location = new System.Drawing.Point(376, 21);
            this.LocalFolderBrowse.Name = "LocalFolderBrowse";
            this.LocalFolderBrowse.Size = new System.Drawing.Size(26, 23);
            this.LocalFolderBrowse.TabIndex = 1;
            this.LocalFolderBrowse.Text = "...";
            this.LocalFolderBrowse.UseVisualStyleBackColor = true;
            this.LocalFolderBrowse.Click += new System.EventHandler(this.LocalFolderBrowse_Click);
            // 
            // ManifestURL
            // 
            this.ManifestURL.Location = new System.Drawing.Point(81, 49);
            this.ManifestURL.Name = "ManifestURL";
            this.ManifestURL.Size = new System.Drawing.Size(289, 20);
            this.ManifestURL.TabIndex = 2;
            this.ManifestURL.Text = "http://patch.patrickshafer.com/Test.xml";
            // 
            // LocalFolder
            // 
            this.LocalFolder.Location = new System.Drawing.Point(81, 23);
            this.LocalFolder.Name = "LocalFolder";
            this.LocalFolder.Size = new System.Drawing.Size(289, 20);
            this.LocalFolder.TabIndex = 0;
            this.LocalFolder.Text = "C:\\Code\\fom-launcher\\TestData\\LocalFolder";
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
            // LocalFolderLabel
            // 
            this.LocalFolderLabel.AutoSize = true;
            this.LocalFolderLabel.Location = new System.Drawing.Point(6, 26);
            this.LocalFolderLabel.Name = "LocalFolderLabel";
            this.LocalFolderLabel.Size = new System.Drawing.Size(62, 13);
            this.LocalFolderLabel.TabIndex = 0;
            this.LocalFolderLabel.Text = "LocalFolder";
            // 
            // ApplyUpdateBox
            // 
            this.ApplyUpdateBox.Controls.Add(this.ApplyUpdateInvoke);
            this.ApplyUpdateBox.Controls.Add(this.ManifestInput);
            this.ApplyUpdateBox.Controls.Add(this.ManifestLabel);
            this.ApplyUpdateBox.Location = new System.Drawing.Point(12, 136);
            this.ApplyUpdateBox.Name = "ApplyUpdateBox";
            this.ApplyUpdateBox.Size = new System.Drawing.Size(424, 89);
            this.ApplyUpdateBox.TabIndex = 1;
            this.ApplyUpdateBox.TabStop = false;
            this.ApplyUpdateBox.Text = "ApplyUpdate()";
            // 
            // ManifestLabel
            // 
            this.ManifestLabel.AutoSize = true;
            this.ManifestLabel.Location = new System.Drawing.Point(6, 26);
            this.ManifestLabel.Name = "ManifestLabel";
            this.ManifestLabel.Size = new System.Drawing.Size(47, 13);
            this.ManifestLabel.TabIndex = 0;
            this.ManifestLabel.Text = "Manifest";
            // 
            // ManifestInput
            // 
            this.ManifestInput.AutoSize = true;
            this.ManifestInput.Location = new System.Drawing.Point(78, 26);
            this.ManifestInput.Name = "ManifestInput";
            this.ManifestInput.Size = new System.Drawing.Size(121, 13);
            this.ManifestInput.TabIndex = 1;
            this.ManifestInput.Text = "Run UpdateCheck() first";
            // 
            // ApplyUpdateInvoke
            // 
            this.ApplyUpdateInvoke.Enabled = false;
            this.ApplyUpdateInvoke.Location = new System.Drawing.Point(295, 51);
            this.ApplyUpdateInvoke.Name = "ApplyUpdateInvoke";
            this.ApplyUpdateInvoke.Size = new System.Drawing.Size(75, 23);
            this.ApplyUpdateInvoke.TabIndex = 2;
            this.ApplyUpdateInvoke.Text = "Invoke";
            this.ApplyUpdateInvoke.UseVisualStyleBackColor = true;
            this.ApplyUpdateInvoke.Click += new System.EventHandler(this.ApplyUpdateInvoke_Click);
            // 
            // DevUI
            // 
            this.AcceptButton = this.UpdateCheckInvoke;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 236);
            this.Controls.Add(this.ApplyUpdateBox);
            this.Controls.Add(this.UpdateCheckBox);
            this.Name = "DevUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FoM Launcher - Development UI";
            this.UpdateCheckBox.ResumeLayout(false);
            this.UpdateCheckBox.PerformLayout();
            this.ApplyUpdateBox.ResumeLayout(false);
            this.ApplyUpdateBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox UpdateCheckBox;
        private System.Windows.Forms.Label ManifestURLLabel;
        private System.Windows.Forms.Label LocalFolderLabel;
        private System.Windows.Forms.Button UpdateCheckInvoke;
        private System.Windows.Forms.Button LocalFolderBrowse;
        private System.Windows.Forms.TextBox ManifestURL;
        private System.Windows.Forms.TextBox LocalFolder;
        private System.Windows.Forms.GroupBox ApplyUpdateBox;
        private System.Windows.Forms.Button ApplyUpdateInvoke;
        private System.Windows.Forms.Label ManifestInput;
        private System.Windows.Forms.Label ManifestLabel;
    }
}

