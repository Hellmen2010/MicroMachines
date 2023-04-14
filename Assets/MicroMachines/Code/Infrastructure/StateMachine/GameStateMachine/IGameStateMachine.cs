using MicroMachines.Code.Infrastructure.ServiceContainer;
using MicroMachines.Code.Infrastructure.StateMachine.States;

namespace MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}