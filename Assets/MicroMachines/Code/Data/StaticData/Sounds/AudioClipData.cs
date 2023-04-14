using System;
using MicroMachines.Code.Data.Enums;
using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }
}