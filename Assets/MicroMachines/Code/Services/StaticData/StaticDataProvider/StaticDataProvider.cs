using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Car;
using MicroMachines.Code.Data.StaticData.Checkpoint;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace MicroMachines.Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/PrefabsData";
        private const string SoundDataPath = "StaticData/SoundData";
        private const string CarsDataPath = "StaticData/CarsData";
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string SpawnpointsDataPath = "StaticData/SpawnpointsData";
        private const string EnvironmentSpritesDataPath = "StaticData/EnvironmentSpritesData";
        private const string CarPhysicsDataPath = "StaticData/CarPhysicsData";

        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);
        public SoundData LoadSoundData() => Resources.Load<SoundData>(SoundDataPath);
        public CarsData LoadCarsData() => Resources.Load<CarsData>(CarsDataPath);
        public GameConfig LoadGameConfig() => Resources.Load<GameConfig>(GameConfigPath);
        public SpawnpointsData LoadSpawnpointsData() => Resources.Load<SpawnpointsData>(SpawnpointsDataPath);
        public EnvironmentSpritesData LoadEnvironmentSpritesData() => Resources.Load<EnvironmentSpritesData>(EnvironmentSpritesDataPath);
        public CarPhysicsData LoadCarPhysicsData() => Resources.Load<CarPhysicsData>(CarPhysicsDataPath);
    }
}