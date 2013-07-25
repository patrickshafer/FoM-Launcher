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

namespace Launcher
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
            FolderDialog.Description = "Please select a folder to apply the patch to";
            FolderDialog.ShowNewFolderButton = true;
            if(Directory.Exists(LocalFolder.Text))
                FolderDialog.SelectedPath = LocalFolder.Text;
            if (FolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if(Directory.Exists(FolderDialog.SelectedPath))
                    LocalFolder.Text = FolderDialog.SelectedPath;

        }
    }
}
