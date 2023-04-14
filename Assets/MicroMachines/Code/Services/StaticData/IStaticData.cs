using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Car;
using MicroMachines.Code.Data.StaticData.Checkpoint;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Data.StaticData.Sounds;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.StaticData
{
    public interface IStaticData : IService
    {
        PrefabsData Prefabs { get; }
        CarsData Cars { get; }
        GameConfig GameConfig { get; }
        SpawnpointsData SpawnpointsData { get; }
        EnvironmentSpritesData EnvironmentSpritesData { get; }
        CarPhysicsData CarPhysicsData { get; }
        SoundData SoundData { get; }
        void LoadStaticData();
    }
}