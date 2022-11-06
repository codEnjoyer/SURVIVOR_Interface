using System;
using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
public abstract class Item: MonoBehaviour
{
    public InventoryItem InventoryItem { get; private set; }

    private void Awake()
    {
        InventoryItem = gameObject.GetComponent<InventoryItem>();
    }
}