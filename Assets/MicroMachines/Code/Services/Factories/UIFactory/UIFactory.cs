using DanielLochner.Assets.SimpleScrollSnap;
using MicroMachines.Code.Core.Buttons;
using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Core.MainMenu;
using MicroMachines.Code.Core.PopUp;
using MicroMachines.Code.Core.Settings;
using MicroMachines.Code.Core.Timer;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Services.EntityContainer;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.Sound;
using MicroMachines.Code.Services.StaticData;
using UnityEngine;

namespace MicroMachines.Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgress _progress;
        private readonly ISoundService _soundService;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer, IGameStateMachine stateMachine,
            IPersistentProgress progress, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _stateMachine = stateMachine;
            _progress = progress;
            _soundService = soundService;
        }
        
        public Transform CreateRootCanvas() => 
            Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public void CreateMainMenu(Transform root)
        {
            MainMenuView view = Object.Instantiate(_staticData.Prefabs.MainMenuPrefab, root);
            _entityContainer.RegisterEntity(view);
        }

        public CarInput CreateCarInput(Transform root)
        {
            CarInput carInput = Object.Instantiate(_staticData.Prefabs.CarInputPrefab, root);
            _entityContainer.RegisterEntity(carInput.GasButton);
            return carInput;
        }

        public void CreateTimer(Transform root)
        {
            TimerView timerView = Object.Instantiate(_staticData.Prefabs.timerViewPrefab, root);
            Timer timer = new Timer(timerView);
            _entityContainer.RegisterEntity(timer);
        }

        public void CreateBackButton(Transform persistantRoot)
        {
            BackButton backButton = Object.Instantiate(_staticData.Prefabs.BackButtonPrefab, persistantRoot);
            _entityContainer.RegisterEntity(backButton);
        }

        public void CreateCarSelectionView(Transform carSelectionRoot)
        {
            DynamicContent dynamicContent = Object.Instantiate(_staticData.Prefabs.CarSelectionPrefab, carSelectionRoot);

            for (int i = 0; i < _staticData.Cars.Cars.Length; i++)
            {
                GameObject carUIGameObject = dynamicContent.Add(i,_staticData.Prefabs.CarUIViewPrefab);
                CarUIView carUIView = carUIGameObject.GetComponent<CarUIView>();
                carUIView.Construct(_staticData.Cars.Cars[i].UISprite);
            }
            _entityContainer.RegisterEntity(dynamicContent);
        }

        public void CreateStartButton(Transform menuRoot)
        {
            StartRaceButton startRaceButton = Object.Instantiate(_staticData.Prefabs.StartRaceButtonPrefab, menuRoot);
            _entityContainer.RegisterEntity(startRaceButton);
        }

        public void CreateCountdown(Transform root)
        {
            Countdown countdown = Object.Instantiate(_staticData.Prefabs.CountdownPrefab, root);
            countdown.Construct(_soundService);
            countdown.Hide();
            _entityContainer.RegisterEntity(countdown);
        }

        public void CreateWinPopUp(Transform root)
        {
            WinPopUp winPopUp = Object.Instantiate(_staticData.Prefabs.WinPopUpPrefab, root);
            winPopUp.Construct(_soundService);
            winPopUp.Hide();
            _entityContainer.RegisterEntity(winPopUp);
        }

        public void CreateBestTime(Transform root)
        {
            BestTimePopUp bestTimePopUp = Object.Instantiate(_staticData.Prefabs.BestTimePopUpPrefab, root);
            bestTimePopUp.SetText(_progress.Progress.BestTime);
            BestButton bestButton = Object.Instantiate(_staticData.Prefabs.BestButtonPrefab, root);
            _entityContainer.RegisterEntity(bestTimePopUp);
            _entityContainer.RegisterEntity(bestButton);
        }

        public void CreateBestTimeView(Transform root)
        {
            BestTimeView view = Object.Instantiate(_staticData.Prefabs.BestTimeViewPrefab, root);
            _entityContainer.RegisterEntity(view);
        }

        public void CreateSettings(Transform root)
        {
            SettingsView view = Object.Instantiate(_staticData.Prefabs.SettingsPrefab, root);
            view.Construct(_staticData.SoundData.VolumeOn, _staticData.SoundData.VolumeOff, _progress.Progress.Settings.Volume);
            Settings settings = new Settings(view, _stateMachine, _soundService, _entityContainer.GetEntity<Countdown>());
            view.Hide();
            _entityContainer.RegisterEntity(settings);
        }

        public void CreateVolumeSettings(Transform root)
        {
            VolumeSettings volumeSettings = Object.Instantiate(_staticData.Prefabs.VolumeSettingsPrefab, root);
            volumeSettings.Construct(_progress.Progress.Settings.Volume);
            _entityContainer.RegisterEntity(volumeSettings);
        }
    }
}