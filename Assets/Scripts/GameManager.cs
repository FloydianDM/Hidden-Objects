using System;
using Hidden_Objects.UI;
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
        public const string ENHANCED_TIME_KEY = "Enhanced Time";
    
        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
            _timeHandler.OnTimeFinished += ProcessGameOver;
        }

        public void StartGame()
        {
            _timeHandler.SetTimer();
            _timeHandler.StartTimer();
            SceneManager.LoadScene(GAME_SCENE);
            FindObjectOfType<AudioPlayer>().PlayButtonSFX();
        }

        public void ProcessGameOver()
        {
            FindObjectOfType<AudioPlayer>().PlayGameOverSFX();
            _timeHandler.StopTimer();

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.SaveHighScore();
           
            FindObjectOfType<UIManager>().ShowGameOverCanvas(true);
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

