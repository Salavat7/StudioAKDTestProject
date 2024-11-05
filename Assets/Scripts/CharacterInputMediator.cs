using System;
using PlayerInput;
using UnityEngine;

namespace Mediators
{
    public class CharacterInputMediator : IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly CharacterBehavior _characterBehavior;

        public CharacterInputMediator(IPlayerInput playerInput, CharacterBehavior characterBehavior)
        {
            _playerInput = playerInput;
            _characterBehavior = characterBehavior;

            _playerInput.Movement += OnMovement;
            _playerInput.RotationOfCamera += OnRotationOfCamera;
            _playerInput.Pick += OnPick;
        }

        public void Dispose()
        {
            _playerInput.Movement -= OnMovement;
            _playerInput.RotationOfCamera -= OnRotationOfCamera;
            _playerInput.Pick -= OnPick;
        }

        private void OnMovement(Vector2 vector)
        {
            _characterBehavior.SetMoving(vector);
        }

        private void OnRotationOfCamera(Vector3 angleOfCameraRotation)
        {
            _characterBehavior.SetRotationOfCamera(angleOfCameraRotation);
        }

        private void OnPick(bool pick)
        {
            _characterBehavior.Pick(pick);
        }
    }
}