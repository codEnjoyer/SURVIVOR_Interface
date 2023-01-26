using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Location : MonoBehaviour, ISerializationCallbackReceiver
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
        var meshFilter = GetComponent<MeshFilter>();
        var meshRender = GetComponent<MeshRenderer>();
        if (meshFilter == null || meshRender == null)
            return;
        if (data != null && data.Prefab != null)
        {
            meshFilter.mesh = data.Prefab.GetComponent<MeshFilter>().sharedMesh;
            meshRender.material = data.Prefab.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else
        {
            var node = Resources.Load<GameObject>("Node");
            meshFilter.mesh = node.GetComponent<MeshFilter>().sharedMesh;
            meshRender.material = node.GetComponent<MeshRenderer>().sharedMaterial;
        }

        var x = transform.position.x;
        var y = meshRender.bounds.size.y * transform.localScale.y / 2;
        var z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
}