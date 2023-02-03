using System;
using System.Collections.Generic;
using System.Linq;
using Extension;
using Graph_and_Map;
using Model.Player.GroupMovement.GroupMovementStates;
using UnityEngine;

namespace Model.Player.GroupMovement
{
    public class GroupMovementLogic : MonoBehaviour
    {
        private StateMachine movementSm;

        public Sleeping Sleeping { get; private set; }

        public WaitingTarget WaitingTarget { get; private set; }

        public Walking Walking { get; private set; }

        [SerializeField] private GameObject firstTurnObject;
        [SerializeField] private GameObject secondTurnObject;
        [SerializeField] private GameObject thirdTurnObject;

        private Node currentNode;
        private LineRenderer firstTurnObjectLineRenderer;
        private LineRenderer secondTurnObjectLineRenderer;
        private LineRenderer thirdTurnObjectLineRenderer;
        private Node targetNode;
        private LineRenderer lineRenderer;
        private Group group;

        private const float Delta = 0.1f;
        private float progress;
        private Queue<Node> way = new();
        public bool CanMove { get; set; }

        public Node CurrentNode
        {
            get
            {
                if (currentNode == null)
                {
                    currentNode = DotGraph.Instance.GetNearestNode(transform.position.To2D());
                    transform.position = currentNode.transform.position;
                    targetNode = currentNode;
                }

                return currentNode;
            }
            private set
            {
                currentNode = value;
                LocationChange?.Invoke(currentNode.Location);
            }
        }

        public event Action<Location> LocationChange;

        private List<Node> GetPath() =>
            PathFinder.FindShortestWay(CurrentNode, DotGraph.Instance.GetNearestNodeToMouse());

        public void CreateWay()
        {
            way = new Queue<Node>(GetPath());
            way.Dequeue();
        }

        public void Move()
        {
            var oldGroupPos = transform.position;
            var newGroupPos = Vector3.Lerp(CurrentNode.transform.position, targetNode.transform.position, progress);
            transform.position = newGroupPos;

            var offset = oldGroupPos - newGroupPos;

            firstTurnObject.transform.localPosition += offset;
            secondTurnObject.transform.localPosition += offset;
            thirdTurnObject.transform.localPosition += offset;

            progress += 0.050f;
            if (IsNearly())
            {
                CurrentNode = targetNode;
                LocationChange?.Invoke(CurrentNode.Location);
                progress = 0;
                if (way.Count == 0 || group.CurrentOnGlobalMapGroupEndurance == 0)
                {
                    movementSm.ChangeState(Sleeping);
                    group.CurrentOnGlobalMapGroupEndurance = 0;
                    group.OnOnGlobalMapMovementEnd();
                    CanMove = true;
                }
                else
                {
                    targetNode = way.Dequeue();
                    group.CurrentOnGlobalMapGroupEndurance -= 1;
                }
            }
        }

        private bool IsNearly()
        {
            var curPos = transform.position.To2D();
            var targetPos = targetNode.PositionIn2D;
            return Vector2.Distance(curPos, targetPos) <= Delta;
        }

        public void DrawPath()
        {
            var path = GetPath();
            var firstList = new List<Vector3>();
            var secondList = new List<Vector3>();
            var thirdList = new List<Vector3>();
            var lastList = new List<Vector3>();
            for (var i = 0; i < path.Count; i++)
            {
                var element = path[i].transform.position;
                if (i < group.CurrentOnGlobalMapGroupEndurance)
                {
                    firstList.Add(element);
                }
                else if (i == group.CurrentOnGlobalMapGroupEndurance)
                {
                    firstList.Add(element);
                    secondList.Add(element);
                }
                else if (i < group.CurrentOnGlobalMapGroupEndurance + group.MaxOnGlobalMapGroupEndurance)
                {
                    secondList.Add(element);
                }
                else if (i == group.CurrentOnGlobalMapGroupEndurance + group.MaxOnGlobalMapGroupEndurance)
                {
                    thirdList.Add(element);
                    secondList.Add(element);
                }
                else if (i < group.CurrentOnGlobalMapGroupEndurance + 2 * group.MaxOnGlobalMapGroupEndurance)
                {
                    thirdList.Add(element);
                }
                else if (i == group.CurrentOnGlobalMapGroupEndurance + 2 * group.MaxOnGlobalMapGroupEndurance)
                {
                    thirdList.Add(element);
                    lastList.Add(element);
                }
                else
                {
                    lastList.Add(element);
                }
            }

            DrawLineRenderer(lineRenderer, firstTurnObject, firstList);
            DrawLineRenderer(firstTurnObjectLineRenderer, secondTurnObject, secondList);
            DrawLineRenderer(secondTurnObjectLineRenderer, thirdTurnObject, thirdList);
            DrawLineRenderer(thirdTurnObjectLineRenderer, lastList);
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
                lineRenderer.SetPositions(nodes.Select(x => x + new Vector3(0, 0.5f, 0)).ToArray());
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }

        public void ClearWay()
        {
            way.Clear();
            lineRenderer.positionCount = 0;
            firstTurnObjectLineRenderer.positionCount = 0;
            secondTurnObjectLineRenderer.positionCount = 0;
            thirdTurnObjectLineRenderer.positionCount = 0;

            firstTurnObject.transform.position = new Vector3(0, -10, 0);
            secondTurnObject.transform.position = new Vector3(0, -10, 0);
            thirdTurnObjectLineRenderer.transform.position = new Vector3(0, -10, 0);
        }

        public void PreparingToMove()
        {
            if (movementSm.CurrentState == Sleeping && !CanMove)
                movementSm.ChangeState(WaitingTarget);
        }

        public void OnTurnEnd()
        {
            CanMove = false;
        }

        #region MonoBehaviourCallBack

        private void Awake()
        {
            movementSm = new StateMachine();
            Sleeping = new Sleeping(this, movementSm);
            WaitingTarget = new WaitingTarget(this, movementSm);
            Walking = new Walking(this, movementSm);

            group = GetComponent<Group>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
            firstTurnObjectLineRenderer = firstTurnObject.GetComponent<LineRenderer>();
            secondTurnObjectLineRenderer = secondTurnObject.GetComponent<LineRenderer>();
            thirdTurnObjectLineRenderer = thirdTurnObject.GetComponent<LineRenderer>();
            movementSm.Initialize(Sleeping);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && movementSm.CurrentState == WaitingTarget &&
                Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out var hitInfo, 200f))
            {
                movementSm.ChangeState(Walking);
            }

            movementSm.CurrentState.Update();
        }

        private void FixedUpdate() => movementSm.CurrentState.FixedUpdate();

        #endregion
    }
}