using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Editor
{
    [EditorTool ("Nodes Net Tool")]
    public class NodesNetTool : EditorTool
    {
        public Texture2D icon;
        private const float innerRadius = 0.6f;
        private const float outerRadius = innerRadius * 1.155f;
        private GameObject[,] Nodes;
        private List<Tuple<Vector3, Vector3>> Edges = new List<Tuple<Vector3, Vector3>>();
        private bool needDrawEdges = true;

        public override GUIContent toolbarIcon =>
            new GUIContent
            {
                image = icon,
                text = "Nodes Net Tool",
                tooltip = "Создает сеть вершин по двум вершинам"
            };

        public override void OnToolGUI(EditorWindow window)
        {
            if (needDrawEdges)
                foreach(var edge in Edges)
                    Debug.DrawLine(edge.Item1, edge.Item2);

            if(Event.current.Equals(Event.KeyboardEvent("k")))
            {
                if (Selection.count == 2) 
                {
                    var firstObj = Selection.gameObjects[0];
                    var secondObj = Selection.gameObjects[1];
                    if ( firstObj.GetComponent<FightNode>() != null
                         && secondObj.GetComponent<FightNode>() != null)
                    {
                        if (Math.Abs(firstObj.transform.position.x - secondObj.transform.position.x) < innerRadius
                            || Math.Abs(firstObj.transform.position.z - secondObj.transform.position.z) < innerRadius)
                            Debug.Log("Слишком маленький участок");
                        else
                        {
                            Debug.Log("Create");
                            CreateNet(firstObj, secondObj);
                        }
                    }
                }
            }

            if (Event.current.Equals(Event.KeyboardEvent("i")))
                needDrawEdges = !needDrawEdges;

            if (Event.current.Equals(Event.KeyboardEvent("j")))
                DeleteNode();

            if (Event.current.Equals(Event.KeyboardEvent("u")))
                ReDrawEdges();

            if (Event.current.Equals(Event.KeyboardEvent("o")))
                ConnectOrDisconnect();

            if (Event.current.Equals(Event.KeyboardEvent("p")))
                ChangeVisibility();
        }

        private void CreateNet(GameObject firstObj, GameObject secondObj)
        {
            var rows = (int)(Math.Abs(firstObj.transform.position.z - secondObj.transform.position.z) / (1.5 * outerRadius)) + 1;
            var columns = (int)(Math.Abs(firstObj.transform.position.x - secondObj.transform.position.x) / (2 * innerRadius)) + 1;

            // secondObj.transform.position = firstObj.transform.position 
            // + new Vector3( (columns - 1) * innerRadius * Math.Sign (secondObj.transform.position.x - firstObj.transform.position.x),
            // 0 , (rows - 1) * innerRadius * Math.Sign (secondObj.transform.position.z - firstObj.transform.position.z));

            Nodes = new GameObject[rows, columns];
            Nodes[0, 0] = firstObj;
            Nodes[rows - 1, columns - 1] = secondObj;

            firstObj.GetComponent<FightNode>().Neighbours.Clear();
            secondObj.GetComponent<FightNode>().Neighbours.Clear();

            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    if ((i != 0 && j != rows - 1) || (i != columns - 1 && j != 0))
                    {
                        var newNode = Instantiate(firstObj);
                        newNode.transform.parent = firstObj.transform.parent;
                    
                        newNode.transform.position = firstObj.transform.position 
                                                     + new Vector3((i * 2 * innerRadius + (j % 2) * innerRadius) * Math.Sign (secondObj.transform.position.x - firstObj.transform.position.x),
                                                         0, j * 1.5f *outerRadius * Math.Sign (secondObj.transform.position.z - firstObj.transform.position.z)); 

                        Nodes[j, i] = newNode;
                        Nodes[j, i].GetComponent<FightNode>().Index = "j: " + j + " i: " + i;
                    }
                }
            }

            MakeNeighbours(Nodes, rows, columns);
        }

        private void MakeNeighbours(GameObject[,] nodes, int rows, int columns)
        {
            Debug.Log("Make Neighbours");
            for(var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    if (i != 0)
                    {
                        nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j, i - 1].GetComponent<FightNode>());
                        Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j, i - 1].transform.position));
                    }
                    if (j != 0)
                    {
                        nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j - 1, i].GetComponent<FightNode>());
                        Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j - 1, i].transform.position));
                    }
                    if (i != columns - 1)
                    {
                        nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j, i + 1].GetComponent<FightNode>());
                        Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j, i + 1].transform.position));
                    }
                    if (j != rows - 1)
                    {
                        nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j + 1, i].GetComponent<FightNode>());
                        Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j + 1, i].transform.position));
                    }
                    if (j % 2 == 0)
                    {
                        if (i != 0)
                        {
                            if (j != 0)
                            {
                                nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j - 1, i - 1].GetComponent<FightNode>());
                                Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j - 1, i - 1].transform.position));
                            }
                            if (j != rows - 1)
                            {
                                nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j + 1, i - 1].GetComponent<FightNode>());
                                Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j + 1, i - 1].transform.position));
                            }
                        }
                    }
                    else
                    {
                        if (i != columns - 1)
                        {
                            if (j != 0)
                            {
                                nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j - 1, i + 1].GetComponent<FightNode>());
                                Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j - 1, i + 1].transform.position));
                            }
                            if (j != rows - 1)
                            {
                                nodes[j, i].GetComponent<FightNode>().Neighbours.Add(nodes[j + 1, i + 1].GetComponent<FightNode>());
                                Edges.Add(new Tuple<Vector3, Vector3>(nodes[j, i].transform.position, nodes[j + 1, i + 1].transform.position));
                            }
                        }
                    }
                }
            }
        }

        private void DeleteNode()
        {
            foreach(var nodeObj in Selection.gameObjects)
            {
                var node = nodeObj.GetComponent<FightNode>();
                if (node != null)
                {
                    foreach(var neighbour in node.Neighbours)
                        neighbour.Neighbours.Remove(node);
                    DestroyImmediate(nodeObj);
                }
            }
        }

        private void ReDrawEdges()
        {
            Edges.Clear();
            if (Nodes.Length == 0)
                return;
            foreach(var nodeObj in Nodes)
            {
                if (nodeObj != null)      
                    foreach(var neighbour in nodeObj.GetComponent<FightNode>().Neighbours)
                    {
                        var neighbourObjPos = neighbour.GetComponentInParent<Transform>().position;
                        Edges.Add(new Tuple<Vector3, Vector3>(neighbourObjPos, nodeObj.transform.position));
                    }
            }
        }

        private void ConnectOrDisconnect()
        {
            if (Selection.gameObjects.Length == 2)
            {
                var firstNode = Selection.gameObjects[0].GetComponent<FightNode>();
                var secondNode = Selection.gameObjects[1].GetComponent<FightNode>();

                if (firstNode.Neighbours.Contains(secondNode))
                {
                    firstNode.Neighbours.Remove(secondNode);
                    secondNode.Neighbours.Remove(firstNode);
                }
                else
                {
                    firstNode.Neighbours.Add(secondNode);
                    secondNode.Neighbours.Add(firstNode);
                }
            }
            else
            {
                Debug.Log("Выберите только две вершины");
            }
        }

        private void ChangeVisibility()
        {
            foreach(var node in Nodes)
                node.GetComponent<MeshRenderer>().enabled = !node.GetComponent<MeshRenderer>().enabled;
        }
    }
}
   