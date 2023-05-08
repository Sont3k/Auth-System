using _App.Scripts.Editor.Model;
using Cdm.Authentication.Browser;
using Cdm.Authentication.OAuth2;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _App.Scripts.Editor
{
    public class AuthController
    {
        private readonly UserDataModel _userDataModel;
        private AccessTokenResponse _accessTokenResponse; // saved token

        public AuthController(UserDataModel userDataModel)
        {
            _userDataModel = userDataModel;
        }

        public void HandleWrappedAuthButton()
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

            // UpdateViewText();
        }

        // private void UpdateViewText()
        // {
        //     var data =
        //         $"Access Token: {_accessTokenResponse.accessToken}\n" +
        //         $"Expires At: {_accessTokenResponse.expiresAt}\n" +
        //         $"Issued At: {_accessTokenResponse.issuedAt}\n" +
        //         $"Refresh Token: {_accessTokenResponse.refreshToken}\n" +
        //         $"Scope: {_accessTokenResponse.scope}\n" +
        //         $"Token Type: {_accessTokenResponse.tokenType}\n"; 
        //     _authSystemView.SetLogText(data);
        // }
    }
}