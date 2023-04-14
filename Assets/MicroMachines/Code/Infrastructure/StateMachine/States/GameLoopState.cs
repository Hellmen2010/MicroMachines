using MicroMachines.Code.Core.Buttons;
using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Checkpoint;
using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Core.Physics;
using MicroMachines.Code.Core.Timer;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.EntityContainer;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private Car _car;
        private BackButton _backButton;
        private Timer _timer;
        private RaceProgressTracker _raceProgressTracker;
        private Road _road;

        public GameLoopState(IGameStateMachine stateMachine, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            CacheEntities();
            Subscribe();
            _timer.StartTimer();
        }

        public void Exit()
        {
            UnSubscribe();
        }

        private void CacheEntities()
        {
            _car = _entityContainer.GetEntity<Car>();
            _backButton = _entityContainer.GetEntity<BackButton>();
            _timer = _entityContainer.GetEntity<Timer>();
            _raceProgressTracker = _entityContainer.GetEntity<RaceProgressTracker>();
            _road = _entityContainer.GetEntity<Road>();
        }

        private void Subscribe()
        {
            _car.Subscribe();
            _backButton.OnBackButton += MoveToMenu;
            _raceProgressTracker.OnRaceEnded += RaceEnded;
            _road.ExitSender.OnTriggerExit += RaceEnded;
        }

        private void RaceEnded()
        {
            float currentTime = _timer.StopTimer();
            _entityContainer.GetEntity<GasButton>().ReleaseButton();
            MoveToResult(currentTime);
        }

        private void MoveToResult(float currentTime) => _stateMachine.Enter<ResultState, float>(currentTime);

        private void MoveToMenu() => _stateMachine.Enter<MainMenuState>();

        private void UnSubscribe()
        {
            _car.UnSubscribe();
            _backButton.OnBackButton -= MoveToMenu;
            _raceProgressTracker.OnRaceEnded -= RaceEnded;
            _road.ExitSender.OnTriggerExit -= RaceEnded;
        }
    }
}