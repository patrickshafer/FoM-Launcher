
namespace FoM.Launcher.Models
{
    class UserModel
    {
        public bool NeedsLogin { get; set; }
        public string ErrorMessage;
        public string Status;


        public UserModel()
        {
            this.NeedsLogin = true;
        }

        public void ExecuteLogin(string Username, string Password)
        {
            AuthenticationRPC.AuthenticateResult LoginResult = AuthenticationRPC.Login(Username, Password);
            switch (LoginResult.Status)
            {
                case RPCEnvelope.StatusEnum.Error:
                    break;
                case RPCEnvelope.StatusEnum.Success:
                    this.NeedsLogin = false;
                    LauncherApp.Instance.PreferenceInfo.FoMURLList = LoginResult.FoMChannels;
                    LauncherApp.Instance.PreferenceInfo.LauncherURLList = LoginResult.LauncherChannels;
                    LauncherApp.Instance.PreferenceInfo.Save();
                    break;
                default:
                    break;
            }

            this.ErrorMessage = LoginResult.ErrorMessage;
            this.Status = LoginResult.Status.ToString();
        }
    }
}
