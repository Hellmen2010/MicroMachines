using System;
using MicroMachines.Code.Core.Control;
using MicroMachines.Code.Data.Enums;
using MicroMachines.Code.Data.StaticData;
using MicroMachines.Code.Data.StaticData.Car;
using MicroMachines.Code.Services.Sound;
using UnityEngine;

namespace MicroMachines.Code.Core.Car
{
    public class Car
    {
        private readonly CarInput _input;
        private readonly ISoundService _soundService;
        public CarGameView CarView { get; private set; }
        private float _accelerationSpeed;
        private float _driftSpeed;
        private float _turnSpeed;
        private float _maxSpeed;
        private float _rotationAngle;

        public Car(CarInput input, CarGameView carView, CarPhysicsData physicsData, ISoundService soundService)
        {
            _accelerationSpeed = physicsData.AccelerationSpeed;
            _driftSpeed = physicsData.DriftSpeed;
            _turnSpeed = physicsData.TurnSpeed;
            _maxSpeed = physicsData.MaxSpeed;
            _input = input;
            _soundService = soundService;
            CarView = carView;
            _rotationAngle = carView.transform.rotation.eulerAngles.z;
        }

        public void Subscribe()
        {
            _input.GasButton.OnPressed += StartAccelerationCalculation;
            _input.GasButton.OnReleased += StopAcceleration;
            _input.GasButton.OnReleased += StopAccelerationCalculation;
            _input.OnJoystickDrag += MoveSide;
            _input.OnJoystickRelease += KillOrthogonalVelocity;
            CarView.OnUpdate += TryTireScreeching;
        }

        public void UnSubscribe()
        {
            _input.GasButton.OnPressed -= StartAccelerationCalculation;
            _input.GasButton.OnReleased -= StopAcceleration;
            _input.GasButton.OnReleased -= StopAccelerationCalculation;
            _input.OnJoystickDrag -= MoveSide;
            _input.OnJoystickRelease -= KillOrthogonalVelocity;
            CarView.OnUpdate -= TryTireScreeching;
        }

        public void ResetCar(Location spawnLocation)
        {
            StopMovement();
            CarView.transform.position = spawnLocation.Position;
            CarView.transform.rotation = spawnLocation.Rotation;
            _rotationAngle = CarView.transform.rotation.eulerAngles.z;
            CarView.Rb.bodyType = RigidbodyType2D.Dynamic;
            CarView.CarShow();
        }

        public void StopMovement()
        {
            CarView.DisableTrails();
            CarView.Rb.velocity = Vector2.zero;
        }

        private void StartAccelerationCalculation()
        {
            _soundService.PlayDrive(SoundId.Drive);
            CarView.OnFixedUpdate += StartAcceleration;
        }

        private void StopAccelerationCalculation()
        {
            _soundService.StopDrive();
            CarView.OnFixedUpdate -= StartAcceleration;
        }

        private void StartAcceleration()
        {
            if(Vector2.Dot(CarView.transform.up, CarView.Rb.velocity) > _maxSpeed) return;
            CarView.Rb.drag = 0;
            Vector2 forwardVector = CarView.transform.up * _accelerationSpeed;
            CarView.Rb.AddForce(forwardVector, ForceMode2D.Force);
            KillOrthogonalVelocity();
        }

        private void StopAcceleration()
        {
            CarView.Rb.drag = Mathf.Lerp(CarView.Rb.drag, 3.0f, Time.fixedDeltaTime * 3);
            _input.GasButton.OnPressed -= StartAcceleration;
        }

        private void MoveSide(float value)
        {
            float minSpeedForRotation = Mathf.Clamp01(CarView.Rb.velocity.magnitude / 8);
            _rotationAngle -= value * _turnSpeed * minSpeedForRotation * Time.fixedDeltaTime;
            CarView.Rb.MoveRotation(_rotationAngle);
            KillOrthogonalVelocity();
        }

        private void KillOrthogonalVelocity()
        {
            Vector2 forwardVelocity = CarView.transform.up *
                                      Vector2.Dot(CarView.Rb.velocity, CarView.Rb.transform.up);
            Vector2 rightVelocity = CarView.transform.right *
                                    Vector2.Dot(CarView.Rb.velocity, CarView.transform.right);
            CarView.Rb.velocity = forwardVelocity + rightVelocity * _driftSpeed;
        }

        private void TryTireScreeching()
        {
            if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
            {
                CarView.EnableTrails();
                _soundService.PlayDrift(SoundId.Drift);
            }
            else
            {
                _soundService.StopDrift();
                CarView.DisableTrails();
            }
        }

        private float GetLateralVelocity() => Vector2.Dot(CarView.transform.right, CarView.Rb.velocity);

        public void Hide(Action onHided) => CarView.CarHide(onHided);
    }
}