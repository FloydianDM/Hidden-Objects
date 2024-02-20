using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private HiddenObjectData _hiddenObjectData;

        public string GetName()
        {
            return _hiddenObjectData.HiddenObjectName;
        }

        public Sprite GetSprite()
        {
            return _hiddenObjectData.HiddenObjectImage;
        }

        public bool GetIsHidden()
        {
            return _hiddenObjectData.IsHidden;
        }

        public void SetIsHidden(bool isHidden)
        {
            _hiddenObjectData.IsHidden = isHidden;
        }
    }
}

