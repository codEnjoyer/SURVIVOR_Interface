using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LocationData", menuName = "Data/Location Data", order = 50)]
public class LocationData: ScriptableObject
{
    [SerializeField] private List<ItemChance> chancesList;
    [SerializeField] private string locationName;
    public readonly List<BaseItem> ChancesList = new();

    public string LocationName => locationName;

    private void OnEnable()
    {
        foreach (var itemChance in chancesList)
            for (var i = 0; i < itemChance.WeightChance; i++)
                ChancesList.Add(itemChance.Item);
    }
}