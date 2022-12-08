using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum ClothType
{
    Jacket,
    Backpack,
    Boots,
    Underwear,
    Vest,
    Hat,
    Pants
}
    
public enum GunType
{
    PrimaryGun,
    SecondaryGun
}
    
public enum GunModuleType
{
    Grip,
    Spring,
    Shutter,
    Scope,
    Suppressor,
    Tactical
}

public abstract class SpecialCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected InventoryController InventoryController { get; private set; }
    protected BaseItem placedItem;
    private bool isPointerOverCell;
    public BaseItem PlacedItem => placedItem;

    protected virtual void Awake()
    {
        InventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
    }

    protected virtual bool CanInsertIntoSlot()
    {
        return false;
    }

    public abstract void ReDraw();
    
    public void DrawItem()
    {
        var rectTransform = placedItem.gameObject.GetComponent<RectTransform>();
        if (placedItem == null) return;
        placedItem.gameObject.SetActive(true);
        rectTransform.SetParent(GetComponent<RectTransform>());
        rectTransform.anchoredPosition = new Vector2(0,0);
        placedItem.GetComponent<RectTransform>().sizeDelta = placedItem.OnAwakeRectTransformSize;
        placedItem.GetComponent<RectTransform>().localScale = placedItem.OnAwakeRectTransformScale;
        ChangeItemSize(placedItem.gameObject.GetComponent<RectTransform>(),GetComponent<RectTransform>());
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPointerOverCell)
        {
            if (placedItem == null && InventoryController.SelectedItem != null)
            {
                if (CanInsertIntoSlot())
                {
                    PlaceItem(InventoryController.SelectedItem);
                }
            }
            else if (InventoryController.SelectedItem == null)
            {
                GiveItem();
            }
        }
    }

    public abstract void PlaceItem(BaseItem item);

    public void PlaceNullItem()
    {
        placedItem = null;
        
    }

    public abstract void GiveItem();

    protected void ChangeItemSize(RectTransform transform, RectTransform cellTransform)
    {
        if (placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x > cellTransform.sizeDelta.x * cellTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(cellTransform.sizeDelta.x * placedItem.OnAwakeRectTransformScale.x,placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y/(placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x/cellTransform.sizeDelta.x * cellTransform.localScale.x));
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y > cellTransform.sizeDelta.y * cellTransform.localScale.y)
        {
            transform.sizeDelta = new Vector2(placedItem.OnAwakeRectTransformSize.x * placedItem.OnAwakeRectTransformScale.x/(placedItem.OnAwakeRectTransformSize.y * placedItem.OnAwakeRectTransformScale.y/cellTransform.sizeDelta.y * cellTransform.localScale.y),cellTransform.sizeDelta.y * cellTransform.localScale.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverCell = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverCell = false;
    }
}
