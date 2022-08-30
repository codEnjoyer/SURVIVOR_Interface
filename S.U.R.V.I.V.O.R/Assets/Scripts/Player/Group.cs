using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph_and_Map;
using UnityEngine;

namespace Player
{
    public class Group : MonoBehaviour
    {
        public enum Stage
        {
            Sleeping,
            WaitingTarget,
            MovingFromAToB
        }

        public int MovementSpeed;

        public Stage CurrentStage;
        public Node CurrentNode;
        public Vector3 Position;
        private IPlayer[] groupMembers;
        private Node TargetNode;

        private float delta = 0.1f;//Дистанция до ноды, при которой группа считиает, что достигла её и переходит к следующему ребру пути
        private Queue<Node> Way; //Текущий маршрут
        public Group(IPlayer[] groupMembers, Node currentNode)
        {
            this.groupMembers = new IPlayer[4];
            for (var i = 0; i < 4; i++)
            {
                this.groupMembers[i] = groupMembers[i];
            }
            CurrentNode = currentNode;
        }

        public void Update()
        {
            switch (CurrentStage)
            {
                case Stage.Sleeping:

                    break;
                case Stage.WaitingTarget:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 clickPosition;
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 200f))
                        {
                            clickPosition = hitInfo.point;
                        }
                        //Применить метод нахождения ближайшей ноды к clickPosition
                        Node nearestNode;
                        //Построить маршрут до этой ноды от CurrentNode
                        Way = new Queue<Node>();
                        CurrentStage = Stage.MovingFromAToB;


                        SubtractEnergy();
                    }
                    break;
                case Stage.MovingFromAToB:
                    if (Vector3.Distance(CurrentNode.transform.position,TargetNode.transform.position) <= delta)
                    {
                        transform.position = TargetNode.transform.position;
                        CurrentNode = TargetNode;
                        if (Way.Count == 0)
                        {
                            CurrentStage = Stage.Sleeping;
                            break;
                        }
                        TargetNode = Way.Dequeue();
                        break;
                    }
                    Vector3.Lerp(CurrentNode.transform.position, TargetNode.transform.position, MovementSpeed);
                    break;
            }
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

        private void SubtractEnergy()
        {
            foreach (var groupMember in groupMembers)
            {
                groupMember.Energy--;
            }
        }

        private void SubtractWater()
        {
            foreach (var groupMember in groupMembers)
            {
                groupMember.Water--;
            }
        }

        private void SubtractSatiety()
        {
            foreach (var groupMember in groupMembers)
            {
                groupMember.Satiety--;
            }
        }
    }
}



