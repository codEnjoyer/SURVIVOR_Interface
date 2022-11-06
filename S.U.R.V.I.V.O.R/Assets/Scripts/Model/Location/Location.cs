using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Location: MonoBehaviour
{
    public static readonly Random rnd = new();
    [SerializeField] private LocationData data; 
    private List<InventoryItem> chancesList = new ();

    public void Awake()
    {
        if (data == null)
        {
            Debug.Log("У ноды нет локации!");
            return;
        }
        foreach (var itemChance in data.chancesList)
             for (var i = 0; i < itemChance.WeightChance; i++)
                 chancesList.Add(itemChance.Item);
    }

    public InventoryItem GetLoot() => chancesList[rnd.Next(chancesList.Count)];

}