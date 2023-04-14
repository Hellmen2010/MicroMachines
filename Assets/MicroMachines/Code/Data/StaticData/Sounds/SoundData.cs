using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Sounds
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "StaticData/SoundData")]
    public class SoundData : ScriptableObject
    {
        public AudioClipData[] AudioEffectClips;
        public AudioClip BackgroundMusic;
        public Sprite VolumeOn;
        public Sprite VolumeOff;
    }
}