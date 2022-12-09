using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpecialClothCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private ClothType type;
    [SerializeField] private ItemGrid currentInventory;
    private Size zeroInventorySize;
    private Character currentCharacter;

    public Character CurrentCharacter
    {
        get => currentCharacter;
        set
        {
            currentCharacter = value;
            if (currentInventory != null)
                currentInventory.InventoryOwner = CurrentCharacter;
        }
    }
    private bool wasOpened;

    protected override void Awake()
    {
        if (!wasOpened)
        {
            OnFirstOpen();
        }
    }

    private void OnFirstOpen()
    {
        base.Awake();
        zeroInventorySize = Resources.Load<Size>("InventorySizeObjects/0x0");
        wasOpened = true;
    }
    
    public void OnEnable()
    {
        if(!wasOpened)
            OnFirstOpen();
        if (currentInventory != null && PlacedItem == null)
            currentInventory.ChangeState(new InventoryState(zeroInventorySize));
    }


    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        bool isWeared;
        CurrentCharacter.body.WearOrUnWear(item.GetComponent<Clothes>(),false, out isWeared);
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

    public void CheckNewClothes()
    {
        var wearedOnPlayerItem = WearedOnPlayerItem();
        if (wearedOnPlayerItem == null || wearedOnPlayerItem == PlacedItem)
        {
            ReDraw();
        }
        else if (wearedOnPlayerItem != PlacedItem && PlacedItem != null)
        {
            GiveItem();
            placedItem = wearedOnPlayerItem;
            ReDraw();
        }
        else if (wearedOnPlayerItem != PlacedItem && PlacedItem == null)
        {
            placedItem = wearedOnPlayerItem;
            ReDraw();
        }
    }

    public override void GiveItem()
    {
        if (WearedOnPlayerItem() == null) return;
        CurrentCharacter.body.WearOrUnWear(PlacedItem.GetComponent<Clothes>(), true, out var isSuccessful);
        if (!isSuccessful) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        currentInventory.ChangeState(new InventoryState(zeroInventorySize));
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
        var item = CurrentCharacter.body.GetClothByType(type);
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

    private BaseItem WearedOnPlayerItem()
    {
        switch (type)
        {
            case ClothType.Jacket:
                return CurrentCharacter.body.Chest.Jacket != null ? CurrentCharacter.body.Chest.Jacket.GetComponent<BaseItem>() : null;
            case ClothType.Backpack:
                return CurrentCharacter.body.Chest.Backpack != null ? CurrentCharacter.body.Chest.Backpack.GetComponent<BaseItem>() : null;
            case ClothType.Pants:
                return CurrentCharacter.body.RightLeg.Pants != null ? CurrentCharacter.body.RightLeg.Pants.GetComponent<BaseItem>() : null;
            case ClothType.Vest:
                return CurrentCharacter.body.Chest.Vest != null ? CurrentCharacter.body.Chest.Vest.GetComponent<BaseItem>() : null;
            case ClothType.Underwear:
                return CurrentCharacter.body.Chest.Underwear != null ? CurrentCharacter.body.Chest.Underwear.GetComponent<BaseItem>() : null;
            case ClothType.Boots:
                return CurrentCharacter.body.RightLeg.Boots != null ? CurrentCharacter.body.RightLeg.Boots.GetComponent<BaseItem>() : null;
            case ClothType.Hat:
                return CurrentCharacter.body.Head.Hat != null ? CurrentCharacter.body.Head.Hat.GetComponent<BaseItem>() : null;
        }

        return null;
    }
}
