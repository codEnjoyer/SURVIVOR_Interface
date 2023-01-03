using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ContextMenuItem))]
public class Wearable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    private Clothes currentClothes;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Надеть";
        currentClothes = GetComponent<Clothes>();
        item = GetComponent<BaseItem>();
    }
    public void OnButtonClickAction(Vector2 mousePosition)
    {
        var inventory = item.InventoryGrid;
        var character = item.ItemOwner;
        inventory.PickUpItem(item);
        var isSuccessful = character.body.Wear(currentClothes);

        if (!isSuccessful)
            inventory.InsertItem(item);
    }
}
