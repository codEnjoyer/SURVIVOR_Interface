using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryState
{
    private Size size;
    private Item[,] inventoryItemSlot;
    private List<Item> storedItems;

    public Size Size => size;
    public IEnumerable<Item> GetItems => storedItems;

    public InventoryState(Size size)
    {
        this.size = size;
        inventoryItemSlot = new Item[size.Width, size.Height];
        storedItems = new List<Item>();
    }
    
    public Item PickUpItem(int x, int y)
    {
        var returnedItem = inventoryItemSlot[x, y];
        if (returnedItem == null) return null;
        RemoveGridReference(returnedItem);

        storedItems.Remove(returnedItem);

        inventoryItemSlot[x, y] = null;
        return returnedItem;
    }
    
    public bool PlaceItem(Item item, int posX, int posY, ref Item overlapItem)
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
        }

        PlaceItem(item, posX, posY);
        return true;
    }
    
    public void PlaceItem(Item item, int posX, int posY)
    {
        for (int x = 0; x < item.Width; x++)
        {
            for (int y = 0; y < item.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = item;
            }
        }

        item.onGridPositionX = posX;
        item.onGridPositionY = posY;

        storedItems.Add(item);
    }

    private void RemoveGridReference(Item item)
    {
        for (int ix = 0; ix < item.Width; ix++)
        {
            for (int iy = 0; iy < item.Height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }
    
    bool PositionCheck(int posX, int posY) => posX >= 0 && posY >= 0 && posX < size.Width && posY < size.Height;
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => PositionCheck(posX, posY) && PositionCheck(posX + width - 1, posY + height - 1);
    
    private bool OverlapCheck(int posX, int posY, int width, int height, ref Item overlapItem)
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
    
    public Item GetItem(int x, int y) => inventoryItemSlot[x, y];
    
    public Vector2Int? FindSpaceForObject(Item itemToInsert)
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
}