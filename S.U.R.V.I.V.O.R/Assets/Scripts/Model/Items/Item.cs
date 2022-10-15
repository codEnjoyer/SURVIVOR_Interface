using System;

namespace Assets.Scripts.Model
{
    public class Item
    {
        public string Name { get; protected set; }
        public float Weight { get; protected set; }
        public Size InventorySize;
    }
}