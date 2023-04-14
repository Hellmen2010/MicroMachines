using System.Collections.Generic;
using System.Linq;
using MicroMachines.Code.Data.Enums;
using MicroMachines.Code.Data.Progress;
using MicroMachines.Code.Data.StaticData.Sounds;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;
using UnityEngine;

namespace MicroMachines.Code.Services.Sound
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;
        [SerializeField] private AudioSource _driveSource;
        [SerializeField] private AudioSource _driftSource;
        
        private Dictionary<SoundId, AudioClipData> _sounds;
        private ISaveLoad _saveLoad;
        private IPersistentProgress _progress;
        
        private void Awake() => DontDestroyOnLoad(this);

        public void Construct(SoundData soundData, Settings userSettings, ISaveLoad saveLoad, IPersistentProgress progress)
        {
            _saveLoad = saveLoad;
            _progress = progress;
            _sounds = soundData.AudioEffectClips.ToDictionary(s => s.Id);
            
            _musicSource.clip = soundData.BackgroundMusic;
            SetVolume(_progress.Progress.Settings.Volume);
        }

        public void PlayBackgroundMusic() => _musicSource.Play();

        public void PlayEffectSound(SoundId soundId) =>
            _effectsSource.PlayOneShot(_sounds[soundId].Clip);

        public void SetVolume(float volume)
        {
            _musicSource.volume = volume > 0.3f ? 0.3f : volume;
            _effectsSource.volume = volume;
            _driveSource.volume = volume;
            _driftSource.volume = volume;
            _progress.Progress.Settings.Volume = volume;
            _saveLoad.SaveProgress();
        }

        public void PlayDrive(SoundId soundId)
        {
            if(!_driftSource.isPlaying) _driveSource.PlayOneShot(_sounds[soundId].Clip);
        }
        
        public void PlayDrift(SoundId soundId)
        {
            if(!_driftSource.isPlaying) _driftSource.PlayOneShot(_sounds[soundId].Clip);
        }

        public void StopDrive() => _driveSource.Stop();
        public void StopDrift() => _driftSource.Stop();
    }
}