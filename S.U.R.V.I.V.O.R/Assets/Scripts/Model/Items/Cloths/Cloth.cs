using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Cloth : Item
{
    [SerializeField] private int maxArmor;
    [SerializeField] private readonly IInventory inventory;
    public float CurrentArmor { get; private set; }
    public float TotalWeight => Weight + inventory.GetItems().Sum(item => item.Weight);

    public float CalculateBlockedDamage(Shoot shoot)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }
}