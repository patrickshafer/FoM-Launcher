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
    public partial class DevUI : Form
    {
        public DevUI()
        {
            InitializeComponent();
        }

        private void LocalFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            FolderDialog.Description = "Please select a folder read files from";
            FolderDialog.ShowNewFolderButton = true;
            if (Directory.Exists(LocalFolderText.Text))
                FolderDialog.SelectedPath = LocalFolderText.Text;
            if (FolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (Directory.Exists(FolderDialog.SelectedPath))
                    LocalFolderText.Text = FolderDialog.SelectedPath;
        }

        private void PatchFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            FolderDialog.Description = "Please select a folder stage the patch to";
            FolderDialog.ShowNewFolderButton = true;
            if (Directory.Exists(PatchFolderText.Text))
                FolderDialog.SelectedPath = PatchFolderText.Text;
            if (FolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (Directory.Exists(FolderDialog.SelectedPath))
                    PatchFolderText.Text = FolderDialog.SelectedPath;
        }

        private void CreatePatch_Click(object sender, EventArgs e)
        {
            PatchLib.PatchManager.CreatePatch(LocalFolderText.Text, PatchFolderText.Text, ManifestNameText.Text, DistributionURLText.Text);
            MessageBox.Show("Patch Created", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
