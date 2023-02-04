using System.Collections;
using System.Collections.Generic;
using Model.Items;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] private RectTransform highlighter;

    public void SetSize(BaseItem targetItem)
    {
        var size = new Vector2(targetItem.Width * InventoryGrid.TileSize,
            targetItem.Height * InventoryGrid.TileSize);
        highlighter.sizeDelta = size;
    }

    public void SetPosition(InventoryGrid targetGrid, BaseItem targetItem)
    {
        SetPosition(targetGrid, targetItem, targetItem.OnGridPositionX, targetItem.OnGridPositionY);
    }

    public void SetPosition(InventoryGrid targetGrid, BaseItem targetItem, int posX, int posY)
    {
        var position = targetGrid.GetPositionOnGrid(targetItem, posX, posY);
        highlighter.localPosition = position;
    }

    public void Show(bool state)
    {
        highlighter.gameObject.SetActive(state);
    }

    public void SetParent(InventoryGrid targetGrid)
    {
        if(targetGrid == null) return;
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
        highlighter.SetAsLastSibling();
    }
}