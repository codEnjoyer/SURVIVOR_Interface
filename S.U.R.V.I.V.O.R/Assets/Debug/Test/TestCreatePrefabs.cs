

using System.Runtime.Serialization;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class TestCreatePrefabs: MonoBehaviour
{
    public GameObject prefab;
    private void Awake()
    {
        var gm = Instantiate(prefab);
        var prefabPath = AssetDatabase.GenerateUniqueAssetPath(@"Assets/Debug/Test/TestTestTEST.prefab");
        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(gm, prefabPath, out var prefabSuccess);
        
        var testComponent = savedPrefab.AddComponent<TestComponent>();

        var scriptableObjectPath = AssetDatabase.GenerateUniqueAssetPath(@"Assets/Debug/Test/TestTestTEST.asset");
        var testScriptableObject = new TestScriptableObject(10, 20);
        
        Debug.Log(testScriptableObject);
        // testScriptableObject.x = 10;
        // testScriptableObject.y = 20;
        AssetDatabase.CreateAsset(testScriptableObject, scriptableObjectPath);

        testComponent.scriptableObject = testScriptableObject;
        testComponent.i = 10;
        testComponent.s = "Привет мир!";
    }
    
}