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

        InventoryController.SelectedItem = null;

        gunCell.PlacedItem.GetComponent<Gun>().Reload(PlacedItem.GetComponent<Magazine>());
    }

    public override void UpdateItem(BaseItem newItem)
    {
        throw new System.NotImplementedException();
    }

    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        
        PlaceNullItem();
        
        gunCell.PlacedItem.GetComponent<Gun>().Reload(null);
    }

    protected override bool CanInsertIntoSlot()
    {
        var magazine = InventoryController.SelectedItem.GetComponent<Magazine>();
        return  magazine && magazine.Data.Caliber == gunCell.PlacedItem.GetComponent<Gun>().Data.Caliber;
    }

    public override void ReDraw()
    {
        throw new System.NotImplementedException();
    }
}
