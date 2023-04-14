using Cinemachine;
using DanielLochner.Assets.SimpleScrollSnap;
using MicroMachines.Code.Core.Buttons;
using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Core.MainMenu;
using MicroMachines.Code.Core.Physics;
using MicroMachines.Code.Core.PopUp;
using MicroMachines.Code.Core.Settings;
using MicroMachines.Code.Core.Timer;
using UnityEngine;

namespace MicroMachines.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "StaticData/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        public Transform RootCanvasPrefab;
        
        [Header("UI")]
        public MainMenuView MainMenuPrefab;
        public CarInput CarInputPrefab;
        public TimerView timerViewPrefab;
        public BackButton BackButtonPrefab;
        public DynamicContent CarSelectionPrefab;
        public GameObject CarUIViewPrefab;
        public StartRaceButton StartRaceButtonPrefab;
        public Countdown CountdownPrefab;
        public WinPopUp WinPopUpPrefab;
        public BestTimePopUp BestTimePopUpPrefab;
        public BestButton BestButtonPrefab;
        public BestTimeView BestTimeViewPrefab;
        public SettingsView SettingsPrefab;
        public VolumeSettings VolumeSettingsPrefab;

        [Header("SceneObjects")]
        public CarGameView CarGameViewPrefab;
        public Core.Checkpoint.Checkpoint CheckpointPrefab;
        public Road RoadPrefab;
        public CinemachineVirtualCamera CameraPrefab;
    }
}