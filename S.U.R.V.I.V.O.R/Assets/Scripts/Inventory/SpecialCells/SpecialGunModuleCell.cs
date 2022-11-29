using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGunModuleCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GunModuleType type;
    public ICollection<GunModule> gunModules;
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
        
        gunModules.Add(placedItem.GetComponent<GunModule>());
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
        
        gunModules.Remove(placedItem.GetComponent<GunModule>());
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<GunModule>() &&
               InventoryController.SelectedItem.GetComponent<GunModule>().Data.ModuleType == type;
    }
}
