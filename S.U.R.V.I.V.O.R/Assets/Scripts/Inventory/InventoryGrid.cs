using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class InventoryGrid : MonoBehaviour
{
    [FormerlySerializedAs("size")] [SerializeField]
    private Size initializeSize;

    [SerializeField] private InventoryGridBackground inventoryGridBG;

    private Canvas canvas;
    private InventoryState curInventoryState;

    public const float TileSize = 28;

    private Vector2 positionOnGrid;
    private Vector2Int tileGridPosition;

    private RectTransform rectTransform;

    private IEnumerable<BaseItem> storedItems = new List<BaseItem>();
    public int GridSizeWidth => curInventoryState.Size.Width;
    public int GridSizeHeight => curInventoryState.Size.Height;

    public Character InventoryOwner { get; set; }

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        curInventoryState = new InventoryState(initializeSize);
        inventoryGridBG.DrawBackground(this);
        rectTransform.sizeDelta =
            new Vector2(initializeSize.Width * TileSize, initializeSize.Height * TileSize);
        curInventoryState.PlaceItemEvent += OnPlaceItem;
        curInventoryState.PickUpItemEvent += OnPickedItem;
    }
    
    public void ChangeState(InventoryState inventoryState)
    {
        if (curInventoryState != null)
        {
            curInventoryState.PlaceItemEvent -= OnPlaceItem;
            curInventoryState.PickUpItemEvent -= OnPickedItem;
            storedItems = curInventoryState.GetItems();
        }
        curInventoryState = inventoryState;
        curInventoryState.PlaceItemEvent += OnPlaceItem;
        curInventoryState.PickUpItemEvent += OnPickedItem;
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(inventoryState.Size.Width * TileSize, inventoryState.Size.Height * TileSize);
        RedrawGrid();
        inventoryGridBG.DrawBackground(this);
    }

    private void RedrawGrid()
    {
        DestroyAllItems();

        foreach (var item in curInventoryState.GetItems())
        {
            item.gameObject.SetActive(true);
            var position = GetPositionOnGrid(item, item.OnGridPositionX, item.OnGridPositionY);
            item.GetComponent<RectTransform>().SetParent(transform);
            item.GetComponent<RectTransform>().localPosition = position;
        }
    }

    private void DestroyAllItems()
    {
        foreach (var item in storedItems)
            item.gameObject.SetActive(false);
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        var position = rectTransform.position;
        positionOnGrid.x = mousePosition.x - position.x;
        positionOnGrid.y = position.y - mousePosition.y;

        var scaleFactor = canvas.scaleFactor;
        tileGridPosition.x = (int) ((positionOnGrid.x / TileSize) / scaleFactor);
        tileGridPosition.y = (int) ((positionOnGrid.y / TileSize) / scaleFactor);

        return tileGridPosition;
    }

    public bool PlaceItem(BaseItem item, int posX, int posY, ref BaseItem overlapItem)
    {
        var res = curInventoryState.PlaceItem(item, posX, posY, ref overlapItem);
        if (res)
        {
            item.ItemOwner = InventoryOwner;
            var itemRectTransform = item.GetComponent<RectTransform>();
            itemRectTransform.SetParent(rectTransform);

            var position = GetPositionOnGrid(item, posX, posY);
            itemRectTransform.localPosition = position;
        }

        return res;
    }

    public void PlaceItem(BaseItem item, int posX, int posY)
    {
        item.ItemOwner = InventoryOwner;
        var itemRectTransform = item.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);

        var position = GetPositionOnGrid(item, posX, posY);
        itemRectTransform.localPosition = position;


        curInventoryState.PlaceItem(item, posX, posY);
    }

    public Vector2 GetPositionOnGrid(BaseItem item, int posX, int posY) =>
        new(posX * TileSize - posX + 0.5f + (TileSize-1) * item.Width / 2, -(posY * TileSize - posY + 0.5f + (TileSize-1) * item.Height / 2));


    public BaseItem PickUpItem(int x, int y)
    {
        var item = curInventoryState.PickUpItem(x, y);
        if (item is not null)
            item.ItemOwner = null;
        return item;
    }

    public void PickUpItem(BaseItem item)
    {
        if (item is null)
            return;
        item.ItemOwner = null;
        curInventoryState.PickUpItem(item.OnGridPositionX, item.OnGridPositionY);
    }

    public void DestroyItem(BaseItem item)
    {
        PickUpItem(item);
        Destroy(item.gameObject);
    }

    public void Clear()
    {
        foreach (var item in  curInventoryState.GetItems())
            Destroy(item.gameObject);
        curInventoryState?.Clear();
    }

    public bool InsertItem(BaseItem itemToInsert) => curInventoryState.InsertItem(itemToInsert);

    public bool PositionCheck(int posX, int posY) => curInventoryState.PositionCheck(posX, posY);

    public bool BoundryCheck(int posX, int posY, int width, int height) =>
        curInventoryState.BoundryCheck(posX, posY, width, height);

    public Vector2Int? FindSpaceForObject(BaseItem itemToInsert) => curInventoryState.FindSpaceForObject(itemToInsert);

    public BaseItem GetItem(int x, int y) => curInventoryState.GetItem(x, y);

    public IEnumerable<BaseItem> GetItems() => curInventoryState.GetItems();

    private void OnPlaceItem(BaseItem placedItem)
    {
        var position = GetPositionOnGrid(placedItem, placedItem.OnGridPositionX, placedItem.OnGridPositionY);
        var rt = placedItem.GetComponent<RectTransform>();
        rt.SetParent(transform);
        rt.localPosition = position;
    }

    private void OnPickedItem(BaseItem pickedUpItem)
    {
        pickedUpItem.GetComponent<RectTransform>().SetParent(canvas.transform);
    }
}