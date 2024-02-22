using UnityEngine;

namespace Hidden_Objects.Core
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private HiddenObjectData _hiddenObjectData;
        
        public bool IsHidden;

        private void Start()
        {
            IsHidden = _hiddenObjectData.IsHidden;
        }

        public string GetName()
        {
            return _hiddenObjectData.HiddenObjectName;
        }

        public Sprite GetSprite()
        {
            return _hiddenObjectData.HiddenObjectImage;
        }

        public void SetIsHidden(bool shouldHidden)
        {
            IsHidden = shouldHidden;
        }
    }
}

