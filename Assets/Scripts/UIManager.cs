using Hidden_Objects.Core;
using TMPro;
using UnityEngine;

namespace Hidden_Objects.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;

        private TimeHandler _timeHandler;

        private void Start()
        {
            _timeHandler = FindObjectOfType<TimeHandler>();
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
    }
}

