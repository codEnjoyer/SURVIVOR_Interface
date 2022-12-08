using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLayerLogic : MonoBehaviour
{
    public Character CurrentCharacter { get; set; }

    [SerializeField]
    private PlayerCharacteristicsPanel playerCharacteristicsPanel;
    [SerializeField]
    private Size emptyInventorySize;
    [SerializeField]
    private GunInterfaceSet primaryGunSet;
    [SerializeField]
    private GunInterfaceSet secondaryGunSet;
    [SerializeField]
    private GunInterfaceSet meleeWeaponSet;

    private bool wasOpened;
    
    [SerializeField] private Text nameTextBox;
    
    [SerializeField] private SpecialClothCell hatCell;
    [SerializeField] private SpecialClothCell underwearCell;
    [SerializeField] private SpecialClothCell backpackCell;
    [SerializeField] private SpecialClothCell vestCell;
    [SerializeField] private SpecialClothCell jacketCell;
    [SerializeField] private SpecialClothCell pantsCell;
    [SerializeField] private SpecialClothCell bootsCell;

    public void OnFirstOpen()
    {
        wasOpened = true;
        var allCells = new List<SpecialClothCell>
        {
            hatCell,
            underwearCell,
            backpackCell,
            vestCell,
            jacketCell,
            pantsCell,
            bootsCell
        };

        foreach (var cell in allCells)
        {
            cell.CurrentCharacter = CurrentCharacter;
        }


        playerCharacteristicsPanel.Player = CurrentCharacter;
        
        primaryGunSet.CurrentCharacter = CurrentCharacter;

        secondaryGunSet.CurrentCharacter = CurrentCharacter;

        //nameTextBox.text = CurrentCharacter.FirstName;
    }

    private void SubscribeCharacterEvents()
    {
        if (CurrentCharacter == null) return;
        CurrentCharacter.body.WearChanged += OnWearChanged;
    }

    private void UnsubscribeCharacterEvents()
    {        
        if (CurrentCharacter == null) return;
        CurrentCharacter.body.WearChanged -= OnWearChanged;
        // CurrentCharacter.body.OnJacketChanged -= OnJacketChanged;
        // CurrentCharacter.body.OnVestChanged -= OnVestChanged;
        // CurrentCharacter.body.OnBackpackChanged -= OnBackpackChanged;
        // CurrentCharacter.body.OnUnderwearChanged -= OnUnderwearChanged;
        // CurrentCharacter.body.OnPantsChanged -= OnPantsChanged;
        // CurrentCharacter.body.OnHatChanged -= OnHatChanged;
        // CurrentCharacter.body.OnBootsChanged -= OnBootsChanged;
    }
    private void OnWearChanged(ClothType type)
    {
        switch (type)
        {
            case ClothType.Jacket:
                jacketCell.CheckNewClothes();
                break;
            case ClothType.Backpack:
                backpackCell.CheckNewClothes();
                break;
            case ClothType.Pants:
                pantsCell.CheckNewClothes();
                break;
            case ClothType.Vest:
                vestCell.CheckNewClothes();
                break;
            case ClothType.Underwear:
                underwearCell.CheckNewClothes();
                break;
            case ClothType.Boots:
                bootsCell.CheckNewClothes();
                break;
            case ClothType.Hat:
                hatCell.CheckNewClothes();
                break;
        }
    }
    private void PlaceAllItems()
    {
        CheckCellAfterWindowOpen(CurrentCharacter.body.chest.Vest != null ? CurrentCharacter.body.chest.Vest.GetComponent<BaseItem>() : null,vestCell );
        CheckCellAfterWindowOpen(CurrentCharacter.body.chest.Backpack != null ? CurrentCharacter.body.chest.Backpack.GetComponent<BaseItem>() : null, backpackCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.stomach.Pants != null ? CurrentCharacter.body.stomach.Pants.GetComponent<BaseItem>() : null, pantsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.head.Hat != null ? CurrentCharacter.body.head.Hat.GetComponent<BaseItem>() : null, hatCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.chest.Underwear != null ? CurrentCharacter.body.chest.Underwear.GetComponent<BaseItem>() : null, underwearCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.leftLeg.Boots != null ? CurrentCharacter.body.leftLeg.Boots.GetComponent<BaseItem>() : null, bootsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.chest.Jacket != null ? CurrentCharacter.body.chest.Jacket.GetComponent<BaseItem>() : null, jacketCell);
        CheckCellAfterWindowOpen(CurrentCharacter.PrimaryGun != null ? CurrentCharacter.PrimaryGun.GetComponent<BaseItem>() : null,primaryGunSet.gunSlot);
        CheckCellAfterWindowOpen(CurrentCharacter.SecondaryGun != null ? CurrentCharacter.SecondaryGun.GetComponent<BaseItem>() : null,secondaryGunSet.gunSlot);
    }

    private void CheckCellAfterWindowOpen(BaseItem item, SpecialCell cell)
    {
        if (item == null)
            cell.PlaceNullItem();
        else
            cell.PlaceItem(item);
    }
    
    public void OnDisable()
    {
        UnsubscribeCharacterEvents();
    }

    public void OnEnable()
    {
        if (!wasOpened)
            OnFirstOpen();
        SubscribeCharacterEvents();
        PlaceAllItems();
    }
}
