using System;
using System.Collections.Generic;
using MicroMachines.Code.Infrastructure.StateMachine.States;
using MicroMachines.Code.Services.CoroutineRunner;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.Factories.GameFactory;
using MicroMachines.Code.Services.Factories.UIFactory;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;
using MicroMachines.Code.Services.SceneLoader;
using MicroMachines.Code.Services.Sound;
using MicroMachines.Code.Services.StaticData;

namespace MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(
            ServiceContainer.ServiceContainer container,
            ICoroutineRunner coroutineRunner,
            ISoundService soundService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = CreateBootstrapState(container, coroutineRunner, soundService),
                [typeof(LoadProgressState)] = CreateLoadProgressState(container),
                [typeof(CreatePersistentEntitiesState)] = CreatePersistentEntitiesState(container, soundService),
                [typeof(MainMenuState)] = CreateMainMenuState(container, soundService),
                [typeof(CreateGameState)] = CreateGameState(container),
                [typeof(PreparationState)] = CreatePreparationState(container),
                [typeof(GameLoopState)] = CreateGameLoopState(container),
                [typeof(ResultState)] = CreateResultState(container),
            };
        }

        private CreatePersistentEntitiesState CreatePersistentEntitiesState(ServiceContainer.ServiceContainer container, ISoundService soundService) => 
            new (this, container.Single<IUIFactory>(), soundService, container.Single<IPersistentProgress>(), 
                container.Single<IStaticData>(), container.Single<ISaveLoad>());

        private GameLoopState CreateGameLoopState(ServiceContainer.ServiceContainer container) => 
            new (this, container.Single<IEntityContainer>());

        private ResultState CreateResultState(ServiceContainer.ServiceContainer container) => 
            new (this, container.Single<IPersistentProgress>(), container.Single<ISaveLoad>(), container.Single<IEntityContainer>());

        private PreparationState CreatePreparationState(ServiceContainer.ServiceContainer container) => 
            new (this, container.Single<IEntityContainer>(), container.Single<IStaticData>(), container.Single<IPersistentProgress>());

        private CreateGameState CreateGameState(ServiceContainer.ServiceContainer container) => 
            new (this, container.Single<ISceneLoader>(), container.Single<IUIFactory>(), container.Single<IGameFactory>());

        private MainMenuState CreateMainMenuState(ServiceContainer.ServiceContainer container, ISoundService soundService) => 
            new (this, container.Single<ISceneLoader>(), container.Single<IEntityContainer>(), container.Single<IUIFactory>(), soundService);

        private LoadProgressState CreateLoadProgressState(ServiceContainer.ServiceContainer container) =>
            new (this, container.Single<IPersistentProgress>(),
                container.Single<ISaveLoad>());

        private BootstrapState CreateBootstrapState(ServiceContainer.ServiceContainer container, ICoroutineRunner coroutineRunner, ISoundService soundService) => 
            new (this, container, soundService, coroutineRunner);

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
        
        ~GameStateMachine() => _activeState.Exit();
    }
}