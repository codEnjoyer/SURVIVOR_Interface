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

    public string ButtonText { get; }
    
    public bool Extendable { get; }

    public void OnButtonClickAction<T>(T value)
    {
        throw new NotImplementedException();
    }

    public IEnumerable GetValues()
    {
        throw new NotImplementedException();
    }
}
