using System;
using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Locations
{
    [Serializable]
    public class CheckpointData
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
        public int Number;

        public CheckpointData(Vector3 position, Quaternion rotation, Vector3 scale, int number)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Number = number;
        }
    }
}