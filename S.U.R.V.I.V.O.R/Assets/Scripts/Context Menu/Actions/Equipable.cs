using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour, IContextMenuAction
{
    public string Text => "Equip";
    public void Action()
    {
        Debug.Log("Equip");
    }
}
