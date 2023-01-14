using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject extendedMenuPrefab;
    [SerializeField] private Button extendedMenuButtonPrefab;
    private readonly List<Button> storedButtons = new List<Button>();
    private bool isActive;
    private RectTransform extendedMenu;
    private float scaleFactor;

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

    private void Awake()
    {
        scaleFactor = Game.Instance.MainCanvas.scaleFactor;
    }

    private void Update()
    {
        var inBoundsMainMenu = BoundaryCheckMouse(Input.mousePosition, mainMenu);
        var inBoundsExtendedMenu = extendedMenu != null && BoundaryCheckMouse(Input.mousePosition, extendedMenu);
        if (mainMenu.gameObject.activeSelf && !inBoundsMainMenu && !inBoundsExtendedMenu &&
            (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            Close();
        }
    }

    public void CreateContextMenu(List<IContextMenuAction> actions, Vector2 mousePosition)
    {
        mainMenu.transform.SetParent(canvas.transform);
        mainMenu.transform.SetAsLastSibling();

        foreach (var action in actions)
        {
            var button = CreateButton(buttonPrefab, mainMenu.transform, action.ButtonText);
            button.onClick.AddListener(delegate
            {
                if (action.Extendable)
                {
                    extendedMenu = InitializeExtendedMenu(button.transform.position, button.GetComponent<RectTransform>());
                    var extendedActions = action.GetValues();
                    foreach (ITuple extendedAction in extendedActions)
                    {
                        var extendedButton = CreateButton(extendedMenuButtonPrefab, extendedMenu.transform, (string) extendedAction[1]);
                        extendedButton.onClick.AddListener(delegate
                        {
                            action.OnButtonClickAction(extendedAction[0]);
                            Close();
                        });
                    }

                    extendedMenu.transform.position = AdjustPositionExtendedMenu(button.GetComponent<RectTransform>());
                }
                else
                {
                    action.OnButtonClickAction((object)null);
                    Close();
                }
            });
            storedButtons.Add(button);
        }
        
        mainMenu.position = mousePosition;
        mainMenu.sizeDelta = new Vector2(mainMenu.sizeDelta.x,
            buttonPrefab.GetComponent<RectTransform>().rect.height * storedButtons.Count);
        mainMenu.position = AdjustPositionMainMenu();
        
        Show();
    }

    private Vector3 AdjustPositionMainMenu()
    {
        var inScreen = BoundaryCheckScreen(mainMenu);
        var positionMainMenu = mainMenu.position;
        if (!inScreen.Item2)
            positionMainMenu.y += mainMenu.rect.height * scaleFactor;
        positionMainMenu.x = Math.Clamp(positionMainMenu.x, 0, canvas.GetComponent<RectTransform>().rect.width - mainMenu.rect.width);
        return positionMainMenu / scaleFactor;
    }
    
    private Vector3 AdjustPositionExtendedMenu(RectTransform parentButton)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(extendedMenu);
        var positionExtendedMenu = extendedMenu.position;
        var inScreenExtend = BoundaryCheckScreen(extendedMenu.transform);
        if (!inScreenExtend.Item1)
            positionExtendedMenu.x -= (extendedMenu.rect.width + parentButton.rect.width) * scaleFactor;
        if (!inScreenExtend.Item2)
            positionExtendedMenu.y += (extendedMenu.rect.height - parentButton.rect.height) * scaleFactor;
        return positionExtendedMenu / scaleFactor;
    }
    
    private RectTransform InitializeExtendedMenu(Vector3 position, RectTransform parentButton)
    {
        if (extendedMenu != null) Destroy(extendedMenu.gameObject);
        position.x += extendedMenuPrefab.GetComponent<RectTransform>().rect.width;
        return Instantiate(extendedMenuPrefab, position, Quaternion.identity,  parentButton).GetComponent<RectTransform>();
    }

    private Button CreateButton(Button prefab, Transform parent, String text)
    {
        var button = Instantiate(prefab, parent, true);
        button.transform.localScale = Vector3.one;
        var buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
        buttonText.text = text;
        return button;
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
        mainMenu.gameObject.SetActive(true);
        isActive = true;
    }

    private void Close()
    {
        if (!isActive)
            return;
        mainMenu.gameObject.SetActive(false);
        isActive = false;
    }

    private bool BoundaryCheckMouse(Vector2 mousePosition, Transform menu)
    {
        var position = menu.position;
        var rect = menu.GetComponent<RectTransform>().rect;
        rect.size *= scaleFactor;
        return ((mousePosition.x > position.x) && (mousePosition.x < position.x + rect.width)) &&
               ((mousePosition.y < position.y) && (mousePosition.y > position.y - rect.height));
    }
    
    private (bool, bool) BoundaryCheckScreen(Transform menu)
    {
        var canvasRect = canvas.GetComponent<RectTransform>().rect;
        var menuRect = menu.GetComponent<RectTransform>().rect;
        var menuPos = menu.position;
        menuRect.size *= scaleFactor;
        canvasRect.size *= scaleFactor;
        return ((menuPos.x > 0 && menuPos.x + menuRect.width < canvasRect.width), (menuPos.y < canvasRect.height && menuPos.y - menuRect.height > 0));
    }
}