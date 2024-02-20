using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hidden_Objects.Input
{
    public class InputHandler : MonoBehaviour
    {
        public event Action OnTouched;

        private void Update()
        {
            ProcessTouchInput();
        }

        private void ProcessTouchInput()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                if (OnTouched != null)
                {
                    OnTouched();
                }

                GetPosition();
            }
        }

        private Vector2 GetPosition()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            return worldPosition;
        }
    }    
}

