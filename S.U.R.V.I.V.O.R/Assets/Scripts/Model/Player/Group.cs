using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Extension;
using Graph_and_Map;
using Model.Entities.Characters;
using Model.Items;
using Model.Player.GroupMovement;
using Model.SaveSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.Player
{
    [RequireComponent(typeof(GroupMovementLogic))]
    public class Group : MonoBehaviour, ISaved<GroupSave>
    {
        [SerializeField] private int maxOnGlobalMapGroupEndurance = 10;
        [SerializeField] private int currentOnGlobalMapGroupEndurance = 10;
        [SerializeField] private List<Character> currentGroupMembers = new();
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
                character.ManBody.Died += () => currentGroupMembers.Remove(character);
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
                groupMember.ManBody.Energy--;
            }
        }

        private void SubtractWater()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.ManBody.Water--;
            }
        }

        private void SubtractSatiety()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.ManBody.Hunger--;
            }
        }

        private void AddExtraEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                if (groupMember.ManBody.Hunger >= 8)
                    groupMember.ManBody.Energy++;
                if (groupMember.ManBody.Water >= 8)
                    groupMember.ManBody.Energy++;
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
            var resPath = GetComponent<Saved>().ResourcesPath;
            return new GroupSave()
            {
                resourcesPath = resPath,
                maxOnGlobalMapGroupEndurance = MaxOnGlobalMapGroupEndurance,
                currentOnGlobalMapGroupEndurance = CurrentOnGlobalMapGroupEndurance,
                currentGroupMembers = CurrentGroupMembers
                    .Select(x => x.CreateSave())
                    .ToArray(),
                position = transform.position.To2D(),
                isLootAllowedOnThisTurn = IsLootAllowedOnThisTurn,
                canMove = GroupMovementLogic.CanMove
            };
        }
        
        public void Restore(GroupSave save)
        {
            transform.position = save.position.To3D();
            MaxOnGlobalMapGroupEndurance = save.maxOnGlobalMapGroupEndurance;
            CurrentOnGlobalMapGroupEndurance = save.currentOnGlobalMapGroupEndurance;
            IsLootAllowedOnThisTurn = save.isLootAllowedOnThisTurn;

            foreach (var groupMember in currentGroupMembers)
                Destroy(groupMember.gameObject);
            currentGroupMembers.Clear();

            foreach (var characterSave in save.currentGroupMembers)
            {
                var character = Instantiate(
                    Resources.Load<Character>(characterSave.resourcesPath),
                    transform.position,
                    Quaternion.identity,
                    transform
                );
                currentGroupMembers.Add(character);
                character.Restore(characterSave);
            }

            GroupMovementLogic.CanMove = save.canMove;
        }
    }

    [DataContract]
    public class GroupSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public int maxOnGlobalMapGroupEndurance;
        [DataMember] public int currentOnGlobalMapGroupEndurance;
        [DataMember] public CharacterSave[] currentGroupMembers;
        [DataMember] public Vector2 position;
        [DataMember] public bool isLootAllowedOnThisTurn;
        [DataMember] public bool canMove;
    }
}