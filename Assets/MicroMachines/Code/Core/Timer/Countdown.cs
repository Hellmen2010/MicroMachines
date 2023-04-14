using System;
using System.Collections;
using Cinemachine;
using MicroMachines.Code.Data.Enums;
using MicroMachines.Code.Services.Sound;
using TMPro;
using UnityEngine;

namespace MicroMachines.Code.Core.Timer
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private ISoundService _soundService;
        public int Time { get; private set; }

        public void Construct(ISoundService soundService) => 
            _soundService = soundService;

        public void StartCountdown(int maxValue, Action OnTimeOut, Action onPreparationState = null)
        {
            Show();
            StartCoroutine(CountdownRoutine(maxValue, OnTimeOut, onPreparationState));
        }

        public void Hide() => gameObject.SetActive(false);
        private void Show() => gameObject.SetActive(true);

        private void SetText(int value) => _text.text = value.ToString();

        private IEnumerator CountdownRoutine(int maxValue, Action onTimeOut, Action onPreparationState = null)
        {
            Time = maxValue;
            while (Time >= 0)
            {
                _soundService.PlayEffectSound(SoundId.Tick);
                if(Time == 2) onPreparationState?.Invoke();
                SetText(Time);
                yield return new WaitForSecondsRealtime(1f);
                Time--;
            }
            onTimeOut?.Invoke();
            Hide();
        }
    }
}