using UnityEngine;
using UnityEngine.UI;

public class InventoryGridBackground : MonoBehaviour
{
    [SerializeField] private ItemGrid itemGrid;
    private void Start()
    {
        var slot = transform.Find("Slot");
        slot.gameObject.SetActive(false);
        for (int y = 0; y < itemGrid.GridSizeHeight; y++)
        {
            for (int x = 0; x < itemGrid.GridSizeWidth; x++)
            {
                var singleSlot = Instantiate(slot, transform);
                singleSlot.gameObject.SetActive(true);
            }
        }

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(ItemGrid.TileSize, ItemGrid.TileSize);
        GetComponent<RectTransform>().sizeDelta = new Vector2(itemGrid.GridSizeWidth,  itemGrid.GridSizeHeight) * ItemGrid.TileSize;
        GetComponent<RectTransform>().anchoredPosition = itemGrid.GetComponent<RectTransform>().anchoredPosition;
    }
}
