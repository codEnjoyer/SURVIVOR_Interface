using UnityEngine;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private GameObject item;
    [SerializeField] private int weightChance = 1;

    public BaseItem Item => item.GetComponent<BaseItem>();
    public int WeightChance => weightChance;
}