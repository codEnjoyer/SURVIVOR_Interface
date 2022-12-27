using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridBackground : MonoBehaviour
{
    private List<Transform> listSlots = new List<Transform>();

    public void DrawBackground(ItemGrid itemGrid)
    {
        var slot = transform.Find("Slot");
        slot.gameObject.SetActive(false);

        if (listSlots.Count > 0)
            DestroyAllSlots();

        for (int y = 0; y < itemGrid.GridSizeHeight; y++)
        {
            for (int x = 0; x < itemGrid.GridSizeWidth; x++)
            {
                var singleSlot = Instantiate(slot, transform);
                singleSlot.gameObject.SetActive(true);
                listSlots.Add(singleSlot);
            }
        }

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(ItemGrid.TileSize, ItemGrid.TileSize);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(itemGrid.GridSizeWidth, itemGrid.GridSizeHeight) * ItemGrid.TileSize;
        GetComponent<RectTransform>().anchoredPosition = itemGrid.GetComponent<RectTransform>().anchoredPosition;
    }

    private void DestroyAllSlots()
    {
        foreach (var slot in listSlots)
        {
            Destroy(slot.gameObject);
        }

        listSlots.Clear();
    }
}