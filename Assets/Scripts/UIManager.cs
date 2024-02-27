using System.Collections.Generic;
using Hidden_Objects.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hidden_Objects.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private List<Image> _hiddenObjectImages;

        private TimeHandler _timeHandler;
        private ScoreManager _scoreManager;
        private LevelHandler _levelHandler;

        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
            _levelHandler = FindObjectOfType<LevelHandler>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            
            if (_scoreManager != null)
            {
                _scoreManager.OnScoreChanged += ShowScore;
            }

            if (_levelHandler != null)
            {
                _levelHandler.OnActiveHiddenSet += ShowActiveHiddenObjects;
            }

            ShowGameOverCanvas(false);
            ShowScore();
            ShowHighScore();
        }

        private void Update()
        {
            ShowTimer();
        }

        private void ShowTimer()
        {
            if (_timeText == null)
            {
                return;
            }

            int time = Mathf.FloorToInt(_timeHandler.Timer);
            _timeText.text = $"Remained Time: {time}";
        }

        private void ShowScore()
        {
            if (_scoreText == null)
            {
                return;
            }
            
            _scoreText.text = $"Score: {_scoreManager.Score}";
        }

        private void ShowActiveHiddenObjects()
        {
            for (int i = 0; i < _hiddenObjectImages.Count; i++)
            {
                Sprite hiddenObjectImage = _levelHandler.ActiveHiddenObjects[i].GetComponent<DataManager>().GetSprite();
                _hiddenObjectImages[i].sprite = hiddenObjectImage;

                string hiddenObjectName = _levelHandler.ActiveHiddenObjects[i].GetComponent<DataManager>().GetName();
                _hiddenObjectImages[i].GetComponentInChildren<TextMeshProUGUI>().text = hiddenObjectName;
            }
        }

        public void ShowGameOverCanvas(bool shouldShow)
        {
            if (_gameOverCanvas == null)
            {
                return;
            }

            _gameOverCanvas.enabled = shouldShow;
        }

        private void ShowHighScore()
        {
            if (_highScoreText == null)
            {
                return;
            }

            _highScoreText.text = $"High Score: {PlayerPrefs.GetInt(ScoreManager.HIGH_SCORE_KEY), 0}";
        }
    }
}

