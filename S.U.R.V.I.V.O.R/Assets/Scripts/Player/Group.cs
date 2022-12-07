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

        public int MaxOnGlobalMapGroupEndurance => maxOnGlobalMapGroupEndurance;
        public int CurrentOnGlobalMapGroupEndurance => currentOnGlobalMapGroupEndurance;
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
            InputAggregator.OnTurnEndEvent += OnTurnEnd;
            foreach (var character in currentGroupMembers)
            {
                character.body.Died += () => currentGroupMembers.Remove(character);
            }
        }

        public BaseItem Loot()
        {
            throw new NotImplementedException();
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

        public void OnTurnEnd()
        {
            if (currentOnGlobalMapGroupEndurance != maxOnGlobalMapGroupEndurance)
                SubtractEnergy();
            SubtractSatiety();
            SubtractWater();
            AddExtraEnergy();
            ResetAllTurnCharacteristics();
            //Вычислить все характеристки при окончании хода
        }
    }
}