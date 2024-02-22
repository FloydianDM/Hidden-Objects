using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hidden_Objects.Input
{
    public class InputHandler : MonoBehaviour
    {
        public Vector2 WorldPosition { get; private set; }
        public event Action OnTouched;

        private void Update()
        {
            ProcessTouchInput();
        }

        private void ProcessTouchInput()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                OnTouched?.Invoke();
                GetPosition();
            }
        }

        public void GetPosition()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            WorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        }
    }    
}

