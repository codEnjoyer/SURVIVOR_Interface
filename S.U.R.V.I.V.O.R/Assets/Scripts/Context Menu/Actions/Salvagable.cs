using System.Collections;
using System.Collections.Generic;
using Model.Items;
using UnityEngine;


[RequireComponent(typeof(ContextMenuItem))]
public class Salvagable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    
    public bool Extendable { get; private set; }

    private Scrap scrap;

    private InventoryController inventoryController;

    public void Awake()
    {
        ButtonText = "Разобрать";
        Extendable = false;
        inventoryController = InventoryController.Instance;
        scrap = GetComponent<Scrap>();
    }

    public void OnButtonClickAction<T>(T value)
    {
        var itemOwner = scrap.GetComponent<BaseItem>().ItemOwner;
        var salvagedItems = scrap.salvagableItems;
        GetComponent<BaseItem>().Destroy();
        foreach (var packed in salvagedItems)
        {
            bool isSuccess;
            if (itemOwner != null)
                isSuccess = itemOwner.ManBody.PlaceItemToInventory(Instantiate(packed));
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
