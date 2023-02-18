using System.Collections.Generic;
using Model.Entities.Characters;
using Model.GameEntity;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Menu.Main
{
    public class ToFightSceneButton: MonoBehaviour
    {
        [SerializeField] private List<Entity> enemies;
        [SerializeField] private List<Character> ally;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            //var data = new FightData(enemies, ally);
            //FightSceneLoader.SendDataToLoader(data);
            //FightSceneLoader.Load(SceneName.FightScene);
        }
    }
}