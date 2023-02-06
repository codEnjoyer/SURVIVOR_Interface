using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpecialClothCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private ClothType type;
    [SerializeField] private InventoryGrid currentInventory;
    private Size zeroInventorySize;
    private Character currentCharacter;

    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            currentCharacter = value;
            Init();
        }
    }
    private bool wasOpened;

    public override void Init()
    {
        base.Init();
        zeroInventorySize = Resources.Load<Size>("InventorySizeObjects/0x0");
        if (currentInventory != null)
            currentInventory.InventoryOwner = CurrentCharacter;
    }


    public void OnEnable()
    {
        if (currentInventory != null && PlacedItem == null)
            currentInventory.ChangeState(new InventoryState(zeroInventorySize));
    }
    
    public override void PlaceItem(BaseItem item)
    {
        if (item.IsRotated)
            item.Rotate();
        placedItem = item;
        bool isWeared = CurrentCharacter.ManBody.Wear(item.GetComponent<Clothes>());
        if (isWeared)
        {
            item.ItemOwner = CurrentCharacter;
            InventoryController.SelectedItem = null;
        }
        else
        {
            GiveItem();
        }
    }

    public override void GiveItem()
    {
        if (placedItem == null) return;
        var removedClothes = CurrentCharacter.ManBody.UnWear(PlacedItem.GetComponent<Clothes>().Data.ClothType);
        if (removedClothes is null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        currentInventory?.ChangeState(new InventoryState(zeroInventorySize));
        InventoryController.PickUpItemFromSpecialCell(PlacedItem);
        PlaceNullItem();
    }

    public override void ReDraw()
    {
        DrawItem();
        if(currentInventory != null)
            UpdateInventory();
    }
    
    private void UpdateInventory()
    {
        var item = CurrentCharacter.ManBody.GetClothByType(type);
        if (item != null)
            currentInventory.ChangeState(item.Inventory);
        else
        {
            currentInventory.ChangeState(new InventoryState(zeroInventorySize));
        }
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<Clothes>() &&
               InventoryController.SelectedItem.GetComponent<Clothes>().Data.ClothType == type;
    }
}
