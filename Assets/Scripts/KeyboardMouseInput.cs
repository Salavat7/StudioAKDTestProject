using System;
using UnityEngine;
using Zenject;

namespace PlayerInput
{
    public class KeyboardMouseInput : IPlayerInput, ITickable
    {
        public event Action<Vector3> RotationOfCamera;
        public event Action<Vector2> Movement;
        public event Action<bool> Pick;
        private Vector3 _lastMousePos;
        private float _RotationOfCameraSensitivity = 0.25f;

        public KeyboardMouseInput()
        {
            _lastMousePos = Input.mousePosition;
        }

        private Vector3 AngleOfCameraRotation(Vector3 mousePos)
        {
            Vector3 difference = mousePos - _lastMousePos;
            Vector3 coordinatesTransform = new Vector3(-difference.y * _RotationOfCameraSensitivity, difference.x * _RotationOfCameraSensitivity, 0);
            _lastMousePos = Input.mousePosition;
            return coordinatesTransform;
        }

        public void Tick()
        {
            Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Movement?.Invoke(movementVector);
            RotationOfCamera?.Invoke(AngleOfCameraRotation(Input.mousePosition));
            Pick?.Invoke(Input.GetMouseButton(0));
        }
    }
}