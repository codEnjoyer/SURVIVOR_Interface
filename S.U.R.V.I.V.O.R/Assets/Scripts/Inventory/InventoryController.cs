using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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

    private BaseItem selectedItem;

    public BaseItem SelectedItem
    {
        get => selectedItem;
        set
        {
            if (selectedItem != null)//Вещь на курсоре есть
            {
                switch (value)
                {
                    case null://Кладем вещь с курсора в ячейку
                        selectedItem.GetComponent<Image>().raycastTarget = true;
                        break;
                    case not null://Меняем вещи местами
                        selectedItem.GetComponent<Image>().raycastTarget = true;
                        value.GetComponent<Image>().raycastTarget = false;
                        break;
                }
            }
            else
            {
                switch (value)//Вещи на курсоре нет
                {
                    case null://Ничего не происходит
                        break;
                    case not null://Берем вещь из ячейки
                        value.GetComponent<Image>().raycastTarget = false;
                        break;
                }
            }

            selectedItem = value;
        }
    }
    
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
        if (SelectedItem != null)
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

            if (SelectedItem == null)
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
        SelectedItem = item;
        ChangeRectTransform();
    }

    private void ChangeRectTransform()
    {
        if (SelectedItem == null) return;
        rectTransform = SelectedItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
    }
    
    private void PickUpItem(Vector2Int tileGridPosition)
    {
        SelectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        ChangeRectTransform();
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        var complete = selectedItemGrid.PlaceItem(SelectedItem, tileGridPosition.x, tileGridPosition.y,
            ref overlapItem);
        if (complete)
        {
            SelectedItem = null;
            if (overlapItem != null)
            {
                SelectedItem = overlapItem;
                overlapItem = null;
                rectTransform = SelectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void ThrowItemAtLocation() // Перемещение предмета в инвентарь локации при нажатии на пустое пространство
    {
        var locationItemGrid = GameObject.FindGameObjectWithTag("LocationItemGrid").GetComponent<ItemGrid>();
        var positionOnGrid = locationItemGrid.FindSpaceForObject(SelectedItem);
        locationItemGrid.PlaceItem(SelectedItem, positionOnGrid.Value.x, positionOnGrid.Value.y);
        SelectedItem = null;
    }

    private void RotateItem()
    {
        if (SelectedItem == null) return;
        SelectedItem.Rotated();
    }

    private void HandleHighlight()
    {
        var positionOnGrid = GetTileGridPosition();
        if (previousPosition == positionOnGrid) return;
        if (SelectedItem == null)
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
                SelectedItem.Width, SelectedItem.Height));
            inventoryHighlight.SetSize(SelectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, SelectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        var mousePosition = Input.mousePosition;
        if (SelectedItem != null)
        {
            mousePosition.x -= (SelectedItem.Width - 1) * ItemGrid.TileSize / 2;
            mousePosition.y += (SelectedItem.Height - 1) * ItemGrid.TileSize / 2;
        }

        return selectedItemGrid.GetTileGridPosition(mousePosition);
    }

    public void AddItemToInventory(BaseItem item)
    {
        if (SelectedItem != null) return;
        CreateItem(item);
        var itemToInsert = SelectedItem;
        SelectedItem = null;
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
        if (SelectedItem != null) return;
        var inventoryItem = Instantiate(item);
        SelectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        inventoryItem.Set(inventoryItem.ItemData);
    }
}