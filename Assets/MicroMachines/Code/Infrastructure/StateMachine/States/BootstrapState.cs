using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.CoroutineRunner;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.Factories.GameFactory;
using MicroMachines.Code.Services.Factories.UIFactory;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;
using MicroMachines.Code.Services.SceneLoader;
using MicroMachines.Code.Services.Sound;
using MicroMachines.Code.Services.StaticData;
using MicroMachines.Code.Services.StaticData.StaticDataProvider;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(
            IGameStateMachine gameStateMachine,
            ServiceContainer.ServiceContainer container,
            ISoundService soundService,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;

            RegisterServices(container, soundService);
        }

        
        public void Enter() => _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
        
        private void RegisterServices(ServiceContainer.ServiceContainer container, ISoundService soundService)
        {
            container.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            container.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            container.RegisterSingle<IEntityContainer>(new EntityContainer());
            container.RegisterSingle<IPersistentProgress>(new PersistentPlayerProgress());
            container.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
            container.RegisterSingle<ISceneLoader>(new SceneLoader());

            RegisterStaticData(container);

            container.RegisterSingle<ISaveLoad>(new PrefsSaveLoad(container.Single<IPersistentProgress>()));
            container.RegisterSingle<ISoundService>(soundService);
            container.RegisterSingle<IUIFactory>(new UIFactory(container.Single<IStaticData>(), container.Single<IEntityContainer>(),
                container.Single<IGameStateMachine>(), container.Single<IPersistentProgress>(), soundService));
            container.RegisterSingle<IGameFactory>(new GameFactory(container.Single<IStaticData>(), container.Single<IEntityContainer>(), soundService));
        }

        private void RegisterStaticData(ServiceContainer.ServiceContainer container)
        {
            IStaticData staticData = new StaticData(container.Single<IStaticDataProvider>());
            staticData.LoadStaticData();
            container.RegisterSingle<IStaticData>(staticData);
        }
    }
}