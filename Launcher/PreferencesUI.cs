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
        public Preferences.LauncherEditionEnum LauncherEdition
        {
            get
            {
                if (this.DevRadio.Checked)
                    return Preferences.LauncherEditionEnum.Development;
                else
                    return Preferences.LauncherEditionEnum.Live;
            }
            set
            {
                switch (value)
                {
                    case Preferences.LauncherEditionEnum.Development:
                        this.DevRadio.Checked = true;
                        break;
                    case Preferences.LauncherEditionEnum.Live:
                    default:
                        this.LiveRadio.Checked = true;
                        break;
                }
            }
        }
        public bool WindowedMode { get { return this.WindowedModeCheck.Checked; } set { this.WindowedModeCheck.Checked = value; } }
        public bool AutoLaunch { get { return this.AutoLaunchCheck.Checked; } set { this.AutoLaunchCheck.Checked = value; } }

        public PreferencesUI()
        {
            InitializeComponent();
        }
    }
}
