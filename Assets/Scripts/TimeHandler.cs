using System;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class TimeHandler : MonoBehaviour
    {
        public static TimeHandler Instance;

        [SerializeField] private float _gameTimeInMinutes = 2f;

        public float Timer { get; private set; }
        private GameManager _gameManager;
        private bool _isTimerStarted;
        public event Action OnTimeFinished;

        private void Awake()
        {
            ManageSingleton();
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

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameManager.OnGameStart += StartTimer;        
        }

        private void Update()
        {
            if (_isTimerStarted)
            {
                ProcessTimer();
            }
        }

        private void SetTimer()
        {
            float gameTime = 60 * _gameTimeInMinutes;
            Timer = gameTime;
        }

        private void StartTimer()
        {
            SetTimer();
            _isTimerStarted = true;
        }

        private void ProcessTimer()
        {
            Timer -= Time.deltaTime;

            CheckTimer();
        }

        private void CheckTimer()
        {
            if (Timer <= 0)
            {
                OnTimeFinished?.Invoke();
            }
        }

        public void ResetTimer()
        {
            _isTimerStarted = false;
            SetTimer();
        }
    } 
}

