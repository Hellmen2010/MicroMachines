using System;

namespace MicroMachines.Code.Core.Checkpoint
{
    public class RaceProgressTracker
    {
        public event Action OnRaceEnded;
        
        public bool IsFinished { get; private set; }
        private readonly Checkpoint[] _checkpoints;
        private int _checkpointsTriggered;
        private int _currentLap;
        private int _endLap;

        public RaceProgressTracker(Checkpoint[] checkpoints) => _checkpoints = checkpoints;

        public void ResetTrackProgress()
        {
            IsFinished = false;
            _currentLap = 0;
            _checkpointsTriggered = 0;
            foreach (Checkpoint checkpoint in _checkpoints) 
                checkpoint.Passed();
            _checkpoints[0].NextToPass();
        }

        public void Subscribe()
        {
            foreach (Checkpoint checkpoint in _checkpoints) 
                checkpoint.OnTrigger += TryCount;
        }
        
        private void UnSubscribe()
        {
            foreach (Checkpoint checkpoint in _checkpoints) 
                checkpoint.OnTrigger -= TryCount;
        }

        private void TryCount(int number)
        {
            if (number != _checkpointsTriggered) return;
            _checkpoints[number].Passed();
            if(IsFinish(number))
            {
                if (_currentLap < _endLap)
                {
                    _currentLap++;
                    _checkpointsTriggered = 0;
                    _checkpoints[0].NextToPass();
                }
                else
                {
                    IsFinished = true;
                    OnRaceEnded?.Invoke();
                    UnSubscribe();
                }
            }
            else
            {
                _checkpointsTriggered++;
                _checkpoints[_checkpointsTriggered].NextToPass();
            }
        }

        private bool IsFinish(int number) => number >= _checkpoints.Length - 1;
    }
}