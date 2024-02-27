using System;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class TimeHandler : MonoBehaviour
    {
        public static TimeHandler Instance;

        [SerializeField] private float _gameTimeInMinutes = 2f;

        public float Timer { get; private set; }
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
        
        private void Update()
        {
            if (_isTimerStarted)
            {
                ProcessTimer();
            }
        }

        public void SetTimer()
        {
            float gameTime;

            if (PlayerPrefs.HasKey(GameManager.ENHANCED_TIME_KEY))
            {
                float enhancedTime = PlayerPrefs.GetFloat(GameManager.ENHANCED_TIME_KEY);
                gameTime = 60 * enhancedTime;

                PlayerPrefs.DeleteKey(GameManager.ENHANCED_TIME_KEY);   // job done
            }
            else
            {
                gameTime = 60 * _gameTimeInMinutes;
            }

            Timer = gameTime;
        }

        public void StartTimer()
        {
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

        public void StopTimer()
        {
            _isTimerStarted = false;
        }
    } 
}

