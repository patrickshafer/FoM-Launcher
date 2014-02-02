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
        public string LauncherURL
        {
            get { return LauncherApp.Instance.PreferenceInfo.LauncherURL; }
            set { LauncherApp.Instance.PreferenceInfo.LauncherURL = value; }
        }
        public List<string> FoMURLList { get { return LauncherApp.Instance.PreferenceInfo.FoMURLList; } }
        public string FoMURL
        {
            get { return LauncherApp.Instance.PreferenceInfo.FoMURL; }
            set { LauncherApp.Instance.PreferenceInfo.FoMURL = value; }
        }
        public bool AutoLaunch
        {
            get { return LauncherApp.Instance.PreferenceInfo.AutoLaunch; }
            set { LauncherApp.Instance.PreferenceInfo.AutoLaunch = value; }
        }
        public bool WindowedMode
        {
            get { return LauncherApp.Instance.PreferenceInfo.WindowedMode; }
            set { LauncherApp.Instance.PreferenceInfo.WindowedMode = value; }
        }

        private DelegateCommand _SavePreferences;
        public System.Windows.Input.ICommand SavePreferences
        {
            get
            {
                if (this._SavePreferences == null)
                    this._SavePreferences = new DelegateCommand(new Action(this.SavePreferencesExecute));
                return this._SavePreferences;
            }
        }
        private void SavePreferencesExecute()
        {
            LauncherApp.Instance.PreferenceInfo.Save();
        }
    }
}
