using System;
using System.Collections.Generic;
using System.Linq;
using Model.GameEntity;
using Model.GameEntity.Health;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ManBody : Body, IWearClothes
{
    private int energy;
    private int hunger;
    private int water;

    private int maxEnergy = 10;
    private int maxHunger = 10;
    private int maxWater = 10;

    private IWearClothes[] wearClothesBodyPart;

    public ManBody()
    {
        Head = new ManHead(this);
        Chest = new ManChest(this);
        Stomach = new ManStomach(this);
        LeftArm = new ManArm(this);
        RightArm = new ManArm(this);
        LeftLeg = new ManLeg(this);
        RightLeg = new ManLeg(this);

        bodyParts.AddRange(new List<BodyPart> {Head, Chest, Stomach, LeftArm, RightArm, LeftLeg, RightLeg});
        Energy = maxEnergy;
        Hunger = maxHunger;
        Water = maxWater;
        MaxCriticalLoses = bodyParts.Count;
        
        wearClothesBodyPart = bodyParts.OfType<IWearClothes>().ToArray();
    }

    public event Action<Health> PlayerTired;
    public event Action<Health> PlayerHungry;
    public event Action<Health> PlayerThirsty;

    public event Action<int> EnergyChange;
    public event Action<int> HungerChange;
    public event Action<int> WaterChange;
    public event Action<ClothType> WearChanged;

    public ManHead Head { get; }
    public ManChest Chest { get; }
    public ManStomach Stomach { get; }
    public ManArm LeftArm { get; }
    public ManArm RightArm { get; }
    public ManLeg LeftLeg { get; }
    public ManLeg RightLeg { get; }


    public int Energy
    {
        get => energy;
        set
        {
            if (value <= 0)
            {
                energy = 0;
                PlayerTired?.Invoke(Health);
            }
            else if (value > maxEnergy)
                energy = maxEnergy;
            else
                energy = value;

            EnergyChange?.Invoke(energy);
        }
    }

    public int Hunger
    {
        get => hunger;
        set
        {
            if (value <= 0)
            {
                hunger = 0;
                PlayerHungry?.Invoke(Health);
            }
            else if (value > maxHunger)
                hunger = maxHunger;
            else
                hunger = value;

            HungerChange?.Invoke(hunger);
        }
    }

    public int Water
    {
        get => water;
        set
        {
            if (value <= 0)
            {
                water = 0;
                PlayerThirsty?.Invoke(Health);
            }
            else if (value > maxWater)
                water = maxWater;
            else
                water = value;

            WaterChange?.Invoke(water);
        }
    }

    public Clothes GetClothByType(ClothType type)
    {
        switch (type)
        {
            case ClothType.Backpack:
                return Chest.Backpack;
            case ClothType.Boots:
                return RightLeg.Boots;
            case ClothType.Pants:
                return RightLeg.Pants;
            case ClothType.Hat:
                return Head.Hat;
            case ClothType.Jacket:
                return Chest.Jacket;
            case ClothType.Underwear:
                return Chest.Underwear;
            case ClothType.Vest:
                return Chest.Vest;
        }

        return default;
    }

    public bool PlaceItemToInventory(BaseItem itemToPlace)
    {
        var clothes = GetClothes();
        foreach (var cloth in clothes)
        {
            if (cloth != null && cloth.Inventory != null && cloth.Inventory.InsertItem(itemToPlace))    
            {
                return true;
            }
        }
        if (!LocationInventory.Instance.LocationInventoryGrid.InsertItem(itemToPlace)) Object.Destroy(itemToPlace);
        return false;
    }

    public bool Wear(Clothes clothesToWear)
    {
        var result = wearClothesBodyPart.Any(x => x.Wear(clothesToWear));
        WearChanged?.Invoke(clothesToWear.Data.ClothType);
        return result;
    }
    

    public Clothes UnWear(ClothType clothType)
    {
        WearChanged?.Invoke(clothType);
        foreach (var bodyPart in wearClothesBodyPart)
        {
            var clothes = bodyPart.UnWear(clothType);
            if (clothes is not null)
                return clothes;
        }

        return null;
    }

    public IEnumerable<Clothes> GetClothes()
    {
        var clothes = new List<Clothes>();
        foreach (var bodyPart in wearClothesBodyPart)
        {
            foreach (var cloth in bodyPart.GetClothes())
            {
                clothes.Add(cloth);
            }
        }

        return clothes.Distinct().Where(x => x is not null);
    }
}