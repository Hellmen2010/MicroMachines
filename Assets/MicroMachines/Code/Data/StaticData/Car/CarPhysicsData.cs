using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Car
{
    [CreateAssetMenu(fileName = "CarPhysicsData", menuName = "StaticData/CarPhysicsData")]
    public class CarPhysicsData : ScriptableObject
    {
        public float AccelerationSpeed = 30;
        public float DriftSpeed = 0.95f;
        public float TurnSpeed = 100f;
        public float MaxSpeed = 20;
    }
}