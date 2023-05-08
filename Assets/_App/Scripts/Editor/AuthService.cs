using _App.Scripts.Editor.Model;

namespace _App.Scripts.Editor
{
    public class AuthService
    {
        public UserDataModel UserData { get; }

        public AuthService()
        {
            UserData = new UserDataModel
            {
                ClientID = "User #1",
                RedirectUri = "http://localhost:8080/myauthapp/oauth2/",
                Scope = "Auth Example",
                ServerUri = "http://localhost:8001"
            };
        }
    }
}