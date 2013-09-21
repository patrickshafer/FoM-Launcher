using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoM.Launcher
{
    public partial class PreferencesUI : Form
    {
        public string LauncherURL { get { return this.LauncherURLText.Text; } set { this.LauncherURLText.Text = value; } }
        public bool WindowedMode { get { return this.WindowedModeCheck.Checked; } set { this.WindowedModeCheck.Checked = value; } }
        public bool AutoLaunch { get { return this.AutoLaunchCheck.Checked; } set { this.AutoLaunchCheck.Checked = value; } }

        public PreferencesUI()
        {
            InitializeComponent();
        }
    }
}
