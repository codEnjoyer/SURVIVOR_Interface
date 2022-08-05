using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NearestNodeFinder : MonoBehaviour
{
    public GameObject Trigger;
    public bool isNeedToFindNode;
    private GameObject NearestNode;
    private List<GameObject> findedNodes;
    private int oldMassiveLength = 100;
    private bool isAllNodesFound;

    void Start()
    {
        findedNodes = new List<GameObject>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Node")
        {
            if (!findedNodes.Contains(other.GameObject()))
                findedNodes.Add(other.gameObject);
        }

        isAllNodesFound = oldMassiveLength == findedNodes.Count;
        oldMassiveLength = findedNodes.Count;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Node")
        {
            if (findedNodes.Contains(other.GameObject()))
                findedNodes.Remove(other.gameObject);
        }
    }

    private void FindNearestNode()
    {
        var minDistance = float.PositiveInfinity;
        foreach (var findedNode in findedNodes)
        {
            var currentDistance = Vector3.Distance(findedNode.transform.position, Trigger.transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                NearestNode = findedNode;
            }
        }
    }

    public GameObject GetNearestNode()
    {
        switch (isAllNodesFound)
        {
            case false:
                return null;
            case true:
                var result = NearestNode;
                NearestNode = null;
                findedNodes = new List<GameObject>();
                isAllNodesFound = false;
                isNeedToFindNode = false;
                return result;
        }
        
    }

    void Update()
    {
        if (isAllNodesFound && isNeedToFindNode)
        {
            Debug.Log("findingobj");
            FindNearestNode();
        }
    }
}
