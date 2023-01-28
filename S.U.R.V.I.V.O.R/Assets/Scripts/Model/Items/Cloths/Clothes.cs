using System;
using System.Linq;
using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Wearable))]
public class Clothes : MonoBehaviour, ISaved<ClothSave>
{
    [SerializeField] private ClothData data;
    [SerializeField] private InventoryState inventory;

    private BaseItem baseItem;
    public float CurrentArmor { get; private set; }

    public InventoryState Inventory => inventory;
    public ClothData Data => data;
    public float TotalWeight => baseItem.Data.Weight + inventory.GetItems().Sum(item => item.Data.Weight);

    public float CalculateBlockedDamage(DamageInfo damageInfo)
    {
        //TODO расчитать колличество заблокированного урона
        throw new NotImplementedException();
    }

    private void Start()
    {
        CurrentArmor = data.MaxArmor;
        inventory = new InventoryState(data.InventorySize);
        baseItem = gameObject.GetComponent<BaseItem>();
    }
    
    public ClothSave CreateSave()
    {
        var itemSave = GetComponent<BaseItem>().CreateSave();
        return itemSave.componentSaves.OfType<ClothSave>().First();
    }

    private ClothSave HiddenCreateSave()
    {
        return new ClothSave()
        {
            currentArmor = CurrentArmor,
            inventory = inventory?.GetItems().Select(x => x.CreateSave()).ToArray()
        };
    }
}

[DataContract]
public class ClothSave: ComponentSave
{
    [DataMember] public float currentArmor;
    [DataMember] public ItemSave[] inventory;
}