using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ContextMenuItem))]
public class Wearable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    
    public bool Extendable { get; private set;}

    private Clothes currentClothes;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Надеть";
        Extendable = true;
        currentClothes = GetComponent<Clothes>();
        item = GetComponent<BaseItem>();
    }


    public void OnButtonClickAction<T>(T value)
    {
        item.gameObject.SetActive(false);
        var inventory = item.InventoryGrid;
        var character = value as Character;
        inventory.PickUpItem(item);
        var isSuccessful = character.body.Wear(currentClothes);
        
        if (!isSuccessful)
        {
            inventory.InsertItem(item);
            item.gameObject.SetActive(true);
        }
    }
}
