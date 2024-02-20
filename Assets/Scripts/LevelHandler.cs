using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class LevelHandler : MonoBehaviour
    {
        public static LevelHandler Instance;
        
        [SerializeField] private List<HiddenObjectData> _hiddenObjects;
        [SerializeField] private List<HiddenObjectData> _activeHiddenObjects;

        private void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}

