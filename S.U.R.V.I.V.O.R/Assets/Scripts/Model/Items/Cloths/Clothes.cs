using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Wearable))]
public class Clothes : MonoBehaviour
{
    [SerializeField] private ClothData data;
    [SerializeField] private InventoryState inventory;
    
    private BaseItem baseItem;
    public float CurrentArmor { get; private set; }

    public InventoryState Inventory => inventory;
    public ClothData Data => data;

    public float TotalWeight => baseItem.Weight + inventory.GetItems.Sum(item => item.Weight);

    public float CalculateBlockedDamage(DamageInfo damageInfo)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }

    private void Start()
    {
        inventory = new InventoryState(data.InventorySize);
        baseItem = gameObject.GetComponent<BaseItem>();
    }
}