using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph_and_Map
{
    public class Node : MonoBehaviour, IEnumerable<(Node start, Node end)>
    {
        [SerializeField]
        public List<Node> neighborhoods { get; private set; }
        [SerializeField] private LocationType location;
        private LineRenderer line;
        public Vector2 positionIn2D => new(transform.position.x,transform.position.z);
        public IEnumerator<(Node start, Node end)> GetEnumerator()
        {
            neighborhoods.RemoveAll(node => node == null);
            foreach (var neighborhood in neighborhoods)
                yield return (this, neighborhood);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Awake()
        {
            neighborhoods = new List<Node>();
            var index = 0;
            line = GetComponent<LineRenderer>(); 
            neighborhoods.RemoveAll(node => node == null);
            line.positionCount = neighborhoods.Count * 2;
            foreach (var neighborhood in neighborhoods)
            {
                line.SetPosition(index,transform.position);
                index++;
                line.SetPosition(index,neighborhood.transform.position);
                index++;
            }
        }
        
    }
}