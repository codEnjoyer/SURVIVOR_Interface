using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Wearable))]
public class Clothes : MonoBehaviour, ISaved<ClothesData>
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
        if (inventory == null)
            inventory = new InventoryState(data.InventorySize);
        baseItem = gameObject.GetComponent<BaseItem>();
    }
    
    public ClothesData CreateData()
    {
        var itemSave = GetComponent<BaseItem>().CreateData();
        return itemSave.componentSaves.OfType<ClothesData>().First();
    }

    public void Restore(ClothesData data)
    {
        GetComponent<BaseItem>().Restore(data.itemData);
    }

    private void HiddenRestore(ItemData data)
    {
        var clothSave = data.componentSaves.OfType<ClothesData>().First();
        CurrentArmor = clothSave.currentArmor;
        inventory = new InventoryState(this.data.InventorySize);
        foreach (var itemSave in clothSave.inventory)
        {
            var item = Instantiate(Resources.Load<BaseItem>(itemSave.resourcesPath));
            item.Restore(itemSave);
            inventory.PlaceItem(item, item.OnGridPositionX, item.OnGridPositionY);
            item.gameObject.SetActive(false);
        }
    }
    private ClothesData HiddenCreateSave()
    {
        return new ClothesData()
        {
            currentArmor = CurrentArmor,
            inventory = inventory?.GetItems().Select(x => x.CreateData()).ToArray()
        };
    }
}

[DataContract]
public class ClothesData: ComponentSave
{
    [DataMember] public float currentArmor;
    [DataMember] public ItemData[] inventory;
}