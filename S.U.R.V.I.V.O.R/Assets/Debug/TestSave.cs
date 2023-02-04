using System;
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
        var path = Application.persistentDataPath + "/test.xml";
        var save = SaveManager.ReadObject<GameSave>(path);
        Game.Instance.Restore(save);
        Debug.Log("Load");
    }

    public void SaveItem()
    {
        var path = Application.persistentDataPath + "/itemSave.xml";
        var save = item.CreateSave();
        SaveManager.WriteObject(path, save);
    }
}
