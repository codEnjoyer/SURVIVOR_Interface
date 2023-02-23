using System.Collections;
using System.Collections.Generic;
using Model.Items;
using UnityEngine;

public class GunMagazineSpecialCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    public SpecialGunCell gunCell;

    
    protected override void PlaceItem(BaseItem item)
    {
        if (item.IsRotated) item.Rotate();
        placedItem = item;
        InventoryController.SelectedItem = gunCell.CurrentGun.Reload(PlacedItem.GetComponent<Magazine>())?.GetComponent<BaseItem>();
    }

    protected override void GiveItem()
    {
        if (PlacedItem == null) return;

        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;

        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        
        PlaceNullItem();
        
        gunCell.PlacedItem.GetComponent<Gun>().Reload(null);
    }

    protected override bool CanInsertIntoSlot()
    {
        var magazine = InventoryController.SelectedItem.GetComponent<Magazine>();
        return  magazine && gunCell.PlacedItem && magazine.Data.Caliber == gunCell.PlacedItem.GetComponent<Gun>().Data.Caliber;
    }

    protected override void ReDraw()
    {
        DrawItem();
    }
}
