using UnityEngine;

namespace Hidden_Objects.UX
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Instance;

        [SerializeField] private AudioClip _buttonSFX;
        [SerializeField] private AudioClip _endSFX;

        private AudioSource _audioPlayer;

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
            _audioPlayer = GetComponent<AudioSource>();
        }

        public void PlayButtonSFX()
        {
            _audioPlayer.PlayOneShot(_buttonSFX);
        }

        public void PlayGameOverSFX()
        {
            _audioPlayer.PlayOneShot(_endSFX);
        }
    }
    
}

