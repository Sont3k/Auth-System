using _App.Scripts.Model;
using _App.Scripts.View;
using Cdm.Authentication.Browser;
using Cdm.Authentication.OAuth2;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _App.Scripts.Presenter
{
    public class AuthSystemPresenter
    {
        private readonly AuthSystemView _authSystemView;
        private readonly UserDataModel _userDataModel;

        private AccessTokenResponse _accessTokenResponse; // saved token

        public AuthSystemPresenter(AuthSystemView authSystemView, UserDataModel userDataModel)
        {
            _authSystemView = authSystemView;
            _userDataModel = userDataModel;

            _authSystemView.OnAuthButtonPress += HandleWrappedAuthButton;
        }

        private void HandleWrappedAuthButton()
        {
            HandleAuthButton().Forget();
        }
        
        private async UniTask HandleAuthButton()
        {
            var crossPlatformBrowser = new CrossPlatformBrowser();
            crossPlatformBrowser.platformBrowsers.Add(RuntimePlatform.WindowsEditor, new StandaloneBrowser());
            crossPlatformBrowser.platformBrowsers.Add(RuntimePlatform.WindowsPlayer, new StandaloneBrowser());
            crossPlatformBrowser.platformBrowsers.Add(RuntimePlatform.OSXEditor, new StandaloneBrowser());
            crossPlatformBrowser.platformBrowsers.Add(RuntimePlatform.OSXPlayer, new StandaloneBrowser());
            crossPlatformBrowser.platformBrowsers.Add(RuntimePlatform.IPhonePlayer, new ASWebAuthenticationSessionBrowser());

            using var authenticationSession = new AuthenticationSession(_userDataModel.GetAuthFlow(), crossPlatformBrowser);

            await authenticationSession.AuthenticateAsync();
            _accessTokenResponse = await authenticationSession.GetOrRefreshTokenAsync();

            UpdateViewText();
        }

        private void UpdateViewText()
        {
            var data =
                $"Access Token: {_accessTokenResponse.accessToken}\n" +
                $"Expires At: {_accessTokenResponse.expiresAt}\n" +
                $"Issued At: {_accessTokenResponse.issuedAt}\n" +
                $"Refresh Token: {_accessTokenResponse.refreshToken}\n" +
                $"Scope: {_accessTokenResponse.scope}\n" +
                $"Token Type: {_accessTokenResponse.tokenType}\n"; 
            _authSystemView.SetLogText(data);
        }
    }
}