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
        [SerializeField] private List<Image> _hiddenObjectImages;

        private TimeHandler _timeHandler;
        private ScoreManager _scoreManager;
        private LevelHandler _levelHandler;

        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
            _levelHandler = FindObjectOfType<LevelHandler>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            
            _scoreManager.OnScoreChanged += ShowScore;
            _levelHandler.OnActiveHiddenSet += ShowActiveHiddenObjects;

            ShowScore();
        }

        private void Update()
        {
            ShowTimer();
        }

        private void ShowTimer()
        {
            int time = Mathf.FloorToInt(_timeHandler.Timer);
            _timeText.text = $"Remained Time: {time}";
        }

        private void ShowScore()
        {
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
    }
}

