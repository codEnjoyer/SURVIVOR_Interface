using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using UnityEditor;

public class Movement_Logic : MonoBehaviour
{
    public Button moveButton;
    public AIPath aiPath;
    public Text text;
    public LineRenderer lineRenderer; 
    public int MaxGroupEnergy;
    private int CurrentGroupEnergy;
    public LayerMask whatCanBeClickedOn;
    private bool isWaitingCord;
    private Vector3 Target;


    public void Awake()
    {
        CurrentGroupEnergy = MaxGroupEnergy;
        moveButton.onClick.AddListener(MoveButtonOnClick);
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
    }

    public void MoveButtonOnClick()
    {
        isWaitingCord = true;
        moveButton.enabled = false;
        aiPath.maxSpeed = 0;
    }

    public void Update()
    {
        if (isWaitingCord)
        {
            aiPath.GetComponent<AIDestinationSetter>().target.position = GetClickPosition();
            DrawPath(aiPath);
        }
        var currentTarget = aiPath.GetComponent<AIDestinationSetter>().target.position;

        if (Input.GetMouseButtonDown(0) && isWaitingCord)
        {
            GetPathAfterCostCheck(aiPath, Target);
            aiPath.maxSpeed = 2;
            isWaitingCord = false;
        }

        if (Vector3.Distance(aiPath.position, currentTarget) < 0.05)
        {
            aiPath.Teleport(currentTarget);
            text.text = "you reached the target";
        }
    }

    public void DrawPath(AIPath path)
    {
        var buffer = new List<Vector3>();
        path.GetRemainingPath(buffer, out var stale);
        lineRenderer.SetPositions(buffer.Select(x => x = new Vector3(x.x, x.y + 0.1f, x.z)).ToArray());
        lineRenderer.positionCount = buffer.Count;
    }

    public void GetPathAfterCostCheck(AIPath aipath, Vector3 target)
    {
        aipath.GetComponent<AIDestinationSetter>().target.position = GetClickPosition();
        foreach (var node in aipath.GetComponent<Seeker>().GetCurrentPath().path)
        {
            CurrentGroupEnergy -=  (int)node.Penalty;
            if (CurrentGroupEnergy < 0)
            {
                aipath.GetComponent<AIDestinationSetter>().target.position = node.RandomPointOnSurface();
                break;
            }
        }
    }

    public Vector3 GetClickPosition()
    {
        var myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(myRay, out var hitInfo, 100, whatCanBeClickedOn))
        {
            return hitInfo.point;
        }

        throw new InvalidOperationException("Нет слоя для клика");
    }

    public void OnTurnEnd()
    {
        CurrentGroupEnergy = MaxGroupEnergy;
        moveButton.enabled = true;
    }
}
