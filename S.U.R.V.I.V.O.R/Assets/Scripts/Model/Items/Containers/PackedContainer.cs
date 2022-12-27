using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unpackable))]
public class PackedContainer : MonoBehaviour
{
    [SerializeField] private PackedContainerData data;

    public IEnumerable<GameObject> Unpack()
    {
        return data.PackedItems;
    }
}
