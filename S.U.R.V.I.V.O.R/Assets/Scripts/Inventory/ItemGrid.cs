using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemGrid : MonoBehaviour
{
    [SerializeField] private Size size;
    
    public InventoryState curInventoryState { get; private set; }
    
    private Canvas canvas;
    [SerializeField] private InventoryGridBackground inventoryGridBG;
    
    public const float TileSize = 50;

    private Vector2 positionOnGrid;
    private Vector2Int tileGridPosition;

    private readonly List<InventoryItem> instantiateItems = new List<InventoryItem>();

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
        rectTransform.sizeDelta = new Vector2(inventoryState.Size.Width * TileSize, inventoryState.Size.Height  * TileSize);
        RedrawGrid();
        inventoryGridBG.DrawBackground(this);
    }

    private void RedrawGrid()
    {
        DestroyAllItems();
        
        foreach (var item in curInventoryState.GetItems)
        {
            instantiateItems.Add(item);
            item.gameObject.SetActive(true);
            var position = GetPositionOnGrid(item, item.onGridPositionX, item.onGridPositionY);
            item.GetComponent<RectTransform>().localPosition = position;
        }
    }
    
    private void DestroyAllItems()
    {
        foreach (var item in instantiateItems)
            item.gameObject.SetActive(false);

        instantiateItems.Clear();
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

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapInventoryItem)
    {
        var res = curInventoryState.PlaceItem(inventoryItem, posX, posY, ref overlapInventoryItem);
        if (res)
            PlaceItem(inventoryItem, posX, posY);
        return res;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        var itemRectTransform = inventoryItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);
        
        var position = GetPositionOnGrid(inventoryItem, posX, posY);
        itemRectTransform.localPosition = position;
        
        instantiateItems.Add(inventoryItem);

        curInventoryState.PlaceItem(inventoryItem, posX, posY);
    }

    public Vector2 GetPositionOnGrid(InventoryItem inventoryItem, int posX, int posY) => 
        new(posX * TileSize + TileSize * inventoryItem.Width / 2, -(posY * TileSize + TileSize * inventoryItem.Height / 2));
    

    public InventoryItem PickUpItem(int x, int y) => curInventoryState.PickUpItem(x, y);
    
    public bool BoundryCheck(int posX, int posY, int width, int height) => curInventoryState.BoundryCheck(posX, posY, width, height);

    public Vector2Int? FindSpaceForObject(InventoryItem inventoryItemToInsert) => curInventoryState.FindSpaceForObject(inventoryItemToInsert);

    public InventoryItem GetItem(int x, int y) => curInventoryState.GetItem(x, y);

    public IEnumerable<InventoryItem> GetItems => curInventoryState.GetItems;
}