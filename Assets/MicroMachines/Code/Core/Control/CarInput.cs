using System;
using UnityEngine;

namespace MicroMachines.Code.Core.Control
{
    public class CarInput : MonoBehaviour
    {
        public event Action<float> OnJoystickDrag;
        public event Action OnJoystickRelease;

        public GasButton GasButton;
        [SerializeField] private Joystick _joystick;

        private void FixedUpdate()
        {
            if (Mathf.Abs(_joystick.Horizontal) > 0.1f) 
                OnJoystickDrag?.Invoke(_joystick.Horizontal);
            else
                OnJoystickRelease?.Invoke();
        }
    }
}