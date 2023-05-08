using _App.Scripts.Mocking;
using _App.Scripts.View;
using UnityEditor;
using UnityEngine;

namespace _App.Scripts.Editor
{
    [CustomEditor(typeof(AuthSystemView))]
    public class AuthSystemViewEditor : UnityEditor.Editor
    {
        private MockServer _mockServer;
        private AuthService _authService;
        private AuthController _authController;

        private void Awake()
        {
            InitMockServer();
            _authService = new AuthService();
            _authController = new AuthController(_authService.UserData);
            _mockServer.StartServer();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Auth"))
            {
                _authController.HandleWrappedAuthButton();
            }
            
            GUILayout.Label(_authController.AccessTokenResponseLog);
        }

        private void OnDestroy()
        {
            _mockServer.StopServer();
        }
        
        private void InitMockServer()
        {
            _mockServer = new MockServer();
            _mockServer.AccessTokenResponses.Add(new MockServerAccessTokenResponse
            {
                username = "user"
            });
        }
    }
}