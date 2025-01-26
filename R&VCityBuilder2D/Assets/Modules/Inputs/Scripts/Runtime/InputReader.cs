using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Inputs.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "New InputReader", menuName = "Modules/Input System/InputReader")]
    public class InputReader : ScriptableObject, IControls.IPlayerActions
    {
        public Vector2 PointPosition { get; private set; }
        public Vector2 MovementInputValue { get; private set; }
        public Vector2 ZoomInputValue { get; private set; }
        public event Action OnClickButtonDown;

        private IControls controls;
        
        public void InitializeInputs()
        {
            controls = new IControls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
        }

        public void DeinitializeInputs()
        {
            controls.Player.Disable();
        }
        
        public void OnInputPosition(InputAction.CallbackContext context)
        {
           PointPosition = context.ReadValue<Vector2>();
        }

        public void OnInputButton(InputAction.CallbackContext context)
        {
            if (context.performed) OnClickButtonDown?.Invoke();
        }

        public void OnInputKeyBinding(InputAction.CallbackContext context)
        {
           MovementInputValue = context.ReadValue<Vector2>();
        }

        public void OnInputZoom(InputAction.CallbackContext context)
        {
           ZoomInputValue = context.ReadValue<Vector2>();
        }
    }
}