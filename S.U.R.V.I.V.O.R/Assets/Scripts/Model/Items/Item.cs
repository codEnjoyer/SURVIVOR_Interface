using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    [SerializeField] private float weight;
    public float Weight => weight;

    public int onGridPositionX { get; set; }
    public int onGridPositionY { get; set; }

    public int Height => !rotated ? data.Height : data.Width;

    public int Width => !rotated ? data.Width : data.Height;

    public bool rotated { get; set; }


    public void Set(ItemData itemData)
    {
        var scaleFactor = GetComponentInParent<Canvas>().scaleFactor;

        var size = new Vector2(itemData.Width * ItemGrid.TileSize * scaleFactor,
            itemData.Height * ItemGrid.TileSize * scaleFactor);
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }
}