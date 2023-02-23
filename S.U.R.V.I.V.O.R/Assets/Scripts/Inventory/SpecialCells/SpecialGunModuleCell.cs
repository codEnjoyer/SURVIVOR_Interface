using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGunModuleCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GunModuleType type;
    private Gun currentGun;

    public Gun CurrentGun
    {
        get => currentGun;
        set
        {
            currentGun = value;
        }
    }

    public override void PlaceItem(BaseItem item)
    {
        if (item.IsRotated)
            item.Rotate();
        placedItem = item;
        InventoryController.SelectedItem = null;
        CurrentGun.AddGunModule(placedItem.GetComponent<GunModule>());
    }

    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);

        PlaceNullItem();
        
        CurrentGun.RemoveGunModule(placedItem.GetComponent<GunModule>());
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<GunModule>() &&
               InventoryController.SelectedItem.GetComponent<GunModule>().Data.ModuleType == type;
    }

    public override void ReDraw()
    {
        throw new System.NotImplementedException();
    }
}
