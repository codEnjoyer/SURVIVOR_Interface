using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unpackable))]
public class PackedContainer : MonoBehaviour
{
    [SerializeField] private PackedContainerData data;

    public IEnumerable<BaseItem> Unpack()
    {
        return data.PackedItems;
    }
}
