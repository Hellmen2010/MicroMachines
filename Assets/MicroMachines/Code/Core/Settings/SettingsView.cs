using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Settings
{
    public class SettingsView : MonoBehaviour
    {
        public event Action OnUnPause;
        public event Action OnCloseClicked;
        public event Action OnBackButton;
        public event Action OnRestartButton;
        public event Action<float> OnVolumeButton;
        public event Action<float> OnVolumeChanged;

        public Button RestartButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _closeButtonOut;
        [SerializeField] private Button _closeButtonIn;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _volumeButton;
        [SerializeField] private Slider _volume;
        [SerializeField] private GameObject _settingsPanel;
        private Sprite _volumeOn;
        private Sprite _volumeOff;

        public void Construct(Sprite volumeOn, Sprite volumeOff, float volume)
        {
            _volumeOn = volumeOn;
            _volumeOff = volumeOff;
            SetVolume(volume);
        }

        private void SetVolume(float volume)
        {
            _volume.value = volume;
            TryChangeVolumeSprite();
        }

        private void Start()
        {
            _settingsButton.onClick.AddListener(SwitchPanelState);
            _closeButtonIn.onClick.AddListener(CloseClicked);
            _closeButtonOut.onClick.AddListener(CloseClicked);
            _backButton.onClick.AddListener(BackButtonClicked);
            RestartButton.onClick.AddListener(RestartButtonClicked);
            _volumeButton.onClick.AddListener(VolumeButtonClicked);
            _volume.onValueChanged.AddListener(VolumeChanged);
        }

        private void VolumeChanged(float value)
        {
            OnVolumeChanged?.Invoke(value);
            TryChangeVolumeSprite();
        }

        private void TryChangeVolumeSprite()
        {
            _volumeButton.image.sprite = _volume.value > 0
                ? _volumeOn
                : _volumeOff;
        }

        private void VolumeButtonClicked()
        {
            TryChangeVolumeSprite();
            OnVolumeButton?.Invoke(_volume.value);
        }

        private void RestartButtonClicked()
        {
            OnRestartButton?.Invoke();
            Hide();
        }

        private void BackButtonClicked() => OnBackButton?.Invoke();

        private void CloseClicked()
        {
            OnCloseClicked?.Invoke();
            Hide();
        }

        private void SwitchPanelState()
        {
            if (!_settingsPanel.activeInHierarchy)
            {
                Show();
            }
            else
            {
                OnUnPause?.Invoke();
                Hide();
            }
        }

        public void Hide()
        {
            _settingsPanel.SetActive(false);
            _closeButtonOut.gameObject.SetActive(false);
        }

        private void Show()
        {
            _settingsPanel.SetActive(true);
            _closeButtonOut.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(SwitchPanelState);
            _closeButtonIn.onClick.RemoveListener(CloseClicked);
            _closeButtonOut.onClick.RemoveListener(CloseClicked);
            _backButton.onClick.RemoveListener(BackButtonClicked);
            RestartButton.onClick.RemoveListener(RestartButtonClicked);
            _volumeButton.onClick.RemoveListener(VolumeButtonClicked);
            _volume.onValueChanged.RemoveListener(VolumeChanged);
        }
    }
}