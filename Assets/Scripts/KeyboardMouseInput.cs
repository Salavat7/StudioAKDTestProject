using System;
using UnityEngine;
using Zenject;

namespace PlayerInput
{
    public class KeyboardMouseInput : IPlayerInput, ITickable
    {
        public event Action<Vector3> RotationOfCamera;
        public event Action<float> HorizontalInput;
        public event Action<float> VerticalInput;
        public event Action<bool> Pick;

        private bool _enable;
        private Vector3 _lastMousePos;
        private float _RotationOfCameraSensitivity = 0.25f;

        public KeyboardMouseInput()
        {
            _enable = true;
            _lastMousePos = Input.mousePosition;
        }

        public void Disable()
        {
            _enable = false;
        }

        public void Enable()
        {
            _enable = true;
        }

        public void Update()
        {
            if (_enable)
            {
                HorizontalInput?.Invoke(Input.GetAxis("Horizontal"));
                VerticalInput?.Invoke(Input.GetAxis("Vertical"));
                RotationOfCamera?.Invoke(AngleOfCameraRotation(Input.mousePosition));
                Pick?.Invoke(Input.GetMouseButton(0));
            }
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
            Update();
        }
    }
}