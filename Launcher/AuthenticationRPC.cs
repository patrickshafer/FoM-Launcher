using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace FoM.Launcher
{
    public class AuthenticationRPC
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string RPCEndPoint = @"http://login.fomportal.com:8081/login-closedbeta2.php";

        public static AuthenticateResult Login(string Username, string Password)
        {
            Log.Debug(String.Format("Starting Login() for {0}", Username));

            Token UserToken = AuthenticationRPC.GetTokenRPC(Username);
            Log.Debug(String.Format("Got Token for user: {0}", UserToken));

            string Hash = MD5Hash(String.Format("{0}:{1}", UserToken.ID, MD5Hash(Password)));
            AuthenticateResult Result = AuthenticationRPC.AuthenticateRPC(UserToken, Hash);
            Log.Info(String.Format("Result: {0} - {1}", Result.Status, (Result.Status == RPCEnvelope.StatusEnum.Success ? Result.UpdateURL : Result.ErrorMessage)));

            return Result;
        }
        private static string MD5Hash(string Input)
        {
            MD5 Hasher = MD5.Create();
            byte[] InputBytes = Encoding.ASCII.GetBytes(Input);
            byte[] HashBytes = Hasher.ComputeHash(InputBytes);

            StringBuilder HashBuilder = new StringBuilder();
            foreach (byte HashByte in HashBytes)
                HashBuilder.Append(HashByte.ToString("x2"));

            return HashBuilder.ToString();
        }

        private static Token GetTokenRPC(string Username)
        {
            Token RetVal = null;
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("Username", Username);

            RPCEnvelope Result = RPCCall("GetToken", Params);

            if (Result.Status == RPCEnvelope.StatusEnum.Success)
                RetVal = Result.Token;

            return RetVal;
        }
        private static AuthenticateResult AuthenticateRPC(Token UserToken, string Hash)
        {
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("UserToken", UserToken.ID.ToString());
            Params.Add("Hash", Hash);

            RPCEnvelope Result = RPCCall("Authenticate", Params);

            return Result.Authenticate;
        }

        private static RPCEnvelope RPCCall(string Method, Dictionary<string, string> Params)
        {
            
            UriBuilder RequestBuilder = new UriBuilder(RPCEndPoint);
            List<string> QueryList = new List<string>();
            Params.Add("Method", Method);
            RPCEnvelope ResultMessage = null;

            foreach (KeyValuePair<string, string> KVP in Params)
                QueryList.Add(String.Format("{0}={1}", KVP.Key.ToLowerInvariant(), KVP.Value));

            RequestBuilder.Query = String.Join("&", QueryList);

            Log.Debug(String.Format("AuthRPC Request: {0}", RequestBuilder.Uri));

            using (WebClient HttpRequest = new WebClient())
            {
                using (StreamReader RPCEnvelopeStream = new StreamReader(HttpRequest.OpenRead(RequestBuilder.Uri)))
                {
                    XmlSerializer Serializer = new XmlSerializer(typeof(RPCEnvelope));
                    ResultMessage = (RPCEnvelope)Serializer.Deserialize(RPCEnvelopeStream);
                }
            }
            return ResultMessage;
        }

        public class Token
        {
            [XmlElement("ID")]
            public string ID;
            [XmlElement("created")]
            public DateTime Created;
            [XmlElement("expires")]
            public DateTime Expires;
            [XmlElement("username")]
            public string Username;
            [XmlElement("remoteIP")]
            public string RemoteIP;
            [XmlElement("active")]
            public bool Active;

            public override string ToString()
            {
                return String.Format("{0:B} {1}@{2}{3}", this.ID, this.Username, this.RemoteIP, (this.Active? "":" [INACTIVE]"));
            }
        }
        public class AuthenticateResult
        {
            [XmlElement("status")]
            public RPCEnvelope.StatusEnum Status;
            [XmlElement("errorMessage")]
            public string ErrorMessage;
            [XmlElement("updateURL")]
            public string UpdateURL;
            [XmlArray("FoMChannels"), XmlArrayItem("url")]
            public List<string> FoMChannels;
            [XmlArray("LauncherChannels"), XmlArrayItem("url")]
            public List<string> LauncherChannels;
        }
    }
    public class RPCEnvelope
    {
        [XmlElement("status")]
        public StatusEnum Status;
        [XmlElement("token")]
        public AuthenticationRPC.Token Token;
        [XmlElement("authenticate")]
        public AuthenticationRPC.AuthenticateResult Authenticate;

        public enum StatusEnum
        {
            [XmlEnum("error")]
            Error,
            [XmlEnum("success")]
            Success
        }
    }
}
