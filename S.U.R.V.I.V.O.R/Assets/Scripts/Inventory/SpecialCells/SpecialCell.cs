using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
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
    Gun,
    MleeWeapon
}
public class SpecialCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SpecialCellType CellType;
    [SerializeField] 
    private Transform canvasTransform;
    private RectTransform rectTransform;
    private Vector3 itemSize;
    private InventoryController inventoryController;
    private BaseItem PlacedItem;
    public SpecialCellType cellType => CellType;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(1);
        if (PlacedItem == null && inventoryController.selectedItem != null)
        {
            Debug.Log(2);
            if (inventoryController.selectedItem.SpecialCellType == cellType)
            {
                Debug.Log(3);
                PlaceItem(inventoryController.selectedItem);
            }
        }
        else
        {
            GiveItem();
        }
    }
    
    private void PlaceItem(BaseItem item)
    {
        PlacedItem = item;
        var itemRectTransform = item.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);
        itemRectTransform.localPosition = new Vector2(0,0);
        itemRectTransform.localScale = rectTransform.localScale;
        ChangeItemSize(PlacedItem.GetComponent<RectTransform>());
        inventoryController.selectedItem = null;
    }
    
    private void GiveItem()
    {
        PlacedItem.GetComponent<RectTransform>().sizeDelta = itemSize;
        inventoryController.selectedItem = PlacedItem;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        PlacedItem = null;
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
