using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace FoM.Launcher
{
    public class AuthenticationRPC
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string RPCEndPoint = @"http://staff.fomportal.com/launcher-login/login-alpha.php";

        public static void Login(string Username, string Password)
        {
            Log.Debug(String.Format("Starting Login() for {0}", Username));
            
            Token UserToken = AuthenticationRPC.GetTokenRPC(Username);
            Log.Debug(UserToken);
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
        private static AuthenticateResult AuthenticateRPC(Token UserToken, string Hash)
        {
            return new AuthenticateResult();
        }
        public class Token
        {
            [XmlElement("id")]
            public Guid ID;
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
        private class AuthenticateResult
        {
        }
    }
    public class RPCEnvelope
    {
        [XmlElement("status")]
        public StatusEnum Status;
        [XmlElement("token")]
        public AuthenticationRPC.Token Token;
        public enum StatusEnum
        {
            [XmlEnum("error")]
            Error,
            [XmlEnum("success")]
            Success
        }
    }
}
