using UnityEngine;

namespace MicroMachines.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "EnvironmentSpritesData", menuName = "StaticData/EnvironmentSpritesData")]
    public class EnvironmentSpritesData : ScriptableObject
    {
        public Sprite RoadHighlighted;
        public Sprite RoadUnHighlighted;
    }
}