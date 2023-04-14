using System;
using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Settings
{
    public class VolumeSettings : MonoBehaviour
    {
        public event Action<float> OnVolumeChanged; 

        [SerializeField] private Button _button;
        [SerializeField] private Slider _slider;

        public void Construct(float volume)
        {
            _slider.value = volume;
            HideSlider();
        }

        private void Start()
        {
            _button.onClick.AddListener(SwitchSliderDisplay);
            _slider.onValueChanged.AddListener(VolumeChanged);
        }

        private void VolumeChanged(float value) => OnVolumeChanged?.Invoke(_slider.value);

        private void SwitchSliderDisplay() => 
            _slider.gameObject.SetActive(!_slider.gameObject.activeInHierarchy);

        private void OnDisable() => HideSlider();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SwitchSliderDisplay);
            _slider.onValueChanged.RemoveListener(VolumeChanged);
        }

        private void HideSlider() =>_slider.gameObject.SetActive(false);
        public void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}