using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.Json;

namespace Graph_and_Map
{
    public class DotGraph:MonoBehaviour
    {
        public static DotGraph instance;
        public List<Node> nodes = new();
        public readonly KdTree kdTree = new();
        private Camera mainCamera;
        private Vector3 mPos;
        private Node nearestNode;
        private LineRenderer line;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                nodes.AddRange(FindObjectsOfType<Node>());
                kdTree.AddRange(nodes);
                line = GetComponent<LineRenderer>();
            }
            else if (instance == this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var rayHit))
                mPos = rayHit.point;
            nearestNode = kdTree.GetNeighbour(ConvertTo2D(mPos));
            line.SetPositions(new[]{mPos,nearestNode.transform.position});
        }
        
        private Vector2 ConvertTo2D(Vector3 v3) => new(v3.x, v3.z);
    }
}