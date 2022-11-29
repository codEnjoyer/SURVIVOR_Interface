using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BaseItem : MonoBehaviour
{
    [SerializeField] public BaseItemData itemData;
    public int OnGridPositionX { get; set; }
    public int OnGridPositionY { get; set; }

    public Vector3 OnAwakeRectTransformSize { get; set; }
    
    public Vector3 OnAwakeRectTransformScale { get; set; }
    
    public int Height => !rotated ? size.Height : size.Width;

    public int Width => !rotated ? size.Width : size.Height;

    public bool rotated { get; set; }
    public Size size => itemData.Size;
    public float Weight => itemData.Weight;
    public BaseItemData ItemData => itemData;

    public void SetBaseItemData(BaseItemData newBaseItemData)
    {
        itemData = newBaseItemData;
    }

    public SpecialCellType SpecialCellType => itemData.SpecialCellType;

    public void Awake()
    {
        if (itemData == null || itemData.Icon == null)
            return;
        
        gameObject.AddComponent<Image>().sprite = itemData.Icon;
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void Set(BaseItemData itemData)
     {
         var rt = gameObject.GetComponent<RectTransform>();
         var scaleFactor = GetComponentInParent<Canvas>().scaleFactor;
         var size = new Vector2(itemData.Size.Width * ItemGrid.TileSize * scaleFactor,
             itemData.Size.Height * ItemGrid.TileSize * scaleFactor);
         rt.sizeDelta = size;
         OnAwakeRectTransformScale = rt.localScale;
         OnAwakeRectTransformSize = rt.sizeDelta;
     }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }
}