using UnityEngine.Serialization;

[System.Serializable]
public class ItemChance
{
    public Item item;
    public int chanceWeight;

    public ItemChance(Item item, int chanceWeight)
    {
        this.item = item;
        this.chanceWeight = chanceWeight;
    }
}