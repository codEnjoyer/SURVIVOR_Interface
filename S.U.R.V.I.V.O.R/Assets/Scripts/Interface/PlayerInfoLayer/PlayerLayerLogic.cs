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
    [SerializeField] private int numberOfCharacter;
    
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
    
    
    [SerializeField] private Text nameTextBox;
    
    [SerializeField] private SpecialClothCell hatCell;
    [SerializeField] private SpecialClothCell underwearCell;
    [SerializeField] private SpecialClothCell backpackCell;
    [SerializeField] private SpecialClothCell vestCell;
    [SerializeField] private SpecialClothCell jacketCell;
    [SerializeField] private SpecialClothCell pantsCell;
    [SerializeField] private SpecialClothCell bootsCell;

    public void Awake()
    {
        var x = Game.Instance.ChosenGroup.CurrentGroupMembers.ToArray()[numberOfCharacter];
        CurrentCharacter = x;
        
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
        CheckCellAfterWindowOpen(CurrentCharacter.body.Chest.Vest != null ? CurrentCharacter.body.Chest.Vest.GetComponent<BaseItem>() : null,vestCell );
        CheckCellAfterWindowOpen(CurrentCharacter.body.Chest.Backpack != null ? CurrentCharacter.body.Chest.Backpack.GetComponent<BaseItem>() : null, backpackCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.Stomach.Pants != null ? CurrentCharacter.body.Stomach.Pants.GetComponent<BaseItem>() : null, pantsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.Head.Hat != null ? CurrentCharacter.body.Head.Hat.GetComponent<BaseItem>() : null, hatCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.Chest.Underwear != null ? CurrentCharacter.body.Chest.Underwear.GetComponent<BaseItem>() : null, underwearCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.LeftLeg.Boots != null ? CurrentCharacter.body.LeftLeg.Boots.GetComponent<BaseItem>() : null, bootsCell);
        CheckCellAfterWindowOpen(CurrentCharacter.body.Chest.Jacket != null ? CurrentCharacter.body.Chest.Jacket.GetComponent<BaseItem>() : null, jacketCell);
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
        SubscribeCharacterEvents();
        PlaceAllItems();
    }
}
