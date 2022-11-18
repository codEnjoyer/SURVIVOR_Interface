using System.Collections.Generic;
using UnityEngine;

namespace Graph_and_Map
{
    public class Node : MonoBehaviour, IKdTreePoint
    {
        public List<Node> neighborhoods = new ();
        private LineRenderer line;
        public Vector2 PositionIn2D => transform.position.To2D();
        public void DrawEdges()
        {
            var index = 0;
            line = GetComponent<LineRenderer>();
            line.positionCount = 0;
            neighborhoods.RemoveAll(node => node == null);
            line.positionCount = neighborhoods.Count * 2;
            foreach (var (start, end) in GetEdges())
            {
                line.SetPosition(index,start.transform.position);
                index++;
                line.SetPosition(index,end.transform.position);
                index++;
            }
        }

        public IEnumerable<(Node,Node)> GetEdges()
        {
            neighborhoods.RemoveAll(node => node == null);
            foreach (var neighborhood in neighborhoods)
            {
                yield return (this, neighborhood);
            }
        }

        public void Awake()
        {
            DrawEdges();
        }
    }
}