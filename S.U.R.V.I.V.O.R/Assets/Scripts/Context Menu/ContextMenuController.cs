using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] private RectTransform panel;
    public Button buttonPrefab;
    private List<Button> storedButtons = new List<Button>();
    private bool isActive;

    private static ContextMenuController instance;

    public static ContextMenuController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ContextMenuController)) as ContextMenuController;
                if (instance == null)
                {
                    instance = new ContextMenuController();
                }
            }

            return instance;
        }
    }

    private void Update()
    {
        var inBounds = BoundaryCheckMouse(Input.mousePosition);
        if (panel.gameObject.activeSelf && !inBounds &&
            (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            Close();
        }
    }

    public void CreateContextMenu(List<IContextMenuAction> items, Vector2 mousePosition)
    {
        var scaleFactor = canvas.scaleFactor;
        panel.transform.SetParent(canvas.transform);
        panel.transform.SetAsLastSibling();
        var inScreen = BoundaryCheckScreen(mousePosition);
        if (!inScreen.Item2)
            mousePosition.y += panel.rect.height * scaleFactor;
        mousePosition.x = Math.Clamp(mousePosition.x, 0, canvas.GetComponent<RectTransform>().rect.width - panel.rect.width);
        
        panel.anchoredPosition = mousePosition / scaleFactor;

        foreach (var item in items)
        {
            var button = Instantiate(buttonPrefab, panel.transform, true);
            button.transform.localScale = Vector3.one;
            var buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
            buttonText.text = item.ButtonText;
            button.onClick.AddListener(delegate { item.OnButtonClickAction(mousePosition); });
            storedButtons.Add(button);
        }

        Show();
    }

    public void Clear()
    {
        foreach (var button in storedButtons)
        {
            Destroy(button.gameObject);
        }

        storedButtons.Clear();
    }

    private void Show()
    {
        if (isActive)
            return;
        panel.gameObject.SetActive(true);
        isActive = true;
    }

    private void Close()
    {
        if (!isActive)
            return;
        panel.gameObject.SetActive(false);
        isActive = false;
    }

    private bool BoundaryCheckMouse(Vector2 mousePosition)
    {
        var scaleFactor = canvas.scaleFactor;
        var position = this.panel.position;
        var rect = this.panel.rect;
        rect.size *= scaleFactor;
        return ((mousePosition.x > position.x) && (mousePosition.x < position.x + rect.width)) &&
               ((mousePosition.y < position.y) && (mousePosition.y > position.y - rect.height));
    }
    
    private (bool, bool) BoundaryCheckScreen(Vector2 mousePosition)
    {
        var scaleFactor = canvas.scaleFactor;
        var rect = canvas.GetComponent<RectTransform>().rect;
        var panelRect = this.panel.rect;
        panelRect.size *= scaleFactor;
        rect.size *= scaleFactor;
        return ((mousePosition.x > 0 && mousePosition.x + panelRect.width < rect.width), (mousePosition.y < rect.height && mousePosition.y - panelRect.height > 0));
    }
}