using global::System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Inventory
    {
        public readonly Size InventorySize;
        public readonly List<Item> Items;

        public Inventory(Size size)
        {
            InventorySize = size;
            Items = new List<Item>();
        }
    }
}