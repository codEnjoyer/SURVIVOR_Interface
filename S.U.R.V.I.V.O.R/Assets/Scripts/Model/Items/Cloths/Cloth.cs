using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Cloth : Item
{
    [SerializeField] private int maxArmor;
    [SerializeField] private readonly Inventory inventory;
    public float CurrentArmor { get; private set; }
    public float TotalWeight => itemData.weight + inventory.items.Sum(item => item.itemData.weight);

    public float CalculateBlockedDamage(Shoot shoot)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }
}