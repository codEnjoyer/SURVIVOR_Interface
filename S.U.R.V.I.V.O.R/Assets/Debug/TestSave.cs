using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using Model;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    public Game game;
    public BaseItem item;

    public void SaveGame()
    {
        var path = Application.persistentDataPath + "/test.xml";
        var save = game.CreateSave();
        SaveManager.WriteObject(path, save);
        Debug.Log("Save");
    }

    public void LoadGame()
    {
        Game.Instance.Clear();
        StartCoroutine(RestoreGameCoroutine());
    }

    public void SaveItem()
    {
        var path = Application.persistentDataPath + "/itemSave.xml";
        var save = item.CreateSave();
        SaveManager.WriteObject(path, save);
    }
    
    IEnumerator RestoreGameCoroutine()
    {
        yield return null;
        var path = Application.persistentDataPath + "/test.xml";
        var save = SaveManager.ReadObject<GameSave>(path);
        Game.Instance.Restore(save);
        Debug.Log("Load");
    }
}
