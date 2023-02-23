using System;
using Extension;
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

#if UNITY_EDITOR
        private void FindPath()
        {
            if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(gameObject))
                return;
            var path = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            if (string.IsNullOrEmpty(path))
                return;
            Path = path;
            if (Path.Contains("Assets/Resources"))
                ResourcesPath = Path
                    .Replace("Assets/Resources/", String.Empty)
                    .Replace(".prefab", String.Empty);
            else
                ResourcesPath = null;
            
        }

        private void OnValidate()
        {
            if (!UnityEditor.EditorApplication.isCompiling)
            {
                FindPath();
            }
        }
#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            OnValidate();
#endif
        }
        public void OnAfterDeserialize() {}
    }
}