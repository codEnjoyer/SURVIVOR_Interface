using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Cloth : MonoBehaviour
{
    [SerializeField] private ClothData data;
    private BaseItem baseItem;
    private InventoryState inventory;
    public float CurrentArmor { get; private set; }
    public float TotalWeight => baseItem.Weight + inventory.GetItems.Sum(item => item.Weight);

    public float CalculateBlockedDamage(Shoot shoot)
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