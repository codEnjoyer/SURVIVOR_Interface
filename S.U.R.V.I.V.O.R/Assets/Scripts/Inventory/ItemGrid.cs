using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemGrid : MonoBehaviour
{
    [SerializeField] private Size size;
    
    public InventoryState curInventoryState { get; private set; }
    
    public Character InventoryOwner { get; set; }
    
    private Canvas canvas;
    [SerializeField] private InventoryGridBackground inventoryGridBG;

    public InventoryGridBackground InventoryGridBg => inventoryGridBG;

    public const float TileSize = 40;

    private Vector2 positionOnGrid;
    private Vector2Int tileGridPosition;

    private RectTransform rectTransform;

    public int GridSizeWidth => curInventoryState.Size.Width;
    public int GridSizeHeight => curInventoryState.Size.Height;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        curInventoryState = new InventoryState(size);
        Init(size.Width, size.Height);
    }

    private void Init(int width, int height)
    {
        inventoryGridBG.DrawBackground(this);
        rectTransform.sizeDelta = new Vector2(width * TileSize, height * TileSize);
    }

    public void ChangeState(InventoryState inventoryState)
    {
        curInventoryState = inventoryState;
        GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryState.Size.Width * TileSize, inventoryState.Size.Height  * TileSize);
        RedrawGrid();
        inventoryGridBG.DrawBackground(this);
    }

    private void RedrawGrid()
    {
        DestroyAllItems();
        
        foreach (var item in curInventoryState.GetItems)
        {
            item.gameObject.SetActive(true);
            var position = GetPositionOnGrid(item, item.OnGridPositionX, item.OnGridPositionY);
            item.GetComponent<RectTransform>().SetParent(transform);
            item.GetComponent<RectTransform>().localPosition = position;
        }
    }
    
    private void DestroyAllItems()
    {
        foreach (var item in curInventoryState.GetItems)
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

    public bool InsertItem(BaseItem itemToInsert) => curInventoryState.InsertItem(itemToInsert);
    
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
        new(posX * TileSize + TileSize * item.Width / 2, -(posY * TileSize + TileSize * item.Height / 2));


    public BaseItem PickUpItem(int x, int y)
    {
        var item = curInventoryState.PickUpItem(x, y);
        item.ItemOwner = null;
        return item;
    }

    public void Clear()
    {
        foreach (var item in curInventoryState.GetItems)
        {
            Destroy(item.gameObject);
        }
        curInventoryState?.Clear();
    }
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => curInventoryState.BoundryCheck(posX, posY, width, height);

    public Vector2Int? FindSpaceForObject(BaseItem itemToInsert) => curInventoryState.FindSpaceForObject(itemToInsert);

    public BaseItem GetItem(int x, int y) => curInventoryState.GetItem(x, y);

    public IEnumerable<BaseItem> GetItems => curInventoryState.GetItems;
}