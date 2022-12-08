using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWearable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; }

    public Character Character{ get; set; }
    
    private Clothes currentClothes;

    public void Awake()
    {
        currentClothes = GetComponent<Clothes>();
    }

    public void OnButtonClickAction()
    {
        Debug.Log("Equip");
    }
}
