using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryController inventoryController;
    private InventoryGrid inventoryGrid;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        inventoryGrid = GetComponent<InventoryGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.SelectedInventoryGrid = inventoryGrid;
        inventoryController.IsPointerUnderInventory = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryController.SelectedInventoryGrid = null;
        inventoryController.IsPointerUnderInventory = false;
    }
}