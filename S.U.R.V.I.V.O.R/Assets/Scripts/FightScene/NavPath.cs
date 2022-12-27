using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public static class NavPath
{
    public static List<GameObject> Characters = new List<GameObject>();
    public static GameObject CurrentCharacter { get; set; }
    public static NavMeshAgent Agent { get; set; }
    public static NavMeshPath Path { get; set; }
    public static Vector3 TargetPoint;
    public static Vector3[] EdgePoints = new Vector3[360];
    private static float[] rations = new float[360];

    public static Vector3 ProectionToField(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }

    public static bool TryMoveCharacter(Vector3 targetPoint)
    {
        TargetPoint = targetPoint;
        Agent.CalculatePath(TargetPoint, Path);

        if (Path.status == NavMeshPathStatus.PathComplete)
        {
            Debug.Log(GetRemainingDistance(Path));
            if (GetRemainingDistance(Path) < CurrentCharacter.GetComponent<FightCharacter>().Energy)
            {
                Debug.Log("OK");
                return true;
            }
            else
                Debug.Log("Слишком далеко");
        }

        return false;
    }

    public static bool TryMoveToEnemy(GameObject enemyObj)
    {
        if (enemyObj.tag != "Character")
            return false;
        var radius = enemyObj.GetComponent<FightCharacter>().radius;
        var nodesColliders = Physics.OverlapSphere(ProectionToField(enemyObj.transform.position), radius * 2f);
        var sortedNearColliders = nodesColliders
            .Where(collider => collider.gameObject.GetComponent<FightNode>() != null)
            .Where(collider => NodesNav.AvailableNodes.ContainsKey(collider.gameObject.GetComponent<FightNode>()))
            .OrderBy(collider => Vector3.Distance(collider.transform.position, CurrentCharacter.transform.position))
            .ToArray();
        foreach (var collider in sortedNearColliders)
        {
            Agent.CalculatePath(ProectionToField(collider.transform.position), Path);
            if (Path.status == NavMeshPathStatus.PathComplete)
            {
                Debug.Log(GetRemainingDistance(Path));
                if (GetRemainingDistance(Path) < CurrentCharacter.GetComponent<FightCharacter>().Energy)
                {
                    collider.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    TargetPoint = collider.transform.position;

                    Debug.Log("OK");
                    return true;
                }
                else
                    Debug.Log("Слишком далеко");
            }
        }

        return false;
    }

    private static Collider[] GetSortedNearColliders(GameObject enemyObj)
    {
        var radius = enemyObj.GetComponent<FightCharacter>().radius;
        var nodesColliders = Physics.OverlapSphere(ProectionToField(enemyObj.transform.position), radius * 2f);
        return nodesColliders
            .Where(collider => collider.gameObject.GetComponent<FightNode>() != null)
            .Where(collider => NodesNav.AvailableNodes.ContainsKey(collider.gameObject.GetComponent<FightNode>()))
            .OrderBy(collider => Vector3.Distance(collider.transform.position, CurrentCharacter.transform.position))
            .ToArray();
    }

    public static float GetRemainingDistance(NavMeshPath path)
    {
        var distance = 0f;
        var corners = path.corners;
        if (corners.Length > 2)
        {
            for (var i = 1; i < corners.Length; i++)
            {
                distance += Vector3.Distance(corners[i - 1], corners[i]);
            }
        }
        else
            distance = Vector3.Distance(ProectionToField(Agent.transform.position), TargetPoint);

        return distance;
    }

    public static void GetAccessibleArea()
    {
        var startPoint = CurrentCharacter.transform.position;
        var maxDistance = CurrentCharacter.GetComponent<FightCharacter>().Energy;
        var tempPath = new NavMeshPath();

        for (var i = 0; i < rations.Length; i++)
            rations[i] = 0;

        for (var i = 1; i <= 10; i++)
        {
            for (var j = 0; j < 360; j++)
            {
                var angle = (float) (2 * Math.PI * (j / 360));
                var point = new Vector3(startPoint.x + Mathf.Sin(angle) * maxDistance * i / 10, 0,
                    startPoint.z + MathF.Cos(angle) * maxDistance * i / 10);
                //startPoint + new Vector3(Mathf.Sin(angle) * maxDistance * i / 10,
                //0, MathF.Cos(angle) * maxDistance * i / 10);
                Debug.Log(point);
                Agent.CalculatePath(point, tempPath);

                if (tempPath.status == NavMeshPathStatus.PathComplete
                    && GetRemainingDistance(tempPath) <= maxDistance)
                {
                    Debug.Log("PathCom");
                    rations[j] = i / 10;
                }
            }
        }

        GetEdgesPoint();
    }

    private static void GetEdgesPoint()
    {
        var startPoint = CurrentCharacter.transform.position;
        var maxDistance = CurrentCharacter.GetComponent<FightCharacter>().Energy;

        for (var i = 0; i < rations.Length; i++)
        {
            var angle = (float) (2 * Math.PI * (i / 360));
            var deltaX = Mathf.Sin(angle) * maxDistance * rations[i];
            var deltaZ = Mathf.Cos(angle) * maxDistance * rations[i];
            EdgePoints[i] = new Vector3(startPoint.x + deltaX, 0, startPoint.z + deltaZ);
        }
    }

    private static bool IsNearPointsAreAccessible()
    {
        var radius = CurrentCharacter.GetComponent<FightCharacter>().radius;
        var direction = ProectionToField(CurrentCharacter.transform.position) - TargetPoint;
        var offset = Vector3.ClampMagnitude(direction, 1);

        var pathes = new NavMeshPath[4];

        for (var i = 0; i < 4; i++)
            pathes[i] = new NavMeshPath();

        Agent.CalculatePath(TargetPoint + offset, pathes[0]);
        Agent.CalculatePath(TargetPoint - offset, pathes[1]);
        Agent.CalculatePath(TargetPoint + new Vector3(offset.z, 0, offset.x), pathes[2]);
        Agent.CalculatePath(TargetPoint - new Vector3(offset.z, 0, offset.x), pathes[3]);

        foreach (var path in pathes)
        {
            if (path.status == NavMeshPathStatus.PathComplete)
                return true;
        }

        return false;
    }

    private static bool IsNearNodesAreAccessible()
    {
        var radius = CurrentCharacter.GetComponent<FightCharacter>().radius;
        var nearNodes = Physics.OverlapSphere(TargetPoint, radius);
        return true;
    }
}