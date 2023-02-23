using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;

public class SpecialGunCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;

    [SerializeField] private GunType type;
    [SerializeField] private GunMagazineSpecialCell magazineSlot;
    [SerializeField] private SpecialGunModuleCell springSlot;
    [SerializeField] private SpecialGunModuleCell shutterSlot;
    [SerializeField] private SpecialGunModuleCell scopeSlot;
    [SerializeField] private SpecialGunModuleCell gripSlot;
    [SerializeField] private SpecialGunModuleCell tacticalSlot;
    [SerializeField] private SpecialGunModuleCell supressorSlot;

    private List<SpecialGunModuleCell> allSlots;

    private Character currentCharacter;

    private Gun currentGun;

    public Gun CurrentGun
    {
        get => currentGun;

        set => currentGun = value;
    }

    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            currentCharacter = value;
            CurrentGun = GetGun(type);
            Init();
        }
    }

    private bool wasOpened;

    public override void UpdateItem(BaseItem item)
    {
        if (placedItem == null)
        {
            if (item != null)
            {
                item.gameObject.SetActive(true);
                placedItem = item;
            }
        }
        else
        {
            if (item != placedItem)
            {
                placedItem.gameObject.SetActive(false);
                PlaceNullItem();
                if (item != null)
                {
                    placedItem = item;
                }
            }
        }
        ReDraw();
    }

    protected override void PlaceItem(BaseItem item)
    {
        if (item.IsRotated)
            item.Rotate();
        placedItem = item;
        item.ItemOwner = CurrentCharacter;
        CurrentGun = placedItem.GetComponent<Gun>();
        ChangeCharacterGuns();
        InventoryController.SelectedItem = null;
    }

    protected override void GiveItem()
    {
        if (placedItem == null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        PlaceNullItem();
    }

    protected override void PlaceNullItem()
    {
        base.PlaceNullItem();
        CurrentGun = null;
    }

    protected override void ReDraw()
    {
        DrawItem();
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<Gun>() &&
               InventoryController.SelectedItem.GetComponent<Gun>().Data.GunType == type;
    }

    private void ChangeCharacterGuns()
    {
        switch (type)
        {
            case GunType.PrimaryGun:
                CurrentCharacter.PrimaryGun = CurrentGun;
                break;
            case GunType.SecondaryGun:
                CurrentCharacter.SecondaryGun = CurrentGun;
                break;
        }
    }

    private Gun GetGun(GunType type)
    {
        switch (type)
        {
            case GunType.PrimaryGun:
                return CurrentCharacter.PrimaryGun;

            case GunType.SecondaryGun:
                return CurrentCharacter.SecondaryGun;
        }

        return null;
    }
}
