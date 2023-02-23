using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;

public class SpecialGunCell : SpecialCell
{
    // [SerializeField] private Transform canvasTransform;
    //
    // [SerializeField] private GunType type;
    // [SerializeField]
    // private GunMagazineSpecialCell magazineSlot;
    // [SerializeField]
    // private SpecialGunModuleCell springSlot;
    // [SerializeField]
    // private SpecialGunModuleCell shutterSlot;
    // [SerializeField]
    // private SpecialGunModuleCell scopeSlot;
    // [SerializeField]
    // private SpecialGunModuleCell gripSlot;
    // [SerializeField]
    // private SpecialGunModuleCell tacticalSlot;
    // [SerializeField]
    // private SpecialGunModuleCell supressorSlot;
    //
    // private List<SpecialGunModuleCell> allSlots;
    //
    // private Character currentCharacter;
    //
    // private Gun currentGun;
    //
    // public Gun CurrentGun
    // {
    //     get => currentGun;
    //
    //     private set
    //     {
    //         //UnsubscribeGunEvents();
    //         currentGun = value;
    //         //OnGunChanged();
    //         //SubscribeGunEvents();
    //     }
    // }
    //
    // public Character CurrentCharacter
    // {
    //     get => currentCharacter;
    //     set
    //     {
    //         currentCharacter = value;
    //         CurrentGun = GetGun(type);
    //         Init();
    //     }
    // }
    //
    // public override void Init()
    // {
    //     base.Init();
    //     magazineSlot.Init();
    // }
    //
    // protected override void PlaceItem(BaseItem item)
    // {
    //     if (item.IsRotated)
    //         item.Rotate();
    //     placedItem = item;
    //     CurrentGun = placedItem.GetComponent<Gun>();
    //     ChangeCharacterGuns();
    //     InventoryController.SelectedItem = null;
    // }
    //
    // protected override void GiveItem()
    // {
    //     if (placedItem == null) return;
    //     placedItem.gameObject.SetActive(true);
    //
    //     var gun = GetGun(type);
    //     if (gun is null) return;
    //     
    //     PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
    //     PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
    //     PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
    //     
    //     InventoryController.PickUpItemFromSpecialCell(PlacedItem);
    //     PlaceNullItem();
    //     ChangeCharacterGuns();
    // }
    //
    // protected override void PlaceNullItem()
    // {
    //     base.PlaceNullItem();
    //     CurrentGun = null;
    // }
    //
    // protected override bool CanInsertIntoSlot()
    // {
    //     var x = InventoryController.SelectedItem.GetComponent<Gun>() != null &&
    //             InventoryController.SelectedItem.GetComponent<Gun>().Data.GunType == type;
    //     return x;
    // }
    //
    // public override void CheckNewItem(BaseItem item)
    // {
    //     base.CheckNewItem(item);
    //     var mag = CurrentGun && CurrentGun.CurrentMagazine ? CurrentGun.CurrentMagazine.GetComponent<BaseItem>() : null;
    //     magazineSlot.CheckNewItem(mag);
    // }
    //
    // public void UpdateGunItem(Gun gun)
    // {
    //     UpdateItem(gun && gun.GetComponent<BaseItem>() ? gun.GetComponent<BaseItem>() : null);
    //     CurrentGun = gun;
    //     magazineSlot.UpdateItem(CurrentGun && CurrentGun.CurrentMagazine ? CurrentGun.CurrentMagazine.GetComponent<BaseItem>() : null);
    // }
    //
    // protected override void ReDraw()
    // {
    //     DrawItem();
    // }
    //
    //
    // private void ChangeCharacterGuns()
    // {
    //     switch (type)
    //     {
    //         case GunType.PrimaryGun:
    //             CurrentCharacter.PrimaryGun = CurrentGun;
    //             break;
    //         case GunType.SecondaryGun:
    //             CurrentCharacter.SecondaryGun = CurrentGun;
    //             break;
    //     }  
    // }
    //
    // private Gun GetGun(GunType type)
    // {
    //     var x = type == GunType.PrimaryGun ? CurrentCharacter.PrimaryGun : CurrentCharacter.SecondaryGun;
    //     return x;
    // }
    //
    // // private void OnModulesChanged(GunModuleType moduleType)
    // // {
    // //     switch (moduleType)
    // //     {
    // //         case GunModuleType.Magazine:
    // //             magazineSlot.CheckNewItem(CurrentGun && CurrentGun.CurrentMagazine ? CurrentGun.CurrentMagazine.GetComponent<BaseItem>() : null);
    // //             break;
    // //     }
    // // }
    // //
    // // private void OnGunChanged()
    // // {
    // //     OnModulesChanged(GunModuleType.Magazine);
    // // }
    //
    // // private void UnsubscribeGunEvents()
    // // {
    // //     if (CurrentGun != null)
    // //         CurrentGun.OnModulesChanged -= OnModulesChanged;
    // // }
    // //
    // // private void SubscribeGunEvents()
    // // {
    // //     if (CurrentGun != null)
    // //         CurrentGun.OnModulesChanged += OnModulesChanged;
    // // }
    //
    // // private void OnEnable()
    // // {
    // //     SubscribeGunEvents();
    // // }
    // //
    // // private void OnDisable()
    // // {
    // //     UnsubscribeGunEvents();
    // // }

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
                return CurrentCharacter.SecondaryGun = CurrentGun;
        }

        return null;
    }

}
