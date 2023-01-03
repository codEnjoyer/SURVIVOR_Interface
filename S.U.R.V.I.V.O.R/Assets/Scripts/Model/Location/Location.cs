using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Location : MonoBehaviour
{
    [SerializeField] private LocationData data;
    public LocationData Data => data;

    public void Awake()
    {
        if (data == null)
        {
            Debug.Log("У ноды нет локации!");
        }
    }

    public void UpdatePrefab()
    {
        //
        if (data != null && data.Prefab != null)
        {
            GetComponent<MeshFilter>().mesh = data.Prefab.GetComponent<MeshFilter>().sharedMesh;
            GetComponent<MeshRenderer>().material = data.Prefab.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else
        {
            var node = Resources.Load<GameObject>("Node");
            GetComponent<MeshFilter>().mesh = node.GetComponent<MeshFilter>().sharedMesh;
            GetComponent<MeshRenderer>().material = node.GetComponent<MeshRenderer>().sharedMaterial;
        }

        var x = transform.position.x;
        var y = GetComponent<MeshRenderer>().bounds.size.y * transform.localScale.y / 2;
        var z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}