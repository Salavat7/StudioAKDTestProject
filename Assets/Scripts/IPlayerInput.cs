using System;
using UnityEngine;

namespace PlayerInput
{
    public interface IPlayerInput
    {
        public event Action<Vector3> RotationOfCamera;
        public event Action<float> HorizontalInput;
        public event Action<float> VerticalInput;

        public void Update();
        public void Enable();
        public void Disable();
    }
}