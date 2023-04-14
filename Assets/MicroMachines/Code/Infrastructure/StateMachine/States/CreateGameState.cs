using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.Factories.GameFactory;
using MicroMachines.Code.Services.Factories.UIFactory;
using MicroMachines.Code.Services.SceneLoader;
using UnityEngine;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class CreateGameState : IPayloadedState<int>
    {
        private const string SceneName = "Game";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private CarInput _input;
        private int _carNumber;


        public CreateGameState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter(int carNumber)
        {
            _carNumber = carNumber;
            _sceneLoader.LoadScene(SceneName, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            CreateUI();
            CreateSceneObjects();
            MoveToPreparation();
        }

        private void MoveToPreparation() => _stateMachine.Enter<PreparationState>();

        private void CreateSceneObjects()
        {
            _gameFactory.CreateCar(_input, _carNumber);
            _gameFactory.CreateCheckpoints();
            _gameFactory.CreateRoad();
            _gameFactory.CreateCamera();
        }

        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas(); 
            _input = _uiFactory.CreateCarInput(root);
            CreateTimer();
            _uiFactory.CreateBestTimeView(root);
            _uiFactory.CreateCountdown(root);
            _uiFactory.CreateWinPopUp(root);
            _uiFactory.CreateSettings(root);
        }

        private void CreateTimer()
        {
            Transform timerRoot = _uiFactory.CreateRootCanvas();
            timerRoot.GetComponent<Canvas>().sortingOrder = -1;
            _uiFactory.CreateTimer(timerRoot);
        }

        public void Exit()
        {
            
        }
    }
}