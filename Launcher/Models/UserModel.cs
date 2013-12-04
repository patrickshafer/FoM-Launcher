
namespace FoM.Launcher.Models
{
    class UserModel
    {
        public bool NeedsLogin { get; set; }
        public string ErrorMessage;
        public string Status;
        public string UpdateURL;


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
                    System.Windows.Forms.MessageBox.Show(LoginResult.ErrorMessage, "Login error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    break;
                case RPCEnvelope.StatusEnum.Success:
                    this.NeedsLogin = false;
                    break;
                default:
                    break;
            }

            this.ErrorMessage = LoginResult.ErrorMessage;
            this.Status = LoginResult.Status.ToString();
            this.UpdateURL = LoginResult.UpdateURL;
        }
    }
}
