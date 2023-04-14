using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Core.Settings;
using MicroMachines.Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace MicroMachines.Code.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        Transform CreateRootCanvas();
        void CreateMainMenu(Transform root);
        CarInput CreateCarInput(Transform root);
        void CreateTimer(Transform root);
        void CreateBackButton(Transform persistantRoot);
        void CreateCarSelectionView(Transform carSelectionRoot);
        void CreateStartButton(Transform menuRoot);
        void CreateCountdown(Transform root);
        void CreateWinPopUp(Transform root);
        void CreateBestTime(Transform root);
        void CreateBestTimeView(Transform root);
        void CreateSettings(Transform root);
        void CreateVolumeSettings(Transform root);
    }
}