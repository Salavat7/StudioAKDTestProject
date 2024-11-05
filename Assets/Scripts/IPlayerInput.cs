using System;
using UnityEngine;

namespace PlayerInput
{
    public interface IPlayerInput
    {
        public event Action<Vector3> RotationOfCamera;
        public event Action<Vector2> Movement;
        public event Action<bool> Pick;
    }
}