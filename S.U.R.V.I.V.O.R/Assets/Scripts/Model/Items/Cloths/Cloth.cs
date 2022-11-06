using System;
using System.Linq;
using UnityEngine;


public class Cloth : InventoryItem
{
    [SerializeField] private ClothData data;
    private InventoryState inventory;
    public float CurrentArmor { get; private set; }
    public float TotalWeight => Weight + inventory.GetItems.Sum(item => item.Weight);

    public float CalculateBlockedDamage(Shoot shoot)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }

    private void Awake()
    {
        inventory = new InventoryState(data.InventorySize);
    }
}