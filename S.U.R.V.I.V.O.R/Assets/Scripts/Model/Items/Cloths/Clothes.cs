using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Clothes : MonoBehaviour
{
    [SerializeField] private ClothData data;
    private BaseItem baseItem;
    [SerializeField]
    private InventoryState inventory;
    public float CurrentArmor { get; private set; }

    public InventoryState Inventory => inventory;

    public float TotalWeight => baseItem.Weight + inventory.GetItems.Sum(item => item.Weight);

    public float CalculateBlockedDamage(DamageInfo damageInfo)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }

    private void Awake()
    {
        inventory = new InventoryState(data.InventorySize);
        baseItem = gameObject.GetComponent<BaseItem>();
    }
}