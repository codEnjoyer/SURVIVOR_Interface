using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public abstract class Location 
{
    Random rnd = new ();
    public Item GetLoot(List<Tuple<Item,int>> list)
    {
        var chancesList = new List<Item>();
        foreach (var tuple in list)
        {
            var item = tuple.Item1;
            var weight = tuple.Item2;
            for (var i = 0; i < weight; i++)
            {
                chancesList.Add(item);
            }
        }
        var totalListWeight = chancesList.Count;
        var resultItem = rnd.Next(totalListWeight);
        return chancesList[resultItem];
    }
}