using UnityEngine;
using UnityEngine.UI;


public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }
    private InventoryGrid selectedInventoryGrid;

    public InventoryGrid SelectedInventoryGrid
    {
        get => selectedInventoryGrid;
        set
        {
            selectedInventoryGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }

    private BaseItem selectedItem;

    public BaseItem SelectedItem
    {
        get => selectedItem;
        set
        {
            if (selectedItem != null) //Вещь на курсоре есть
            {
                switch (value)
                {
                    case null: //Кладем вещь с курсора в ячейку
                        selectedItem.GetComponent<Image>().raycastTarget = true;
                        break;
                    case not null: //Меняем вещи местами
                        selectedItem.GetComponent<Image>().raycastTarget = true;
                        value.GetComponent<Image>().raycastTarget = false;
                        break;
                }
            }
            else
            {
                switch (value) //Вещи на курсоре нет
                {
                    case null: //Ничего не происходит
                        break;
                    case not null: //Берем вещь из ячейки
                        value.GetComponent<Image>().raycastTarget = false;
                        break;
                }
            }

            selectedItem = value;
        }
    }

    private BaseItem overlapItem;
    private RectTransform rectTransform;

    [SerializeField] private Transform canvasTransform;

    private InventoryHighlight inventoryHighlight;
    private BaseItem itemToHighlight;
    private Vector2Int? previousPosition;
    public bool IsPointerUnderInventory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Init();
        }
    }

    private void Init()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        if (SelectedItem != null)
            rectTransform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedInventoryGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        if (Input.GetMouseButtonDown(0) && IsPointerUnderInventory)
        {
            var mousePosition = Input.mousePosition;
            var tileGridPosition = GetTileGridPosition(mousePosition);

            if (SelectedItem == null)
            {
                PickUpItem(tileGridPosition);
            }
            else
            {
                PlaceItem(tileGridPosition);
            }
        }

        HandleHighlight();
        inventoryHighlight.transform.SetParent(inventoryHighlight.transform.parent);
    }

    public void PickUpItemFromSpecialCell(BaseItem item)
    {
        SelectedItem = item;
        ChangeRectTransform();
    }


    private void ChangeRectTransform()
    {
        if (SelectedItem == null) return;
        rectTransform = SelectedItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        SelectedItem = selectedInventoryGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        ChangeRectTransform();
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        var complete = selectedInventoryGrid.PlaceItem(SelectedItem, tileGridPosition.x, tileGridPosition.y,
            ref overlapItem);
        if (complete)
        {
            SelectedItem = null;
            if (overlapItem != null)
            {
                SelectedItem = overlapItem;
                overlapItem = null;
                rectTransform = SelectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    public void
        ThrowItemAtLocation(
            BaseItem item) // Перемещение предмета в инвентарь локации при нажатии на пустое пространство
    {
        var locationItemGrid = GameObject.FindGameObjectWithTag("LocationItemGrid").GetComponent<InventoryGrid>();
        var positionOnGrid = locationItemGrid.FindSpaceForObject(item);
        locationItemGrid.PlaceItem(item, positionOnGrid.Value.x, positionOnGrid.Value.y);
    }

    private void RotateItem()
    {
        if (SelectedItem == null) return;
        SelectedItem.Rotate();
    }

    private void HandleHighlight()
    {
        var mousePosition = Input.mousePosition;
        var positionOnGrid = GetTileGridPosition(mousePosition);
        if (!selectedInventoryGrid.PositionCheck(positionOnGrid.x, positionOnGrid.y))
            return;
        if (previousPosition == positionOnGrid) return;
        if (SelectedItem == null)
        {
            itemToHighlight = selectedInventoryGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedInventoryGrid, itemToHighlight);
                inventoryHighlight.transform.SetAsLastSibling();
                inventoryHighlight.SetParent(selectedInventoryGrid);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedInventoryGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y,
                SelectedItem.Width, SelectedItem.Height));
            inventoryHighlight.SetSize(SelectedItem);
            inventoryHighlight.SetPosition(selectedInventoryGrid, SelectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private Vector2Int GetTileGridPosition(Vector3 mousePosition)
    {
        if (SelectedItem != null)
        {
            mousePosition.x -= (SelectedItem.Width - 1) * InventoryGrid.TileSize / 2;
            mousePosition.y += (SelectedItem.Height - 1) * InventoryGrid.TileSize / 2;
        }

        return selectedInventoryGrid.GetTileGridPosition(mousePosition);
    }

    public void AddItemToInventory(BaseItem item)
    {
        if (SelectedItem != null) return;
        CreateItem(item);
        var itemToInsert = SelectedItem;
        SelectedItem = null;
        InsertItem(itemToInsert);
        selectedInventoryGrid = null;
    }

    private void InsertItem(BaseItem itemToInsert)
    {
        var positionOnGrid = selectedInventoryGrid.FindSpaceForObject(itemToInsert);
        if (positionOnGrid == null)
        {
            Destroy(itemToInsert);
            return;
        }

        selectedInventoryGrid.PlaceItem(itemToInsert, positionOnGrid.Value.x, positionOnGrid.Value.y);
    }

    private void CreateItem(BaseItem item)
    {
        if (SelectedItem != null) return;
        var inventoryItem = Instantiate(item);
        SelectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
    }
}