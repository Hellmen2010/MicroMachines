using MicroMachines.Code.Data.Progress;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.PersistentProgress
{
    public interface IPersistentProgress : IService
    {
        PlayerProgress Progress { get; set; }
    }
}