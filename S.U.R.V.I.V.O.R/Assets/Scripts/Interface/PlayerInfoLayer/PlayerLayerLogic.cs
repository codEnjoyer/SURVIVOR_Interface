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
    private Character character;
    public Character CurrentCharacter
    {
        get => character;
        set => character = value;
    }
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
            cell.currentCharacter = CurrentCharacter;
        }


        playerCharacteristicsPanel.Player = CurrentCharacter;
        
        primaryGunSet.CurrentCharacter = CurrentCharacter;

        secondaryGunSet.CurrentCharacter = CurrentCharacter;

        //nameTextBox.text = CurrentCharacter.FirstName;
    }

    public void Start()
    {
        OnFirstOpen();
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
    
    private void OnHatChanged()
    {
        hatCell.DrawItem();
    }
    
    private void OnBootsChanged()
    {
        bootsCell.DrawItem();
    }

    private void OnWearChanged(ClothType type)
    {
        switch (type)
        {
            case ClothType.Jacket:
                jacketCell.ReDraw();
                break;
        }
    }

    private void OnUnderwearChanged()
    {
        underwearCell.DrawItem();
    }
    private void OnGunsChanged()
    {
        jacketCell.DrawItem();
    }
    
    /*
    private void OnPrimaryGunTaken()
    {
        CurrentCharacter.PrimaryGun = null;
    }
    
    private void OnPrimaryGunPlaced()
    {
        CurrentCharacter.PrimaryGun = primaryGunSet.CurrentInterfaceSetGun;
    }
    
    private void OnSecondaryGunTaken()
    {
        CurrentCharacter.SecondaryGun = null;
    }
    
    private void OnSecondaryGunPlaced()
    {
        CurrentCharacter.SecondaryGun = secondaryGunSet.CurrentInterfaceSetGun;
    }
    

    private void OnJacketTaken()
    {
        CurrentCharacter.body.chest.Jacket = null;
        jacketInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnVestPlaced()
    {
        CurrentCharacter.body.chest.Vest = vestCell.PlacedItem.GetComponent<Clothes>();
        vestInventory.ChangeState(vestCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnVestTaken()
    {
        CurrentCharacter.body.chest.Vest = null;
        vestInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnBackpackPlaced()
    {
        CurrentCharacter.body.chest.Backpack = backpackCell.PlacedItem.GetComponent<Clothes>();
        backpackInventory.ChangeState(backpackCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnBackpackTaken()
    {
        CurrentCharacter.body.chest.Backpack = null;
        backpackInventory.ChangeState(new InventoryState(emptyInventorySize));
    }
    
    private void OnPantsPlaced()
    {
        CurrentCharacter.body.leftLeg.Pants = pantsCell.PlacedItem.GetComponent<Clothes>();
        CurrentCharacter.body.rightLeg.Pants = pantsCell.PlacedItem.GetComponent<Clothes>();
        pantsInventory.ChangeState(pantsCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnPantsTaken()
    {
        CurrentCharacter.body.leftLeg.Pants = null;
        CurrentCharacter.body.rightLeg.Pants = null;
        pantsInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnHatPlaced()
    {
        CurrentCharacter.body.head.Hat = hatCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnHatTaken()
    {
        CurrentCharacter.body.head.Hat = null;
    }

    private void OnBootsPlaced()
    {
        CurrentCharacter.body.leftLeg.Boots = bootsCell.PlacedItem.GetComponent<Clothes>();
        CurrentCharacter.body.rightLeg.Boots = bootsCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnBootsTaken()
    {
        CurrentCharacter.body.rightLeg.Boots = null;
        CurrentCharacter.body.leftLeg.Boots = null;
    }
    
    private void OnUnderwearPlaced()
    {
        CurrentCharacter.body.chest.Underwear = underwearCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnUnderwearTaken()
    {
        CurrentCharacter.body.chest.Underwear = null;
    }
    */

    private void PlaceAllItems()
    {
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Backpack, backpackCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.stomach.Pants, pantsCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.head.Hat, hatCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Underwear, underwearCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.leftLeg.Boots, bootsCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Jacket, jacketCell);
        
        CheckGunSetAfterWindowOpen(CurrentCharacter.PrimaryGun,primaryGunSet);
        CheckGunSetAfterWindowOpen(CurrentCharacter.SecondaryGun,secondaryGunSet);
    }

    private void CheckClothCellAfterWindowOpen(Clothes cloth, SpecialClothCell cell)
    {
        if (cloth == null) return;
        cell.PlaceItem(cloth.GetComponent<BaseItem>());
        cell.ReDraw();
    }

    private void CheckGunSetAfterWindowOpen(Gun gun, GunInterfaceSet interfaceSet)
    {
        if (gun == null) return;
        interfaceSet.gunSlot.PlaceItem(gun.GetComponent<BaseItem>());
        interfaceSet.gunSlot.DrawItem();
    }

    public void OnDisable()
    {
        UnsubscribeCharacterEvents();
    }

    public void OnEnable()
    {
        SubscribeCharacterEvents();
        if (!wasOpened)
            OnFirstOpen();
        PlaceAllItems();
    }
}
