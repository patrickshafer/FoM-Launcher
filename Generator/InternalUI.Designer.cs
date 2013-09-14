namespace FoM.Generator
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
            this.SidebarPicture = new System.Windows.Forms.PictureBox();
            this.Wizard1 = new System.Windows.Forms.Panel();
            this.W1IntroText = new System.Windows.Forms.Label();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.Wizard2 = new System.Windows.Forms.Panel();
            this.W2Browse = new System.Windows.Forms.Button();
            this.LocalFolder = new System.Windows.Forms.TextBox();
            this.W2Instructions = new System.Windows.Forms.Label();
            this.Wizard3 = new System.Windows.Forms.Panel();
            this.W3Browse = new System.Windows.Forms.Button();
            this.StagingFolder = new System.Windows.Forms.TextBox();
            this.W3Instructions = new System.Windows.Forms.Label();
            this.Wizard4 = new System.Windows.Forms.Panel();
            this.Channel = new System.Windows.Forms.ComboBox();
            this.W4Instructions = new System.Windows.Forms.Label();
            this.Wizard5 = new System.Windows.Forms.Panel();
            this.PatchURL = new System.Windows.Forms.TextBox();
            this.W5Intro = new System.Windows.Forms.Label();
            this.Wizard6 = new System.Windows.Forms.Panel();
            this.W6Wait = new System.Windows.Forms.Label();
            this.BtnCreate = new System.Windows.Forms.Button();
            this.W6Intro = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SidebarPicture)).BeginInit();
            this.Wizard1.SuspendLayout();
            this.Wizard2.SuspendLayout();
            this.Wizard3.SuspendLayout();
            this.Wizard4.SuspendLayout();
            this.Wizard5.SuspendLayout();
            this.Wizard6.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidebarPicture
            // 
            this.SidebarPicture.Image = ((System.Drawing.Image)(resources.GetObject("SidebarPicture.Image")));
            this.SidebarPicture.Location = new System.Drawing.Point(0, 0);
            this.SidebarPicture.Name = "SidebarPicture";
            this.SidebarPicture.Size = new System.Drawing.Size(157, 393);
            this.SidebarPicture.TabIndex = 1;
            this.SidebarPicture.TabStop = false;
            // 
            // Wizard1
            // 
            this.Wizard1.Controls.Add(this.W1IntroText);
            this.Wizard1.Location = new System.Drawing.Point(163, 0);
            this.Wizard1.Name = "Wizard1";
            this.Wizard1.Size = new System.Drawing.Size(385, 266);
            this.Wizard1.TabIndex = 2;
            // 
            // W1IntroText
            // 
            this.W1IntroText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W1IntroText.Location = new System.Drawing.Point(3, 9);
            this.W1IntroText.Name = "W1IntroText";
            this.W1IntroText.Size = new System.Drawing.Size(356, 191);
            this.W1IntroText.TabIndex = 0;
            this.W1IntroText.Text = resources.GetString("W1IntroText.Text");
            // 
            // BtnBack
            // 
            this.BtnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnBack.Location = new System.Drawing.Point(291, 358);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(75, 23);
            this.BtnBack.TabIndex = 3;
            this.BtnBack.Text = "&Back";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Location = new System.Drawing.Point(372, 358);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(75, 23);
            this.BtnNext.TabIndex = 4;
            this.BtnNext.Text = "&Next";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(473, 358);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Wizard2
            // 
            this.Wizard2.Controls.Add(this.W2Browse);
            this.Wizard2.Controls.Add(this.LocalFolder);
            this.Wizard2.Controls.Add(this.W2Instructions);
            this.Wizard2.Location = new System.Drawing.Point(554, 0);
            this.Wizard2.Name = "Wizard2";
            this.Wizard2.Size = new System.Drawing.Size(385, 266);
            this.Wizard2.TabIndex = 6;
            // 
            // W2Browse
            // 
            this.W2Browse.Location = new System.Drawing.Point(332, 105);
            this.W2Browse.Name = "W2Browse";
            this.W2Browse.Size = new System.Drawing.Size(29, 23);
            this.W2Browse.TabIndex = 2;
            this.W2Browse.Text = "...";
            this.W2Browse.UseVisualStyleBackColor = true;
            this.W2Browse.Click += new System.EventHandler(this.W2Browse_Click);
            // 
            // LocalFolder
            // 
            this.LocalFolder.Location = new System.Drawing.Point(6, 108);
            this.LocalFolder.Name = "LocalFolder";
            this.LocalFolder.Size = new System.Drawing.Size(320, 20);
            this.LocalFolder.TabIndex = 1;
            // 
            // W2Instructions
            // 
            this.W2Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W2Instructions.Location = new System.Drawing.Point(3, 9);
            this.W2Instructions.Name = "W2Instructions";
            this.W2Instructions.Size = new System.Drawing.Size(379, 96);
            this.W2Instructions.TabIndex = 0;
            this.W2Instructions.Text = "Please select a folder to create the patch from.\r\n\r\nNote: this folder must not co" +
    "ntain a copy of the launcher.  Also, it should not have an autoexec.cfg or other" +
    " client-generated files.";
            // 
            // Wizard3
            // 
            this.Wizard3.Controls.Add(this.W3Browse);
            this.Wizard3.Controls.Add(this.StagingFolder);
            this.Wizard3.Controls.Add(this.W3Instructions);
            this.Wizard3.Location = new System.Drawing.Point(554, 272);
            this.Wizard3.Name = "Wizard3";
            this.Wizard3.Size = new System.Drawing.Size(385, 266);
            this.Wizard3.TabIndex = 7;
            // 
            // W3Browse
            // 
            this.W3Browse.Location = new System.Drawing.Point(332, 209);
            this.W3Browse.Name = "W3Browse";
            this.W3Browse.Size = new System.Drawing.Size(29, 23);
            this.W3Browse.TabIndex = 2;
            this.W3Browse.Text = "...";
            this.W3Browse.UseVisualStyleBackColor = true;
            this.W3Browse.Click += new System.EventHandler(this.W3Browse_Click);
            // 
            // StagingFolder
            // 
            this.StagingFolder.Location = new System.Drawing.Point(6, 211);
            this.StagingFolder.Name = "StagingFolder";
            this.StagingFolder.Size = new System.Drawing.Size(320, 20);
            this.StagingFolder.TabIndex = 1;
            // 
            // W3Instructions
            // 
            this.W3Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W3Instructions.Location = new System.Drawing.Point(3, 11);
            this.W3Instructions.Name = "W3Instructions";
            this.W3Instructions.Size = new System.Drawing.Size(379, 197);
            this.W3Instructions.TabIndex = 0;
            this.W3Instructions.Text = resources.GetString("W3Instructions.Text");
            // 
            // Wizard4
            // 
            this.Wizard4.Controls.Add(this.Channel);
            this.Wizard4.Controls.Add(this.W4Instructions);
            this.Wizard4.Location = new System.Drawing.Point(163, 408);
            this.Wizard4.Name = "Wizard4";
            this.Wizard4.Size = new System.Drawing.Size(385, 266);
            this.Wizard4.TabIndex = 8;
            // 
            // Channel
            // 
            this.Channel.FormattingEnabled = true;
            this.Channel.Items.AddRange(new object[] {
            "fom-alpha",
            "launcher-alpha",
            "generator-alpha"});
            this.Channel.Location = new System.Drawing.Point(88, 222);
            this.Channel.Name = "Channel";
            this.Channel.Size = new System.Drawing.Size(196, 21);
            this.Channel.TabIndex = 1;
            this.Channel.Text = "fom-alpha";
            // 
            // W4Instructions
            // 
            this.W4Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W4Instructions.Location = new System.Drawing.Point(3, 9);
            this.W4Instructions.Name = "W4Instructions";
            this.W4Instructions.Size = new System.Drawing.Size(379, 210);
            this.W4Instructions.TabIndex = 0;
            this.W4Instructions.Text = resources.GetString("W4Instructions.Text");
            // 
            // Wizard5
            // 
            this.Wizard5.Controls.Add(this.PatchURL);
            this.Wizard5.Controls.Add(this.W5Intro);
            this.Wizard5.Location = new System.Drawing.Point(554, 544);
            this.Wizard5.Name = "Wizard5";
            this.Wizard5.Size = new System.Drawing.Size(385, 266);
            this.Wizard5.TabIndex = 9;
            // 
            // PatchURL
            // 
            this.PatchURL.Location = new System.Drawing.Point(21, 158);
            this.PatchURL.Name = "PatchURL";
            this.PatchURL.Size = new System.Drawing.Size(340, 20);
            this.PatchURL.TabIndex = 1;
            this.PatchURL.Text = "http://199.192.201.38/launcher/";
            // 
            // W5Intro
            // 
            this.W5Intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W5Intro.Location = new System.Drawing.Point(3, 9);
            this.W5Intro.Name = "W5Intro";
            this.W5Intro.Size = new System.Drawing.Size(379, 121);
            this.W5Intro.TabIndex = 0;
            this.W5Intro.Text = resources.GetString("W5Intro.Text");
            // 
            // Wizard6
            // 
            this.Wizard6.Controls.Add(this.W6Wait);
            this.Wizard6.Controls.Add(this.BtnCreate);
            this.Wizard6.Controls.Add(this.W6Intro);
            this.Wizard6.Location = new System.Drawing.Point(160, 680);
            this.Wizard6.Name = "Wizard6";
            this.Wizard6.Size = new System.Drawing.Size(385, 266);
            this.Wizard6.TabIndex = 10;
            // 
            // W6Wait
            // 
            this.W6Wait.Location = new System.Drawing.Point(67, 108);
            this.W6Wait.Name = "W6Wait";
            this.W6Wait.Size = new System.Drawing.Size(269, 61);
            this.W6Wait.TabIndex = 2;
            this.W6Wait.Text = "Please wait while the patch is created.  As Tick hasn\'t bothered to create an asy" +
    "ncronous version of the CreatePatch() function, the UI will be locked/frozen unt" +
    "il this is complete.";
            this.W6Wait.Visible = false;
            // 
            // BtnCreate
            // 
            this.BtnCreate.Location = new System.Drawing.Point(131, 60);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(104, 23);
            this.BtnCreate.TabIndex = 1;
            this.BtnCreate.Text = "Create Patch";
            this.BtnCreate.UseVisualStyleBackColor = true;
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // W6Intro
            // 
            this.W6Intro.AutoSize = true;
            this.W6Intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.W6Intro.Location = new System.Drawing.Point(141, 9);
            this.W6Intro.Name = "W6Intro";
            this.W6Intro.Size = new System.Drawing.Size(81, 17);
            this.W6Intro.TabIndex = 0;
            this.W6Intro.Text = "Execution...";
            // 
            // InternalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(951, 960);
            this.Controls.Add(this.Wizard6);
            this.Controls.Add(this.Wizard5);
            this.Controls.Add(this.Wizard4);
            this.Controls.Add(this.Wizard3);
            this.Controls.Add(this.Wizard2);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.Wizard1);
            this.Controls.Add(this.SidebarPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "InternalUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FoD Generator - Internal";
            this.Load += new System.EventHandler(this.InternalUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SidebarPicture)).EndInit();
            this.Wizard1.ResumeLayout(false);
            this.Wizard2.ResumeLayout(false);
            this.Wizard2.PerformLayout();
            this.Wizard3.ResumeLayout(false);
            this.Wizard3.PerformLayout();
            this.Wizard4.ResumeLayout(false);
            this.Wizard5.ResumeLayout(false);
            this.Wizard5.PerformLayout();
            this.Wizard6.ResumeLayout(false);
            this.Wizard6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox SidebarPicture;
        private System.Windows.Forms.Panel Wizard1;
        private System.Windows.Forms.Label W1IntroText;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Panel Wizard2;
        private System.Windows.Forms.Button W2Browse;
        private System.Windows.Forms.TextBox LocalFolder;
        private System.Windows.Forms.Label W2Instructions;
        private System.Windows.Forms.Panel Wizard3;
        private System.Windows.Forms.Label W3Instructions;
        private System.Windows.Forms.Button W3Browse;
        private System.Windows.Forms.TextBox StagingFolder;
        private System.Windows.Forms.Panel Wizard4;
        private System.Windows.Forms.ComboBox Channel;
        private System.Windows.Forms.Label W4Instructions;
        private System.Windows.Forms.Panel Wizard5;
        private System.Windows.Forms.Label W5Intro;
        private System.Windows.Forms.TextBox PatchURL;
        private System.Windows.Forms.Panel Wizard6;
        private System.Windows.Forms.Label W6Wait;
        private System.Windows.Forms.Button BtnCreate;
        private System.Windows.Forms.Label W6Intro;
    }
}