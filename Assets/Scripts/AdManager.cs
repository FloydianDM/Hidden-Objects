using UnityEngine;
using UnityEngine.Advertisements;

namespace Hidden_Objects.Core
{
    public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        // The script is attached to GameManager objects, no need to make it singleton separately!

        public static AdManager Instance;

        private string _androidGameId = "5561947";
        private string _iOSGameId = "5561946";
        private bool _isTestMode = true;
        private string _androidAdUnitId = "Rewarded_Android";
        private string _iOSAdUnitId = "Rewarded_iOS";

        private string _gameId;
        private string _adUnitId;
        private GameManager _gameManager;

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
                InitializeAds();
            }
        }

        private void InitializeAds()
        {
    #if UNITY_IOS
            _gameId = _iOSGameId;
            _adUnitId = _iOSAdUnitId;
    #elif UNITY_ANDROID
            _gameId = _androidGameId;
            _adUnitId = _androidAdUnitId;
    #elif UNITY_EDITOR
            _gameId = _androidGameId;
            _adUnitId = _androidAdUnitId;
    #endif

            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(_gameId, _isTestMode, this);
            }            
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ad Initialization Complete");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Ad Initialization Failed");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Advertisement.Show(placementId, this);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Ad Load Failed");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Ad Show Failed");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            if (placementId.Equals(_adUnitId))
            {
                // reward player

                _gameManager.AddTime(3);
            }
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            if (placementId.Equals(_adUnitId))
            {
                // reward player

                _gameManager.AddTime(3);
            }
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
            {
                // reward player

                _gameManager.AddTime(3);
            }
        }

        public void ShowAd(GameManager gameManager)
        {
            _gameManager = gameManager;

            Advertisement.Load(_adUnitId, this);           
        }
    }
}

