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

        //Обьекты Ходов
        public GameObject FirstTurnObject;
        public GameObject SecondTurnObject;
        public GameObject ThirdTurnObject;
        private LineRenderer FirstTurnObjectLineRenderer;
        private LineRenderer SecondTurnObjectLineRenderer;
        private LineRenderer ThirdTurnObjectLineRenderer;

        public DotGraph Graph;

        public Stage CurrentStage;
        public Node CurrentNode;
        private Node TargetNode;
        private LineRenderer LineRenderer;
        private GroupGameLogic GroupGameLogic;

        private float delta = 0.1f;//Дистанция до ноды, при которой группа считиает, что достигла её и переходит к следующему ребру пути
        private float progress;//Текущий прогресс на отрезке пути между нодами
        private Queue<Node> Way; //Текущий маршрут

        public void Awake()
        {
            GroupGameLogic = GetComponent<GroupGameLogic>();
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.positionCount = 0;
            FirstTurnObjectLineRenderer = FirstTurnObject.GetComponent<LineRenderer>();
            SecondTurnObjectLineRenderer = SecondTurnObject.GetComponent<LineRenderer>();
            ThirdTurnObjectLineRenderer = ThirdTurnObject.GetComponent<LineRenderer>();
        }
        
        public void Start()
        {
            Way = new Queue<Node>();
            transform.position = CurrentNode.transform.position;
            TargetNode = CurrentNode;
        }

        public void Update()
        {
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
                    if (Way.Count == 0 || GroupGameLogic.CurrentGroupEndurance == 0)
                    {
                        ClearWay();
                    }
                    else
                    {
                        TargetNode = Way.Dequeue();
                        GroupGameLogic.CurrentGroupEndurance -= 1;
                    }
                }
                transform.position = Vector3.Lerp(CurrentNode.transform.position, TargetNode.transform.position, progress);
                progress += 0.050f;
            }
        }

        private void DrawWay(List<Node> list)
        {
            var firstList = new List<Vector3>();
            var secondList = new List<Vector3>();
            var thirdList = new List<Vector3>();
            var lastList = new List<Vector3>();
            for (var i = 0; i < list.Count; i++)
            {
                var element = list[i].transform.position;
                if (i < GroupGameLogic.CurrentGroupEndurance)
                {
                    firstList.Add(element);
                }
                else if(i == GroupGameLogic.CurrentGroupEndurance)
                {
                    firstList.Add(element);
                    secondList.Add(element);
                }
                else if (i < GroupGameLogic.CurrentGroupEndurance + GroupGameLogic.MaxGroupEndurance)
                {
                    secondList.Add(element);
                }
                else if (i == GroupGameLogic.CurrentGroupEndurance + GroupGameLogic.MaxGroupEndurance)
                {
                    thirdList.Add(element);
                    secondList.Add(element);
                }
                else if (i < GroupGameLogic.CurrentGroupEndurance + 2 * GroupGameLogic.MaxGroupEndurance)
                {
                    thirdList.Add(element);
                }
                else if (i == GroupGameLogic.CurrentGroupEndurance + 2 * GroupGameLogic.MaxGroupEndurance)
                {
                    thirdList.Add(element);
                    lastList.Add(element);
                }
                else
                {
                    lastList.Add(element);
                }
            }

            DrawLineRenderer(LineRenderer,FirstTurnObject,firstList);
            DrawLineRenderer(FirstTurnObjectLineRenderer, SecondTurnObject, secondList);
            DrawLineRenderer(SecondTurnObjectLineRenderer,ThirdTurnObject, thirdList);
            DrawLineRenderer(ThirdTurnObjectLineRenderer,lastList);
        }

        private void DrawLineRenderer(LineRenderer lineRenderer, GameObject turnObject, List<Vector3> nodes)
        {
            if (nodes.Count > 0)
            {
                lineRenderer.positionCount = nodes.Count;
                lineRenderer.SetPositions(nodes.Select(x => x + new Vector3(0, 0.5f, 0)).ToArray());
                turnObject.transform.position = nodes.Last();
            }
            else
            {
                turnObject.transform.position = new Vector3(0, -10, 0);
                lineRenderer.positionCount = 0;
            }
        }

        private void DrawLineRenderer(LineRenderer lineRenderer, List<Vector3> nodes)
        {
            if (nodes.Count > 0)
            {
                lineRenderer.positionCount = nodes.Count;
                lineRenderer.SetPositions(nodes.Select(x => x + new Vector3(0,0.5f,0)).ToArray());
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }

        private void ClearWay()
        {
            CurrentStage = Stage.Sleeping;
            LineRenderer.positionCount = 0;
            FirstTurnObjectLineRenderer.positionCount = 0;
            SecondTurnObjectLineRenderer.positionCount = 0;
            ThirdTurnObjectLineRenderer.positionCount = 0;

            FirstTurnObject.transform.position = new Vector3(0, -10, 0);
            SecondTurnObject.transform.position = new Vector3(0, -10, 0);
            ThirdTurnObjectLineRenderer.transform.position = new Vector3(0, -10, 0);
        }
    }
}



