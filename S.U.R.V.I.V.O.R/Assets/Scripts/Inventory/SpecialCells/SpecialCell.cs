using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using Model.Items;
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
    Tactical,
    Magazine
}

public abstract class SpecialCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected InventoryController InventoryController { get; private set; }
    protected BaseItem placedItem;
    private bool isPointerOverCell;
    public virtual BaseItem PlacedItem
    {
        get => placedItem;

        protected set => placedItem = value;
    }

    public virtual void Init()
    {
        if (InventoryController == null)
            InventoryController = InventoryController.Instance;
    }

    protected virtual bool CanInsertIntoSlot()
    {
        return false;
    }

    protected abstract void ReDraw();

    protected void DrawItem()
    {
        if (PlacedItem == null) return;
        var rectTransform = PlacedItem.gameObject.GetComponent<RectTransform>();
        PlacedItem.gameObject.SetActive(true);
        rectTransform.SetParent(GetComponent<RectTransform>());
        rectTransform.anchoredPosition = new Vector2(0,0);
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        ChangeItemSize(PlacedItem.gameObject.GetComponent<RectTransform>(),GetComponent<RectTransform>());
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPointerOverCell)
        {
            if (PlacedItem == null && InventoryController.SelectedItem != null)
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

    protected abstract void PlaceItem(BaseItem item);
    
    public virtual void UpdateItem(BaseItem item)
    {
        if (PlacedItem == null)
        {
            if (item != null)
            {
                item.gameObject.SetActive(true);
                PlacedItem = item;
            }
        }
        else
        {
            if (item != PlacedItem)
            {
                PlacedItem.gameObject.SetActive(false);
                PlaceNullItem();
                if (item != null)
                {
                    PlacedItem = item;
                }
            }
        }
        ReDraw();
    }


    protected virtual void PlaceNullItem()
    {
        placedItem = null;
        ReDraw();
    }

    protected abstract void GiveItem();

    private void ChangeItemSize(RectTransform transform, RectTransform cellTransform)
    {
        if (PlacedItem.OnAwakeRectTransformSize.x * PlacedItem.OnAwakeRectTransformScale.x > cellTransform.sizeDelta.x * cellTransform.localScale.x)
        {
            transform.sizeDelta = new Vector2(cellTransform.sizeDelta.x * PlacedItem.OnAwakeRectTransformScale.x,PlacedItem.OnAwakeRectTransformSize.y * PlacedItem.OnAwakeRectTransformScale.y/(PlacedItem.OnAwakeRectTransformSize.x * PlacedItem.OnAwakeRectTransformScale.x/cellTransform.sizeDelta.x * cellTransform.localScale.x));
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (PlacedItem.OnAwakeRectTransformSize.y * PlacedItem.OnAwakeRectTransformScale.y > cellTransform.sizeDelta.y * cellTransform.localScale.y)
        {
            transform.sizeDelta = new Vector2(PlacedItem.OnAwakeRectTransformSize.x * PlacedItem.OnAwakeRectTransformScale.x/(PlacedItem.OnAwakeRectTransformSize.y * PlacedItem.OnAwakeRectTransformScale.y/cellTransform.sizeDelta.y * cellTransform.localScale.y),cellTransform.sizeDelta.y * cellTransform.localScale.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public virtual void CheckNewItem(BaseItem item)
    {
        if (item == null || item == PlacedItem)
        {
            ReDraw();
        }
        else if (item != PlacedItem && PlacedItem != null)
        {
            PlacedItem.gameObject.SetActive(false);
            PlacedItem = item;
            item.gameObject.SetActive(true);
            ReDraw();
        }
        else if (item != PlacedItem && PlacedItem == null)
        {
            PlacedItem = item;
            PlacedItem.gameObject.SetActive(true);
            ReDraw();
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
