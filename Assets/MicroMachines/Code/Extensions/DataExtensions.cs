using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Locations;
using UnityEngine;

namespace MicroMachines.Code.Extensions
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);
        
        public static Location ToLocation(this Transform transform) =>
            new (transform.position, transform.rotation);
    }
}