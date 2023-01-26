using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using UnityEngine;

public class SpecialGunCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GunType type;
    [SerializeField]
    private GunMagazineSpecialCell magazineSlot;
    [SerializeField]
    private SpecialGunModuleCell springSlot;
    [SerializeField]
    private SpecialGunModuleCell shutterSlot;
    [SerializeField]
    private SpecialGunModuleCell scopeSlot;
    [SerializeField]
    private SpecialGunModuleCell gripSlot;
    [SerializeField]
    private SpecialGunModuleCell tacticalSlot;
    [SerializeField]
    private SpecialGunModuleCell supressorSlot;

    private List<SpecialGunModuleCell> allSlots;
    
    private Character currentCharacter;

    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            currentCharacter = value;
            Init();
        }
    }

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
        if (placedItem == null) return;
        placedItem.gameObject.SetActive(true);
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        PlaceNullItem();
        ChangeCharacterGuns();
    }

    protected override bool CanInsertIntoSlot()
    {
        var x = InventoryController.SelectedItem.GetComponent<Gun>() != null &&
                InventoryController.SelectedItem.GetComponent<Gun>().Data.GunType == type;
        return x;
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
                CurrentCharacter.PrimaryGun = currentGun;
                break;
            case GunType.SecondaryGun:
                CurrentCharacter.SecondaryGun = currentGun;
                break;
        }  
    }
}
