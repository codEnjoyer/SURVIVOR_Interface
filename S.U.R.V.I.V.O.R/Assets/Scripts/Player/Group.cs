using System;
using System.Collections.Generic;
using Player.GroupMovement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(GroupMovementLogic))]
    public class Group : MonoBehaviour
    {
        [SerializeField] private int maxOnGlobalMapGroupEndurance = 10;
        [SerializeField] private int currentOnGlobalMapGroupEndurance = 10;
        [SerializeField] private List<Character> currentGroupMembers = new ();
        private int maxGroupMembers;
        private bool isLootAllowedOnThisTurn = true;

        public bool IsLootAllowedOnThisTurn
        {
            get => isLootAllowedOnThisTurn;
            set => isLootAllowedOnThisTurn = value;
        }

        public int MaxOnGlobalMapGroupEndurance => maxOnGlobalMapGroupEndurance;

        public int CurrentOnGlobalMapGroupEndurance
        {
            get => currentOnGlobalMapGroupEndurance;

            set => currentOnGlobalMapGroupEndurance = value;
        } 
        public GroupMovementLogic GroupMovementLogic { get; private set; }
        public Location Location => GroupMovementLogic.CurrentNode.Location;

        public IEnumerable<Character> CurrentGroupMembers => currentGroupMembers;

        public void SetCurrentOnGlobalMapGroupEndurance(int value) => currentOnGlobalMapGroupEndurance = value;

        private void Awake()
        {
            GroupMovementLogic = GetComponent<GroupMovementLogic>();
        }

        void Start()
        {
            TurnController.Instance.AddListener(OnTurnEnd);
            foreach (var character in currentGroupMembers)
            {
                character.body.Died += () => currentGroupMembers.Remove(character);
            }
        }

        public IEnumerable<BaseItem> Loot()
        {
            if (isLootAllowedOnThisTurn)
            {
                SubtractEnergy();
                foreach (var character in currentGroupMembers)
                {
                    yield return character.Loot(Location.Data);
                }
            }
        }

        private void SubtractEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.body.Energy--;
            }
        }

        private void SubtractWater()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.body.Water--;
            }
        }

        private void SubtractSatiety()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.body.Hunger--;
            }
        }

        private void AddExtraEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                if (groupMember.body.Hunger >= 8)
                    groupMember.body.Energy++;
                if (groupMember.body.Water >= 8)
                    groupMember.body.Energy++;
            }
        }

        private void ResetAllTurnCharacteristics()
        {
            currentOnGlobalMapGroupEndurance = maxOnGlobalMapGroupEndurance;
        }

        public void OnOnGlobalMapMovementEnd()
        {
            SubtractEnergy();
        }
        
        public void OnTurnEnd()
        {
            SubtractSatiety();
            SubtractWater();
            AddExtraEnergy();
            ResetAllTurnCharacteristics();
            GroupMovementLogic.OnTurnEnd();
            isLootAllowedOnThisTurn = true;
            //Вычислить все характеристки при окончании хода
        }
    }
}