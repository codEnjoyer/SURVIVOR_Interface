using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(ContextMenuItem))]
public class Reloadable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }
    public bool Extendable { get; private set;}

    private Magazine currentMagazine;

    private BaseItem item;
    

    public void Awake()
    {
        ButtonText = "Зарядить";
        Extendable = true;
        currentMagazine = GetComponent<Magazine>();
        item = GetComponent<BaseItem>();
    }


    public void OnButtonClickAction<T>(T value)
    {
        var inventory = item.InventoryGrid;
        var ammoBox = value as AmmoBox;
        currentMagazine.Load(ammoBox);
        if (ammoBox != null && ammoBox.CurrentNumberAmmo == 0)
        {
            Destroy(gameObject);
        }
    }
    
    public virtual IEnumerable GetValues()
    {
        var result = new List<Tuple<AmmoBox, string>>();
        foreach (var character in GlobalMapController.Instance.ChosenGroup.CurrentGroupMembers)
        {
            foreach (var ammoBox in character.GetItemsFromAllInventoriesByType<AmmoBox>())
            {
                if (ammoBox.Data.Caliber == GetComponent<Magazine>().Data.Caliber)
                    result.Add(new Tuple<AmmoBox, string>(ammoBox, $"{character.FirstName} {ammoBox.Data.Caliber} {ammoBox.Data.AmmoType.TypeOfSingleAmmo} {ammoBox.CurrentNumberAmmo} шт"));
            }
        }
        return result;
    }
}