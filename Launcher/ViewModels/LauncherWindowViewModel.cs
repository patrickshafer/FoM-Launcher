using System;
using System.Windows.Input;


namespace FoM.Launcher.ViewModels
{
    public class LauncherWindowViewModel:BaseViewModel
    {
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
                LauncherApp.Instance.UserInfo.ExecuteLogin(this.Username, this.Password);
        }
        #endregion
    }
}
