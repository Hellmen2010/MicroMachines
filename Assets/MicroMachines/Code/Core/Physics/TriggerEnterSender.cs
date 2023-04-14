using System;
using UnityEngine;

namespace MicroMachines.Code.Core.Physics
{
    public class TriggerEnterSender : MonoBehaviour
    {
        public event Action OnTriggerEnter;

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTriggerEnter?.Invoke();
        }
    }
}