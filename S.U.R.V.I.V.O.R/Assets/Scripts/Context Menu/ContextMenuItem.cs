using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ContextMenuItem : MonoBehaviour, IPointerClickHandler
{
    private List<IContextMenuAction> contextMenuActions; 
    
    private void Start()
    {
        contextMenuActions = GetComponents<IContextMenuAction>().ToList();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        var position = Input.mousePosition;
        ContextMenuController.Instance.Clear();
        ContextMenuController.Instance.CreateContextMenu(contextMenuActions, new Vector2(position.x, position.y));
    }
}
