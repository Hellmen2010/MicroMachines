using MicroMachines.Code.Core.Timer;
using MicroMachines.Code.Infrastructure.StateMachine.GameStateMachine;
using MicroMachines.Code.Infrastructure.StateMachine.States;
using MicroMachines.Code.Services.Sound;
using UnityEngine;

namespace MicroMachines.Code.Core.Settings
{
    public class Settings
    {
        private readonly SettingsView _view;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISoundService _soundService;
        private readonly Countdown _countdown;

        public Settings(SettingsView view, IGameStateMachine stateMachine, ISoundService soundService, Countdown countdown)
        {
            _view = view;
            _stateMachine = stateMachine;
            _soundService = soundService;
            _countdown = countdown;
            Subscribe();
        }

        private void Subscribe()
        {
            _view.OnBackButton += MoveToMenu;
            _view.OnCloseClicked += TryResumeGameLoop;
            _view.OnRestartButton += MoveToPreparation;
            _view.OnUnPause += TryResumeGameLoop;
            _view.OnVolumeButton += _soundService.SetVolume;
            _view.OnVolumeChanged += _soundService.SetVolume;
        }

        private void TryResumeGameLoop()
        {
            if (_countdown.Time > 0) EnableTime();
            else _countdown.StartCountdown(2, EnableTime);
        }
        
        private void EnableTime() => Time.timeScale = 1;

        private void MoveToPreparation()
        {
            EnableTime();
            _stateMachine.Enter<PreparationState>();
        }

        private void MoveToMenu()
        {
            EnableTime();
            _stateMachine.Enter<MainMenuState>();
        }

        public void DisableRestartButton() => _view.RestartButton.interactable = false;
        public void EnableRestartButton() => _view.RestartButton.interactable = true;
    }
}