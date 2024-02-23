using Hidden_Objects.Input;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class PlayerController : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private LevelHandler _levelHandler;

        private void Start()
        {
            _levelHandler = GetComponent<LevelHandler>();
            _inputHandler = FindObjectOfType<InputHandler>();
            _inputHandler.OnTouched += SelectObject;
        }

        private void SelectObject()
        {
            foreach (var obj in _levelHandler.ActiveHiddenObjects)
            {
                if (obj.GetComponent<Collider2D>().bounds.Contains(_inputHandler.WorldPosition))
                {
                    _levelHandler.FindHidden(obj);
                }
            }
        }
    }
}

