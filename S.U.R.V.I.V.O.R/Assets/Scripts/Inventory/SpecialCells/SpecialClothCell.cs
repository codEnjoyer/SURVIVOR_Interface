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
    public Character currentCharacter { get; set; }

    protected override void Awake()
    {
        base.Awake();
        zeroInventorySize = Resources.Load<Size>("InventorySizeObjects/0x0");
        currentInventory = GetComponentInChildren<RectTransform>().GetComponentInChildren<ItemGrid>();
    }

    public void OnEnable()
    {
        if (currentInventory != null)
            currentInventory.ChangeState(new InventoryState(zeroInventorySize));
    }

    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        bool isWeared;
        currentCharacter.body.Wear(item.GetComponent<Clothes>(),false, out isWeared);
        if (isWeared)
        {
            InventoryController.SelectedItem = null;
        }
        else
        {
            GiveItem();
        }
    }
    
    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        currentCharacter.body.Wear(PlacedItem.GetComponent<Clothes>(), true, out var isSuccessful);
        if (!isSuccessful) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItem(PlacedItem);
        PlaceNullItem();
    }

    public void ReDraw()
    {
        DrawItem();
        if(currentInventory != null)
            UpdateInventory();
    }
    
    private void UpdateInventory()
    {
        var item = currentCharacter.body.GetClothByType(type);
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
