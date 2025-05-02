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
        }

        public override void DisableComponent()
        {
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
    }
}