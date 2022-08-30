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

        private float MovementSpeed = 0.1f;

        public DotGraph graph;
        public Stage CurrentStage;
        public Node CurrentNode;
        public Vector3 Position;
        private IPlayer[] groupMembers;
        private Node TargetNode;

        private float delta = 0.1f;//Дистанция до ноды, при которой группа считиает, что достигла её и переходит к следующему ребру пути
        private float progress;//Текущий прогресс на отрезке пути между нодами
        private Queue<Node> Way; //Текущий маршрут
        public Group(IPlayer[] groupMembers)
        {
            this.groupMembers = new IPlayer[4];
            for (var i = 0; i < 4; i++)
            {
                this.groupMembers[i] = groupMembers[i];
            }
        }
        public void Start()
        {
            Way = new Queue<Node>();
            transform.position = CurrentNode.transform.position;
            TargetNode = CurrentNode;
            //InputAggregator.OnTurnEndEvent += OnTurnEnd;
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
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 200f))
                        {
                            var list = PathFinder.FindShortestWay(CurrentNode, graph.GetNearestNode());
                            foreach (var node in list)
                            {
                                Way.Enqueue(node);
                            }

                            Way.Dequeue();
                            CurrentStage = Stage.MovingFromAToB;
                        }
                    }
                    break;
            }
        }

        public void FixedUpdate()
        {
            if (CurrentStage == Stage.MovingFromAToB)
            {
                
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(TargetNode.transform.position.x, TargetNode.transform.position.z)) <= delta)
                {
                    Debug.Log("True");
                    CurrentNode = TargetNode;
                    progress = 0;
                    if (Way.Count == 0)
                    {
                        CurrentStage = Stage.Sleeping;
                    }
                    else
                    {
                        TargetNode = Way.Dequeue();
                        Debug.Log(TargetNode.transform.position);
                        Debug.Log(Vector3.Distance(transform.position, TargetNode.transform.position));
                    }
                }
                else
                {
                    Debug.Log("False");
                }
                transform.position = Vector3.Lerp(CurrentNode.transform.position, TargetNode.transform.position, progress);
                progress += 0.02f;
            }
        }


        private void OnTurnEnd()
        {
            //SubtractWater();
            //SubtractSatiety();
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



