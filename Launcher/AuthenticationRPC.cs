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
    class AuthenticationRPC
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
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("Username", Username);

            RPCCall("GetToken", Params);
            return new Token();
        }

        private static RPCEnvelope RPCCall(string Method, Dictionary<string, string> Params)
        {
            
            UriBuilder RequestBuilder = new UriBuilder(RPCEndPoint);
            List<string> QueryList = new List<string>();
            Params.Add("Method", Method);
            RPCEnvelope ResultMessage = null;

            foreach (KeyValuePair<string, string> KVP in Params)
                QueryList.Add(String.Format("{0}={1}", KVP.Key, KVP.Value));

            RequestBuilder.Query = String.Join("&", QueryList);

            Log.Debug(String.Format("RPC Request: {0}", RequestBuilder.Uri));

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
        private class Token
        {
        }
        private class AuthenticateResult
        {
        }
    }
    public class RPCEnvelope
    {
        public string status;
        public string content;
    }
}
