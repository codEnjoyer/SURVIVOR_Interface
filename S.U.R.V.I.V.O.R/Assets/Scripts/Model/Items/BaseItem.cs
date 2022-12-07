using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BaseItem : MonoBehaviour
{
    [FormerlySerializedAs("itemData")] [SerializeField] private BaseItemData data;
    public int OnGridPositionX { get; set; }
    public int OnGridPositionY { get; set; }
    
    public Vector3 OnAwakeRectTransformSize { get; set; }
    
    public Vector3 OnAwakeRectTransformScale { get; set; }
    
    public int Height => !rotated ? Size.Height : Size.Width;

    public int Width => !rotated ? Size.Width : Size.Height;

    public bool rotated { get; set; }
    public Size Size => data.Size;
    public float Weight => data.Weight;
    public BaseItemData Data => data;
    
    public void Awake()
    {
        if (data == null || data.Icon == null)
            return;
        
        gameObject.AddComponent<Image>().sprite = data.Icon;
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