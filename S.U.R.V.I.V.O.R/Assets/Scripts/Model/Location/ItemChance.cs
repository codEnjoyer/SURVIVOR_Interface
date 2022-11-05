using UnityEngine;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private InventoryItem inventoryItem;
    [SerializeField] private int weightChance = 1;

    public InventoryItem InventoryItem => inventoryItem;
    public int WeightChance => weightChance;
}