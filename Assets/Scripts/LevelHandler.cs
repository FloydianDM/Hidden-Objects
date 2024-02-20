using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class LevelHandler : MonoBehaviour
    {
        public static LevelHandler Instance;
        
        [SerializeField] private List<GameObject> _allObjects;
        [SerializeField] private List<GameObject> _activeHiddenObjects;
        [SerializeField] private int _countOfHide = 5;

        private string _hiddenObjectsTag = "HiddenObjects";

        private void Awake()
        {
            ManageSingleton();
        }

        private void Start()
        {
            _allObjects = new();
            _activeHiddenObjects = new();

            SetObjectsList();
            SetActiveHiddenObjectsList();
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
       
        private void SetObjectsList()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(_hiddenObjectsTag);

            foreach (var obj in objects)
            {
                obj.GetComponent<Collider2D>().enabled = false;
                _allObjects.Add(obj);
            }
        }

        private void SetActiveHiddenObjectsList()
        {   
            int i = 0;

            while (i < _countOfHide)
            {
                int randomIndex = Random.Range(0, _allObjects.Count);

                if (_allObjects[randomIndex].GetComponent<DataManager>().GetIsHidden() == false)
                {
                    _activeHiddenObjects.Add(_allObjects[randomIndex]); 
                    i++;      
                }
            }

            foreach (var hiddenObject in _activeHiddenObjects)
            {
                hiddenObject.GetComponent<DataManager>().SetIsHidden(true);
                hiddenObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}

