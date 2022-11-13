using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public BaseItem selectedItem;

    private BaseItem overlapItem;
    private RectTransform rectTransform;
    
    [SerializeField] private Transform canvasTransform;

    private InventoryHighlight inventoryHighlight;
    private BaseItem itemToHighlight;
    private Vector2Int? previousPosition;
    public ItemGrid itemgridtotest;
    public bool IsPointerUnderInventory;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        if (selectedItem != null)
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

        if (Input.GetMouseButtonDown(0) && IsPointerUnderInventory)
        {
            var tileGridPosition = GetTileGridPosition();

            if (selectedItem == null)
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

    public void PickUpItem(BaseItem item)
    {
        selectedItem = item;
        ChangeRectTransform();
    }

    private void ChangeRectTransform()
    {
        if (selectedItem == null) return;
        rectTransform = selectedItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
    }
    
    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        ChangeRectTransform();
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        var complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y,
            ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void ThrowItemAtLocation() // Перемещение предмета в инвентарь локации при нажатии на пустое пространство
    {
        var locationItemGrid = GameObject.FindGameObjectWithTag("LocationItemGrid").GetComponent<ItemGrid>();
        var positionOnGrid = locationItemGrid.FindSpaceForObject(selectedItem);
        locationItemGrid.PlaceItem(selectedItem, positionOnGrid.Value.x, positionOnGrid.Value.y);
        selectedItem = null;
    }

    private void RotateItem()
    {
        if (selectedItem == null) return;
        selectedItem.Rotated();
    }

    private void HandleHighlight()
    {
        var positionOnGrid = GetTileGridPosition();
        if (previousPosition == positionOnGrid) return;
        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y,
                selectedItem.Width, selectedItem.Height));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        var mousePosition = Input.mousePosition;
        if (selectedItem != null)
        {
            mousePosition.x -= (selectedItem.Width - 1) * ItemGrid.TileSize / 2;
            mousePosition.y += (selectedItem.Height - 1) * ItemGrid.TileSize / 2;
        }

        return selectedItemGrid.GetTileGridPosition(mousePosition);
    }

    public void AddItemToInventory(BaseItem item)
    {
        if (selectedItem != null) return;
        CreateItem(item);
        var itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
        selectedItemGrid = null;
    }

    private void InsertItem(BaseItem itemToInsert)
    {
        var positionOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        if (positionOnGrid == null)
        {
            Destroy(itemToInsert);
            return;
        }
        selectedItemGrid.PlaceItem(itemToInsert, positionOnGrid.Value.x, positionOnGrid.Value.y);
    }

    private void CreateItem(BaseItem item)
    {
        if (selectedItem != null) return;
        var inventoryItem = Instantiate(item);
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        inventoryItem.Set(inventoryItem.ItemData);
    }
}