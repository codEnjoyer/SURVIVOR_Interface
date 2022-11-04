using System.Collections.Generic;
using UnityEngine;

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
}