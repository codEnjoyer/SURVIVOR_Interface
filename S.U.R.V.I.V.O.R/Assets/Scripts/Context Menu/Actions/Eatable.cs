using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Eatable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    public bool Extendable { get; private set;}

    private EatableFood currentFood;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Употребить";
        Extendable = true;
        currentFood = GetComponent<EatableFood>();
        item = GetComponent<BaseItem>();
    }


    public void OnButtonClickAction<T>(T value)
    {
        var character = value as Character;
        character.Eat(currentFood);
    }
}
