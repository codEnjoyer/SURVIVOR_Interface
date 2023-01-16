using System;
using System.Collections;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class BaseItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [FormerlySerializedAs("itemData")] [SerializeField]
    private BaseItemData data;

    public int OnGridPositionX { get; set; }
    public int OnGridPositionY { get; set; }

    public InventoryGrid InventoryGrid => transform.GetComponentInParent<InventoryGrid>();

    public Character ItemOwner { get; set; }

    public Vector3 OnAwakeRectTransformSize { get; set; }

    public Vector3 OnAwakeRectTransformScale { get; set; }

    public int Height => !rotated ? Size.Height : Size.Width;

    public int Width => !rotated ? Size.Width : Size.Height;

    public bool rotated { get; set; }
    public Size Size => data.Size;
    public float Weight => data.Weight;
    public BaseItemData Data => data;

    public void Awake()
    {
        if (data == null || data.Icon == null)
            return;

        gameObject.AddComponent<Image>().sprite = data.Icon;
        gameObject.GetComponent<Image>().raycastTarget = false;

        var rt = gameObject.GetComponent<RectTransform>();
        var scaleFactor = Game.Instance.MainCanvas.scaleFactor;
        var size = new Vector2(((data.Size.Width * InventoryGrid.TileSize) - data.Size.Width - 1) * scaleFactor,
            ((data.Size.Height * InventoryGrid.TileSize) - data.Size.Height - 1) * scaleFactor);
        rt.sizeDelta = size;
        OnAwakeRectTransformScale = rt.localScale;
        OnAwakeRectTransformSize = rt.sizeDelta;
    }

    public void Rotated()
    {
        rotated = !rotated;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90 : 0);
    }

    public void Destroy()
    {
        InventoryGrid.PickUpItem(this);
        Destroy(gameObject);
    }

    #region TooltipRegin

    private bool mouseEnter;
    const float Seconds = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter = true;
        StartCoroutine(ShowTooltipCoroutine());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEnter = false;
        Tooltip.Instance.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mouseEnter = false;
        Tooltip.Instance.HideTooltip();
    }


    IEnumerator ShowTooltipCoroutine()
    {
        yield return new WaitForSeconds(Seconds);
        if (mouseEnter && !ContextMenuController.Instance.IsActive)
            Tooltip.Instance.ShowTooltip(data.Name);
    }

    #endregion
}