using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Buttons
{
    public class BackButton : MonoBehaviour
    {
        public event Action OnBackButton;
        
        [SerializeField] private Button _backButton;

        private void Start() => _backButton.onClick.AddListener(BackButtonPressed);

        private void BackButtonPressed() => OnBackButton?.Invoke();

        private void OnDestroy() => _backButton.onClick.AddListener(BackButtonPressed);

        public void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}