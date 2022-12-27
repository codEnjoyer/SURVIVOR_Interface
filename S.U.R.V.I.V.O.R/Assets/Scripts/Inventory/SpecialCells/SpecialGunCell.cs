using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGunCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GunType type;
    public Character currentCharacter { get; set; }
    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        ChangeCharacterGuns();
        InventoryController.SelectedItem = null;
    }

    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        PlaceNullItem();
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<Gun>() != null &&
               InventoryController.SelectedItem.GetComponent<Gun>().Data.GunType == type;
    }

    public override void ReDraw()
    {
        DrawItem();
    }


    private void ChangeCharacterGuns()
    {
        var currentGun = placedItem?.GetComponent<Gun>();
        switch (type)
        {
            case GunType.PrimaryGun:
                currentCharacter.PrimaryGun = currentGun;
                break;
            case GunType.SecondaryGun:
                currentCharacter.SecondaryGun = currentGun;
                break;
        }  
    }
}
