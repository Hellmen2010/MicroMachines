using TMPro;
using UnityEngine;

namespace MicroMachines.Code.Core.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(float value)
        {
            _text.text = string.Format("{0:00}:{1:00}:{2:00}",
                Mathf.Floor(value / 60),//minutes
                Mathf.Floor(value) % 60,//seconds
                Mathf.Floor((value*100) % 100));//miliseconds
        }
    }
}