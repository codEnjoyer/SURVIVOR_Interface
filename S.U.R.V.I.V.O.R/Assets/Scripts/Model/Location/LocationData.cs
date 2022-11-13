using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New LocationData", menuName = "Data/Location Data", order = 50)]
public class LocationData : ScriptableObject
{
    [SerializeField] private List<ItemChance> chancesList;
    [SerializeField] private string locationName;
    public BaseItem[] ChancesList { get; private set; }

    public string LocationName => locationName;

    private void OnEnable()
    {
        ChancesList = new BaseItem[chancesList.Sum(i => i.WeightChance)];
        var index = 0;
        foreach (var itemChance in chancesList)
            for (var i = 0; i < itemChance.WeightChance; i++)
            {
                ChancesList[index] = itemChance.Item;
                index++;
            }
    }
}