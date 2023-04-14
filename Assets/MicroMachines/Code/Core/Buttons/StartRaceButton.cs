using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Buttons
{
    public class StartRaceButton : MonoBehaviour
    {
        public event Action OnStartButton;
        
        [SerializeField] private Button _startButton;

        private void Start() => _startButton.onClick.AddListener(StartButtonPressed);

        private void StartButtonPressed() => OnStartButton?.Invoke();

        private void OnDestroy() => _startButton.onClick.RemoveListener(StartButtonPressed);
    }
}