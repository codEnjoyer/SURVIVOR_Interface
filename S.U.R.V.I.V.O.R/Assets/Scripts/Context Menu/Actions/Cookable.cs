using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Cookable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    private CookableFood cookableFood;

    private InventoryController inventoryController;
    
    public void Awake()
    {
        ButtonText = "Приготовить";
        cookableFood = GetComponent<CookableFood>();
    }
    public void OnButtonClickAction(Vector2 mousePosition)
    {
        var itemOwner = cookableFood.GetComponent<BaseItem>().ItemOwner;
        var foodAfterCook = itemOwner.Cook(cookableFood);
        foreach (var food in foodAfterCook)
        {
            if (!itemOwner.body.PlaceItemToInventory(food.GetComponent<BaseItem>()))
            {
                Debug.Log($"Вам некуда положить один из предметов, получившихся в результате готовки, он был уничтожен");
            }
        }
    }
}
