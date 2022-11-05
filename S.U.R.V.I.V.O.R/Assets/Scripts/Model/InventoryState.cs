using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventoryState
{
    private Size size;
    private InventoryItem[,] inventoryItemSlot;
    private List<(InventoryItem, LogicalItem)> storedItems;

    public Size Size => size;
    public IEnumerable<(InventoryItem, LogicalItem)> GetItems => storedItems;

    public InventoryState(Size size)
    {
        this.size = size;
        inventoryItemSlot = new InventoryItem[size.Width, size.Height];
        storedItems = new List<(InventoryItem, LogicalItem)>();
    }
    
    public InventoryItem PickUpItem(int x, int y)
    {
        var returnedItem = inventoryItemSlot[x, y];
        if (returnedItem == null) return null;
        RemoveGridReference(returnedItem);

        storedItems.Remove(storedItems.FirstOrDefault(pair => pair.Item1.Equals(returnedItem)));

        inventoryItemSlot[x, y] = null;
        return returnedItem;
    }
    
    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapInventoryItem)
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

        // PlaceItem(inventoryItem, posX, posY);
        return true;
    }
    
    public void PlaceItem((InventoryItem, LogicalItem) pair, int posX, int posY)
    {
        var inventoryItem = pair.Item1;
        for (int x = 0; x < inventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryItem.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        storedItems.Add(pair);
    }

    private void RemoveGridReference(InventoryItem inventoryItem)
    {
        for (int ix = 0; ix < inventoryItem.Width; ix++)
        {
            for (int iy = 0; iy < inventoryItem.Height; iy++)
            {
                inventoryItemSlot[inventoryItem.onGridPositionX + ix, inventoryItem.onGridPositionY + iy] = null;
            }
        }
    }
    
    bool PositionCheck(int posX, int posY) => posX >= 0 && posY >= 0 && posX < size.Width && posY < size.Height;
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => PositionCheck(posX, posY) && PositionCheck(posX + width - 1, posY + height - 1);
    
    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapInventoryItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapInventoryItem == null)
                        overlapInventoryItem = inventoryItemSlot[posX + x, posY + y];
                    else
                    {
                        if (overlapInventoryItem != inventoryItemSlot[posX + x, posY + y])
                            return false;
                    }
                }
            }
        }

        return true;
    }
    
    public InventoryItem GetItem(int x, int y) => inventoryItemSlot[x, y];
    
    public Vector2Int? FindSpaceForObject(InventoryItem inventoryItemToInsert)
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
            if (inventoryItemSlot[posX + x, posY + y] != null)
                return false;

        return true;
    }
}