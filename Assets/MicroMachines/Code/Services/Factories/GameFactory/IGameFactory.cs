using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        void CreateCar(CarInput carInput, int carNumber);
        void CreateCheckpoints();
        void CreateRoad();
        void CreateCamera();
    }
}