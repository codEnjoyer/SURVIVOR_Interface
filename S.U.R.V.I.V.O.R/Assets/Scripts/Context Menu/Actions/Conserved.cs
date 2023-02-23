using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ContextMenuItem))]
public class Conserved : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    public bool Extendable { get; private set; }

    private ConservedFood conservedFood;

    private InventoryController inventoryController;

    public void Awake()
    {
        ButtonText = "Открыть консерву";
        Extendable = false;
        inventoryController = InventoryController.Instance;
        conservedFood = GetComponent<ConservedFood>();
    }

    public void OnButtonClickAction<T>(T value)
    {
        var itemOwner = conservedFood.GetComponent<BaseItem>().ItemOwner;
        //TODO проверить есть ли в инвентаре предмет для открывания
        var foodAfterOpen = conservedFood.Open();
        
        bool isSuccess;
        if (itemOwner != null)
            isSuccess = itemOwner.ManBody.PlaceItemToInventory(Instantiate(foodAfterOpen));
        else
        {
            inventoryController.ThrowItemAtLocation(Instantiate(foodAfterOpen));
            isSuccess = true;
        }

        if (!isSuccess)
        {
            Debug.Log(
                $"Вам некуда положить один из предметов, получившихся в результате распаковки, он был уничтожен");
        }
    }
}