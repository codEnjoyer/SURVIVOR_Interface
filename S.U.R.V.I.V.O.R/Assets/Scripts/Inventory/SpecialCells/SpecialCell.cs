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
    public BodyPart bodyPart;
    
    [SerializeField] 
    private Transform canvasTransform;
    
    [SerializeField]
    private SpecialCellType cellType;
    
    private RectTransform rectTransform;
    private Vector3 itemSize;
    private Vector3 itemScale;
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
        ChangeItemSize(placedItem.GetComponent<RectTransform>());
        inventoryController.selectedItem = null;
        OnItemPlaced.Invoke();
    }
    
    private void GiveItem()
    {
        placedItem.GetComponent<RectTransform>().sizeDelta = itemSize;
        placedItem.GetComponent<RectTransform>().localScale = itemScale;
        placedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        inventoryController.PickUpItem(placedItem);
        OnItemTaked.Invoke();
        placedItem = null;
    }

    private void ChangeItemSize(RectTransform transform)
    {
        itemSize = transform.sizeDelta;
        itemScale = transform.localScale;
        transform.localScale = new Vector3(1, 1, 1);
        if (transform.sizeDelta.x * itemScale.x > rectTransform.sizeDelta.x * rectTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * itemScale.x,itemSize.y * itemScale.y/(itemSize.x * itemScale.x/rectTransform.sizeDelta.x * rectTransform.localScale.x));
        }
        if (transform.sizeDelta.x * itemScale.x > rectTransform.sizeDelta.x * rectTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(itemSize.x * itemScale.x/(itemSize.y * itemScale.y/rectTransform.sizeDelta.y * rectTransform.localScale.y),rectTransform.sizeDelta.y * rectTransform.localScale.y);
        }
    }
}
