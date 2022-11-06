using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    public int onGridPositionX { get; set; }
    public int onGridPositionY { get; set; }

    public int Height => !rotated ? size.Height : size.Width;

    public int Width => !rotated ? size.Width : size.Height;

    public bool rotated { get; set; }
    public Size size => itemData.Size;
    public float Weight => itemData.Weight;
    public ItemData ItemData => itemData;

    public void Set(ItemData itemData)
     {
         var scaleFactor = GetComponentInParent<Canvas>().scaleFactor;
 
         var size = new Vector2(itemData.Size.Width * ItemGrid.TileSize * scaleFactor,
             itemData.Size.Height * ItemGrid.TileSize * scaleFactor);
         GetComponent<RectTransform>().sizeDelta = size;
     }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }
}