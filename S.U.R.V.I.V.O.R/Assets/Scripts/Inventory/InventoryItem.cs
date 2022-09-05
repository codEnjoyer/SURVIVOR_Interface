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
        var scaleFactor = GetComponentInParent<Canvas>().scaleFactor;

        var size = new Vector2(itemData.width * ItemGrid.TileSize * scaleFactor,
            itemData.height * ItemGrid.TileSize * scaleFactor);
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }
}