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
    MeleeWeapon
}
public class SpecialCell : MonoBehaviour, IPointerClickHandler
{
    private InventoryController inventoryController;
    
    [SerializeField] 
    private Transform canvasTransform;
    
    [SerializeField]
    private SpecialCellType cellType;
    
    private RectTransform rectTransform;
    private Vector3 itemSize;
    private BaseItem placedItem;
    public SpecialCellType CellType => cellType;
    public BaseItem PlacedItem => placedItem;

    public UnityEvent OnItemChanged = new ();

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        rectTransform = GetComponent<RectTransform>();
    }

    private bool CanInsertIntoSlot()
    {
        return inventoryController.selectedItem.SpecialCellType == CellType;
    }
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (placedItem == null && inventoryController.selectedItem != null)
        {
            if (CanInsertIntoSlot())
            {
                PlaceItem(inventoryController.selectedItem);
            }
        }
        else if (inventoryController.selectedItem == null)
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
        itemRectTransform.localScale = rectTransform.localScale;
        ChangeItemSize(placedItem.GetComponent<RectTransform>());
        inventoryController.selectedItem = null;
        OnItemChanged.Invoke();
    }
    
    private void GiveItem()
    {
        placedItem.GetComponent<RectTransform>().sizeDelta = itemSize;
        placedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        inventoryController.PickUpItem(placedItem);
        OnItemChanged.Invoke();
        placedItem = null;
    }

    private void ChangeItemSize(RectTransform transform)
    {
        itemSize = transform.sizeDelta;
        if (transform.sizeDelta.x > rectTransform.sizeDelta.x)
        {
            transform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,itemSize.y/(itemSize.x/rectTransform.sizeDelta.x));
        }
        if (transform.sizeDelta.y > rectTransform.sizeDelta.y)
        {
            transform.sizeDelta = new Vector2(itemSize.x/(itemSize.y/rectTransform.sizeDelta.y),rectTransform.sizeDelta.y);
        }
    }
}
