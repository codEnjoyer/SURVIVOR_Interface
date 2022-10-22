[System.Serializable]
public class ItemChance
{
    public Item item;
    public int weigthChance;

    public ItemChance(Item item, int weigthChance)
    {
        this.item = item;
        this.weigthChance = weigthChance;
    }
}