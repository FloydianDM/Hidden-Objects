using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hidden_Objects.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private TimeHandler _timeHandler;
        public const string GAME_SCENE = "Scene_Game";
        public const string END_SCENE = "Scene_End";
        public event Action OnGameStart;

        public void Awake()
        {
            ManageSingleton();
        }

        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
            _timeHandler.OnTimeFinished += ProcessGameOver;
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

        public void StartGame()
        {
            SceneManager.LoadScene(GAME_SCENE);
            OnGameStart?.Invoke();
        }

        public void ProcessGameOver()
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.SaveHighScore();
           
            SceneManager.LoadScene(END_SCENE);
            _timeHandler.OnTimeFinished -= ProcessGameOver;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

