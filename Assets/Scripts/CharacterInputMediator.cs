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

            _playerInput.HorizontalInput += OnHorizontalInput;
            _playerInput.VerticalInput += OnVerticalInput;
            _playerInput.RotationOfCamera += OnRotationOfCamera;
            _playerInput.Pick += OnPick;
        }

        public void Dispose()
        {
            _playerInput.HorizontalInput -= OnHorizontalInput;
            _playerInput.VerticalInput -= OnVerticalInput;
            _playerInput.RotationOfCamera -= OnRotationOfCamera;
        }

        private void OnHorizontalInput(float horizontalInput)
        {
            _characterBehavior.SetRightMoving(horizontalInput);
        }

        private void OnVerticalInput(float verticalInput)
        {
            _characterBehavior.SetForwardMoving(verticalInput);
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