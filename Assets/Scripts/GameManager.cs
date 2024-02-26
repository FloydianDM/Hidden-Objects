using System;
using Hidden_Objects.UX;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hidden_Objects.Core
{
    public class GameManager : MonoBehaviour
    {
        private TimeHandler _timeHandler;
        public const string MENU_SCENE = "Scene_Menu";
        public const string GAME_SCENE = "Scene_Game";
        public const string END_SCENE = "Scene_End";
        public const string ENHANCED_TIME_KEY = "Enhanced Time";
        public event Action OnGameStart;
        public event Action OnGameStop;

        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
            _timeHandler.OnTimeFinished += ProcessGameOver;
        }

        public void StartGame()
        {
            _timeHandler.SetTimer();
            SceneManager.LoadScene(GAME_SCENE);
            OnGameStart?.Invoke();
            FindObjectOfType<AudioPlayer>().PlayButtonSFX();
        }

        public void ProcessGameOver()
        {
            FindObjectOfType<AudioPlayer>().PlayGameOverSFX();
            OnGameStop?.Invoke();

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.SaveHighScore();
           
            SceneManager.LoadScene(END_SCENE);
            _timeHandler.OnTimeFinished -= ProcessGameOver;

            FindObjectOfType<AdManager>().ShowAd(this);
        }

        public void LoadGameMenu()
        {
            FindObjectOfType<AudioPlayer>().PlayButtonSFX();

            SceneManager.LoadScene(MENU_SCENE);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void AddTime(int addedTime)
        {
            PlayerPrefs.SetFloat(ENHANCED_TIME_KEY, addedTime);
        }
    }
}

