using System;
using Extension;
using UnityEditor;
using UnityEngine;

namespace Model.SaveSystem
{
    [ExecuteInEditMode]
    public class Saved : MonoBehaviour, ISerializationCallbackReceiver
    {
        [field: SerializeField]
        [field: ReadOnlyInspector]
        public string ResourcesPath { get; private set; }
    
        [field: SerializeField]
        [field: ReadOnlyInspector]
        public string Path { get; private set; }


        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            Path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            ResourcesPath = Path
                .Replace("Assets/Resources/", String.Empty)
                .Replace(".prefab", String.Empty);
#endif
        }

        public void OnAfterDeserialize() {}
    }
}