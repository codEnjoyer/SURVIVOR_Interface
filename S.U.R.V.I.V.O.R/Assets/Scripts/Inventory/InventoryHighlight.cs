using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] private RectTransform highlighter;

    public void SetSize(InventoryItem targetInventoryItem)
    {
        var size = new Vector2(targetInventoryItem.Width * ItemGrid.TileSize,
            targetInventoryItem.Height * ItemGrid.TileSize);
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetInventoryItem)
    {
        SetPosition(targetGrid, targetInventoryItem, targetInventoryItem.onGridPositionX, targetInventoryItem.onGridPositionY);
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetInventoryItem, int posX, int posY)
    {
        var position = targetGrid.GetPositionOnGrid(targetInventoryItem, posX, posY);
        highlighter.localPosition = position;
    }

    public void Show(bool state)
    {
        highlighter.gameObject.SetActive(state);
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if(targetGrid == null) return;
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }
}