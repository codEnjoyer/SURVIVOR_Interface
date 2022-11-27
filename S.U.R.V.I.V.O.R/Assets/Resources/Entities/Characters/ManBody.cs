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

    public override ICollection<BodyPart> BodyParts { get; }
    protected override int CriticalLoses { get; }

    public ManBody()
    {
        head = new ManHead(this);
        chest = new ManChest(this);
        stomach = new ManStomach(this);
        leftArm = new ManArm(this);
        rightArm = new ManArm(this);
        leftLeg = new ManLeg(this);
        rightLeg = new ManLeg(this);

        BodyParts = new List<BodyPart> {head, chest, stomach, leftArm, rightArm, leftLeg, rightLeg};
        CriticalLoses = BodyParts.Count;
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

    public void Eat(Meal meal)
    {
        throw new NotImplementedException();
    }
}