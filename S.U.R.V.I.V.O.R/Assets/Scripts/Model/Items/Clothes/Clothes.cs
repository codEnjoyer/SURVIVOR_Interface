using System;
using System.Linq;
using UnityEngine;


public class Clothes: Item
{
    [SerializeField] private ClothesData clothesData;
    
    private InventoryState inventory;
    
    public float CurrentArmor { get; private set; }
    public float TotalWeight => InventoryItem.Weight + inventory.GetItems.Sum(i => i.Weight);

    public float CalculateBlockedDamage(Shoot shoot)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }

    private void Awake()
    {
        inventory = new InventoryState(clothesData.InventorySize);
    }
}