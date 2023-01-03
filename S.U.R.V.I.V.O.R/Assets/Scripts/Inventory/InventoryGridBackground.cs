using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridBackground : MonoBehaviour
{
    private List<Transform> listSlots = new List<Transform>();

    public void DrawBackground(InventoryGrid inventoryGrid)
    {
        var slot = transform.Find("Slot");
        slot.gameObject.SetActive(false);

        if (listSlots.Count > 0)
            DestroyAllSlots();

        for (int y = 0; y < inventoryGrid.GridSizeHeight; y++)
        {
            for (int x = 0; x < inventoryGrid.GridSizeWidth; x++)
            {
                var singleSlot = Instantiate(slot, transform);
                singleSlot.gameObject.SetActive(true);
                listSlots.Add(singleSlot);
            }
        }

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(InventoryGrid.TileSize, InventoryGrid.TileSize);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(inventoryGrid.GridSizeWidth, inventoryGrid.GridSizeHeight) * InventoryGrid.TileSize;
        GetComponent<RectTransform>().anchoredPosition = inventoryGrid.GetComponent<RectTransform>().anchoredPosition;
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