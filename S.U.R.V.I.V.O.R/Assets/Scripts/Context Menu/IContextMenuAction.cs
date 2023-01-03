using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IContextMenuAction
{
    public string ButtonText { get; }
    
    public void OnButtonClickAction(Vector2 mousePosition);
}
