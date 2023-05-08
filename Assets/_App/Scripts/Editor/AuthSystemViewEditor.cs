using UnityEditor;
using UnityEngine;

namespace _App.Scripts.Editor
{
    public class AuthSystemViewEditor : EditorWindow
    {
        [MenuItem("Tools/Auth System Window")]
        public static void ShowWindow()
        {
            GetWindow<AuthSystemViewEditor>("Auth System");
        }
        
        public void OnGUI()
        {
            var authService = new AuthService();
            var authController = new AuthController(authService.UserData);
            
            if (GUILayout.Button("Auth"))
            {
                authController.HandleWrappedAuthButton();
            }
        }
    }
}