using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public enum SpecialCellType
{
    None,
    Jacket,
    Backpack,
    Boots,
    Underwear,
    Vest,
    Hat,
    Pants,
    PrimaryGun,
    SecondaryGun,
    MeleeWeapon,
    ModuleGrip,
    ModuleMagazine,
    ModuleSpring,
    ModuleShutter,
    ModuleScope,
    ModuleSuppressor,
    ModuleTactical
}
public class SpecialCell : MonoBehaviour, IPointerClickHandler
{
    private InventoryController inventoryController;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private SpecialCellType cellType;
    
    private RectTransform rectTransform;
    private BaseItem placedItem; 
    public SpecialCellType CellType => cellType;
    public BaseItem PlacedItem => placedItem;
    
    public UnityEvent OnItemPlaced = new ();
    public UnityEvent OnItemTaked = new ();
    

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        rectTransform = GetComponent<RectTransform>();
    }

    private bool CanInsertIntoSlot()
    {
        return inventoryController.SelectedItem.SpecialCellType == CellType;
    }
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (placedItem == null && inventoryController.SelectedItem != null)
        {
            if (CanInsertIntoSlot())
            {
                PlaceItem(inventoryController.SelectedItem);
            }
        }
        else if (inventoryController.SelectedItem == null)
        {
            GiveItem();
        }
    }
    
    public void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        var itemRectTransform = placedItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);
        itemRectTransform.localPosition = new Vector2(0,0);
        ChangeItemSize(itemRectTransform);
        inventoryController.SelectedItem = null;
        OnItemPlaced.Invoke();
    }

    public void PlaceNullItem()
    {
        placedItem = null;
        
    }
    
    private void GiveItem()
    {
        if (placedItem == null) return;
        placedItem.GetComponent<RectTransform>().sizeDelta = placedItem.OnAwakeRectTransformSize;
        placedItem.GetComponent<RectTransform>().localScale = placedItem.OnAwakeRectTransformScale;
        placedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        inventoryController.PickUpItem(placedItem);
        OnItemTaked.Invoke();
        placedItem = null;
    }

    private void ChangeItemSize(RectTransform transform)
    {
        if (placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x > rectTransform.sizeDelta.x * rectTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * placedItem.OnAwakeRectTransformScale.x,placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y/(placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x/rectTransform.sizeDelta.x * rectTransform.localScale.x));
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y > rectTransform.sizeDelta.y * rectTransform.localScale.y)
        {
            transform.sizeDelta = new Vector2(placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x/(placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y/rectTransform.sizeDelta.y * rectTransform.localScale.y),rectTransform.sizeDelta.y * rectTransform.localScale.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
