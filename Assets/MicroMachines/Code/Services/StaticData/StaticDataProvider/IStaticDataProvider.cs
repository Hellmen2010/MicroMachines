using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Car;
using MicroMachines.Code.Data.StaticData.Checkpoint;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Data.StaticData.Sounds;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider : IService
    {
        PrefabsData LoadPrefabsData();
        SoundData LoadSoundData();
        CarsData LoadCarsData();
        GameConfig LoadGameConfig();
        SpawnpointsData LoadSpawnpointsData();
        EnvironmentSpritesData LoadEnvironmentSpritesData();
        CarPhysicsData LoadCarPhysicsData();
    }
}