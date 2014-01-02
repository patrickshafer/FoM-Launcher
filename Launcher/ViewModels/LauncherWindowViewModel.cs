using System;
using System.Net;
using System.Windows.Input;


namespace FoM.Launcher.ViewModels
{
    public class LauncherWindowViewModel:BaseViewModel
    {
        public LauncherWindowViewModel()
        {
            LauncherApp.Instance.PatchInfo.PatchStateChanged += PatchInfo_PatchStateChanged;
            LauncherApp.Instance.PatchInfo.PatchProgressChanged += PatchInfo_PatchProgressChanged;
            LauncherApp.Instance.PatchInfo.PatchCompleted += PatchInfo_PatchCompleted;
            LauncherApp.Instance.PatchInfo.AutoLaunchProgress += PatchInfo_AutoLaunchProgress;
        }

        #region Login Stuff
        private string _Username;
        private DelegateCommand _LoginCommand;
        public string Username
        {
            get { return this._Username; }
            set
            {
                this._Username = value;
                this.RaisePropertyChanged("Username");
                this._LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public string LoginErrorMessage { get { return LauncherApp.Instance.UserInfo.ErrorMessage; } }
        public ICommand LoginCommand
        {
            get
            {
                if (this._LoginCommand == null)
                    this._LoginCommand = new DelegateCommand(new Action<object>(this.ExecuteLoginCommand), new Func<object, bool>(this.CanLoginCommand));
                return this._LoginCommand;
            }
        }
        private bool CanLoginCommand(object parameter)
        {
            return !String.IsNullOrWhiteSpace(this.Username) && this.Username.Length >= 3;
        }
        private void ExecuteLoginCommand(object parameter)
        {
            System.Windows.Controls.PasswordBox PasswordBox;
            if (this.CanLoginCommand(parameter))
            {
                if (parameter is System.Windows.Controls.PasswordBox)
                {
                    PasswordBox = (System.Windows.Controls.PasswordBox)parameter;
                    LauncherApp.Instance.UserInfo.ExecuteLogin(this.Username, PasswordBox.Password);
                    this.RaisePropertyChanged("LoginErrorMessage");
                    if (!LauncherApp.Instance.UserInfo.NeedsLogin)
                        LauncherApp.Instance.PatchInfo.StartUpdate(LauncherApp.Instance.UserInfo.UpdateURL);        //start the patch process
                }
            }
        }
        #endregion

        #region Patch stuff
        public string PatchState { get { return LauncherApp.Instance.PatchInfo.PatchState.ToString(); } }
        public int PatchProgress { get { return LauncherApp.Instance.PatchInfo.PatchProgress; } }
        private int _AutoLaunchTicker = -1;

        void PatchInfo_PatchStateChanged(object sender, EventArgs e)
        {
            switch (LauncherApp.Instance.PatchInfo.PatchState)
            {
                case FoM.Launcher.Models.PatchModel.RuntimeStateEnum.ApplyUpdate:
                    LauncherApp.Instance.PatchInfo.AcquireFoMMutex();
                    break;
                case FoM.Launcher.Models.PatchModel.RuntimeStateEnum.Ready:
                    LauncherApp.Instance.PatchInfo.ReleaseFoMMutex();
                    break;
                default:
                    break;
            }
            this.RaisePropertyChanged("PatchState");
        }
        void PatchInfo_PatchProgressChanged(object sender, EventArgs e)
        {
            this.RaisePropertyChanged("PatchProgress");
        }

        private DelegateCommand _PatchCommand;
        public ICommand PatchCommand
        {
            get
            {
                if (this._PatchCommand == null)
                    this._PatchCommand = new DelegateCommand(new Action(ExecutePatchCommand));
                return this._PatchCommand;
            }
        }
        private void ExecutePatchCommand()
        {
            LauncherApp.Instance.PatchInfo.StartUpdate(LauncherApp.Instance.UserInfo.UpdateURL);
        }

        void PatchInfo_PatchCompleted(object sender, EventArgs e)
        {
            this._LaunchCommand.RaiseCanExecuteChanged();
            LauncherApp.Instance.PatchInfo.StartAutoLaunch();
        }
        private DelegateCommand _LaunchCommand;
        public ICommand LaunchCommand
        {
            get
            {
                if (this._LaunchCommand == null)
                    this._LaunchCommand = new DelegateCommand(new Action(ExecuteLaunchCommand), new Func<bool>(CanLaunchCommand));
                return this._LaunchCommand;
            }
        }
        private void ExecuteLaunchCommand()
        {
            LauncherApp.Instance.PatchInfo.LaunchFoM();
        }
        private bool CanLaunchCommand()
        {
            if (LauncherApp.Instance.PatchInfo.PatchState == Models.PatchModel.RuntimeStateEnum.Ready)
                return true;
            else
                return false;
        }
        public string LaunchCommandCaption
        {
            get
            {
                string RetVal = "Launch";
                if (this._AutoLaunchTicker >= 0)
                    RetVal = String.Format("Launching... ({0})", this._AutoLaunchTicker);
                return RetVal;
            }
        }
        void PatchInfo_AutoLaunchProgress(object sender, Models.PatchModel.AutoLaunchProgressEventArgs e)
        {
            this._AutoLaunchTicker = e.SecondsRemaining;
            this.RaisePropertyChanged("LaunchCommandCaption");
        }


        #endregion

        #region options
        //TODO upgrade options dialog to WPF
        private DelegateCommand _PreferencesCommand;
        public ICommand PreferencesCommand
        {
            get
            {
                if (this._PreferencesCommand == null)
                    this._PreferencesCommand = new DelegateCommand(new Action(this.ExecutePreferencesCommand));
                return this._PreferencesCommand;
            }
        }
        private void ExecutePreferencesCommand()
        {
            using (PreferencesUI PrefDialog = new PreferencesUI())
            {
                Preferences PrefData = Preferences.Load();

                PrefDialog.LauncherEdition = PrefData.LauncherEdition;
                PrefDialog.WindowedMode = PrefData.WindowedMode;
                PrefDialog.AutoLaunch = PrefData.AutoLaunch;

                if (PrefDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PrefData.LauncherEdition = PrefDialog.LauncherEdition;
                    PrefData.WindowedMode = PrefDialog.WindowedMode;
                    PrefData.AutoLaunch = PrefDialog.AutoLaunch;
                    PrefData.Save();
                }
            }
        }
        #endregion

        #region Links
        private DelegateCommand _URLCommand;
        public ICommand URLCommand
        {
            get
            {
                if (this._URLCommand == null)
                    this._URLCommand = new DelegateCommand(new Action<object>(this.ExecuteURLCommand), new Func<object, bool>(this.CanURLCommand));
                return this._URLCommand;
            }
        }

        private bool CanURLCommand(object parameter)
        {
            bool RetVal = false;
            string URL = string.Empty;
            Uri UriResult;
            if (parameter is string)
            {
                URL = (string)parameter;
                RetVal = Uri.TryCreate(URL, UriKind.Absolute, out UriResult) && (UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps);
            }
            return RetVal;
        }

        private void ExecuteURLCommand(object parameter)
        {
            string URL = string.Empty;
            if (parameter is string)
            {
                URL = (string)parameter;
                try
                {
                    System.Diagnostics.Process.Start(URL);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        System.Windows.MessageBox.Show(String.Format("Could not open your browser.  Please visit {0} to continue", URL), "Site link", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    else
                        throw noBrowser;
                }
            }
        }
        #endregion
    }
}
