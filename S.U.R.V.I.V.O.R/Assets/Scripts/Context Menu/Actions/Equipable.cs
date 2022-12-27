using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour, IContextMenuAction
{
    public string Text { get; }
    public void Action()
    {
        Debug.Log("Equip");
    }
}
