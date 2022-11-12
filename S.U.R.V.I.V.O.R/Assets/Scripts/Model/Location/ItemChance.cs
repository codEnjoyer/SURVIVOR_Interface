using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemChance
{
    [SerializeField] private GameObject item;
    [SerializeField] private int weightChance = 1;
    [SerializeField] private Sprite photo;

    public BaseItem Item => item.GetComponent<BaseItem>();
    public int WeightChance => weightChance;
}