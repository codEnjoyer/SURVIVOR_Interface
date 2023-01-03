using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoPanel : MonoBehaviour
{
    [SerializeField] private Text locationName;

    [SerializeField] private LootAmountString gunString;
    [SerializeField] private LootAmountString ammoString;
    [SerializeField] private LootAmountString medicineString;
    [SerializeField] private LootAmountString clothesString;
    [SerializeField] private LootAmountString foodString;
    [SerializeField] private LootAmountString materialsString;
    private List<Color> colors;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.ChosenGroupChange += OnChosenGroupChange;
        Game.Instance.ChosenGroup.GroupMovementLogic.LocationChange += OnLocationChange;
        colors = new List<Color>()
        {
            new (201, 91, 65),
            new (211, 120, 20),
            new (207, 189, 17),
            new (161, 188, 55),
            new (108, 183, 56),
        };
        OnLocationChange(Game.Instance.ChosenGroup.GroupMovementLogic.CurrentNode.Location);
    }

    private void OnChosenGroupChange(Group oldGroup, Group newGroup)
    {
        oldGroup.GroupMovementLogic.LocationChange -= OnLocationChange;
        newGroup.GroupMovementLogic.LocationChange += OnLocationChange;
    }

    private void OnLocationChange(Location loc)
    {
        locationName.text = loc.Data.LocationName;
        gunString.Redraw(CalculateAmountOfLootableObjectsOfType<IWeapon>(loc),colors);
        ammoString.Redraw(CalculateAmountOfLootableObjectsOfType<AmmoBox>(loc),colors);
        medicineString.Redraw(CalculateAmountOfLootableObjectsOfType<Medicine>(loc),colors);
        clothesString.Redraw(CalculateAmountOfLootableObjectsOfType<Clothes>(loc),colors);
        materialsString.Redraw(CalculateAmountOfLootableObjectsOfType<Magazine>(loc),colors);
        foodString.Redraw(CalculateAmountOfLootableObjectsOfType<EatableFood>(loc) + CalculateAmountOfLootableObjectsOfType<CookableFood>(loc),colors);
    }

    private float CalculateAmountOfLootableObjectsOfType<T>(Location loc)
    {
        var lenOfMainArray = float.Parse(loc.Data.LengthOfMainArray.ToString());
        var acceptedChances = 0;
        var itemsChance = loc.Data.AllItemsChances;
        foreach (var pair in itemsChance)
        {
            if (pair.Item.gameObject.GetComponent<T>() != null)
            {
                acceptedChances += pair.WeightChance;
            }
        }
        return acceptedChances/lenOfMainArray;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
