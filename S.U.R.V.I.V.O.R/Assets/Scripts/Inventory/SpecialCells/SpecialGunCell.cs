using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
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

    private Gun currentGun;

    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            UnsubscribeCharacterEvents();
            currentCharacter = value;
            currentGun = GetGun(type);
            SubscribeCharacterEvents();
            Init();
        }
    }

    public override void Init()
    {
        base.Init();
        magazineSlot.Init();
    }

    public override void CheckNewItem(BaseItem item)
    {
        base.CheckNewItem(item);
        var magazine = currentGun?.CurrentMagazine != null ? currentGun.CurrentMagazine.GetComponent<BaseItem>() : null;
        magazineSlot?.CheckNewItem(magazine);
    }

    public override void UpdateItem(BaseItem item)
    {
        base.UpdateItem(item);
        magazineSlot.UpdateItem(currentGun?.CurrentMagazine?.GetComponent<BaseItem>());
    }

    protected override void PlaceItem(BaseItem item)
    {
        if (item.IsRotated)
            item.Rotate();
        placedItem = item;
        ChangeCharacterGuns();
        InventoryController.SelectedItem = null;
    }

    protected override void GiveItem()
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

    protected override void ReDraw()
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

    private void OnGunChanged(GunType type)
    {
        UnsubscribeGunEvents();//TODO Redraw Yourself
        currentGun = GetGun(type);
        CheckNewItem(currentGun.GetComponent<BaseItem>());
        SubscribeGunEvents();
    }
    
    private void OnGunModulesChanged(GunModuleType moduleType)
    {
        currentGun = GetGun(type);//TODO Redraw Module
        switch (moduleType)
        {
            case GunModuleType.Magazine:
                magazineSlot.CheckNewItem(currentGun.CurrentMagazine?.GetComponent<BaseItem>());
                break;

        }
    }
    
    private void SubscribeCharacterEvents()
    {
        if (currentCharacter != null)
        {
            currentCharacter.OnGunsChanged += OnGunChanged;
            if (currentGun != null)
                currentGun.OnModulesChanged += OnGunModulesChanged;
        }
    }
    
    private void UnsubscribeCharacterEvents()
    {
        if (currentCharacter != null)
        {
            currentCharacter.OnGunsChanged -= OnGunChanged;
            if (currentGun != null)
                currentGun.OnModulesChanged -= OnGunModulesChanged;
        }
    }
    
    private void SubscribeGunEvents()
    {
        if (currentGun != null)
            currentGun.OnModulesChanged += OnGunModulesChanged;
    }
    
    private void UnsubscribeGunEvents()
    {
        if (currentGun != null)
            currentGun.OnModulesChanged -= OnGunModulesChanged;
    }

    private Gun GetGun(GunType type)
    {
        return type == GunType.PrimaryGun ? CurrentCharacter.PrimaryGun : CurrentCharacter.SecondaryGun;
    }
}
