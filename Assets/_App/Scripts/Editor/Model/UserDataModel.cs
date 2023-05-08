using Cdm.Authentication.Clients;
using Cdm.Authentication.OAuth2;

namespace _App.Scripts.Editor.Model
{
    public class UserDataModel
    {
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string ServerUri { get; set; }

        public AuthorizationCodeFlow GetAuthFlow()
        {
            return new MockServerAuth(new AuthorizationCodeFlow.Configuration
            {
                clientId = ClientID,
                redirectUri = RedirectUri,
                scope = Scope
            }, ServerUri);
        }
    }
}