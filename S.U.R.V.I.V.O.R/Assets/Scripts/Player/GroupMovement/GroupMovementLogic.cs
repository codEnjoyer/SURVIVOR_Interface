using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph_and_Map;
using UnityEngine;

namespace Player
{
    public class GroupMovementLogic : MonoBehaviour
    {
        public enum Stage
        {
            Sleeping,
            WaitingTarget,
            MovingFromAToB
        }


        public GameObject ObjToSpawn;
        public DotGraph Graph;

        public Stage CurrentStage;
        public Node CurrentNode;
        private Node TargetNode;
        private int CurrentGroupEndurance;
        private LineRenderer LineRenderer;
        private GroupGameLogic GroupGameLogic;

        private float delta = 0.1f;//Дистанция до ноды, при которой группа считиает, что достигла её и переходит к следующему ребру пути
        private float progress;//Текущий прогресс на отрезке пути между нодами
        private Queue<Node> Way; //Текущий маршрут

        public void Awake()
        {
            GroupGameLogic = GetComponent<GroupGameLogic>();
            CurrentGroupEndurance = GroupGameLogic.MaxGroupEndurance;
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.positionCount = 0;
        }
        
        public void Start()
        {
            Way = new Queue<Node>();
            transform.position = CurrentNode.transform.position;
            TargetNode = CurrentNode;
            InputAggregator.OnTurnEndEvent += OnTurnEnd;
        }

        public void Update()
        {
            Debug.Log(CurrentGroupEndurance);
            switch (CurrentStage)
            {
                case Stage.Sleeping:
                    break;
                case Stage.WaitingTarget:
                    var list = PathFinder.FindShortestWay(CurrentNode, Graph.GetNearestNode());
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
                        LineRenderer.positionCount = 0;
                        ObjToSpawn.transform.position = new Vector3(0, -10, 0);
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
            LineRenderer.positionCount = list.Count;
            LineRenderer.SetPositions(list.Select(x => x.transform.position + new Vector3(0, 0.5f, 0)).ToArray());
            for (var nodeNumber = 0; nodeNumber < list.Count; nodeNumber++)
            {
                if (nodeNumber <= CurrentGroupEndurance)
                {
                    ObjToSpawn.transform.position = list[nodeNumber].transform.position;
                }
            }
        }

        private void OnTurnEnd()
        {
            CurrentGroupEndurance = GetComponent<GroupGameLogic>().MaxGroupEndurance;
        }
    }
}



