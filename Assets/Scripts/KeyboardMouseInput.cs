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
        private float _RotationOfCameraSensitivity = 1;

        private Vector3 AngleOfCameraRotation(Vector3 deltaMousePos)
        {
            Vector3 coordinatesTransform = new Vector3(-deltaMousePos.y * _RotationOfCameraSensitivity, deltaMousePos.x * _RotationOfCameraSensitivity, 0);
            return coordinatesTransform;
        }

        public void Tick()
        {
            Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Movement?.Invoke(movementVector);
            RotationOfCamera?.Invoke(AngleOfCameraRotation(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0)));
            Pick?.Invoke(Input.GetMouseButton(0));
        }
    }
}