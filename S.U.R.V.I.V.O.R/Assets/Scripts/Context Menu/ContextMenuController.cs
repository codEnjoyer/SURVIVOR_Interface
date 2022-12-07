using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] private RectTransform panel;
    public Button buttonPrefab;
    private List<Button> storedButtons = new List<Button>();

    private static ContextMenuController instance;
    
    public static ContextMenuController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(ContextMenuController)) as ContextMenuController;
                if(instance == null)
                {
                    instance = new ContextMenuController();
                }
            }
            return instance;
        }
    }
    
    public void CreateContextMenu(List<IContextMenuAction> items, Vector2 position)
    {
        panel.transform.SetParent(canvas.transform);
        panel.transform.SetAsLastSibling();
        panel.anchoredPosition = position;

        foreach(var item in items)
        {
            var button = Instantiate(buttonPrefab, panel.transform, true);
            var buttonText = button.GetComponentInChildren(typeof(Text)) as Text;
            buttonText.text = item.ButtonText;
            // button.onClick.AddListener(delegate { item.Action(); });
            storedButtons.Add(button);
        }
    }

    public void Clear()
    {
        foreach (var button in storedButtons)
        {
            Destroy(button);
        }
    }
}
