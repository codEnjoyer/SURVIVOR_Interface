using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Graph_and_Map;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Editor
{
    [EditorTool ("NodesEditor")]
    public class NodesTool : EditorTool
    {
        public Texture2D icon;
        public Node nodePrefab;
        private List<Node> graph = new();
        private Vector3 mousePosition;
        private string pathToGraph;
        public override GUIContent toolbarIcon =>
            new()
            {
                image = icon,
                text = "Nodes Tool",
                tooltip = "Create, change nodes"
            };
        
        public override void OnActivated()
        {
            foreach (var node in FindObjectsOfType<Node>())
                graph.Add(node);
            base.OnActivated();
        }

        public override void OnToolGUI(EditorWindow window)
        {
            graph.RemoveAll(n => n == null);
            foreach (var node in graph)
            foreach (var (start,end) in node)
                Debug.DrawLine(start.transform.position,end.transform.position);
            
            if(target is GameObject activeObj)
            {
                EditorGUI.BeginChangeCheck();
                var oldPos = activeObj.transform.position;
                var newPos = Handles.PositionHandle(oldPos, Quaternion.identity);
                var offset = newPos - oldPos;
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (var obj in targets.Select(o => (GameObject)o))
                    {
                        Undo.RecordObject(obj.transform, "Move Node");
                        obj.transform.position+=offset;
                    }
                }
            }
            if (Event.current.Equals(Event.KeyboardEvent("k")))
            {
                var activeNodes = Selection.gameObjects
                    .Select(o => o.GetComponent<Node>())
                    .ToArray();
                switch (activeNodes.Length)
                {
                    case 0:
                        CreateNode();
                        break;
                    case 1:
                        ConnectOrDisconnectNodes(CreateNode(),activeNodes[0]);
                        break;
                    case 2:
                        ConnectOrDisconnectNodes(activeNodes[0],activeNodes[1]);
                        break;
                    case > 2:
                        Debug.Log("Too many nodes");
                        break;
                }
            }
            
            base.OnToolGUI(window);
        }

        private Node CreateNode()
        {
            var node = Instantiate(nodePrefab);
            node.transform.position = GetMousePosition();
            Selection.activeObject = node;
            graph.Add(node);
            Undo.RegisterCreatedObjectUndo(node.gameObject,"Create Node");
            return node;
        }
        
        private void ConnectOrDisconnectNodes(Node firstNode, Node secondNode)
        {
            if (firstNode.neighborhoods.Contains(secondNode))
            {
                firstNode.neighborhoods.Remove(secondNode);
                secondNode.neighborhoods.Remove(firstNode);
            }
            else
            {
                firstNode.neighborhoods.Add(secondNode);
                secondNode.neighborhoods.Add(firstNode);
            }
        }

        private Vector3 GetMousePosition()
        {
            var mousePos = Event.current.mousePosition;
            var mouseX = mousePos.x;
            var mouseY = Camera.current.pixelHeight - mousePos.y;
            var myRay = Camera.current.ScreenPointToRay(new Vector3(mouseX, mouseY, 0));
            return Physics.Raycast(myRay, out var hitInfo) ? hitInfo.point : default(Vector2);
        }
    }
}