using System;
using _App.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.View
{
    public class AuthSystemView : MonoBehaviour
    {
        [SerializeField] private Button _authButton;
        private readonly UserDataModel _userDataModel = new();

        public event Action OnAuthButtonPress;

        private void OnEnable()
        {
            _authButton.onClick.AddListener(() => OnAuthButtonPress?.Invoke());
        }

        private void OnDisable()
        {
            _authButton.onClick.RemoveAllListeners();
        }
    }
}
