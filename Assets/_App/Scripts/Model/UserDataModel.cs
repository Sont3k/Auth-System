using Cdm.Authentication.Clients;
using Cdm.Authentication.OAuth2;

namespace _App.Scripts.Model
{
    public class UserDataModel
    {
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }

        public AuthorizationCodeFlow GetAuthFlow()
        {
            return new GitHubAuth(new AuthorizationCodeFlow.Configuration
            {
                clientId = ClientID,
                redirectUri = RedirectUri,
                scope = Scope
            });
        }
    }
}