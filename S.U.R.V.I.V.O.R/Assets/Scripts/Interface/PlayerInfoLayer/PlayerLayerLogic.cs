using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLayerLogic : MonoBehaviour
{
    private Character currentCharacter;
    
    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            var x = value;
            UnsubscribeCharacterEvents();
            currentCharacter = value;
            SubscribeCharacterEvents();
            Init(currentCharacter);
        }
    }

    [SerializeField] private BodyIndicator bodyIndicator;
    [SerializeField]
    private PlayerCharacteristicsPanel playerCharacteristicsPanel;
    [SerializeField]
    private Size emptyInventorySize;
    [SerializeField]
    private SpecialGunCell primaryGunSlot;
    [SerializeField]
    private SpecialGunCell secondaryGunSlot;
    //[SerializeField]
    //private SpecialGunCell meleeWeaponSet;
    
    
    [SerializeField] private Text nameTextBox;
    
    [SerializeField] private SpecialClothCell hatCell;
    [SerializeField] private SpecialClothCell underwearCell;
    [SerializeField] private SpecialClothCell backpackCell;
    [SerializeField] private SpecialClothCell vestCell;
    [SerializeField] private SpecialClothCell jacketCell;
    [SerializeField] private SpecialClothCell pantsCell;
    [SerializeField] private SpecialClothCell bootsCell;

    public void Init(Character characterToInit)
    {
        currentCharacter = characterToInit;
        
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
            if (cell!=null)
            {
                cell.CurrentCharacter = CurrentCharacter;
            }
        }

        if(bodyIndicator != null)
            bodyIndicator.Character = CurrentCharacter;
        if (playerCharacteristicsPanel != null)
            playerCharacteristicsPanel.Player = CurrentCharacter;
        if (primaryGunSlot!=null)
            primaryGunSlot.CurrentCharacter = CurrentCharacter;
        if (secondaryGunSlot!=null)
            secondaryGunSlot.CurrentCharacter = CurrentCharacter;
        if (nameTextBox != null)
            nameTextBox.text = $"{CurrentCharacter.FirstName} {CurrentCharacter.Surname}";
        
        PlaceAllItems();
    }

    private void SubscribeCharacterEvents()
    {
        if (CurrentCharacter == null) return;
        CurrentCharacter.ManBody.WearChanged += OnWearChanged;
        CurrentCharacter.OnGunsChanged += OnGunsChanged;
    }

    private void UnsubscribeCharacterEvents()
    {        
        if (CurrentCharacter == null) return;
        CurrentCharacter.ManBody.WearChanged -= OnWearChanged;
        CurrentCharacter.OnGunsChanged -= OnGunsChanged;
    }

    private void OnGunsChanged(GunType type)
    {
        switch (type)
        {
            case GunType.PrimaryGun:
                primaryGunSlot.CheckNewItem(currentCharacter.PrimaryGun?.GetComponent<BaseItem>());
                break;
            case GunType.SecondaryGun:
                secondaryGunSlot.CheckNewItem(currentCharacter.SecondaryGun?.GetComponent<BaseItem>());
                break;
        }
    }
    
    private void OnWearChanged(ClothType type)
    {
        switch (type)
        {
            case ClothType.Jacket:
                jacketCell.CheckNewItem(currentCharacter.ManBody.Chest.Jacket?.GetComponent<BaseItem>());
                break;
            case ClothType.Backpack:
                backpackCell.CheckNewItem(currentCharacter.ManBody.Chest.Backpack?.GetComponent<BaseItem>());
                break;
            case ClothType.Pants:
                pantsCell.CheckNewItem(currentCharacter.ManBody.LeftLeg.Pants?.GetComponent<BaseItem>());
                break;
            case ClothType.Vest:
                vestCell.CheckNewItem(currentCharacter.ManBody.Chest.Vest?.GetComponent<BaseItem>());
                break;
            case ClothType.Underwear:
                underwearCell.CheckNewItem(currentCharacter.ManBody.Chest.Underwear?.GetComponent<BaseItem>());
                break;
            case ClothType.Boots:
                bootsCell.CheckNewItem(currentCharacter.ManBody.LeftLeg.Boots?.GetComponent<BaseItem>());
                break;
            case ClothType.Hat:
                hatCell.CheckNewItem(currentCharacter.ManBody.Head.Hat?.GetComponent<BaseItem>());
                break;
        }
    }
    private void PlaceAllItems()
    {
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.Chest.Vest != null ? CurrentCharacter.ManBody.Chest.Vest.GetComponent<BaseItem>() : null,vestCell );
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.Chest.Backpack != null ? CurrentCharacter.ManBody.Chest.Backpack.GetComponent<BaseItem>() : null, backpackCell);
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.LeftLeg.Pants != null ? CurrentCharacter.ManBody.LeftLeg.Pants.GetComponent<BaseItem>() : null, pantsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.Head.Hat != null ? CurrentCharacter.ManBody.Head.Hat.GetComponent<BaseItem>() : null, hatCell);
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.Chest.Underwear != null ? CurrentCharacter.ManBody.Chest.Underwear.GetComponent<BaseItem>() : null, underwearCell);
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.LeftLeg.Boots != null ? CurrentCharacter.ManBody.LeftLeg.Boots.GetComponent<BaseItem>() : null, bootsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.ManBody.Chest.Jacket != null ? CurrentCharacter.ManBody.Chest.Jacket.GetComponent<BaseItem>() : null, jacketCell);
        if (primaryGunSlot != null)
            CheckCellAfterWindowOpen(CurrentCharacter.PrimaryGun != null ? CurrentCharacter.PrimaryGun.GetComponent<BaseItem>() : null,primaryGunSlot);
        if (secondaryGunSlot != null)
            CheckCellAfterWindowOpen(CurrentCharacter.SecondaryGun != null ? CurrentCharacter.SecondaryGun.GetComponent<BaseItem>() : null,secondaryGunSlot);
    }

    private void CheckCellAfterWindowOpen(BaseItem item, SpecialCell cell)
    {
        if (cell != null)
            cell.UpdateItem(item);
    }
    
    public void OnDisable()
    {
        UnsubscribeCharacterEvents();
    }

    public void OnEnable()
    {
        SubscribeCharacterEvents();
        if (currentCharacter != null)
            PlaceAllItems();
    }
}
