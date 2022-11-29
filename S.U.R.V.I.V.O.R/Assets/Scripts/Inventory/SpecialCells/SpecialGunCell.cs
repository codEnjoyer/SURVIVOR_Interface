using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGunCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private SpecialGunCellType cellType;
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
        var x = InventoryController.SelectedItem.GetComponent<Gun>();
        return InventoryController.SelectedItem.GetComponent<Gun>() != null &&
               InventoryController.SelectedItem.GetComponent<Gun>().Data.GunCellType == cellType;
    }
}
