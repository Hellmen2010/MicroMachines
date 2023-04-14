using MicroMachines.Code.Data.StaticData.Locations;
using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Checkpoint
{
    [CreateAssetMenu(fileName = "SpawnpointsData", menuName = "StaticData/SpawnpointsData")]
    public class SpawnpointsData : ScriptableObject
    {
        public CheckpointData[] CheckPointsData;
        public Location CarSpawnLocation;
    }
}