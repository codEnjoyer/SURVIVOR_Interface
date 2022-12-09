using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWearable : MonoBehaviour, IContextMenuAction
{
    public string ButtonText { get; private set; }

    public Character Character{ get; set; }
    
    private Clothes currentClothes;
    

    public void Awake()
    {
        ButtonText = "Надеть";
        currentClothes = GetComponent<Clothes>();
    }
    public void OnButtonClickAction(Vector2 mousePosition)
    {
        currentClothes.GetComponent<BaseItem>().ItemOwner.body.Wear(currentClothes,false,out var isSucessful);

        if (!isSucessful)
            Debug.Log($"Одежда {currentClothes} не может быть надета");
        else
            ItemPickedUp?.Invoke(currentClothes.GetComponent<BaseItem>());
    }

    public event Action<BaseItem> ItemPickedUp;
}
