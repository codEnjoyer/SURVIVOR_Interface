using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SpecialClothCellType
{
    Jacket,
    Backpack,
    Boots,
    Underwear,
    Vest,
    Hat,
    Pants
}
    
public enum SpecialGunCellType
{
    PrimaryGun,
    SecondaryGun,
    MeleeWeapon,
}
    
public enum GunModuleType
{
    Grip,
    Spring,
    Shutter,
    Scope,
    Suppressor,
    Tactical
}
public class SpecialClothCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private SpecialClothCellType cellType;
    
    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        var itemRectTransform = placedItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(GetComponent<RectTransform>());
        itemRectTransform.localPosition = new Vector2(0,0);
        ChangeItemSize(itemRectTransform,GetComponent<RectTransform>());
        InventoryController.SelectedItem = null;
        OnItemPlaced.Invoke();
    }

    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.onAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.onAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItem(PlacedItem);
        OnItemTaked.Invoke();
        PlaceNullItem();
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<Clothes>() &&
               InventoryController.SelectedItem.GetComponent<Clothes>().Data.SpecialClothCellType == cellType;
    }
}
