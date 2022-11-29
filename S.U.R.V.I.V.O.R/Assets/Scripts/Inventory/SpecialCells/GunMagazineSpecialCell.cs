using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagazineSpecialCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    public SpecialGunCell gunCell;
    
    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated) item.Rotated();
        placedItem = item;
        
        var itemRectTransform = placedItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(GetComponent<RectTransform>());
        itemRectTransform.localPosition = new Vector2(0,0);
        
        ChangeItemSize(itemRectTransform,GetComponent<RectTransform>());
        
        InventoryController.SelectedItem = null;
        
        OnItemPlaced.Invoke();

        gunCell.PlacedItem.GetComponent<Gun>().Reload(PlacedItem.GetComponent<Magazine>());
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

        gunCell.PlacedItem.GetComponent<Gun>().Reload(null);
    }

    protected override bool CanInsertIntoSlot()
    {
        var magazine = InventoryController.SelectedItem.GetComponent<Magazine>();
        return  magazine && magazine.Data.Caliber == gunCell.PlacedItem.GetComponent<Gun>().Data.Caliber;
    }
}
