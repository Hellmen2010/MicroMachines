using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.Factories.UIFactory;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;
using MicroMachines.Code.Services.Sound;
using MicroMachines.Code.Services.StaticData;
using UnityEngine;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class CreatePersistentEntitiesState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly ISoundService _soundService;
        private readonly IPersistentProgress _progress;
        private readonly IStaticData _staticData;
        private readonly ISaveLoad _saveLoad;

        public CreatePersistentEntitiesState(IGameStateMachine stateMachine, IUIFactory uiFactory, ISoundService soundService, IPersistentProgress progress,
            IStaticData staticData, ISaveLoad saveLoad)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _soundService = soundService;
            _progress = progress;
            _staticData = staticData;
            _saveLoad = saveLoad;
        }

        public void Enter()
        {
            CreatePersistentEntities();
            _soundService.Construct(_staticData.SoundData, _progress.Progress.Settings, _saveLoad, _progress);
            _soundService.PlayBackgroundMusic();
            MoveToMainMenu();
        }

        public void Exit()
        {
        }

        private void CreatePersistentEntities()
        {
            Transform persistantRoot = CreatePersistentCanvas();
            _uiFactory.CreateBackButton(persistantRoot);
        }

        private Transform CreatePersistentCanvas()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            root.GetComponent<Canvas>().sortingOrder = 5;
            Object.DontDestroyOnLoad(root);
            return root;
        }

        private void MoveToMainMenu() => _stateMachine.Enter<MainMenuState>();
    }
}