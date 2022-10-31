using System.Collections.Generic;

public interface IInventory
{
    public IEnumerable<Item> GetItems();
}