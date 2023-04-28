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

            // Opens a browser to log user in
            var accessTokenResponse = await authenticationSession.AuthenticateAsync();

            // Authentication header can be used to make authorized http calls.
            var authenticationHeader = accessTokenResponse.GetAuthenticationHeader();

            // Gets the current acccess token, or refreshes if it is expired.
            accessTokenResponse = await authenticationSession.GetOrRefreshTokenAsync();

            // Gets new access token by using the refresh token.
            var newAccessTokenResponse = await authenticationSession.RefreshTokenAsync();

            // Or you can get new access token with specified refresh token (i.e. stored on the local disk to prevent multiple sign-in for each app launch)
            newAccessTokenResponse = await authenticationSession.RefreshTokenAsync("my_refresh_token");
        }
    }
}