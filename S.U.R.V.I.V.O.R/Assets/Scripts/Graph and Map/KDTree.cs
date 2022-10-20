using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph_and_Map
{
    public class KdTree: IEnumerable<Node>
    {
        public Node value { get; private set; }
        public readonly bool splitX;
        public int count { get; private set; }
        
        private KdTree left;
        private KdTree right;
        private bool isEmpty => count == 0;
        private KdTree(Node value, KdTree parent)
        {
            this.value = value;
            splitX = !parent.splitX;
            count = 1;
        }

        public KdTree()
        {
            splitX = true;
        }
    
        public void AddRange(IEnumerable<Node> data)
        {
            foreach (var point in data)
                Add(point);
        }
    
        public void Add(Node node)
        {
            if(node is null)
                return;
            if (isEmpty)
            {
                value = node;
                count++;
                return;
            }

            var currentNode = this;
            while (true)
            {
                currentNode.count++;
                if (currentNode.splitX && node.positionIn2D.x < currentNode.value.positionIn2D.x ||
                    !currentNode.splitX && node.positionIn2D.y < currentNode.value.positionIn2D.y)
                {
                    if (currentNode.left == null)
                    {
                        currentNode.left = new KdTree(node, currentNode);
                        break;
                    }

                    currentNode = currentNode.left;
                }
                else
                {
                    if (currentNode.right == null)
                    {
                        currentNode.right = new KdTree(node, currentNode);
                        break;
                    }

                    currentNode = currentNode.right;
                }
            }
        }
        
        private static double minDist;
        private static Node result;

        public Node GetNeighbour(Vector2 target)
        {
            if (isEmpty) return null;
            minDist = int.MaxValue;
            result = default;
            InnerGetClosest(this, target);
            return result;
        }

        private static void InnerGetClosest(KdTree tree, Vector2 target)
        {
            while (true)
            {
                if (tree is null) return;
                var curDist = (target - tree.value.positionIn2D).magnitude;
                if (curDist < minDist)
                {
                    result = tree.value;
                    minDist = curDist;
                }

                if (tree.splitX && target.x < tree.value.positionIn2D.x ||
                    !tree.splitX && target.y < tree.value.positionIn2D.y)
                    InnerGetClosest(tree.left, target);
                else
                    InnerGetClosest(tree.right, target);
                var rang = tree.splitX
                    ? Math.Abs(tree.value.positionIn2D.x - target.x)
                    : Math.Abs(tree.value.positionIn2D.y - target.y);
                if (rang > minDist) return;
                if (tree.splitX && target.x < tree.value.positionIn2D.x ||
                    !tree.splitX && target.y < tree.value.positionIn2D.y)
                    tree = tree.right;
                else
                    tree = tree.left;
            }
        }

        public IEnumerator<Node> GetEnumerator()
        {
            foreach (var point in left)
                yield return point;
            yield return value;
            foreach (var point in right)
                yield return point;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}