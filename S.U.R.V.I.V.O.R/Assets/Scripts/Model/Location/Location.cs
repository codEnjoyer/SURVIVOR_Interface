using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Location: MonoBehaviour
{
    private static readonly Random rnd = new();
    [SerializeField] private LocationData data;

    public LocationData Data => data;

    public void Awake()
    {
        if (data == null)
        {
            Debug.Log("У ноды нет локации!");
        }
    }

    public BaseItem GetLoot() => data.ChancesList[rnd.Next(data.ChancesList.Length)];

}