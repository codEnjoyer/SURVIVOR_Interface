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
        nameTextBox.text = CurrentCharacter.name;
        
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

    public void Update()
    {
        Debug.Log(CurrentCharacter.Body.Chest.Jacket);
    }

    public void OnFoodChanged()
    {
        playerCharacteristicsPanel.OnFoodChanged();
    }
    
    public void OnWaterChanged()
    {
        playerCharacteristicsPanel.OnWaterChanged();
    }
    
    public void OnEnegryChanged()
    {
        playerCharacteristicsPanel.OnEnegryChanged();
    }
    
    public void OnHealthChanged()
    {
        playerCharacteristicsPanel.OnHealthChanged();
    }

    private void OnJacketPlaced()
    {
        CurrentCharacter.Body.Chest.Jacket = jacketCell.PlacedItem.GetComponent<Clothes>();
        jacketInventory.ChangeState(jacketCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnJacketTaken()
    {
        CurrentCharacter.Body.Chest.Jacket = null;
        jacketInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnVestPlaced()
    {
        CurrentCharacter.Body.Chest.Vest = vestCell.PlacedItem.GetComponent<Clothes>();
        vestInventory.ChangeState(vestCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnVestTaken()
    {
        CurrentCharacter.Body.Chest.Vest = null;
        vestInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnBackpackPlaced()
    {
        CurrentCharacter.Body.Chest.Backpack = backpackCell.PlacedItem.GetComponent<Clothes>();
        backpackInventory.ChangeState(backpackCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnBackpackTaken()
    {
        CurrentCharacter.Body.Chest.Backpack = null;
        backpackInventory.ChangeState(new InventoryState(emptyInventorySize));
    }
    
    private void OnPantsPlaced()
    {
        CurrentCharacter.Body.LeftLeg.Pants = pantsCell.PlacedItem.GetComponent<Clothes>();
        CurrentCharacter.Body.RightLeg.Pants = pantsCell.PlacedItem.GetComponent<Clothes>();
        pantsInventory.ChangeState(pantsCell.PlacedItem.GetComponent<Clothes>().Inventory);
    }
    
    private void OnPantsTaken()
    {
        CurrentCharacter.Body.LeftLeg.Pants = null;
        CurrentCharacter.Body.RightLeg.Pants = null;
        pantsInventory.ChangeState(new InventoryState(emptyInventorySize));
    }

    private void OnHatPlaced()
    {
        CurrentCharacter.Body.Head.Hat = hatCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnHatTaken()
    {
        CurrentCharacter.Body.Head.Hat = null;
    }

    private void OnBootsPlaced()
    {
        CurrentCharacter.Body.LeftLeg.Boots = bootsCell.PlacedItem.GetComponent<Clothes>();
        CurrentCharacter.Body.RightLeg.Boots = bootsCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnBootsTaken()
    {
        CurrentCharacter.Body.LeftLeg.Boots = null;
        CurrentCharacter.Body.RightLeg.Boots = null;
    }
    
    private void OnUnderwearPlaced()
    {
        CurrentCharacter.Body.Chest.Underwear = underwearCell.PlacedItem.GetComponent<Clothes>();
    }
    
    private void OnUnderwearTaken()
    {
        CurrentCharacter.Body.Chest.Underwear = null;
    }
    
    private void UpdateAllInventories()
    {
        if (CurrentCharacter.Body.Chest.Jacket != null)
        {
            jacketInventory.ChangeState(jacketCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.Body.Chest.Vest != null)
        {
            vestInventory.ChangeState(vestCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.Body.Chest.Backpack != null)
        {
            backpackInventory.ChangeState(backpackCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        if (CurrentCharacter.Body.LeftLeg.Pants != null)
        {
            pantsInventory.ChangeState(pantsCell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
    }

    private void PlaceAllItems()
    {
        if (CurrentCharacter.Body.Chest.Jacket != null)
            jacketCell.PlaceItem(CurrentCharacter.Body.Chest.Jacket.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.Chest.Vest != null)
            vestCell.PlaceItem(CurrentCharacter.Body.Chest.Vest.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.Chest.Backpack != null)
            backpackCell.PlaceItem(CurrentCharacter.Body.Chest.Backpack.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.LeftLeg.Pants != null)
            pantsCell.PlaceItem(CurrentCharacter.Body.LeftLeg.Pants.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.Head.Hat != null)
            hatCell.PlaceItem(CurrentCharacter.Body.Head.Hat.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.Chest.Underwear != null)
            underwearCell.PlaceItem(CurrentCharacter.Body.Chest.Underwear.gameObject.GetComponent<BaseItem>());
        if (CurrentCharacter.Body.LeftLeg.Boots != null)
            bootsCell.PlaceItem(CurrentCharacter.Body.LeftLeg.Boots.gameObject.GetComponent<BaseItem>());
    }

    public void OnOpen()
    {
        PlaceAllItems();
        UpdateAllInventories();
    }
}
