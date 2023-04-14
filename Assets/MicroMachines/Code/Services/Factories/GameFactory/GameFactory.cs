using Cinemachine;
using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Checkpoint;
using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Core.Physics;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.Sound;
using MicroMachines.Code.Services.StaticData;
using UnityEngine;

namespace MicroMachines.Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;

        public GameFactory(IStaticData staticData, IEntityContainer entityContainer, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _soundService = soundService;
        }
        public void CreateCar(CarInput carInput, int carNumber)
        {
            CarGameView gameView = Object.Instantiate(
                _staticData.Prefabs.CarGameViewPrefab, 
                _staticData.SpawnpointsData.CarSpawnLocation.Position, 
                _staticData.SpawnpointsData.CarSpawnLocation.Rotation);
            gameView.Construct(_staticData.Cars.Cars[carNumber].GameSprite);
            Car car = new Car(carInput, gameView, _staticData.CarPhysicsData, _soundService);
            _entityContainer.RegisterEntity(car);
        }

        public void CreateCheckpoints()
        {
            Checkpoint[] checkpoints = new Checkpoint[_staticData.SpawnpointsData.CheckPointsData.Length];
            for (var i = 0; i < _staticData.SpawnpointsData.CheckPointsData.Length; i++)
            {
                int n = _staticData.SpawnpointsData.CheckPointsData[i].Number;
                CheckpointData checkpointData = _staticData.SpawnpointsData.CheckPointsData[i];
                checkpoints[n] = Object.Instantiate(_staticData.Prefabs.CheckpointPrefab, checkpointData.Position, checkpointData.Rotation);
                checkpoints[n].transform.localScale = checkpointData.Scale;
                checkpoints[n].Construct(_staticData.EnvironmentSpritesData, n);
                if(n == _staticData.SpawnpointsData.CheckPointsData.Length - 1) checkpoints[n].MakeTransparent();
            }

            RaceProgressTracker raceProgressTracker = new RaceProgressTracker(checkpoints);
            _entityContainer.RegisterEntity(raceProgressTracker);
        }

        public void CreateRoad()
        {
            Road road = Object.Instantiate(_staticData.Prefabs.RoadPrefab);
            _entityContainer.RegisterEntity(road);
        }

        public void CreateCamera()
        {
            CinemachineVirtualCamera camera = Object.Instantiate(_staticData.Prefabs.CameraPrefab);
            _entityContainer.RegisterEntity(camera);
        }
    }
}