using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Vector2 MovementValue { get; private set; }
        public event Action AttackBasicEvent;
        public event Action AttackHeavyEvent;
        public event Action DashEvent;
        public bool IsBlocking { get; private set; }

        private Controls _controls;
    
        private void Start()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        private void OnDestroy()
        {
            _controls.Player.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnAttackBasic(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }
            AttackBasicEvent?.Invoke();
        }

        public void OnAttackHeavy(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }
            AttackHeavyEvent?.Invoke();
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsBlocking = true;
            }
            else if (context.canceled)
            {
                IsBlocking = false;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }
            DashEvent?.Invoke();
        }
    }
}
