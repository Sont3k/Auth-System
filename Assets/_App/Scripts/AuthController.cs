using _App.Scripts.Model;
using _App.Scripts.Presenter;
using _App.Scripts.View;
using UnityEngine;

namespace _App.Scripts
{
    public class AuthController : MonoBehaviour
    {
        [SerializeField] private AuthSystemView _authSystemView;

        private AuthSystemPresenter _authSystemPresenter;
        private UserDataModel _userDataModel;

        private void Start()
        {
            _userDataModel = new UserDataModel
            {
                ClientID = "User #1",
                RedirectUri = "http://localhost:8080/myauthapp/oauth2/",
                Scope = "Auth Example",
                ServerUri = "http://localhost:8001"
            };
            _authSystemPresenter = new AuthSystemPresenter(_authSystemView, _userDataModel);
        }
    }
}