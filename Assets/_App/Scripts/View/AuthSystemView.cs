using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace _App.Scripts.View
{
    public class AuthSystemView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _consoleLog;
        [SerializeField] private Button _authButton;

        public event Action OnAuthButtonPress;

        private void OnEnable()
        {
            _authButton.onClick.AddListener(() => OnAuthButtonPress?.Invoke());
        }

        private void OnDisable()
        {
            _authButton.onClick.RemoveAllListeners();
        }

        public void SetLogText(string text)
        {
            _consoleLog.text += text;
        }
    }
}
