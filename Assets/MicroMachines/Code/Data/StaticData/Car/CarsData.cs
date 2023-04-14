using UnityEngine;

namespace MicroMachines.Code.Data.StaticData.Car
{
    [CreateAssetMenu(fileName = "CarsData", menuName = "StaticData/CarsData")]
    public class CarsData : ScriptableObject
    {
        public CarData[] Cars;
    }
}