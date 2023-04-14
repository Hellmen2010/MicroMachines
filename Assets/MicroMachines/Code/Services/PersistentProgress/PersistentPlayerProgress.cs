using MicroMachines.Code.Data.Progress;

namespace MicroMachines.Code.Services.PersistentProgress
{
    public class PersistentPlayerProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }
    }
}