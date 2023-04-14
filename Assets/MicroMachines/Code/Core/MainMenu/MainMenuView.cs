using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStartButton;
        
        [SerializeField] private Button _startButton;

        private void Start() => _startButton.onClick.AddListener(StartButtonClicked);

        private void StartButtonClicked() => OnStartButton?.Invoke();

        private void OnDestroy() => _startButton.onClick.RemoveListener(StartButtonClicked);

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}