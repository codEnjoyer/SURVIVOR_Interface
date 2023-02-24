using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Extension;
using Graph_and_Map;
using Model.Entities.Characters;
using Model.Player.GroupMovement;
using Model.SaveSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.Player
{
    [RequireComponent(typeof(GroupMovementLogic))]
    public class Group : MonoBehaviour, ISaved<GroupData>
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

        public GroupData CreateData()
        {
            var resPath = GetComponent<Saved>().ResourcesPath;
            var cgm = CurrentGroupMembers
                .Select(x => x.CreateData())
                .ToArray();
            return new GroupData()
            {
                resourcesPath = resPath,
                maxOnGlobalMapGroupEndurance = MaxOnGlobalMapGroupEndurance,
                currentOnGlobalMapGroupEndurance = CurrentOnGlobalMapGroupEndurance,
                currentGroupMembers = cgm,
                position = transform.position.To2D(),
                isLootAllowedOnThisTurn = IsLootAllowedOnThisTurn,
                canMove = GroupMovementLogic.CanMove
            };
        }
        
        public void Restore(GroupData data)
        {
            transform.position = data.position.To3D();
            MaxOnGlobalMapGroupEndurance = data.maxOnGlobalMapGroupEndurance;
            CurrentOnGlobalMapGroupEndurance = data.currentOnGlobalMapGroupEndurance;
            IsLootAllowedOnThisTurn = data.isLootAllowedOnThisTurn;

            foreach (var groupMember in currentGroupMembers)
                Destroy(groupMember.gameObject);
            currentGroupMembers.Clear();

            foreach (var characterSave in data.currentGroupMembers)
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

            GroupMovementLogic.CanMove = data.canMove;
        }
    }

    [DataContract]
    public class GroupData
    {
        [DataMember] public string resourcesPath;
        [DataMember] public int maxOnGlobalMapGroupEndurance;
        [DataMember] public int currentOnGlobalMapGroupEndurance;
        [DataMember] public CharacterData[] currentGroupMembers;
        [DataMember] public Vector2 position;
        [DataMember] public bool isLootAllowedOnThisTurn;
        [DataMember] public bool canMove;
    }
}