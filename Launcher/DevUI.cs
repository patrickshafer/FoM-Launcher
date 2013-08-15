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
        private void InvokeButton_Click(object sender, EventArgs e)
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
                FoM.PatchLib.PatchManager.ApplyPatch(LocalFolder.Text, ManifestURL.Text);
                MessageBox.Show("ApplyPatch() complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
