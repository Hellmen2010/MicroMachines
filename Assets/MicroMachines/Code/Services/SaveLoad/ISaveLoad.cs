using MicroMachines.Code.Data.Progress;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.SaveLoad
{
    public interface ISaveLoad : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}