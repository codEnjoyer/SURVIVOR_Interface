using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;
    
    public int onGridPositionX;
    public int onGridPositionY;

    public int Height
    {
        get
        {
            if (!rotated)
                return itemData.height;
            return itemData.width;
        }
    }
    
    public int Width
    {
        get
        {
            if (!rotated)
                return itemData.width;
            return itemData.height;
        }
    }

    public bool rotated;
    
    
    public void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        var size = new Vector2(itemData.width * ItemGrid.TileSizeWidth, itemData.height * ItemGrid.TileSizeHeight);
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }
}