using System;
using MicroMachines.Code.Data.Enums;
using MicroMachines.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.PopUp
{
    public class WinPopUp : MonoBehaviour
    {
        public event Action OnBackButton;
        public event Action OnRestartButton;
        
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _currentTime;
        [SerializeField] private TMP_Text _recordTime;
        private ISoundService _soundService;

        public void Construct(ISoundService soundService) => 
            _soundService = soundService;

        private void Start()
        {
            _backButton.onClick.AddListener(BackButtonClicked);
            _restartButton.onClick.AddListener(RestartButtonClicked);
        }

        public void Show()
        {
            _soundService.PlayEffectSound(SoundId.Finish);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);

        private void RestartButtonClicked() => OnRestartButton?.Invoke();

        private void BackButtonClicked() => OnBackButton?.Invoke();

        public void SetRecordText(float value) => _recordTime.text = ConvertToTime(value);

        public void SetCurrentTimeText(float value) => _currentTime.text = ConvertToTime(value);

        private string ConvertToTime(float value)
        {
            return string.Format("{0:00}:{1:00}:{2:00}",
                Mathf.Floor(value / 60),//minutes
                Mathf.Floor(value) % 60,//seconds
                Mathf.Floor((value*100) % 100));//miliseconds
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(BackButtonClicked);
            _restartButton.onClick.RemoveListener(RestartButtonClicked);
        }
    }
}