using UnityEngine;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private int weightChance = 1;

    public InventoryItem Item => item;
    public int WeightChance => weightChance;
}