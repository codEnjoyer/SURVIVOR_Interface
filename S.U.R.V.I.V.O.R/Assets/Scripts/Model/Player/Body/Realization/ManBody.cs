using System;
using System.Collections.Generic;
using UnityEngine;

public class ManBody: Body
{
    public readonly ManHead head = new();
    public readonly ManChest chest = new();
    public readonly ManStomach stomach = new();
    public readonly ManArm leftArm = new();
    public readonly ManArm rightArm = new();
    public readonly ManLeg leftLeg = new();
    public readonly ManLeg rightLeg = new();

    public ManBody()
    {
        bodyParts = new List<BodyPart>{head, chest, stomach, leftArm, rightArm, leftLeg, rightLeg};
        Energy = MaxEnergy;
        Hunger = MaxHunger;
        Water = MaxWater;
    }

    public const int MaxEnergy = 10;
    public const int MaxHunger = 10;
    public const int MaxWater = 10;
    public float Endurance => throw new NotImplementedException();

    private int energy;
    public int Energy
    {
        get => energy;
        set
        {
            if (value <= 0)
            {
                energy = 0;
                PlayerTired?.Invoke();
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
                PlayerHungry?.Invoke();
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
                PlayerThirsty?.Invoke();
            }
            else if (value > MaxWater)
                water = MaxWater;
            else
                water = value;
            WaterChange?.Invoke(water);
        }
    }

    public event Action PlayerTired;
    public event Action PlayerHungry;
    public event Action PlayerThirsty;
    
    public event Action<int> EnergyChange;
    public event Action<int> HungerChange;
    public event Action<int> WaterChange;
}