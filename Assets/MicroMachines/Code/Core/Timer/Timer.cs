using System.Collections;
using UnityEngine;

namespace MicroMachines.Code.Core.Timer
{
    public class Timer
    {
        private readonly TimerView _view;
        private float _currentTime;
        private Coroutine _timerRoutine;

        public Timer(TimerView view) => _view = view;

        public void StartTimer() => 
            _timerRoutine = _view.StartCoroutine(TimerRoutine());

        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                _view.SetText(_currentTime);
                yield return null;
                _currentTime += Time.deltaTime;
            }
        }

        public float StopTimer()
        {
            _view.StopCoroutine(_timerRoutine);
            return _currentTime;
        }

        public void ResetTimer()
        {
            if (_timerRoutine == null) return;
            _view.StopCoroutine(_timerRoutine);
            _currentTime = 0;
            _view.SetText(_currentTime);
        }
    }
}