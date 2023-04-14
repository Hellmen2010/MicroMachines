using System;
using MicroMachines.Code.Core.Physics;
using MicroMachines.Code.Data.StaticData;
using UnityEngine;

namespace MicroMachines.Code.Core.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        public event Action<int> OnTrigger;

        [SerializeField] private SpriteRenderer _road;
        [SerializeField] private TriggerEnterSender triggerEnter;
        private Sprite _roadHighlighted;
        private Sprite _roadUnHighlighted;
        private int _number;

        private void Start() => triggerEnter.OnTriggerEnter += CheckpointTriggered;

        private void CheckpointTriggered() => OnTrigger?.Invoke(_number);

        public void Construct(EnvironmentSpritesData data, int number)
        {
            _number = number;
            _roadHighlighted = data.RoadHighlighted;
            _roadUnHighlighted = data.RoadUnHighlighted;
        }

        public void MakeTransparent() => _road.color = new Color(_road.color.r, _road.color.g, _road.color.b, 0);

        public void Passed() => _road.sprite = _roadUnHighlighted;

        public void NextToPass() => _road.sprite = _roadHighlighted;

        private void OnDestroy() => triggerEnter.OnTriggerEnter -= CheckpointTriggered;
    }
}