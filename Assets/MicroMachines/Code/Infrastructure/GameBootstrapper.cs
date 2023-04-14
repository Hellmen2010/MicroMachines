using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Infrastructure.StateMachine.States;
using MicroMachines.Code.Services.CoroutineRunner;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.Sound;
using UnityEngine;

namespace MicroMachines.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(ServiceContainer.ServiceContainer.Container, this, _soundService);
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void OnDestroy() => ServiceContainer.ServiceContainer.Container.Single<IEntityContainer>().Dispose();
    }
}