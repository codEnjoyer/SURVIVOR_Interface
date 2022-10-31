using System.Collections.Generic;

using UnityEngine;

namespace Graph_and_Map
{
    public class DotGraph:MonoBehaviour
    {
        public static DotGraph instance;
        public readonly List<Node> nodes = new();
        public readonly KdTree kdTree = new();
        private Camera mainCamera;
        private Vector3 mPos;

        public Node GetNearestNode()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var rayHit))
                mPos = rayHit.point;
            return kdTree.GetNeighbour(new Vector2(mPos.x, mPos.z));
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                nodes.AddRange(FindObjectsOfType<Node>());
                kdTree.AddRange(nodes);
            }
            else if (instance == this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }
    }
}