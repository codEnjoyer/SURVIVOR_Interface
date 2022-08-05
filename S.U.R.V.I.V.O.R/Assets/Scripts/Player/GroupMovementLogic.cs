using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GroupMovementLogic : MonoBehaviour
{
    public Button Button;
    public GameObject Trigger;
    private List<GameObject> foundedNodes;
    private bool isWaitingCord;
    void Start()
    {
        foundedNodes = new List<GameObject>();
        Button.onClick.AddListener(OnButtonClick);
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Node")
        {
            Debug.Log("Enter");
            if(!foundedNodes.Contains(other.GameObject()))
                foundedNodes.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Node")
        {
            Debug.Log("Exit");
            if (foundedNodes.Contains(other.GameObject()))
                foundedNodes.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isWaitingCord)
        {
            Vector3 clickPosition;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo,200f))
            {
                clickPosition = hitInfo.point;
                Trigger.transform.position = clickPosition;
                isWaitingCord = false;
            }
        }
        Debug.Log(foundedNodes.Count);
    }


    private void OnButtonClick()
    {
        isWaitingCord = true;
        Button.enabled = false;
    }

    private void OnTurnEnd()
    {
        Button.enabled = true;
    }

    private void FindNearestNode()
    {
        var mindistance = float.PositiveInfinity;
        foreach (var node in foundedNodes)
        {
            var currentDistance = Vector3.Distance(node.GetComponent<Transform>().position, Trigger.transform.position);
            if (mindistance > currentDistance)
            {
                mindistance = currentDistance;
                nearestNode = node;
            }
        }
    }
}
