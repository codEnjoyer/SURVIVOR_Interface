using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Entities.Characters;
using Model.GameEntity;
using Model.Player;
using Scenes;
using UnityEngine;

[CreateAssetMenu(fileName = "New FightData", menuName = "Data/Fight Data", order = 50)]
public class Fight : ScriptableObject
{
    [SerializeField] private List<Entity> enemies;
    [SerializeField] private SceneName sceneName;

    private int chosenGroupIndex = -1;

    public void Initialization()
    {
        var data = new FightData(enemies, GlobalMapController.Instance.ChosenGroup.CurrentGroupMembers);
        chosenGroupIndex = GlobalMapController.Instance.ChosenGroupIndex;

        FightSceneController.CurrentData = data;
        SceneTransition.LoadScene(sceneName);
    }

    public void End(IEnumerable<CharacterData> characters)
    {
        if (characters == null)
            GlobalMapController.Data.groupSaves.Remove(GlobalMapController.Data.groupSaves[chosenGroupIndex]);
        else
            GlobalMapController.Data.groupSaves[chosenGroupIndex].currentGroupMembers = characters.ToArray();
        SceneTransition.LoadScene(SceneName.GlobalMapScene);
        chosenGroupIndex = -1;
    }
}