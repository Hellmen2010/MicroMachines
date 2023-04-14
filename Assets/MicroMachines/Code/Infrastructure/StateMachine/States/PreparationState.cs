using System.Collections;
using Cinemachine;
using MicroMachines.Code.Core.Buttons;
using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Checkpoint;
using MicroMachines.Code.Core.Physics;
using MicroMachines.Code.Core.Settings;
using MicroMachines.Code.Core.Timer;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.StaticData;
using UnityEngine;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class PreparationState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IPersistentProgress _progress;
        private RaceProgressTracker _raceProgressTracker;
        private BackButton _backButton;
        private Car _car;
        private Countdown _countdown;
        private Timer _timer;
        private BestTimeView _bestTimeView;
        private Settings _settings;
        private CinemachineVirtualCamera _camera;
        private Road _road;

        public PreparationState(IGameStateMachine stateMachine, IEntityContainer entityContainer, IStaticData staticData, IPersistentProgress progress)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _staticData = staticData;
            _progress = progress;
        }

        public void Enter()
        {
            CacheEntities();
            _backButton.Show();
            _backButton.OnBackButton += MoveToMenu;
            _settings.DisableRestartButton();
            SetupCamera();
            _countdown.StartCountdown(_staticData.GameConfig.Countdown, MoveToGameLoop, CameraApproach);
            ResetGameVariables();
        }

        private void SetupCamera()
        {
            _camera.Follow = _road.transform;
            _camera.m_Lens.OrthographicSize = 8.5f;
        }

        private void ResetGameVariables()
        {
            _timer.ResetTimer();
            _raceProgressTracker.ResetTrackProgress();
            _raceProgressTracker.Subscribe();
            _car.ResetCar(_staticData.SpawnpointsData.CarSpawnLocation);
            _bestTimeView.SetText(_progress.Progress.BestTime);
        }

        private void CacheEntities()
        {
            _backButton = _entityContainer.GetEntity<BackButton>();
            _car = _entityContainer.GetEntity<Car>();
            _raceProgressTracker = _entityContainer.GetEntity<RaceProgressTracker>();
            _countdown = _entityContainer.GetEntity<Countdown>();
            _timer = _entityContainer.GetEntity<Timer>();
            _bestTimeView = _entityContainer.GetEntity<BestTimeView>();
            _settings = _entityContainer.GetEntity<Settings>();
            _camera = _entityContainer.GetEntity<CinemachineVirtualCamera>();
            _road = _entityContainer.GetEntity<Road>();
        }

        private void MoveToMenu() => _stateMachine.Enter<MainMenuState>();

        private void MoveToGameLoop() => _stateMachine.Enter<GameLoopState>();

        public void Exit()
        {
            _backButton.OnBackButton -= MoveToMenu;
            _settings.EnableRestartButton();
            CameraApproach();
        }

        private void CameraApproach() => _camera.StartCoroutine(CameraApproachRoutine());

        private IEnumerator CameraApproachRoutine()
        {
            _camera.Follow = _car.CarView.transform;
            while (_camera.m_Lens.OrthographicSize > 3.1f)
            {
                _camera.m_Lens.OrthographicSize -= Time.deltaTime*2;
                yield return null;
            }
        }
    }
}