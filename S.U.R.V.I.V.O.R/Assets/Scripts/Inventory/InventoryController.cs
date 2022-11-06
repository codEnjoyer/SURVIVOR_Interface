using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }

    private InventoryItem selectedInventoryItem;
    private InventoryItem overlapInventoryItem;
    private RectTransform rectTransform;
    
    [SerializeField] private Transform canvasTransform;

    private InventoryHighlight inventoryHighlight;
    private InventoryItem inventoryItemToHighlight;
    private Vector2Int? previousPosition;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        if (selectedInventoryItem != null)
            rectTransform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            // if (selectedItem != null && Input.GetMouseButtonDown(0))
            // {
            //     ThrowItemAtLocation();
            // }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var tileGridPosition = GetTileGridPosition();

            if (selectedInventoryItem == null)
            {
                PickUpItem(tileGridPosition);
            }
            else
            {
                PlaceItem(tileGridPosition);
            }
        }

        HandleHighlight();
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedInventoryItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedInventoryItem != null)
        {
            rectTransform = selectedInventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        var complete = selectedItemGrid.PlaceItem(selectedInventoryItem, tileGridPosition.x, tileGridPosition.y,
            ref overlapInventoryItem);
        if (complete)
        {
            selectedInventoryItem = null;
            if (overlapInventoryItem != null)
            {
                selectedInventoryItem = overlapInventoryItem;
                overlapInventoryItem = null;
                rectTransform = selectedInventoryItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void ThrowItemAtLocation() // Перемещение предмета в инвентарь локации при нажатии на пустое пространство
    {
        var locationItemGrid = GameObject.FindGameObjectWithTag("LocationItemGrid").GetComponent<ItemGrid>();
        var positionOnGrid = locationItemGrid.FindSpaceForObject(selectedInventoryItem);
        locationItemGrid.PlaceItem(selectedInventoryItem, positionOnGrid.Value.x, positionOnGrid.Value.y);
        selectedInventoryItem = null;
    }

    private void RotateItem()
    {
        if (selectedInventoryItem == null) return;
        selectedInventoryItem.Rotated();
    }

    private void HandleHighlight()
    {
        var positionOnGrid = GetTileGridPosition();
        if (previousPosition == positionOnGrid) return;
        if (selectedInventoryItem == null)
        {
            inventoryItemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (inventoryItemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(inventoryItemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, inventoryItemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y,
                selectedInventoryItem.Width, selectedInventoryItem.Height));
            inventoryHighlight.SetSize(selectedInventoryItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedInventoryItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        var mousePosition = Input.mousePosition;
        if (selectedInventoryItem != null)
        {
            mousePosition.x -= (selectedInventoryItem.Width - 1) * ItemGrid.TileSize / 2;
            mousePosition.y += (selectedInventoryItem.Height - 1) * ItemGrid.TileSize / 2;
        }

        return selectedItemGrid.GetTileGridPosition(mousePosition);
    }

    public void AddItemToInventory(InventoryItem inventoryItem)
    {
        if (selectedInventoryItem != null) return;
        CreateItem(inventoryItem);
        var itemToInsert = selectedInventoryItem;
        selectedInventoryItem = null;
        InsertItem(itemToInsert);
        selectedItemGrid = null;
    }

    private void InsertItem(InventoryItem inventoryItemToInsert)
    {
        var positionOnGrid = selectedItemGrid.FindSpaceForObject(inventoryItemToInsert);
        if (positionOnGrid == null)
        {
            Destroy(inventoryItemToInsert);
            return;
        }
        selectedItemGrid.PlaceItem(inventoryItemToInsert, positionOnGrid.Value.x, positionOnGrid.Value.y);
    }

    private void CreateItem(InventoryItem inventoryItem)
    {
        if (selectedInventoryItem != null) return;
        var newInvItem = Instantiate(inventoryItem);
        selectedInventoryItem = newInvItem;
        rectTransform = newInvItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        newInvItem.Set(newInvItem.ItemData);
    }
}