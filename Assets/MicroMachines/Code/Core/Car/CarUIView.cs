using UnityEngine;
using UnityEngine.UI;

namespace MicroMachines.Code.Core.Car
{
    public class CarUIView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void Construct(Sprite sprite) => _image.sprite = sprite;
    }
}