using System.Collections.Generic;
using global::System.Linq;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Group : MonoBehaviour
    {
        public int MaxOnGlobalMapGroupEndurance;
        public int CurrentOnGlobalMapGroupEndurance;

        public List<Character> currentGroupMembers {get; private set;}
        private int maxGroupMembers;

        public readonly Location location;

        public void Awake()
        {
            currentGroupMembers = new List<Character> { new Character()};
            InputAggregator.OnTurnEndEvent += OnTurnEnd;
        }

        public Item Loot()
        {
            return location.GetLoot();
        }

        public void SubstracOnMove()
        {
            //Вычесть характеристике всех игрков группы при перемещении по карте
        }

        private void SubtractEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.Body.Food--;
            }
        }

        private void SubtractWater()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.Body.Water--;
            }
        }

        private void SubtractSatiety()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.Body.Food--;
            }
        }

        private void AddExtraEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                if (groupMember.Body.Food >= 8)
                    groupMember.Body.Energy++;
                if (groupMember.Body.Water >= 8)
                    groupMember.Body.Energy++;
            }
        }

        public void OnTurnEnd()
        {
            currentGroupMembers.Select(character => character.Body.Energy--);
            CurrentOnGlobalMapGroupEndurance = MaxOnGlobalMapGroupEndurance;
            //Вычислить все характеристки при окончании хода
        }
    }
}