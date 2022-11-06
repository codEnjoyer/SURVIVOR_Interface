using System.Collections.Generic;
using System.Linq;
using Graph_and_Map;
using UnityEngine;

namespace Player
{
    public class GroupMovementLogic : MonoBehaviour
    {
        private StateMachine movementSm;
        private Sleeping sleeping;
        private WaitingTarget waitingTarget;
        private Walking walking;

        [SerializeField] private GameObject firstTurnObject;
        [SerializeField] private GameObject secondTurnObject;
        [SerializeField] private GameObject thirdTurnObject;
        [SerializeField] private Node currentNode;

        private LineRenderer firstTurnObjectLineRenderer;
        private LineRenderer secondTurnObjectLineRenderer;
        private LineRenderer thirdTurnObjectLineRenderer;
        private Node targetNode;
        private LineRenderer lineRenderer;
        private Group group;

        private const float Delta = 0.1f;
        private float progress;
        private Queue<Node> way = new();

        public Node CurrentNode => currentNode;

        private List<Node> GetPath() => PathFinder.FindShortestWay(currentNode, DotGraph.instance.GetNearestNode());

        public void CreateWay()
        {
            way = new Queue<Node>(GetPath());
            way.Dequeue();
        }

        public void Move()
        {
            transform.position = Vector3.Lerp(currentNode.transform.position, targetNode.transform.position, progress);
            progress += 0.050f;
            if (IsNearly())
            {
                currentNode = targetNode;
                progress = 0;
                if (way.Count == 0 || group.CurrentOnGlobalMapGroupEndurance == 0)
                    movementSm.ChangeState(sleeping);
                else
                {
                    targetNode = way.Dequeue();
                    group.CurrentOnGlobalMapGroupEndurance -= 1;
                }
            }
        }

        private bool IsNearly()
        {
            var curPos = SwitchTo2d(transform.position);
            var targetPos = SwitchTo2d(targetNode.transform.position);
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
            if (movementSm.CurrentState == sleeping)
                movementSm.ChangeState(waitingTarget);
        }
        
        private Vector2 SwitchTo2d(Vector3 v3) => new(v3.x, v3.z);
        

        #region MonoBehaviourCallBack

        private void Awake()
        {
            movementSm = new StateMachine();
            sleeping = new Sleeping(this, movementSm);
            waitingTarget = new WaitingTarget(this, movementSm);
            walking = new Walking(this, movementSm);
            movementSm.Initialize(sleeping);

            group = GetComponent<Group>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
            firstTurnObjectLineRenderer = firstTurnObject.GetComponent<LineRenderer>();
            secondTurnObjectLineRenderer = secondTurnObject.GetComponent<LineRenderer>();
            thirdTurnObjectLineRenderer = thirdTurnObject.GetComponent<LineRenderer>();
        }

        private void Start()
        {
            if (currentNode == null)
                Debug.Log("Нет стартовой ноды!");
            else
            {
                transform.position = currentNode.transform.position;
                targetNode = currentNode;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && movementSm.CurrentState == waitingTarget &&
                Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out var hitInfo, 200f))
            {
                movementSm.ChangeState(walking);
            }

            movementSm.CurrentState.Update();
        }

        private void FixedUpdate() => movementSm.CurrentState.FixedUpdate();

        #endregion
    }
}