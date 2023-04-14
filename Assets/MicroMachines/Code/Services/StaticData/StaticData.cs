using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Car;
using MicroMachines.Code.Data.StaticData.Checkpoint;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Data.StaticData.Sounds;
using MicroMachines.Code.Services.StaticData.StaticDataProvider;

namespace MicroMachines.Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public PrefabsData Prefabs { get; private set; }
        
        public CarsData Cars { get; private set; }
        public GameConfig GameConfig { get; private set; }
        public SpawnpointsData SpawnpointsData { get; private set; }
        public EnvironmentSpritesData EnvironmentSpritesData { get; private set; }
        public CarPhysicsData CarPhysicsData { get; private set; }
        public SoundData SoundData { get; private set; }

        private readonly IStaticDataProvider _staticDataProvider;

        public StaticData(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void LoadStaticData()
        {
            Prefabs = _staticDataProvider.LoadPrefabsData();
            Cars = _staticDataProvider.LoadCarsData();
            GameConfig = _staticDataProvider.LoadGameConfig();
            SpawnpointsData = _staticDataProvider.LoadSpawnpointsData();
            EnvironmentSpritesData = _staticDataProvider.LoadEnvironmentSpritesData();
            CarPhysicsData = _staticDataProvider.LoadCarPhysicsData();
            SoundData = _staticDataProvider.LoadSoundData();
        }
    }
}