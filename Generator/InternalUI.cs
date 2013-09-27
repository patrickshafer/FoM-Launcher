using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FoM.Generator
{
    public partial class InternalUI : Form
    {
        private PanelManager _PanelManager = new PanelManager();
        public InternalUI()
        {
            InitializeComponent();
            this.Size = new Size(580, 435);
        }

        private void InternalUI_Load(object sender, EventArgs e)
        {
            this._PanelManager.NextButton = BtnNext;
            this._PanelManager.BackButton = BtnBack;

            this._PanelManager.AddPanel(Wizard1);
            this._PanelManager.AddPanel(Wizard2);
            this._PanelManager.AddPanel(Wizard3);
            this._PanelManager.AddPanel(Wizard4);
            this._PanelManager.AddPanel(Wizard5);
            this._PanelManager.AddPanel(Wizard6);

            this._PanelManager.First();

            this.FormClosing += InternalUI_FormClosing;

        }

        void InternalUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
                if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
        }

        private class PanelManager
        {
            private List<Panel> Panels = new List<Panel>();
            private int CurrentIndex = 0;

            public Button NextButton { get; set; }
            public Button BackButton { get; set; }

            public void AddPanel(Panel NewPanel)
            {
                this.Panels.Add(NewPanel);
            }
            private void ShowIndex(int Index)
            {
                Point PanelLocation = new Point(163, 0);
                if (this.Panels[CurrentIndex].Visible)
                    this.Panels[CurrentIndex].Hide();
                this.CurrentIndex = Index;
                this.Panels[CurrentIndex].Location = PanelLocation;
                this.Panels[CurrentIndex].Show();
            }
            public void First()
            {
                this.ShowIndex(0);
                this.BackButton.Enabled = false;
                if (Panels.Count > 1)
                    this.NextButton.Enabled = true;
                else
                    this.NextButton.Enabled = false;
            }
            public void Next()
            {
                if (Panels.Count > CurrentIndex + 1)
                {
                    this.ShowIndex(CurrentIndex + 1);

                    this.BackButton.Enabled = true;

                    this.NextButton.Enabled = Panels.Count > CurrentIndex + 1;
                }
            }
            public void Back()
            {
                if (CurrentIndex > 0)
                {
                    this.ShowIndex(CurrentIndex - 1);
                    this.NextButton.Enabled = true;

                    this.BackButton.Enabled = CurrentIndex > 0;
                }
            }
        }

        private void W2Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog BrowseDlg = new FolderBrowserDialog();

            if (LocalFolder.Text.Length > 0)
                if (Directory.Exists(LocalFolder.Text))
                    BrowseDlg.SelectedPath = LocalFolder.Text;

            BrowseDlg.Description = "Browse to the folder you want to create the patch from";
            if (BrowseDlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                LocalFolder.Text = BrowseDlg.SelectedPath;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            _PanelManager.Next();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _PanelManager.Back();
        }

        private void W3Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog BrowseDlg = new FolderBrowserDialog();

            if (StagingFolder.Text.Length > 0)
                if (Directory.Exists(StagingFolder.Text))
                    BrowseDlg.SelectedPath = StagingFolder.Text;

            BrowseDlg.Description = "Browse to the folder you want to stage the patch to";
            if (BrowseDlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                StagingFolder.Text = BrowseDlg.SelectedPath;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;
            if (LocalFolder.Text.Trim().Length == 0)
                ErrorMessage = "You must select a local folder";
            if (!Directory.Exists(LocalFolder.Text.Trim()))
                ErrorMessage = "The Local Folder does not exist";
            if (StagingFolder.Text.Trim().Length == 0)
                ErrorMessage = "You must select a staging folder";
            if (!Directory.Exists(StagingFolder.Text.Trim()) && ErrorMessage == String.Empty)
                Directory.CreateDirectory(StagingFolder.Text.Trim());
            if (Channel.Text.Trim().Length == 0)
                ErrorMessage = "You must enter a channel name";
            if (Channel.Text.Trim().Contains(" "))
                ErrorMessage = "Channel names can not have spaces";
            if (PatchURL.Text.Trim().Length == 0)
                ErrorMessage = "You need a patch URL";

            if (ErrorMessage != String.Empty)
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                W6Result.Visible = true;
                this.Refresh();
                PatchLib.PatchManager.CreatePatch(LocalFolder.Text.Trim(), StagingFolder.Text.Trim(), Channel.Text.Trim(), PatchURL.Text.Trim());
                W6Result.Text = "Patch Completed.\r\n\r\n";
                foreach (KeyValuePair<string, UInt32> CRCValue in GetCRC32s())
                {
                    W6Result.Text += String.Format("REPLACE INTO CRC32(filename, crc32) VALUES ('{0}', {1});\r\n", CRCValue.Key, CRCValue.Value);
                }
            }
        }
        private Dictionary<string, UInt32> GetCRC32s()
        {
            Dictionary<string, UInt32> CRCList = new Dictionary<string, UInt32>();
            List<string> FileList = new List<string>();
            Crc32 Hasher = new Crc32();
            string FileName;
            byte[] HashResult;
            string HashBuffer;

            FileList.Add(@"fom_client.exe");
            FileList.Add(@"Resources/CShell.dll");
            FileList.Add(@"Resources/Object.lto");

            foreach (string SrcFile in FileList)
            {
                FileName = Path.Combine(LocalFolder.Text.Trim(), SrcFile);
                if (File.Exists(FileName))
                {
                    HashResult = Hasher.ComputeHash(File.ReadAllBytes(FileName));
                    HashBuffer = string.Empty;
                    foreach (byte b in HashResult)
                        HashBuffer += b.ToString("x2");
                    CRCList["/" + SrcFile] = Convert.ToUInt32(HashBuffer, 16);
                }
            }
            return CRCList;
        }
    }
}
