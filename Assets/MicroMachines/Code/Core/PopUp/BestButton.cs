using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.PopUp
{
    public class BestButton : MonoBehaviour
    {
        public event Action OnBestButton;
        
        [SerializeField] private Button _bestButton;

        private void Start() => _bestButton.onClick.AddListener(BestButtonClicked);

        private void BestButtonClicked() => OnBestButton?.Invoke();

        private void OnDestroy() => _bestButton.onClick.RemoveListener(BestButtonClicked);

        public void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}