using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.UI;
using UnityEngine.UIElements;

[EditorTool ("NodesEditor")]
public class NodesTool : EditorTool
{
    public Texture2D icon;
    public GameObject NodePrefab;
    public DotGraph Graph;
    private List<Tuple<Vector3, Vector3>> Lines = new List<Tuple<Vector3, Vector3>>();

    public override GUIContent toolbarIcon
    {
        get 
        {
            return new GUIContent
            {
                image = icon,
                text = "Nodes Tool",
                tooltip = "Create, change nodes"
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        foreach (var tuple in Lines)
        {
            Debug.DrawLine(tuple.Item1, tuple.Item2);
        }

        if (Event.current.Equals(Event.KeyboardEvent("k")))
        {
            switch (Selection.objects.Length)
            {
                case 0:
                    CreateNode();
                    break;
                case 1:
                    ConnectOrDisconnectNodes(Selection.activeObject.GetComponent<Node>(),CreateNode().GetComponent<Node>());
                    break;
                case 2:
                    ConnectOrDisconnectNodes(Selection.objects[0].GetComponent<Node>(), Selection.objects[1].GetComponent<Node>());
                    break;
                case > 2:
                    Debug.Log("Нельзя провести соединение, слишком много выделенных объектов");
                    break;
            }
        }

        if (Event.current.Equals(Event.KeyboardEvent("j")))
        {
            Lines.Clear();
            var objectsForLook = GameObject.FindGameObjectsWithTag("Node");
            foreach (var obj in objectsForLook)
            {
                var node = obj.GetComponent<Node>();
                foreach (var nodeNeighborhood in node.Neighborhoods)
                {
                    Lines.Add(Tuple.Create(node.GetComponentInParent<Transform>().position, nodeNeighborhood.GetComponentInParent<Transform>().position));
                }
            }
        }
    }

    private GameObject CreateNode()
    {
        var obj = Instantiate(NodePrefab);
        var mouseX = Event.current.mousePosition.x;
        var mouseY = Camera.current.pixelHeight - Event.current.mousePosition.y;
        var myRay = Camera.current.ScreenPointToRay(new Vector3(mouseX, mouseY, 0));
        if (Physics.Raycast(myRay, out var hitInfo, 100))
        {
            obj.transform.position = hitInfo.point;
        }
        if(Selection.activeObject != null)
            Selection.activeObject = obj;
        return obj;
    }

    private void ConnectOrDisconnectNodes(Node firstNode, Node secondNode)
    {
        if (firstNode.Neighborhoods.Contains(secondNode))
        {
            firstNode.Neighborhoods.Remove(secondNode);
            secondNode.Neighborhoods.Remove(firstNode);
        }
        else
        {
            firstNode.Neighborhoods.Add(secondNode);
            secondNode.Neighborhoods.Add(firstNode);
            Lines.Add(new Tuple<Vector3, Vector3>(firstNode.GetComponentInParent<Transform>().position, secondNode.GetComponentInParent<Transform>().position));
        }
    }
}