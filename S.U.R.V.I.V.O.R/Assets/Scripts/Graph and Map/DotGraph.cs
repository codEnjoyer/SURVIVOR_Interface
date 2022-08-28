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
        public readonly List<Node> nodes = new();
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
            nearestNode = kdTree.GetNeighbour(new Vector2(mPos.x, mPos.z));
            line.SetPositions(new[]{mPos,nearestNode.transform.position});
        }
    }
}