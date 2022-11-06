using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventoryState
{
    private Size size;
    private InventoryItem[,] itemSlots;
    private List<InventoryItem> storedItems;
    public Size Size => size;
    public IEnumerable<InventoryItem> GetItems => storedItems;
    
    public InventoryState(Size size)
    {
        this.size = size;
        itemSlots = new InventoryItem[size.Width, size.Height];
        storedItems = new List<InventoryItem>();
    }
    
    public InventoryItem PickUpItem(int x, int y)
    {
        var returnedItem = itemSlots[x, y];
        if (returnedItem == null) return null;
        RemoveGridReference(returnedItem);

        storedItems.Remove(returnedItem);

        itemSlots[x, y] = null;
        return returnedItem;
    }
    
    public bool PlaceItem(InventoryItem inventoryInventoryItem, int posX, int posY, ref InventoryItem overlapInventoryInventoryItem)
    {
        if (!BoundryCheck(posX, posY, inventoryInventoryItem.Width, inventoryInventoryItem.Height))
            return false;

        if (!OverlapCheck(posX, posY, inventoryInventoryItem.Width, inventoryInventoryItem.Height, ref overlapInventoryInventoryItem))
        {
            overlapInventoryInventoryItem = null;
            return false;
        }

        if (overlapInventoryInventoryItem != null)
        {
            RemoveGridReference(overlapInventoryInventoryItem);
        }

        PlaceItem(inventoryInventoryItem, posX, posY);
        return true;
    }
    
    public void PlaceItem(InventoryItem inventoryInventoryItem, int posX, int posY)
    {
        for (int x = 0; x < inventoryInventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryInventoryItem.Height; y++)
            {
                itemSlots[posX + x, posY + y] = inventoryInventoryItem;
            }
        }

        inventoryInventoryItem.onGridPositionX = posX;
        inventoryInventoryItem.onGridPositionY = posY;

        storedItems.Add(inventoryInventoryItem);
    }

    private void RemoveGridReference(InventoryItem inventoryInventoryItem)
    {
        for (int ix = 0; ix < inventoryInventoryItem.Width; ix++)
        {
            for (int iy = 0; iy < inventoryInventoryItem.Height; iy++)
            {
                itemSlots[inventoryInventoryItem.onGridPositionX + ix, inventoryInventoryItem.onGridPositionY + iy] = null;
            }
        }
    }
    
    bool PositionCheck(int posX, int posY) => posX >= 0 && posY >= 0 && posX < size.Width && posY < size.Height;
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => PositionCheck(posX, posY) && PositionCheck(posX + width - 1, posY + height - 1);
    
    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapInventoryInventoryItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (itemSlots[posX + x, posY + y] != null)
                {
                    if (overlapInventoryInventoryItem == null)
                        overlapInventoryInventoryItem = itemSlots[posX + x, posY + y];
                    else
                    {
                        if (overlapInventoryInventoryItem != itemSlots[posX + x, posY + y])
                            return false;
                    }
                }
            }
        }

        return true;
    }
    
    public InventoryItem GetItem(int x, int y) => itemSlots[x, y];
    
    public Vector2Int? FindSpaceForObject(InventoryItem inventoryInventoryItemToInsert)
    {
        var height = size.Height - inventoryInventoryItemToInsert.Height + 1;
        var width = size.Width - inventoryInventoryItemToInsert.Width + 1;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, inventoryInventoryItemToInsert.Width, inventoryInventoryItemToInsert.Height))
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