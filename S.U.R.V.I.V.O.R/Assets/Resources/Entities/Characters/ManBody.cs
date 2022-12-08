using System;
using System.Collections.Generic;
using UnityEngine;

public class ManBody : Body
{
    public readonly ManHead head;
    public readonly ManChest chest;
    public readonly ManStomach stomach;
    public readonly ManArm leftArm;
    public readonly ManArm rightArm;
    public readonly ManLeg leftLeg;
    public readonly ManLeg rightLeg;
    

    public ManBody()
    {
        head = new ManHead(this);
        chest = new ManChest(this);
        stomach = new ManStomach(this);
        leftArm = new ManArm(this);
        rightArm = new ManArm(this);
        leftLeg = new ManLeg(this);
        rightLeg = new ManLeg(this);

        bodyParts.AddRange(new List<BodyPart> {head, chest, stomach, leftArm, rightArm, leftLeg, rightLeg});
        Energy = MaxEnergy;
        Hunger = MaxHunger;
        Water = MaxWater;
        
    }

    public const int MaxEnergy = 10;
    public const int MaxHunger = 10;
    public const int MaxWater = 10;
    private int energy;

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
            else if (value > MaxEnergy)
                energy = MaxEnergy;
            else
                energy = value;

            EnergyChange?.Invoke(energy);
        }
    }

    private int hunger;

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
            else if (value > MaxHunger)
                hunger = MaxHunger;
            else
                hunger = value;

            HungerChange?.Invoke(hunger);
        }
    }

    private int water;

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
            else if (value > MaxWater)
                water = MaxWater;
            else
                water = value;

            WaterChange?.Invoke(water);
        }
    }

    public event Action<Health> PlayerTired;
    public event Action<Health> PlayerHungry;
    public event Action<Health> PlayerThirsty;

    public event Action<int> EnergyChange;
    public event Action<int> HungerChange;
    public event Action<int> WaterChange;

    public event Action<ClothType> WearChanged;

    public void Eat(Meal meal)
    {
        throw new NotImplementedException();
    }

    public void Wear(Clothes clothToWear,bool shouldUnwear ,out bool isSuccessful)
    {
        var valueToWear = shouldUnwear ? null : clothToWear;
        if (clothToWear == null)
        {
            isSuccessful = false;
            return;
        }
        switch (clothToWear.Data.ClothType)
        {
            case ClothType.Backpack:
                chest.Backpack = valueToWear;
                break;
            case ClothType.Boots:
                leftLeg.Boots = valueToWear;
                rightLeg.Boots = valueToWear;
                break;
            case ClothType.Pants:
                leftLeg.Pants = valueToWear;
                rightLeg.Pants = valueToWear;
                break;
            case ClothType.Hat:
                head.Hat = valueToWear;
                break;
            case ClothType.Jacket:
                chest.Jacket = valueToWear;
                break;
            case ClothType.Underwear:
                chest.Underwear = valueToWear;
                break;
            case ClothType.Vest:
                chest.Vest = valueToWear;
                break;
        }
        isSuccessful = true;
        WearChanged?.Invoke(clothToWear.Data.ClothType);
    }

    public Clothes GetClothByType(ClothType type)
    {
        switch (type)
        {
            case ClothType.Backpack:
                return chest.Backpack;
            case ClothType.Boots:
                return rightLeg.Boots;
            case ClothType.Pants:
                return rightLeg.Pants;
            case ClothType.Hat:
                return head.Hat;
            case ClothType.Jacket:
                return chest.Jacket;
            case ClothType.Underwear:
                return chest.Underwear;
            case ClothType.Vest:
                return chest.Vest;
        }

        return default;
    }
}