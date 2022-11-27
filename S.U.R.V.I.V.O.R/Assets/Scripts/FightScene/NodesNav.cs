using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class NodesNav
{
    public static List<GameObject> Nodes;

    public static List<FightNode> ObstacleNodes;

    public static Dictionary<FightNode, FightNode> AvailableNodes;

    public static List<Vector3> Path;

    public static Vector3 CurrentTargetPoint;

    private static int currentPathNodeIndex;


    private const float innerRadius = 0.6f;   //From NodesNetTool
    public static void InitializeNodesLists(GameObject graph)
    {
        Nodes = new List<GameObject>();
        foreach(Transform node in graph.transform)
            Nodes.Add(node.gameObject);
        ObstacleNodes = new List<FightNode>();
        AvailableNodes = new Dictionary<FightNode, FightNode>();
        Path = new List<Vector3>();
    }

    public static Vector3 Projection(Vector3 point)
    {
        return new Vector3(point.x, 0, point.z);
    }

    public static void FindObstacleNode(GameObject otherCharacterObj)
    {
        var obstacleNode =  GetNearestNode(otherCharacterObj.transform.position);
        var node = obstacleNode.GetComponent<FightNode>();
        node.Type = NodeType.Occupied;
        ObstacleNodes.Add(node);
        obstacleNode.GetComponent<Renderer>().material.color = Color.red;
        obstacleNode.GetComponent<Renderer>().enabled = true;
    }

    public static GameObject GetNearestNode(Vector3 point)
    {
        var colliders = Physics.OverlapSphere(point, innerRadius * 3);
        var minDistance = float.MaxValue;
        GameObject nearestNode = colliders[0].transform.gameObject;
        foreach(var collider in colliders)
        { 
            var obj = collider.transform.gameObject;
            if(obj.GetComponent<FightNode>() == null || obj.transform.tag == "Graph" 
                || obj.GetComponent<FightNode>().Type == NodeType.Obstacle)
                continue;
            var distance = Vector3.Distance(point, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNode = collider.gameObject;
            }
        }

        return nearestNode;
    }

    public static void FindAvailableArea(GameObject characterObj)
    {
        AvailableNodes = new Dictionary<FightNode, FightNode>();
        var character = characterObj.GetComponent<FightCharacter>();
        var maxNodesDistance = characterObj.GetComponent<FightCharacter>().Energy;
        var startNode = GetNearestNode(characterObj.transform.position).GetComponent<FightNode>();
        WidthSearch(startNode, maxNodesDistance);
        foreach(var node in AvailableNodes)
        {
            var gameObject = node.Key.transform.gameObject;
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private static void WidthSearch(FightNode node, int remainingNodes)
    {
        if(node == null) 
            return;
        var nodesQueue = new Queue<(FightNode, int)>();
        nodesQueue.Enqueue((node, remainingNodes));
        Debug.Log("IN");
        if (ObstacleNodes == null)
            ObstacleNodes = new List<FightNode>();
        
        while(nodesQueue.Count != 0)
        {
            var currentNodeTuple = nodesQueue.Dequeue();
            if (currentNodeTuple.Item2 <= 0)
                continue;
    
            foreach(var neighbour in currentNodeTuple.Item1.Neighbours)
            {
                if(neighbour.Type == NodeType.Free && !AvailableNodes.ContainsKey(neighbour)
                    && neighbour.Type != NodeType.Obstacle)
                {
                    AvailableNodes[neighbour] = currentNodeTuple.Item1;
                    nodesQueue.Enqueue((neighbour, currentNodeTuple.Item2 - 1));
                }
            }
        }

        Debug.Log(AvailableNodes.Count());
    }

    public static void CleanAreasLists()
    {
        foreach(var node in ObstacleNodes)
        {
            node.Type = NodeType.Free;
            var nodeObj = node.transform.gameObject;
            nodeObj.GetComponent<Renderer>().enabled = false;
        }
        ObstacleNodes.Clear();

        foreach(var node in AvailableNodes)
        {
            var nodeObj = node.Key.transform.gameObject;
            nodeObj.GetComponent<Renderer>().enabled = false;
        }
        AvailableNodes.Clear();
    }

    public static FightNode GetNearestNodeNearEnemy(GameObject enemyObj, Vector3 hitPoint)
    {
        var colliders = Physics.OverlapSphere(Projection(enemyObj.transform.position), innerRadius * 3);
        var nearestNode = colliders[0].GetComponent<FightNode>();
        var minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            var node = collider.transform.gameObject.GetComponent<FightNode>();
            var distance = Vector3.Distance(hitPoint, collider.transform.position);
            if(node != null && AvailableNodes.ContainsKey(node) && distance < minDistance)
                {
                    minDistance = distance;
                    nearestNode = node;
                }
        }

        return nearestNode;
    }

    public static bool TryFindPath(FightNode start, FightNode finish)
    {
        Path.Clear();
        if (start == null || finish == null)
            return false;

        var resultPath = new List<Vector3>();
        var currentNode = finish;

        while (true)
        {
            if (currentNode == start)
            {
                resultPath.Add(currentNode.transform.position);
                Path = resultPath;
                return true;
            }

            if (!AvailableNodes.ContainsKey(currentNode))
                return false;
            
            resultPath.Add(currentNode.transform.position);
            currentNode = AvailableNodes[currentNode];
        }
    }

    public static void StartMoveCharacter(GameObject characterObj)
    {
        currentPathNodeIndex = Path.Count - 1;
    }

    public static bool IsEndMoveCharacter()
    {
        return currentPathNodeIndex < 0;
    }

    public static void MoveCharacterByPath(GameObject characterObj)
    { 
        var targetPoint = Path[currentPathNodeIndex];
        var moveDirection = 
        Vector3.ClampMagnitude(Projection(targetPoint) - Projection(characterObj.transform.position),
            3f * Time.deltaTime);
        
        var lookDirecton = new Vector3(targetPoint.x, characterObj.transform.position.y, targetPoint.z);
        characterObj.transform.LookAt(lookDirecton);
        characterObj.transform.Translate(moveDirection, Space.World);

        targetPoint = Path[currentPathNodeIndex];
        if(Vector3.Distance(Projection(characterObj.transform.position), targetPoint) < 0.1f)
        {
            currentPathNodeIndex--;
        }
    }
}
