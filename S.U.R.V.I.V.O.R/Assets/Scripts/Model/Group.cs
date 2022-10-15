using System.Collections.Generic;
using Assets.Scripts.Model.Items;
using Assets.Scripts.Model.Player.Entity.Realization;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Group
    {
        public readonly List<Character> currentGroupMembers;
        private int maxGroupMembers;


        public readonly Location location;

        public Item Loot()
        {
            return location.GetLoot();
        }

        public void SubstracOnMove()
        {
            //Вычесть характеристике всех игркокв группы при перемещении по карте
        }

        public void OnTurnEnd()
        {
            //Вычислить все характеристки при окончании хода
        }

        private void SubtractEnergy()
        {
            foreach (var groupMember in currentGroupMembers)
            {
                groupMember.Body.Energy--;
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
                    groupMember.Energy++;
                if (groupMember.Body.Water >= 8)
                    groupMember.Energy++;
            }
        }
    }
}