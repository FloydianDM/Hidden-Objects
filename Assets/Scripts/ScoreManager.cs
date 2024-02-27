using System;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }
        public const string HIGH_SCORE_KEY = "HighScore";
        public event Action OnScoreChanged;

        public void AddScore(int addedScore)
        {
            Score += addedScore;
            OnScoreChanged?.Invoke();
        }

        public void SaveHighScore()
        {
            if (PlayerPrefs.HasKey(HIGH_SCORE_KEY) && Score > PlayerPrefs.GetInt(HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, Score);
            }
            else if (!PlayerPrefs.HasKey(HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, Score);    
            }
        }
    }
}

