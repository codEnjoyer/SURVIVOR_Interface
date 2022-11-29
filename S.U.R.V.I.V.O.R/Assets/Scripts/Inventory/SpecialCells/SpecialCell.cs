using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpecialCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected InventoryController InventoryController { get; private set; }
    protected BaseItem placedItem;
    protected bool isPointerOverCell;
    public BaseItem PlacedItem => placedItem;
    
    public UnityEvent OnItemPlaced = new ();
    public UnityEvent OnItemTaked = new ();
    

    private void Awake()
    {
        InventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
    }

    protected virtual bool CanInsertIntoSlot()
    {
        return false;
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
    
    public virtual void PlaceItem(BaseItem item)
    {
        return;
    }

    public void PlaceNullItem()
    {
        placedItem = null;
        
    }

    public virtual void GiveItem()
    {
        return;
    }

    protected void ChangeItemSize(RectTransform transform, RectTransform cellTransform)
    {
        if (placedItem.onAwakeRectTransformSize.x * placedItem.onAwakeRectTransformScale.x > cellTransform.sizeDelta.x * cellTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(cellTransform.sizeDelta.x * placedItem.onAwakeRectTransformScale.x,placedItem.onAwakeRectTransformSize.y * placedItem.onAwakeRectTransformScale.y/(placedItem.onAwakeRectTransformSize.x * placedItem.onAwakeRectTransformScale.x/cellTransform.sizeDelta.x * cellTransform.localScale.x));
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (placedItem.onAwakeRectTransformSize.y * placedItem.onAwakeRectTransformScale.y > cellTransform.sizeDelta.y * cellTransform.localScale.y)
        {
            transform.sizeDelta = new Vector2(placedItem.onAwakeRectTransformSize.x * placedItem.onAwakeRectTransformScale.x/(placedItem.onAwakeRectTransformSize.y * placedItem.onAwakeRectTransformScale.y/cellTransform.sizeDelta.y * cellTransform.localScale.y),cellTransform.sizeDelta.y * cellTransform.localScale.y);
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
