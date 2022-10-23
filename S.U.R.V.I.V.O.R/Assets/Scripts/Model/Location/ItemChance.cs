using UnityEngine;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private Item item;
    [SerializeField] private int weightChance = 1;

    public Item Item => item;
    public int WeightChance => weightChance;
}