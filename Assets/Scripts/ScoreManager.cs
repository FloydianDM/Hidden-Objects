using UnityEngine;

namespace Hidden_Objects.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }
        public const string HIGH_SCORE_KEY = "HighScore";

        public void AddScore(int addedScore)
        {
            Score += addedScore;
        }

        public void SaveHighScore()
        {
            if (PlayerPrefs.HasKey(HIGH_SCORE_KEY) && Score > PlayerPrefs.GetInt(HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
            }
            else if (!PlayerPrefs.HasKey(HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);    
            }
        }
    }
}

