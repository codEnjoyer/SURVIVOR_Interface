using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Cookable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    public bool Extendable { get; private set; }

    private CookableFood cookableFood;

    private InventoryController inventoryController;

    public void Awake()
    {
        ButtonText = "Приготовить";
        Extendable = true;
        inventoryController = InventoryController.Instance;
        cookableFood = GetComponent<CookableFood>();
    }

    public void OnButtonClickAction<T>(T value)
    {
        var character = value as Character;
        var itemOwner = cookableFood.GetComponent<BaseItem>().ItemOwner;
        var foodAfterCook = character.Cook(cookableFood);

        foreach (var packed in foodAfterCook)
        {
            bool isSuccess;
            if (itemOwner != null)
                isSuccess = itemOwner.body.PlaceItemToInventory(Instantiate(packed));
            else
            {
                inventoryController.ThrowItemAtLocation(Instantiate(packed));
                isSuccess = true;
            }

            if (!isSuccess)
            {
                Debug.Log(
                    $"Вам некуда положить один из предметов, получившихся в результате распаковки, он был уничтожен");
            }
        }
    }
}
