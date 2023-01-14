using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ContextMenuItem))]
public class Wearable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    
    public bool Extendable { get; private set;}

    private Clothes currentClothes;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Надеть";
        Extendable = true;
        currentClothes = GetComponent<Clothes>();
        item = GetComponent<BaseItem>();
    }


    public void OnButtonClickAction<T>(T value)
    {
        var inventory = item.InventoryGrid;
        var character = value as Character;
        inventory.PickUpItem(item);
        var isSuccessful = character.body.Wear(currentClothes);
        
        if (!isSuccessful)
            inventory.InsertItem(item);
    }

    public IEnumerable GetValues()
    {
        var result = new List<Tuple<Character, string>>();
        foreach (var character in Game.Instance.ChosenGroup.CurrentGroupMembers)
        {
            result.Add(new Tuple<Character, string>(character, $"{character.FirstName} {character.Surname}"));
        }
        return result;
    }
}
