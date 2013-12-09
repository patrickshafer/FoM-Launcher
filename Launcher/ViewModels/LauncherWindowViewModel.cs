using System;
using System.Windows.Input;


namespace FoM.Launcher.ViewModels
{
    public class LauncherWindowViewModel:BaseViewModel
    {
        public LauncherWindowViewModel()
        {
            LauncherApp.Instance.PatchInfo.PatchStateChanged += PatchInfo_PatchStateChanged;
            LauncherApp.Instance.PatchInfo.PatchProgressChanged += PatchInfo_PatchProgressChanged;
        }

        #region Login Stuff
        private string _Username;
        private string _Password;
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
        public string Password
        {
            get { return this._Password; }
            set
            {
                this._Password = value;
                this.RaisePropertyChanged("Password");
                this._LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public string LoginErrorMessage { get { return LauncherApp.Instance.UserInfo.ErrorMessage; } }
        public ICommand LoginCommand
        {
            get
            {
                if (this._LoginCommand == null)
                    this._LoginCommand = new DelegateCommand(new Action(this.ExecuteLoginCommand), new Func<bool>(this.CanLoginCommand));
                return this._LoginCommand;
            }
        }
        private bool CanLoginCommand()
        {
            bool CanUsername = !String.IsNullOrWhiteSpace(this.Username) && this.Username.Length >= 3;
            bool CanPassword = !String.IsNullOrWhiteSpace(this.Password) && this.Password.Length >= 3;
            return CanUsername && CanPassword;
        }
        private void ExecuteLoginCommand()
        {
            if (this.CanLoginCommand())
            {
                LauncherApp.Instance.UserInfo.ExecuteLogin(this.Username, this.Password);
                this.RaisePropertyChanged("LoginErrorMessage");
                if (!LauncherApp.Instance.UserInfo.NeedsLogin)
                    LauncherApp.Instance.PatchInfo.StartUpdate(LauncherApp.Instance.UserInfo.UpdateURL);        //start the patch process
            }
        }
        #endregion

        #region Patch stuff
        public string PatchState { get { return LauncherApp.Instance.PatchInfo.PatchState.ToString(); } }
        public int PatchProgress { get { return LauncherApp.Instance.PatchInfo.PatchProgress; } }
        void PatchInfo_PatchStateChanged(object sender, EventArgs e)
        {
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
    }
}
