using System.Collections.Generic;
using Model;
using Model.GameEntity;
using UnityEngine;
using Model.SaveSystem;


[CreateAssetMenu(fileName = "New FightData", menuName = "Data/Fight Data", order = 50)]
public class Fight: ScriptableObject
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private SceneName sceneName;

    public void Initialization()
    {
        var gameSave = Game.Instance.CreateSave();
        Debug.Log("CreateSave");
        var data = new FightData(enemies, gameSave.groupSaves[gameSave.chosenGroupIndex].currentGroupMembers);
        FightSceneLoader.SendDataToLoader(data);
        FightSceneLoader.Load(sceneName);
    }
}