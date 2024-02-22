using UnityEngine;

namespace Hidden_Objects.Core
{
    [CreateAssetMenu]
    public class HiddenObjectData : ScriptableObject
    {
        public string HiddenObjectName;
        public Sprite HiddenObjectImage;
        public bool IsHidden;
    }
}

