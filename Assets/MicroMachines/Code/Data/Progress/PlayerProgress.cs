using System;

namespace MicroMachines.Code.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public Settings Settings;
        public float BestTime;

        public PlayerProgress()
        {
            Settings = new Settings();
            BestTime = 0;
        }
    }
}