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
            this.WindowedModeLabel = new System.Windows.Forms.Label();
            this.WindowedModeCheck = new System.Windows.Forms.CheckBox();
            this.AutoLaunchLabel = new System.Windows.Forms.Label();
            this.AutoLaunchCheck = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButtonx = new System.Windows.Forms.Button();
            this.LauncherEditionGroup = new System.Windows.Forms.GroupBox();
            this.LiveRadio = new System.Windows.Forms.RadioButton();
            this.DevRadio = new System.Windows.Forms.RadioButton();
            this.LauncherEditionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // WindowedModeLabel
            // 
            this.WindowedModeLabel.AutoSize = true;
            this.WindowedModeLabel.Location = new System.Drawing.Point(12, 99);
            this.WindowedModeLabel.Name = "WindowedModeLabel";
            this.WindowedModeLabel.Size = new System.Drawing.Size(88, 13);
            this.WindowedModeLabel.TabIndex = 2;
            this.WindowedModeLabel.Text = "Windowed Mode";
            // 
            // WindowedModeCheck
            // 
            this.WindowedModeCheck.AutoSize = true;
            this.WindowedModeCheck.Location = new System.Drawing.Point(41, 115);
            this.WindowedModeCheck.Name = "WindowedModeCheck";
            this.WindowedModeCheck.Size = new System.Drawing.Size(65, 17);
            this.WindowedModeCheck.TabIndex = 3;
            this.WindowedModeCheck.Text = "Enabled";
            this.WindowedModeCheck.UseVisualStyleBackColor = true;
            // 
            // AutoLaunchLabel
            // 
            this.AutoLaunchLabel.AutoSize = true;
            this.AutoLaunchLabel.Location = new System.Drawing.Point(12, 135);
            this.AutoLaunchLabel.Name = "AutoLaunchLabel";
            this.AutoLaunchLabel.Size = new System.Drawing.Size(65, 13);
            this.AutoLaunchLabel.TabIndex = 4;
            this.AutoLaunchLabel.Text = "AutoLaunch";
            // 
            // AutoLaunchCheck
            // 
            this.AutoLaunchCheck.AutoSize = true;
            this.AutoLaunchCheck.Enabled = false;
            this.AutoLaunchCheck.Location = new System.Drawing.Point(41, 151);
            this.AutoLaunchCheck.Name = "AutoLaunchCheck";
            this.AutoLaunchCheck.Size = new System.Drawing.Size(65, 17);
            this.AutoLaunchCheck.TabIndex = 5;
            this.AutoLaunchCheck.Text = "Enabled";
            this.AutoLaunchCheck.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(15, 185);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButtonx
            // 
            this.CancelButtonx.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtonx.Location = new System.Drawing.Point(96, 185);
            this.CancelButtonx.Name = "CancelButtonx";
            this.CancelButtonx.Size = new System.Drawing.Size(75, 23);
            this.CancelButtonx.TabIndex = 7;
            this.CancelButtonx.Text = "Cancel";
            this.CancelButtonx.UseVisualStyleBackColor = true;
            // 
            // LauncherEditionGroup
            // 
            this.LauncherEditionGroup.Controls.Add(this.DevRadio);
            this.LauncherEditionGroup.Controls.Add(this.LiveRadio);
            this.LauncherEditionGroup.Location = new System.Drawing.Point(15, 12);
            this.LauncherEditionGroup.Name = "LauncherEditionGroup";
            this.LauncherEditionGroup.Size = new System.Drawing.Size(156, 72);
            this.LauncherEditionGroup.TabIndex = 8;
            this.LauncherEditionGroup.TabStop = false;
            this.LauncherEditionGroup.Text = "Launcher Edition";
            // 
            // LiveRadio
            // 
            this.LiveRadio.AutoSize = true;
            this.LiveRadio.Checked = true;
            this.LiveRadio.Location = new System.Drawing.Point(6, 19);
            this.LiveRadio.Name = "LiveRadio";
            this.LiveRadio.Size = new System.Drawing.Size(77, 17);
            this.LiveRadio.TabIndex = 0;
            this.LiveRadio.TabStop = true;
            this.LiveRadio.Text = "Alpha/Live";
            this.LiveRadio.UseVisualStyleBackColor = true;
            // 
            // DevRadio
            // 
            this.DevRadio.AutoSize = true;
            this.DevRadio.Location = new System.Drawing.Point(6, 42);
            this.DevRadio.Name = "DevRadio";
            this.DevRadio.Size = new System.Drawing.Size(120, 17);
            this.DevRadio.TabIndex = 1;
            this.DevRadio.Text = "Alpha/Development";
            this.DevRadio.UseVisualStyleBackColor = true;
            // 
            // PreferencesUI
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 225);
            this.Controls.Add(this.LauncherEditionGroup);
            this.Controls.Add(this.CancelButtonx);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AutoLaunchCheck);
            this.Controls.Add(this.AutoLaunchLabel);
            this.Controls.Add(this.WindowedModeCheck);
            this.Controls.Add(this.WindowedModeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.LauncherEditionGroup.ResumeLayout(false);
            this.LauncherEditionGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WindowedModeLabel;
        private System.Windows.Forms.CheckBox WindowedModeCheck;
        private System.Windows.Forms.Label AutoLaunchLabel;
        private System.Windows.Forms.CheckBox AutoLaunchCheck;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButtonx;
        private System.Windows.Forms.GroupBox LauncherEditionGroup;
        private System.Windows.Forms.RadioButton DevRadio;
        private System.Windows.Forms.RadioButton LiveRadio;
    }
}