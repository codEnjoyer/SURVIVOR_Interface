using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "New LocationData", menuName = "Data/Location Data", order = 50)]
public class LocationData : ScriptableObject
{
    [Header("Отображение")] [SerializeField]
    private GameObject prefab;

    [SerializeField] private string locationName;

    [Header("Основные данные")] [SerializeField]
    private List<ItemChance> chancesList;

    [SerializeField] private List<FightChance> fightChancesList;
    [SerializeField] [Range(0, 1)] private float fightChance;

    private static readonly Random rnd = new();
    private BaseItem[] itemChances;
    private Fight[] fightChances;

    public int LengthOfMainArray { get; private set; }
    public IEnumerable<ItemChance> AllItemsChances => chancesList;
    public string LocationName => locationName;
    public GameObject Prefab => prefab;

    private void OnEnable()
    {
        itemChances = new BaseItem[chancesList.Sum(i => i.WeightChance)];
        LengthOfMainArray = itemChances.Length;
        var index = 0;
        foreach (var chance in chancesList)
            for (var i = 0; i < chance.WeightChance; i++)
            {
                itemChances[index] = chance.Item;
                index++;
            }


        fightChances = new Fight[fightChancesList.Sum(i => i.WeightChance)];
        index = 0;
        foreach (var chance in fightChancesList)
            for (var i = 0; i < chance.WeightChance; i++)
            {
                fightChances[index] = chance.Fight;
                index++;
            }
    }

    public BaseItem GetLoot() => itemChances[rnd.Next(itemChances.Length)];

    public bool CheckFight()
    {
        if (rnd.NextDouble() < fightChance)
        {
            
            var fight = fightChances[rnd.Next(fightChances.Length)];
            fight.Initialization();
            return true;
        }

        return false;
    }
}