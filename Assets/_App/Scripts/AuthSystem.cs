using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts
{
    public class AuthSystem : MonoBehaviour
    {
        [SerializeField] private Button _authButton;

        private void OnEnable()
        {
            _authButton.onClick.AddListener(HandleAuthButton);
        }

        private void OnDisable()
        {
            _authButton.onClick.RemoveAllListeners();
        }
        
        private void HandleAuthButton()
        {
            
        }
    }
}
