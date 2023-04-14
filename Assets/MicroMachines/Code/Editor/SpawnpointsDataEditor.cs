using MicroMachines.Code.Core.Car;
using MicroMachines.Code.Core.Checkpoint;
using MicroMachines.Code.Data.StaticData.Checkpoint;
using MicroMachines.Code.Data.StaticData.Locations;
using MicroMachines.Code.Extensions;
using UnityEditor;
using UnityEngine;

namespace MicroMachines.Code.Editor
{
    [CustomEditor(typeof(SpawnpointsData))]
    public class SpawnpointsDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Collect") == false) return;

            SpawnpointsData spawnpointsData = (SpawnpointsData)target;
            CheckpointMarker[] checkpoints = FindObjectsOfType<CheckpointMarker>();
            spawnpointsData.CheckPointsData = new CheckpointData[checkpoints.Length];

            for (int i = 0; i < checkpoints.Length; i++)
            {
                var checkpointData = new CheckpointData(
                    checkpoints[i].transform.position, 
                    checkpoints[i].transform.rotation, 
                    checkpoints[i].transform.localScale,
                    checkpoints[i].Number);
                spawnpointsData.CheckPointsData[i] = checkpointData;
            }

            spawnpointsData.CarSpawnLocation = FindObjectOfType<CarGameView>().transform.ToLocation();

            EditorUtility.SetDirty(spawnpointsData);
        }
    }
}