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
        //Игровые характеристики
        public int MaxGroupEndurance;
        private int CurrentGroupEndurance;
        private IPlayer[] groupMembers;

        //Характеристики движения по маршруту
        public GameObject objToSpawn;
        public DotGraph graph;
        public Stage CurrentStage;
        public Node CurrentNode;
        public Vector3 Position;
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
            GetComponent<LineRenderer>().positionCount = 0;
            Way = new Queue<Node>();
            transform.position = CurrentNode.transform.position;
            TargetNode = CurrentNode;
            InputAggregator.OnTurnEndEvent += OnTurnEnd;
            CurrentGroupEndurance = MaxGroupEndurance;
        }
        public void Update()
        {
            Debug.Log(CurrentGroupEndurance);
            switch (CurrentStage)
            {
                case Stage.Sleeping:
                    break;
                case Stage.WaitingTarget:
                    var list = PathFinder.FindShortestWay(CurrentNode, graph.GetNearestNode());
                    DrawWay(list);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 200f))
                        {
                            Way.Clear();
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
                    CurrentNode = TargetNode;
                    progress = 0;
                    if (Way.Count == 0 || CurrentGroupEndurance == 0)
                    {
                        CurrentStage = Stage.Sleeping;
                        GetComponent<LineRenderer>().positionCount = 0;
                        objToSpawn.transform.position = new Vector3(0, -10, 0);
                    }
                    else
                    {
                        TargetNode = Way.Dequeue();
                        CurrentGroupEndurance -= 1;
                    }
                }
                transform.position = Vector3.Lerp(CurrentNode.transform.position, TargetNode.transform.position, progress);
                progress += 0.05f;
            }
        }

        private void DrawWay(List<Node> list)
        {
            var lr = GetComponent<LineRenderer>();
            lr.positionCount = list.Count;
            lr.SetPositions(list.Select(x => x.transform.position + new Vector3(0, 0.5f, 0)).ToArray());
            for (var nodeNumber = 0; nodeNumber < list.Count; nodeNumber++)
            {
                if (nodeNumber <= CurrentGroupEndurance)
                {
                    objToSpawn.transform.position = list[nodeNumber].transform.position;
                }
            }
        }

        private void OnTurnEnd()
        {
            CurrentGroupEndurance = MaxGroupEndurance;
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



