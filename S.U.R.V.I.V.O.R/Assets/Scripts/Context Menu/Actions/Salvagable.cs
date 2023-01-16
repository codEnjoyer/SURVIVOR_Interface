using System.Collections;
using System.Collections.Generic;
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
        scrap = GetComponent<Scrap>();
    }

    public void OnButtonClickAction<T>(T value)
    {
        var itemOwner = scrap.GetComponent<BaseItem>().ItemOwner;
        var salvagedItems = scrap.salvagableItems;
        GetComponent<BaseItem>().Destroy();
        foreach (var packed in salvagedItems)
        {
            var isSuccess = itemOwner.body.PlaceItemToInventory(Instantiate(packed));
            if (!isSuccess) 
            {
                Debug.Log($"Вам некуда положить один из предметов, получившихся в результате распаковки, он был уничтожен");
            }
        }
    }
}
