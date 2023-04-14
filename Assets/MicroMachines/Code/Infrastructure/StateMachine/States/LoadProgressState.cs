using MicroMachines.Code.Data.Progress;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;

        public LoadProgressState(IGameStateMachine gameStateMachine, IPersistentProgress playerProgress,
            ISaveLoad saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<CreatePersistentEntitiesState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() =>
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
    }
}