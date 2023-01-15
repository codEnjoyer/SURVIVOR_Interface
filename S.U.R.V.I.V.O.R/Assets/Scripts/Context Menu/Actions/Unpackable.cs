using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(ContextMenuItem))]
public class Unpackable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    
    public bool Extendable { get; private set; }

    private PackedContainer packedContainer;

    private InventoryController inventoryController;

    public void Awake()
    {
        ButtonText = "Распаковать";
        Extendable = false;
        packedContainer = GetComponent<PackedContainer>();
    }

    public void OnButtonClickAction<T>(T value)
    {
        var itemOwner = packedContainer.GetComponent<BaseItem>().ItemOwner;
        var unPackedItems = packedContainer.Unpack();
        
        foreach (var packed in unPackedItems)
        {
            var isSuccess = itemOwner.body.PlaceItemToInventory(Instantiate(packed));

            if (!isSuccess)
            {
                Debug.Log(
                    $"Вам некуда положить один из предметов, получившихся в результате распаковки, он был уничтожен");
            }
        }
    }
}