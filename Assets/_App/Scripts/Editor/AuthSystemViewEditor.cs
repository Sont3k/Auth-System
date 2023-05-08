using _App.Scripts.Mocking;
using UnityEditor;
using UnityEngine;

namespace _App.Scripts.Editor
{
    public class AuthSystemViewEditor : EditorWindow
    {
        private MockServer _mockServer;
        private AuthService _authService;
        private AuthController _authController;
        
        [MenuItem("Tools/Auth System Window")]
        public static void ShowWindow()
        {
            GetWindow<AuthSystemViewEditor>("Auth System");
        }

        private void Awake()
        {
            InitMockServer();
            _authService = new AuthService();
            _authController = new AuthController(_authService.UserData);
            _mockServer.StartServer();
        }

        private void OnDestroy()
        {
            _mockServer.StopServer();
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Auth"))
            {
                _authController.HandleWrappedAuthButton();
            }
            
            GUILayout.Label(_authController.AccessTokenResponseLog);
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