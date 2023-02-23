using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using Model;
using Model.SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class TestSave : MonoBehaviour
{
    [FormerlySerializedAs("globalMapGame")] [FormerlySerializedAs("game")] [FormerlySerializedAs("globalMap")] public GlobalMapController globalMapController;
    public BaseItem item;

    public void SaveGame()
    {
        var path = Application.persistentDataPath + "/test.xml";
        var save = globalMapController.CreateData();
        SaveManager.WriteObject(path, save);
        Debug.Log("Save");
    }

    public void LoadGame()
    {
        GlobalMapController.Instance.Clear();
        StartCoroutine(RestoreGameCoroutine());
    }

    public void SaveItem()
    {
        var path = Application.persistentDataPath + "/itemSave.xml";
        var save = item.CreateData();
        SaveManager.WriteObject(path, save);
    }
    
    IEnumerator RestoreGameCoroutine()
    {
        yield return null;
        var path = Application.persistentDataPath + "/test.xml";
        var save = SaveManager.ReadObject<GlobalMapData>(path);
        GlobalMapController.Instance.Restore(save);
        Debug.Log("Load");
    }
}
