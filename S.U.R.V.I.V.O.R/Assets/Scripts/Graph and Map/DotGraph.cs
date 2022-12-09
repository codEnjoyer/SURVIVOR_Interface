using System.Collections.Generic;

using UnityEngine;

namespace Graph_and_Map
{
    public class DotGraph:MonoBehaviour
    {
        public static DotGraph Instance { get; private set; }
        private readonly List<Node> nodes = new();
        private readonly KdTree kdTree = new();
        private Camera mainCamera;
        private Vector3 mPos;

        public Node GetNearestNode()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var rayHit))
                mPos = rayHit.point;
            return (Node)kdTree.GetNeighbour(new Vector2(mPos.x, mPos.z));
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                nodes.AddRange(FindObjectsOfType<Node>());
                kdTree.AddRange(nodes);
            }
            else if (Instance == this)
                Destroy(gameObject);
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public IEnumerable<Node> GetNodes() => nodes;
    }
}