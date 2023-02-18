using System.Collections.Generic;
using Model.Entities.Characters;
using Model.GameEntity;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Menu.Main
{
    public class ToFightSceneButton: MonoBehaviour
    {
        [SerializeField] private Fight fight;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            fight.Initialization();
        }
    }
}