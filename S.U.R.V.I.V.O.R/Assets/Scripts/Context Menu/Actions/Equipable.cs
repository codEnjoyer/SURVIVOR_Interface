using System;
using System.Collections;
using System.Collections.Generic;
using Model.Entities.Characters;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Equipable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    
    public bool Extendable { get; private set;}

    private Gun currentGun;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Экипировать";
        Extendable = true;
        currentGun = GetComponent<Gun>();
        item = GetComponent<BaseItem>();
    }


    public void OnButtonClickAction<T>(T value)
    {
        currentGun.gameObject.SetActive(false);
        var inventory = item.InventoryGrid;
        var character = value as Character;
        inventory.PickUpItem(item);
        switch (currentGun.Data.GunType)
        {
            case GunType.PrimaryGun:
                character.PrimaryGun = currentGun;
                break;
            case GunType.SecondaryGun:
                character.SecondaryGun = currentGun;
                break;
        }
    }
}
