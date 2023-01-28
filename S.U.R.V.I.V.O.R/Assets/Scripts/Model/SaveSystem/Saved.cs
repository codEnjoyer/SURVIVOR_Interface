using System;
using Extension;
using UnityEngine;

namespace Model.SaveSystem
{
    [ExecuteInEditMode]
    public class Saved : MonoBehaviour
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
            Path = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            if (Path.Contains("Assets/Resources"))
                ResourcesPath = Path
                    .Replace("Assets/Resources/", String.Empty)
                    .Replace(".prefab", String.Empty);
            else
                ResourcesPath = null;
            
        }

        private void OnValidate()
        {
            FindPath();
        }
#endif
    }
}