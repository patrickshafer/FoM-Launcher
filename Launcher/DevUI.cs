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

namespace FoM.Launcher
{
    public partial class DevUI : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FoM.PatchLib.Manifest _Manifest;
        
        private FoM.PatchLib.Manifest _SelfUpdateManifest;
        private string _SelfUpdateURL = "http://patch.patrickshafer.com/launcher.xml";
        private bool _Bootstrap = false;

        public DevUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Folder browse dialog for invoking the ApplyPatch() function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            FolderDialog.Description = "Please select a folder to apply the patch to";
            FolderDialog.ShowNewFolderButton = true;
            if(Directory.Exists(LocalFolder.Text))
                FolderDialog.SelectedPath = LocalFolder.Text;
            if (FolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if(Directory.Exists(FolderDialog.SelectedPath))
                    LocalFolder.Text = FolderDialog.SelectedPath;

        }

        /// <summary>
        /// Event handler for the button to invoke ApplyPatch()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCheckInvoke_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;

            if (LocalFolder.Text.Trim().Length == 0)
                ErrorMessage = "LocalFolder is empty";
            else
                if (!Directory.Exists(LocalFolder.Text.Trim()))
                    ErrorMessage = String.Format("LocalFolder \"{0}\" does not exist", LocalFolder.Text.Trim());

            if (ManifestURL.Text.Trim().Length == 0)
                ErrorMessage = "ManifestURL is empty";

            if (ErrorMessage.Length > 0)
            {
                MessageBox.Show("Validation Error: " + ErrorMessage, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //FoM.PatchLib.PatchManager.ApplyPatch(LocalFolder.Text, ManifestURL.Text);
                this._Manifest = FoM.PatchLib.PatchManager.UpdateCheck(LocalFolder.Text, ManifestURL.Text);

                ManifestInput.Text = String.Format("Update Needed: {0:True;0;False}", _Manifest.NeedsUpdate.GetHashCode());
                ApplyUpdateInvoke.Enabled = _Manifest.NeedsUpdate;

                MessageBox.Show(String.Format("UpdateCheck() returned {0:True;0;False}", this._Manifest.NeedsUpdate.GetHashCode()), "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ApplyUpdateInvoke_Click(object sender, EventArgs e)
        {
            FoM.PatchLib.PatchManager.ApplyPatch(this._Manifest);
            MessageBox.Show("ApplyPatch() complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SelfUpdateCheckButton_Click(object sender, EventArgs e)
        {
            Log.Info("Entering SelfUpdateCheckButton_Click");
            this._SelfUpdateManifest = FoM.PatchLib.PatchManager.UpdateCheck(Directory.GetCurrentDirectory(), this._SelfUpdateURL);
            MessageBox.Show(String.Format("SelfUpdateCheck: Needs Update: {0:True;0;False}", this._SelfUpdateManifest.NeedsUpdate), "Self-Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BootstrapButton.Enabled = this._SelfUpdateManifest.NeedsUpdate && !this._Bootstrap;
            InstallButton.Enabled = this._SelfUpdateManifest.NeedsUpdate && this._Bootstrap;
        }

        private void BootstrapButton_Click(object sender, EventArgs e)
        {
            string BootstrapPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), String.Format("_{0}", Path.GetFileName(Application.ExecutablePath)));
            Log.Info(string.Format("Bootstrap Path: {0}", BootstrapPath));
            File.Copy(Application.ExecutablePath, BootstrapPath, true);
            System.Diagnostics.Process.Start(BootstrapPath);
            Application.Exit();
        }

        private void DevUI_Load(object sender, EventArgs e)
        {
            LocalFolder.Text = Path.GetDirectoryName(Application.ExecutablePath);
            string ExeName = Path.GetFileName(Application.ExecutablePath);
            if (ExeName.StartsWith("_"))
            {
                this._Bootstrap = true;
                ModeText.Text = "Bootstrap";
            }
            else
            {
                if (File.Exists("_" + ExeName))
                    File.Delete("_" + ExeName);
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            FoM.PatchLib.PatchManager.ApplyPatch(this._SelfUpdateManifest);

            string MainExe = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.GetFileName(Application.ExecutablePath).Substring(1));
            System.Diagnostics.Process.Start(MainExe);
            Application.Exit();
        }
    }
}
