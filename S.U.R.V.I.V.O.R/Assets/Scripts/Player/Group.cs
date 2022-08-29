using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph_and_Map;
using UnityEngine;

namespace Player
{
    public class Group
    {

        public Node CurrentNode;
        public Vector3 Position;
        private IPlayer[] GroupMembers;
        public int GroupPossibleRange;


        public Group(IPlayer[] groupMembers, Node currentNode)
        {
            GroupMembers = new IPlayer[4];
            for (var i = 0; i < 4; i++)
            {
                GroupMembers[i] = groupMembers[i];
            }
            CurrentNode = currentNode;
        }

        public void Start()
        {
            InputAggregator.OnTurnEndEvent += OnTurnEnd;
        }

        private void OnTurnEnd()
        {
            SubtractWater();
            SubtractSatiety();
        }

        public void MoveGroup(Node TargetNode)
        {
            SubtractEnergy();
            CurrentNode = TargetNode;
            //Переместить группу в ноду, изменяя значение CurrentNode по мере продвижения
        }

        private void SubtractEnergy()
        {
            foreach (var groupMember in GroupMembers)
            {
                groupMember.Energy--;
            }
        }

        private void SubtractWater()
        {
            foreach (var groupMember in GroupMembers)
            {
                groupMember.Water--;
            }
        }

        private void SubtractSatiety()
        {
            foreach (var groupMember in GroupMembers)
            {
                groupMember.Satiety--;
            }
        }
    }
}
