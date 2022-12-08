using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ContextMenuItem : MonoBehaviour, IPointerClickHandler
{
    private List<IContextMenuAction> contextMenuActions;

    public UnityEvent Action;

    private void Start()
    {
        contextMenuActions = GetComponents<IContextMenuAction>().ToList();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        ContextMenuController.Instance.Clear();
        ContextMenuController.Instance.CreateContextMenu(contextMenuActions, Input.mousePosition);
    }
}