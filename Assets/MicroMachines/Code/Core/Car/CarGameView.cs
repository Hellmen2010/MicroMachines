using System;
using System.Collections;
using UnityEngine;

namespace MicroMachines.Code.Core.Car
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CarGameView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _carSprite;
        [SerializeField] private TrailRenderer _trailRenderer1;
        [SerializeField] private TrailRenderer _trailRenderer2;
        public event Action OnFixedUpdate;
        public event Action OnUpdate;
        
        public Rigidbody2D Rb;

        public void Construct(Sprite sprite) => _carSprite.sprite = sprite;
        
        private void Awake() => DisableTrails();

        private void Update() => OnUpdate?.Invoke();

        private void FixedUpdate() => OnFixedUpdate?.Invoke();
        
        public void EnableTrails()
        {
            _trailRenderer1.emitting = true;
            _trailRenderer2.emitting = true;
        }

        public void DisableTrails()
        {
            _trailRenderer1.emitting = false;
            _trailRenderer2.emitting = false;
        }

        public void CarHide(Action OnCompleted = null) => 
            StartCoroutine(CarHideRoutine(OnCompleted));

        public void CarShow() => _carSprite.color = new Color(_carSprite.color.r, _carSprite.color.g, _carSprite.color.b, 255);
        
        private IEnumerator CarHideRoutine(Action onCompleted)
        {
            float alpha = 1f;
            while (_carSprite.color.a > 0)
            {
                _carSprite.color = new Color(_carSprite.color.r, _carSprite.color.g, _carSprite.color.b, alpha -= Time.deltaTime);
                yield return null;
            }
            _carSprite.color = new Color(_carSprite.color.r, _carSprite.color.g, _carSprite.color.b, 0);
            yield return new WaitForSeconds(0.5f);
            onCompleted?.Invoke();
        }
    }
}