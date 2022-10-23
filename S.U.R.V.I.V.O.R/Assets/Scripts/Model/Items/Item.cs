using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;

    public int onGridPositionX { get; set; }
    public int onGridPositionY { get; set; }

    public int Height => !rotated ? data.height : data.width;

    public int Width => !rotated ? data.width : data.height;

    public bool rotated { get; set; }


    public void Set(ItemData itemData)
    {
        this.data = itemData;

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