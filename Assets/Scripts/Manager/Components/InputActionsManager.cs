using System;
using UnityEngine;

namespace WinterUniverse
{
    public class InputActionsManager : BasicComponent
    {
        private InputActions _inputActions;
        private Vector2 _moveInput;

        public override void InitializeComponent()
        {
            _inputActions = new();
        }

        public override void EnableComponent()
        {
            _inputActions.Enable();
            _inputActions.Player.Dash.performed += ctx => OnDashPerfomed();
            _inputActions.Player.Attack.performed += ctx => OnAttackPerfomed();
            _inputActions.Player.ToggleCursor.performed += ctx => OnToggleCursorPerfomed();
        }

        public override void DisableComponent()
        {
            _inputActions.Player.Dash.performed -= ctx => OnDashPerfomed();
            _inputActions.Player.Attack.performed -= ctx => OnAttackPerfomed();
            _inputActions.Player.ToggleCursor.performed -= ctx => OnToggleCursorPerfomed();
            _inputActions.Disable();
        }

        public override void UpdateComponent()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.Game)
            {
                _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
                GameManager.StaticInstance.Player.Locomotion.MoveDirection = (GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x + GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y).normalized;
            }
            else
            {
                GameManager.StaticInstance.Player.Locomotion.MoveDirection = Vector3.zero;
            }
        }

        private void OnDashPerfomed()
        {
            if (GameManager.StaticInstance.InputMode != InputMode.Game)
            {
                return;
            }
            GameManager.StaticInstance.Player.Locomotion.PerformDash();
        }

        private void OnAttackPerfomed()
        {
            if (GameManager.StaticInstance.InputMode != InputMode.Game)
            {
                return;
            }
            GameManager.StaticInstance.Player.Combat.PerformAttack(true, out _);
        }

        private void OnToggleCursorPerfomed()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.Game)
            {
                GameManager.StaticInstance.SetInputMode(InputMode.UI);
            }
            else
            {
                GameManager.StaticInstance.SetInputMode(InputMode.Game);
            }
        }
    }
}