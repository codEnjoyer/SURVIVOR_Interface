﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Extension;
using Model.Entities.Characters;
using Model.Items;
using Model.Player.GroupMovement;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Player
{
    [RequireComponent(typeof(GroupMovementLogic))]
    public class Group : MonoBehaviour, ISaved<GroupSave>
    {
        [SerializeField] private int maxOnGlobalMapGroupEndurance = 10;
        [SerializeField] private int currentOnGlobalMapGroupEndurance = 10;
        [SerializeField] private List<Character> currentGroupMembers = new ();
        private int maxGroupMembers;
        public bool IsLootAllowedOnThisTurn { get; set; } = true;
        
        public int MaxOnGlobalMapGroupEndurance
        {
            get => maxOnGlobalMapGroupEndurance;
            set
            {
                if (value < 0)
                    throw new InvalidOperationException();
                maxOnGlobalMapGroupEndurance = value;
            }
        }

        public int CurrentOnGlobalMapGroupEndurance
        {
            get => currentOnGlobalMapGroupEndurance;

            set
            {
                if (value < 0 || value > maxOnGlobalMapGroupEndurance)
                    throw new InvalidOperationException();
                currentOnGlobalMapGroupEndurance = value;
            }
        } 
        public GroupMovementLogic GroupMovementLogic { get; private set; }
        public Location Location => GroupMovementLogic.CurrentNode.Location;

        public IEnumerable<Character> CurrentGroupMembers => currentGroupMembers;
        
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
            if (IsLootAllowedOnThisTurn)
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
            IsLootAllowedOnThisTurn = true;
            //Вычислить все характеристки при окончании хода
        }

        public GroupSave CreateSave()
        {
            return new GroupSave()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                maxOnGlobalMapGroupEndurance = MaxOnGlobalMapGroupEndurance,
                currentOnGlobalMapGroupEndurance = CurrentOnGlobalMapGroupEndurance,
                currentGroupMembers = CurrentGroupMembers
                    .Select(x => x.CreateSave())
                    .ToArray(),
                position = transform.position.To2D(),
                isLootAllowedOnThisTurn = IsLootAllowedOnThisTurn
            };
        }
    }
    
    [DataContract(Namespace = "Model.Player")]
    public class GroupSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public int maxOnGlobalMapGroupEndurance;
        [DataMember] public int currentOnGlobalMapGroupEndurance;
        [DataMember] public CharacterSave[] currentGroupMembers;
        [DataMember] public Vector2 position;
        [DataMember] public bool isLootAllowedOnThisTurn;
    }
}