using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(Salvagable))]
public class Scrap : MonoBehaviour
{
    [SerializeField] private List<ItemChance> SalvagableItems;

    private List<BaseItem> itemChances;
    public void Awake()
    {
        itemChances = new BaseItem[SalvagableItems.Sum(i => i.WeightChance)].ToList();
        var index = 0;
        foreach (var chance in SalvagableItems)
            for (var i = 0; i < chance.WeightChance; i++)
            {
                itemChances[index] = chance.Item;
                index++;
            }
    }

    public IEnumerable<BaseItem> salvagableItems => itemChances;
}
