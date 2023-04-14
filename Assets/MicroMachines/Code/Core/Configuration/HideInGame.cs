using UnityEngine;

namespace MicroMachines.Code.Core.Configuration
{
    public class HideInGame : MonoBehaviour
    {
        private void Start() => gameObject.SetActive(false);
    }
}