using System;
using System.Collections;
using System.Collections.Generic;
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

        playerCharacteristicsPanel.Player = CurrentCharacter;
        
        CheckAndShowInventory(backpackInventory,backpackCell);
        CheckAndShowInventory(vestInventory,vestCell);
        CheckAndShowInventory(jacketInventory,jacketCell);
        CheckAndShowInventory(pantsInventory,pantsCell);
        
        hatCell.OnItemChanged.AddListener(OnHatChanged);
        underwearCell.OnItemChanged.AddListener(OnUnderwearChanged);
        backpackCell.OnItemChanged.AddListener(OnBackpackChanged);
        vestCell.OnItemChanged.AddListener(OnVestChanged);
        jacketCell.OnItemChanged.AddListener(OnJacketChanged);
        pantsCell.OnItemChanged.AddListener(OnPantsChanged);
        backpackCell.OnItemChanged.AddListener(OnBootsChanged);
        
        nameTextBox.text = CurrentCharacter.name;
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
    
    private void CheckAndShowInventory(ItemGrid itemGrid, SpecialCell cell)
    {
        if (cell.PlacedItem != null && cell.PlacedItem.GetComponent<Clothes>() != null)
        {
            itemGrid.ChangeState(cell.PlacedItem.GetComponent<Clothes>().Inventory);
        }
        else
        {
            itemGrid.gameObject.SetActive(false);
        }
    }

    private void OnHatChanged()
    {
        OnClothChanged(CurrentCharacter.Body.Head.Hat, hatCell.PlacedItem);
    }

    private void OnUnderwearChanged()
    {
        OnClothChanged(CurrentCharacter.Body.Chest.Underwear,underwearCell.PlacedItem);
    }

    private void OnBackpackChanged()
    {
        OnClothChanged(CurrentCharacter.Body.Chest.Backpack,backpackCell.PlacedItem);
        CheckAndShowInventory(backpackInventory,backpackCell);
    }

    private void SetInventory(ItemGrid inventory, SpecialCell cell)
    {
        inventory.InventoryGridBg.gameObject.SetActive(true);
        inventory.gameObject.SetActive(true);
        inventory.ChangeState(cell.PlacedItem.GetComponent<Clothes>().Inventory);
    }

    private void DeSetInventory(ItemGrid inventory)
    {
        inventory.InventoryGridBg.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
    }
    private void OnJacketChanged()
    {
        if (jacketCell.PlacedItem == null)//Сняли
        {
            CurrentCharacter.Body.Chest.Jacket = null;
            DeSetInventory(jacketInventory);
        }
        else//Надели
        {
            CurrentCharacter.Body.Chest.Jacket = jacketCell.PlacedItem.GetComponent<Clothes>();
            SetInventory(jacketInventory,jacketCell);
        }
        
        
    }
    
    private void OnVestChanged()
    {
        OnClothChanged(CurrentCharacter.Body.Chest.Vest,jacketCell.PlacedItem);
        CheckAndShowInventory(vestInventory,vestCell);
    }
    
    private void OnPantsChanged()
    {
        OnClothChanged(CurrentCharacter.Body.LeftLeg.Pants,jacketCell.PlacedItem);
        OnClothChanged(CurrentCharacter.Body.RightLeg.Pants,jacketCell.PlacedItem);
        CheckAndShowInventory(pantsInventory,pantsCell);
    }
    
    private void OnBootsChanged()
    {
        OnClothChanged(CurrentCharacter.Body.LeftLeg.Boots,bootsCell.PlacedItem);
        OnClothChanged(CurrentCharacter.Body.RightLeg.Boots,bootsCell.PlacedItem);
    }
    private void OnClothChanged(Clothes clothes, BaseItem baseItem)
    {
        clothes = baseItem.GetComponent<Clothes>();
    }

    private void CheckAndPlaceItemAfterOpening(SpecialCell cell, Clothes clothesToReplace)
    {
        if (clothesToReplace && clothesToReplace.GetComponentInParent<BaseItem>())
        {
            cell.PlaceItem(CurrentCharacter.Body.Head.Hat.GetComponentInParent<BaseItem>());
        }
    }
    
    public void OnOpen()
    {
        CheckAndPlaceItemAfterOpening(hatCell,CurrentCharacter.Body.Head.Hat);
        CheckAndPlaceItemAfterOpening(underwearCell, CurrentCharacter.Body.Chest.Underwear);
        CheckAndPlaceItemAfterOpening(vestCell,CurrentCharacter.Body.Chest.Vest);
        CheckAndShowInventory(vestInventory,vestCell);
        CheckAndPlaceItemAfterOpening(jacketCell,CurrentCharacter.Body.Chest.Jacket);
        CheckAndShowInventory(jacketInventory,jacketCell);
        CheckAndPlaceItemAfterOpening(backpackCell,CurrentCharacter.Body.Chest.Backpack);
        CheckAndShowInventory(backpackInventory,backpackCell);
        CheckAndPlaceItemAfterOpening(pantsCell, CurrentCharacter.Body.LeftLeg.Pants);
        CheckAndShowInventory(pantsInventory,pantsCell);
        CheckAndPlaceItemAfterOpening(bootsCell, CurrentCharacter.Body.LeftLeg.Boots);
    }
}
