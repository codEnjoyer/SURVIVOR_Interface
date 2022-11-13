using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private BaseItem item;
    [SerializeField] private int weightChance = 1;

    public BaseItem Item => item;
    public int WeightChance => weightChance;
}