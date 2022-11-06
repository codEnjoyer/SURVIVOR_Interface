using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventoryState
{
    private Size size;
    private Item[,] itemSlots;
    private List<Item> storedItems;
    public Size Size => size;
    public IEnumerable<Item> GetItems => storedItems;

    public InventoryState(Size size)
    {
        this.size = size;
        itemSlots = new Item[size.Width, size.Height];
        storedItems = new List<Item>();
    }
    
    public Item PickUpItem(int x, int y)
    {
        var returnedItem = itemSlots[x, y];
        if (returnedItem == null) return null;
        RemoveGridReference(returnedItem);

        storedItems.Remove(returnedItem);

        itemSlots[x, y] = null;
        return returnedItem;
    }
    
    public bool PlaceItem(Item inventoryItem, int posX, int posY, ref Item overlapInventoryItem)
    {
        if (!BoundryCheck(posX, posY, inventoryItem.Width, inventoryItem.Height))
            return false;

        if (!OverlapCheck(posX, posY, inventoryItem.Width, inventoryItem.Height, ref overlapInventoryItem))
        {
            overlapInventoryItem = null;
            return false;
        }

        if (overlapInventoryItem != null)
        {
            RemoveGridReference(overlapInventoryItem);
        }

        PlaceItem(inventoryItem, posX, posY);
        return true;
    }
    
    public void PlaceItem(Item inventoryItem, int posX, int posY)
    {
        for (int x = 0; x < inventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryItem.Height; y++)
            {
                itemSlots[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        storedItems.Add(inventoryItem);
    }

    private void RemoveGridReference(Item inventoryItem)
    {
        for (int ix = 0; ix < inventoryItem.Width; ix++)
        {
            for (int iy = 0; iy < inventoryItem.Height; iy++)
            {
                itemSlots[inventoryItem.onGridPositionX + ix, inventoryItem.onGridPositionY + iy] = null;
            }
        }
    }
    
    bool PositionCheck(int posX, int posY) => posX >= 0 && posY >= 0 && posX < size.Width && posY < size.Height;
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => PositionCheck(posX, posY) && PositionCheck(posX + width - 1, posY + height - 1);
    
    private bool OverlapCheck(int posX, int posY, int width, int height, ref Item overlapInventoryItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (itemSlots[posX + x, posY + y] != null)
                {
                    if (overlapInventoryItem == null)
                        overlapInventoryItem = itemSlots[posX + x, posY + y];
                    else
                    {
                        if (overlapInventoryItem != itemSlots[posX + x, posY + y])
                            return false;
                    }
                }
            }
        }

        return true;
    }
    
    public Item GetItem(int x, int y) => itemSlots[x, y];
    
    public Vector2Int? FindSpaceForObject(Item inventoryItemToInsert)
    {
        var height = size.Height - inventoryItemToInsert.Height + 1;
        var width = size.Width - inventoryItemToInsert.Width + 1;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, inventoryItemToInsert.Width, inventoryItemToInsert.Height))
                    return new Vector2Int(x, y);
            }
        }

        return null;
    }
    
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            if (itemSlots[posX + x, posY + y] != null)
                return false;

        return true;
    }
}