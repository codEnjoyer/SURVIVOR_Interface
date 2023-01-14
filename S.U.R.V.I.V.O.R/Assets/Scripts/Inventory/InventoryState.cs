using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryState
{
    private readonly Size size;
    private readonly BaseItem[,] inventoryItemSlot;

    public Size Size => size;

    public IEnumerable<BaseItem> GetItems()
    {
        var array = new BaseItem[inventoryItemSlot.GetLength(0) * inventoryItemSlot.GetLength(1)];
        var i = 0;
        foreach (var item in inventoryItemSlot)
        {
            array[i] = item;
            i++;
        }

        return array.Distinct().Where(x => x is not null);
    }

    public event Action InventoryChange;
    public event Action<BaseItem> PlaceItemEvent;
    public event Action<BaseItem> PickUpItemEvent;

    public InventoryState(Size size)
    {
        if (size == null)
        {
            size = Resources.Load<Size>("InventorySizeObjects/0x0");
            Debug.Log("Ошибка, при создании InventoryState был передан null вместо Size");
        }

        this.size = size;
        inventoryItemSlot = new BaseItem[size.Width, size.Height];
    }

    public BaseItem PickUpItem(int x, int y)
    {
        var returnedItem = inventoryItemSlot[x, y];
        if (returnedItem == null) return null;
        RemoveGridReference(returnedItem);
        inventoryItemSlot[x, y] = null;
        PickUpItemEvent?.Invoke(returnedItem);
        InventoryChange?.Invoke();
        return returnedItem;
    }

    public bool PlaceItem(BaseItem item, int posX, int posY, ref BaseItem overlapItem)
    {
        if (!BoundryCheck(posX, posY, item.Width, item.Height))
            return false;

        if (!OverlapCheck(posX, posY, item.Width, item.Height, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            RemoveGridReference(overlapItem);
            PickUpItemEvent?.Invoke(overlapItem);
        }

        PlaceItem(item, posX, posY);
        return true;
    }

    public void PlaceItem(BaseItem item, int posX, int posY)
    {
        for (int x = 0; x < item.Width; x++)
        {
            for (int y = 0; y < item.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = item;
            }
        }

        item.OnGridPositionX = posX;
        item.OnGridPositionY = posY;
        PlaceItemEvent?.Invoke(item);
        InventoryChange?.Invoke();
    }


    private void RemoveGridReference(BaseItem item)
    {
        for (int ix = 0; ix < item.Width; ix++)
        {
            for (int iy = 0; iy < item.Height; iy++)
            {
                inventoryItemSlot[item.OnGridPositionX + ix, item.OnGridPositionY + iy] = null;
            }
        }
    }

    public bool PositionCheck(int posX, int posY) => posX >= 0 && posY >= 0 && posX < size.Width && posY < size.Height;

    public bool BoundryCheck(int posX, int posY, int width, int height) =>
        PositionCheck(posX, posY) && PositionCheck(posX + width - 1, posY + height - 1);

    private bool OverlapCheck(int posX, int posY, int width, int height, ref BaseItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    else
                    {
                        if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                            return false;
                    }
                }
            }
        }

        return true;
    }

    public BaseItem GetItem(int x, int y) => inventoryItemSlot[x, y];

    public bool InsertItem(BaseItem itemToInsert)
    {
        var positionOnGrid = FindSpaceForObject(itemToInsert);
        if (positionOnGrid == null)
        {
            return false;
        }

        itemToInsert.OnGridPositionX = positionOnGrid.Value.x;
        itemToInsert.OnGridPositionY = positionOnGrid.Value.y;
        PlaceItem(itemToInsert, positionOnGrid.Value.x, positionOnGrid.Value.y);
        return true;
    }

    public Vector2Int? FindSpaceForObject(BaseItem itemToInsert)
    {
        var height = size.Height - itemToInsert.Height + 1;
        var width = size.Width - itemToInsert.Width + 1;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height))
                    return new Vector2Int(x, y);
            }
        }

        return null;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            if (inventoryItemSlot[posX + x, posY + y] != null)
                return false;

        return true;
    }


    public void Clear()
    {
        for (var i = 0; i < inventoryItemSlot.GetLength(0); i++)
        for (int j = 0; j < inventoryItemSlot.GetLength(1); j++)
            inventoryItemSlot[i, j] = null;
    }
}