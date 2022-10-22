using UnityEngine;


public class ClothData: ItemData
{
    [SerializeField] private int maxArmor;
    [SerializeField] private ItemGrid inventory;

    public int MaxArmor => maxArmor;
    public ItemGrid Inventory => inventory;
}