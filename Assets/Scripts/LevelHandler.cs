using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class LevelHandler : MonoBehaviour
    {
        public static LevelHandler Instance;
        
        [SerializeField] private int _countOfHide = 5;

        private List<GameObject> _allObjects;
        public List<GameObject> ActiveHiddenObjects { get; private set; }
        private int _countOfFound = 0;
        private ScoreManager _scoreManager;
        private GameManager _gameManager;
        public const string HIDDEN_OBJECTS_TAG = "HiddenObjects";
        public event Action OnActiveHiddenSet;

        private void Awake()
        {
            ManageSingleton();
        }

        private void Start()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
            _gameManager = FindObjectOfType<GameManager>();

            _allObjects = new();
            ActiveHiddenObjects = new();

            SetObjectsList();
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
            GameObject[] objects = GameObject.FindGameObjectsWithTag(HIDDEN_OBJECTS_TAG);

            foreach (var obj in objects)
            {
                obj.GetComponent<Collider2D>().enabled = false;
                _allObjects.Add(obj);
            }

            SetActiveHiddenObjectsList();
        }

        private void SetActiveHiddenObjectsList()
        {   
            ActiveHiddenObjects.Clear();

            int i = 0;

            while (true)
            {
                int randomIndex = UnityEngine.Random.Range(0, _allObjects.Count);

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
                PrepareHidden(hiddenObject);
            }

            OnActiveHiddenSet?.Invoke();
        }

        private void PrepareHidden(GameObject hiddenObject)
        {
            hiddenObject.GetComponent<Collider2D>().enabled = true;
            hiddenObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        public void FindHidden(GameObject hiddenObject)
        {
            hiddenObject.GetComponent<Collider2D>().enabled = false;
            hiddenObject.GetComponent<SpriteRenderer>().enabled = false;
            
            _countOfFound++;
            _scoreManager.AddScore(100);

            if (_countOfFound >= _countOfHide)
            {
                _gameManager.ProcessGameOver();
            }
        }
    }
}

