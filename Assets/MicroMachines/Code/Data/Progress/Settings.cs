using System;

namespace MicroMachines.Code.Data.Progress
{
    [Serializable]
    public class Settings
    {
        public float Volume;
        
        public Settings() => Volume = 0.55f;
    }
}