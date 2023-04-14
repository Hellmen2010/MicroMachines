using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.PopUp
{
    public class BestTimePopUp : MonoBehaviour
    {
        public event Action OnCloseButton;

        public GameObject PopUp;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _closeButton;
        
        public void SetText(float value)
        {
            _text.text = string.Format("{0:00}:{1:00}:{2:00}",
                Mathf.Floor(value / 60),//minutes
                Mathf.Floor(value) % 60,//seconds
                Mathf.Floor((value*100) % 100));//miliseconds
        }

        public void Hide() => PopUp.SetActive(false);
        public void Show() => PopUp.SetActive(true);
        
        private void Start() => _closeButton.onClick.AddListener(CloseButtonPressed);
        private void CloseButtonPressed() => OnCloseButton?.Invoke();
        private void OnDestroy() => _closeButton.onClick.RemoveListener(CloseButtonPressed);
    }
}