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

    [SerializeField] private Text nameTextBox;
    
    [SerializeField] private SpecialCell hatCell;
    [SerializeField] private SpecialCell underwearCell;
    [SerializeField] private SpecialCell backpackCell;
    [SerializeField] private SpecialCell vestCell;
    [SerializeField] private SpecialCell jacketCell;
    [SerializeField] private SpecialCell pantsCell;
    [SerializeField] private SpecialCell bootsCell;

    [SerializeField] private ItemGrid backpackInventory;
    [SerializeField] private ItemGrid vestInventory;
    [SerializeField] private ItemGrid jacketInventory;
    [SerializeField] private ItemGrid pantsInventory;
    
    public void Start()
    {
        backpackInventory.ChangeState(new InventoryState(emptyInventorySize));
        vestInventory.ChangeState(new InventoryState(emptyInventorySize));
        jacketInventory.ChangeState(new InventoryState(emptyInventorySize));
        pantsInventory.ChangeState(new InventoryState(emptyInventorySize));
        
        playerCharacteristicsPanel.Player = CurrentCharacter;
        
        primaryGunSet.Player = CurrentCharacter;
        primaryGunSet.GunPlaced.AddListener(OnPrimaryGunPlaced);
        primaryGunSet.GunTaken.AddListener(OnPrimaryGunTaken);
        
        secondaryGunSet.Player = CurrentCharacter;
        secondaryGunSet.GunPlaced.AddListener(OnSecondaryGunPlaced);
        secondaryGunSet.GunTaken.AddListener(OnSecondaryGunTaken);
        
        nameTextBox.text = CurrentCharacter.FirstName;
        
        jacketCell.OnItemPlaced.AddListener(OnJacketPlaced);
        jacketCell.OnItemTaked.AddListener(OnJacketTaken);
        
        vestCell.OnItemPlaced.AddListener(OnVestPlaced);
        vestCell.OnItemTaked.AddListener(OnVestTaken);
        
        backpackCell.OnItemPlaced.AddListener(OnBackpackPlaced);
        backpackCell.OnItemTaked.AddListener(OnBackpackTaken);
        
        underwearCell.OnItemPlaced.AddListener(OnUnderwearPlaced);
        underwearCell.OnItemTaked.AddListener(OnUnderwearTaken);
        
        hatCell.OnItemPlaced.AddListener(OnHatPlaced);
        hatCell.OnItemTaked.AddListener(OnHatTaken);
        
        pantsCell.OnItemPlaced.AddListener(OnPantsPlaced);
        pantsCell.OnItemTaked.AddListener(OnPantsTaken);
        
        bootsCell.OnItemPlaced.AddListener(OnBootsPlaced);
        bootsCell.OnItemTaked.AddListener(OnBootsTaken);
    }

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
        CurrentCharacter.SecondaryGun = primaryGunSet.CurrentInterfaceSetGun;
    }
    
    private void OnJacketPlaced()
    {
        CurrentCharacter.body.chest.Jacket = jacketCell.PlacedItem.GetComponent<Clothes>();
        jacketInventory.ChangeState(jacketCell.PlacedItem.GetComponent<Clothes>().Inventory);
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
    
    private void UpdateAllInventories()
    {
        if (jacketCell.PlacedItem != null)
        {
            jacketInventory.ChangeState(jacketCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.body.chest.Vest != null)
        {
            vestInventory.ChangeState(vestCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.body.chest.Backpack != null)
        {
            backpackInventory.ChangeState(backpackCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.body.leftLeg.Pants != null)
        {
            pantsInventory.ChangeState(pantsCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        //TODO если вещи в спец слоте нету, то вместо инвенторя отрисовать замочек
    }

    private void PlaceAllItems()
    {
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Jacket, jacketCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Backpack, backpackCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.leftLeg.Pants, pantsCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.head.Hat, hatCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.chest.Underwear, underwearCell);
        CheckClothCellAfterWindowOpen(CurrentCharacter.body.leftLeg.Boots, bootsCell);
    }

    private void CheckClothCellAfterWindowOpen(Clothes cloth, SpecialCell cell)
    {
        if (cloth != null)
            cell.PlaceItem(cloth.gameObject.GetComponent<BaseItem>());
        else
            cell.PlaceNullItem();
    }

    private void CheckClothInventoryAfterWindowOpen(ItemGrid inventory, SpecialCell cell)
    {

    }
    
    public void OnOpen()
    {
        PlaceAllItems();
        UpdateAllInventories();//ОБЯЗАТЕЛЬНО ПОСЛЕ РАЗМЕЩЕНИЯ ПРЕДМЕТОВ, В МЕТОДЕ ИДЕТ ПРОВЕРКА НА РАЗМЕЩЕНИЕ ПРЕДМЕТА В КЛЕТКЕ, А НЕ НА ОДЕЖДУ ПЕРСОНАЖА
    }
}
