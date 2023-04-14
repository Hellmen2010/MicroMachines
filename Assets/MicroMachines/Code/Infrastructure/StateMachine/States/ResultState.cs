using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Checkpoint;
using MicroMachines.Code.Core.PopUp;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class ResultState : IPayloadedState<float>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgress _progress;
        private readonly ISaveLoad _saveLoad;
        private readonly IEntityContainer _entityContainer;
        private WinPopUp _winPopUp;
        private Car _car;
        private RaceProgressTracker _raceProgress;

        public ResultState(IGameStateMachine stateMachine, IPersistentProgress progress, ISaveLoad saveLoad, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _progress = progress;
            _saveLoad = saveLoad;
            _entityContainer = entityContainer;
        }

        public void Enter(float currentTime)
        {
            CacheEntities();
            Subscribe();
            _car.StopMovement();
            SetupPopUp(currentTime);
            _saveLoad.SaveProgress();
            _car.Hide(_winPopUp.Show);
        }

        private void SetupPopUp(float currentTime)
        {
            if (_raceProgress.IsFinished)
            {
                TryUpdateRecordTime(currentTime);
                _winPopUp.SetCurrentTimeText(currentTime);
            }
            else
            {
                _winPopUp.SetCurrentTimeText(0);
            }

            _winPopUp.SetRecordText(_progress.Progress.BestTime);
        }

        private void TryUpdateRecordTime(float currentTime)
        {
            if (_progress.Progress.BestTime > currentTime || _progress.Progress.BestTime == 0)
                _progress.Progress.BestTime = currentTime;
        }

        private void Subscribe()
        {
            _winPopUp.OnBackButton += MoveToMenu;
            _winPopUp.OnRestartButton += MoveToPreparation;
        }
        
        private void UnSubscribe()
        {
            _winPopUp.OnBackButton -= MoveToMenu;
            _winPopUp.OnRestartButton -= MoveToPreparation;
        }

        private void MoveToPreparation() => _stateMachine.Enter<PreparationState>();

        private void MoveToMenu() => _stateMachine.Enter<MainMenuState>();

        private void CacheEntities()
        {
            _winPopUp = _entityContainer.GetEntity<WinPopUp>();
            _car = _entityContainer.GetEntity<Car>();
            _raceProgress = _entityContainer.GetEntity<RaceProgressTracker>();
        }

        public void Exit()
        {
            UnSubscribe();
            _winPopUp.Hide();
        }
    }
}