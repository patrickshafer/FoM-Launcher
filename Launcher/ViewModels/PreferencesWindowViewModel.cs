using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoM.Launcher.ViewModels
{
    class PreferencesWindowViewModel:BaseViewModel
    {
        public List<string> LauncherURLList { get { return LauncherApp.Instance.PreferenceInfo.LauncherURLList; } }
        public string LauncherURL { get { return LauncherApp.Instance.PreferenceInfo.LauncherURL; } }
        public List<string> FoMURLList { get { return LauncherApp.Instance.PreferenceInfo.FoMURLList; } }
        public string FoMURL { get { return LauncherApp.Instance.PreferenceInfo.FoMURL; } }
        public bool AutoLaunch { get { return LauncherApp.Instance.PreferenceInfo.AutoLaunch; } }
        public bool WindowedMode { get { return LauncherApp.Instance.PreferenceInfo.WindowedMode; } }
    }
}
