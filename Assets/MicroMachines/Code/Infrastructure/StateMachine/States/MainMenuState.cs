using DanielLochner.Assets.SimpleScrollSnap;
using MicroMachines.Code.Core.Buttons;
using MicroMachines.Code.Core.MainMenu;
using MicroMachines.Code.Core.PopUp;
using MicroMachines.Code.Core.Settings;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.Factories.UIFactory;
using MicroMachines.Code.Services.SceneLoader;
using MicroMachines.Code.Services.Sound;
using UnityEngine;

namespace MicroMachines.Code.Infrastructure.StateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEntityContainer _entityContainer;
        private readonly IUIFactory _uiFactory;
        private readonly ISoundService _soundService;
        private MainMenuView _mainMenuView;
        private StartRaceButton _startRaceRaceButton;
        private Transform _carSelectionCanvas;
        private BackButton _backButton;
        private BestButton _bestButton;
        private BestTimePopUp _bestTimePopUp;
        private VolumeSettings _volumeSettings;

        private const string MenuScene = "Menu";

        public MainMenuState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IEntityContainer entityContainer, IUIFactory uiFactory, 
            ISoundService soundService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _entityContainer = entityContainer;
            _uiFactory = uiFactory;
            _soundService = soundService;
        }

        public void Enter() => _sceneLoader.LoadScene(MenuScene, OnSceneLoaded);

        public void Exit()
        {
            UnSubscribe();
        }

        private void OnSceneLoaded()
        {
            CreateUI();
            CacheEntities();
            Subscribe();
            _backButton.Hide();
            _bestTimePopUp.Hide();
            _carSelectionCanvas.gameObject.SetActive(false);
        }

        private void CreateUI()
        {
            Transform menuRoot = _uiFactory.CreateRootCanvas();
            _uiFactory.CreateMainMenu(menuRoot);
            _uiFactory.CreateBestTime(menuRoot);
            _uiFactory.CreateVolumeSettings(menuRoot);
            
            _carSelectionCanvas = _uiFactory.CreateRootCanvas();
            _carSelectionCanvas.GetComponent<Canvas>().sortingOrder = 1;
            
            _uiFactory.CreateCarSelectionView(_carSelectionCanvas);
            _uiFactory.CreateStartButton(_carSelectionCanvas);
        }

        private void CacheEntities()
        {
            _mainMenuView = _entityContainer.GetEntity<MainMenuView>();
            _startRaceRaceButton = _entityContainer.GetEntity<StartRaceButton>();
            _backButton = _entityContainer.GetEntity<BackButton>();
            _bestButton = _entityContainer.GetEntity<BestButton>();
            _bestTimePopUp = _entityContainer.GetEntity<BestTimePopUp>();
            _volumeSettings = _entityContainer.GetEntity<VolumeSettings>();
        }

        private void Subscribe()
        {
            _mainMenuView.OnStartButton += ShowCarSelection;
            _startRaceRaceButton.OnStartButton += MoveToGameCreation;
            _backButton.OnBackButton += HideCarSelection;
            _bestButton.OnBestButton += SwitchBestTimeDisplay;
            _bestTimePopUp.OnCloseButton += HideBestTime;
            _volumeSettings.OnVolumeChanged += _soundService.SetVolume;
        }

        private void SwitchBestTimeDisplay()
        {
            if (_bestTimePopUp.PopUp.activeInHierarchy)
                _bestTimePopUp.Hide();
            else
                _bestTimePopUp.Show();
        }
        
        private void HideBestTime() => _bestTimePopUp.Hide();

        private void UnSubscribe()
        {
            _mainMenuView.OnStartButton -= ShowCarSelection;
            _startRaceRaceButton.OnStartButton -= MoveToGameCreation;
            _backButton.OnBackButton -= HideCarSelection;
            _bestButton.OnBestButton -= SwitchBestTimeDisplay;
            _bestTimePopUp.OnCloseButton -= HideBestTime;
            _volumeSettings.OnVolumeChanged -= _soundService.SetVolume;
        }

        private void MoveToGameCreation() => 
            _stateMachine.Enter<CreateGameState, int>(_entityContainer.GetEntity<DynamicContent>().GetSelectedPanel());

        private void ShowCarSelection()
        {
            _mainMenuView.Hide();
            _bestButton.Hide();
            _volumeSettings.Hide();
            _backButton.Show();
            _carSelectionCanvas.gameObject.SetActive(true);
        }

        private void HideCarSelection()
        {
            _mainMenuView.Show();
            _bestButton.Show();
            _volumeSettings.Show();
            _backButton.Hide();
            _carSelectionCanvas.gameObject.SetActive(false);
        }
    }
}