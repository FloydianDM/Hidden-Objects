using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class LevelHandler : MonoBehaviour
    {
        public static LevelHandler Instance;
        
        [SerializeField] private List<GameObject> _allObjects;
        [SerializeField] private int _countOfHide = 5;

        public List<GameObject> ActiveHiddenObjects { get; private set; }
        private string _hiddenObjectsTag = "HiddenObjects";

        private void Awake()
        {
            ManageSingleton();
        }

        private void Start()
        {
            _allObjects = new();
            ActiveHiddenObjects = new();

            SetObjectsList();
            SetActiveHiddenObjectsList();
        }

        private void ManageSingleton()
        {
            if (Instance != null && Instance != this)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
       
        private void SetObjectsList()
        {
            _allObjects.Clear();
            GameObject[] objects = GameObject.FindGameObjectsWithTag(_hiddenObjectsTag);

            foreach (var obj in objects)
            {
                obj.GetComponent<Collider2D>().enabled = false;
                _allObjects.Add(obj);
            }
        }

        private void SetActiveHiddenObjectsList()
        {   
            ActiveHiddenObjects.Clear();

            int i = 0;

            while (true)
            {
                int randomIndex = Random.Range(0, _allObjects.Count);

                if (_allObjects[randomIndex].GetComponent<DataManager>().IsHidden == false)
                {
                    ActiveHiddenObjects.Add(_allObjects[randomIndex]); 
                    _allObjects[randomIndex].GetComponent<DataManager>().SetIsHidden(true);
                    i++;      
                }
                else
                {
                    continue;
                }
                
                if (i == _countOfHide)
                {
                    break;
                }
            }

            foreach (var hiddenObject in ActiveHiddenObjects)
            {
                MakeHidden(hiddenObject);
            }
        }

        private void MakeHidden(GameObject hiddenObject)
        {
            hiddenObject.GetComponent<Collider2D>().enabled = true;
            hiddenObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void MakeVisible(GameObject hiddenObject)
        {
            hiddenObject.GetComponent<Collider2D>().enabled = false;
            hiddenObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

