using Seedon;
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
            _inputActions.Player.ToggleConsole.performed += ctx => OnToggleConsolePerfomed();
            _inputActions.UI.Pick1.performed += ctx => OnPick1Perfomed();
            _inputActions.UI.Pick2.performed += ctx => OnPick2Perfomed();
            _inputActions.UI.Pick3.performed += ctx => OnPick3Perfomed();
        }

        public override void DisableComponent()
        {
            _inputActions.Player.Dash.performed -= ctx => OnDashPerfomed();
            _inputActions.Player.Attack.performed -= ctx => OnAttackPerfomed();
            _inputActions.Player.ToggleCursor.performed -= ctx => OnToggleCursorPerfomed();
            _inputActions.Player.ToggleConsole.performed -= ctx => OnToggleConsolePerfomed();
            _inputActions.UI.Pick1.performed -= ctx => OnPick1Perfomed();
            _inputActions.UI.Pick2.performed -= ctx => OnPick2Perfomed();
            _inputActions.UI.Pick3.performed -= ctx => OnPick3Perfomed();
            _inputActions.Disable();
        }

        public override void UpdateComponent()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.Game)
            {
                _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
                GameManager.StaticInstance.Player.Locomotion.MoveDirection = (GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x + GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y).normalized;
                Plane groundPlane = new(Vector3.up, Vector3.zero);
                Ray cameraRay = Camera.main.ScreenPointToRay(_inputActions.UI.Point.ReadValue<Vector2>());
                if (groundPlane.Raycast(cameraRay, out float length))
                {
                    GameManager.StaticInstance.Player.Locomotion.LookPoint = cameraRay.GetPoint(length);
                }
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

        private void OnToggleConsolePerfomed()
        {
            ConsoleToGUI.StaticInstance.ToggleConsole();
        }

        private void OnPick1Perfomed()
        {
            GameManager.StaticInstance.UIManager.ImplantSelectionUI.SelectImplantByIndex(0);
        }

        private void OnPick2Perfomed()
        {
            GameManager.StaticInstance.UIManager.ImplantSelectionUI.SelectImplantByIndex(1);
        }

        private void OnPick3Perfomed()
        {
            GameManager.StaticInstance.UIManager.ImplantSelectionUI.SelectImplantByIndex(2);
        }
    }
}