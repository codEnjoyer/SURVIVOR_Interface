using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Wearable))]
public class Clothes : MonoBehaviour, ISaved<ClothesSave>
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
    
    public ClothesSave CreateSave()
    {
        var itemSave = GetComponent<BaseItem>().CreateSave();
        return itemSave.componentSaves.OfType<ClothesSave>().First();
    }

    public void Restore(ClothesSave save)
    {
        GetComponent<BaseItem>().Restore(save.itemSave);
    }

    private void HiddenRestore(ItemSave save)
    {
        var clothSave = save.componentSaves.OfType<ClothesSave>().First();
        CurrentArmor = clothSave.currentArmor;
        inventory = new InventoryState(data.InventorySize);
        foreach (var itemSave in clothSave.inventory)
        {
            var item = Instantiate(Resources.Load<BaseItem>(itemSave.resourcesPath));
            item.Restore(itemSave);
            inventory.PlaceItem(item, item.OnGridPositionX, item.OnGridPositionY);
            item.gameObject.SetActive(false);
        }
    }
    private ClothesSave HiddenCreateSave()
    {
        return new ClothesSave()
        {
            currentArmor = CurrentArmor,
            inventory = inventory?.GetItems().Select(x => x.CreateSave()).ToArray()
        };
    }
}

[DataContract]
public class ClothesSave: ComponentSave
{
    [DataMember] public float currentArmor;
    [DataMember] public ItemSave[] inventory;
}