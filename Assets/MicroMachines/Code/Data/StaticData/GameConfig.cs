using UnityEngine;

namespace MicroMachines.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int Countdown;
    }
}