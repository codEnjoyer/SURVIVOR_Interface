using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Cloth : Item
{
    public float CurrentArmor { get; private set; }
    private ClothData clothData => (ClothData) data;
    public float TotalWeight => data.weight + clothData.Inventory.storedItems.Sum(item => item.data.weight);

    public float CalculateBlockedDamage(Shoot shoot)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }
}