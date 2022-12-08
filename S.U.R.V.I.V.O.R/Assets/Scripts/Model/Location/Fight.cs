using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New FightData", menuName = "Data/Fight Data", order = 50)]
public class Fight: ScriptableObject
{
    [SerializeField] private List<Entity> enemies;
    [SerializeField] private SceneName sceneName;

    public void Initialization()
    {
        var data = new FightData(enemies, Game.Instance.ChosenGroup.CurrentGroupMembers);
        FightSceneLoader.SendDataToLoader(data);
        FightSceneLoader.Load(sceneName);
    }
}