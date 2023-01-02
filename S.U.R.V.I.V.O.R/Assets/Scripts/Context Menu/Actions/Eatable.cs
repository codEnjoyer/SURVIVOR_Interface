using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Eatable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    private EatableFood eatableFood;

    private InventoryController inventoryController;
    
    public void Awake()
    {
        ButtonText = "Съесть";
        eatableFood = GetComponent<EatableFood>();
    }
    public void OnButtonClickAction(Vector2 mousePosition)
    {
        var itemOwner = eatableFood.GetComponent<BaseItem>().ItemOwner;
        itemOwner.Eat(eatableFood);
        Debug.Log($"Вам некуда положить один из предметов, получившихся в результате готовки, он был уничтожен");
    }
}
