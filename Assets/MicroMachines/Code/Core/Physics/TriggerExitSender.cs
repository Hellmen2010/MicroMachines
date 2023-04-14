using System;
using UnityEngine;

namespace MicroMachines.Code.Core.Physics
{
    public class TriggerExitSender : MonoBehaviour
    {
        public event Action OnTriggerExit;

        private void OnTriggerExit2D(Collider2D col) => OnTriggerExit?.Invoke();
    }
}