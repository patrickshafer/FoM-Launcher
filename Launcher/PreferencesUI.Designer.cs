namespace FoM.Launcher
{
    partial class PreferencesUI
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
            this.LauncherURLLabel = new System.Windows.Forms.Label();
            this.LauncherURLText = new System.Windows.Forms.TextBox();
            this.WindowedModeLabel = new System.Windows.Forms.Label();
            this.WindowedModeCheck = new System.Windows.Forms.CheckBox();
            this.AutoLaunchLabel = new System.Windows.Forms.Label();
            this.AutoLaunchCheck = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LauncherURLLabel
            // 
            this.LauncherURLLabel.AutoSize = true;
            this.LauncherURLLabel.Location = new System.Drawing.Point(12, 9);
            this.LauncherURLLabel.Name = "LauncherURLLabel";
            this.LauncherURLLabel.Size = new System.Drawing.Size(109, 13);
            this.LauncherURLLabel.TabIndex = 0;
            this.LauncherURLLabel.Text = "Laucher Update URL";
            // 
            // LauncherURLText
            // 
            this.LauncherURLText.Location = new System.Drawing.Point(41, 25);
            this.LauncherURLText.Name = "LauncherURLText";
            this.LauncherURLText.ReadOnly = true;
            this.LauncherURLText.Size = new System.Drawing.Size(193, 20);
            this.LauncherURLText.TabIndex = 1;
            // 
            // WindowedModeLabel
            // 
            this.WindowedModeLabel.AutoSize = true;
            this.WindowedModeLabel.Location = new System.Drawing.Point(12, 59);
            this.WindowedModeLabel.Name = "WindowedModeLabel";
            this.WindowedModeLabel.Size = new System.Drawing.Size(88, 13);
            this.WindowedModeLabel.TabIndex = 2;
            this.WindowedModeLabel.Text = "Windowed Mode";
            // 
            // WindowedModeCheck
            // 
            this.WindowedModeCheck.AutoSize = true;
            this.WindowedModeCheck.Location = new System.Drawing.Point(41, 75);
            this.WindowedModeCheck.Name = "WindowedModeCheck";
            this.WindowedModeCheck.Size = new System.Drawing.Size(65, 17);
            this.WindowedModeCheck.TabIndex = 3;
            this.WindowedModeCheck.Text = "Enabled";
            this.WindowedModeCheck.UseVisualStyleBackColor = true;
            // 
            // AutoLaunchLabel
            // 
            this.AutoLaunchLabel.AutoSize = true;
            this.AutoLaunchLabel.Location = new System.Drawing.Point(12, 109);
            this.AutoLaunchLabel.Name = "AutoLaunchLabel";
            this.AutoLaunchLabel.Size = new System.Drawing.Size(65, 13);
            this.AutoLaunchLabel.TabIndex = 4;
            this.AutoLaunchLabel.Text = "AutoLaunch";
            // 
            // AutoLaunchCheck
            // 
            this.AutoLaunchCheck.AutoSize = true;
            this.AutoLaunchCheck.Location = new System.Drawing.Point(41, 125);
            this.AutoLaunchCheck.Name = "AutoLaunchCheck";
            this.AutoLaunchCheck.Size = new System.Drawing.Size(65, 17);
            this.AutoLaunchCheck.TabIndex = 5;
            this.AutoLaunchCheck.Text = "Enabled";
            this.AutoLaunchCheck.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(96, 165);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(177, 165);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // PreferencesUI
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 200);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AutoLaunchCheck);
            this.Controls.Add(this.AutoLaunchLabel);
            this.Controls.Add(this.WindowedModeCheck);
            this.Controls.Add(this.WindowedModeLabel);
            this.Controls.Add(this.LauncherURLText);
            this.Controls.Add(this.LauncherURLLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LauncherURLLabel;
        private System.Windows.Forms.TextBox LauncherURLText;
        private System.Windows.Forms.Label WindowedModeLabel;
        private System.Windows.Forms.CheckBox WindowedModeCheck;
        private System.Windows.Forms.Label AutoLaunchLabel;
        private System.Windows.Forms.CheckBox AutoLaunchCheck;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}