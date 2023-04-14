using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Control
{
    public class GasButton : Button
    {
        public event Action OnPressed;
        public event Action OnReleased;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPressed?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnReleased?.Invoke();
        }
        
        public void ReleaseButton() => OnReleased?.Invoke();
    }
}