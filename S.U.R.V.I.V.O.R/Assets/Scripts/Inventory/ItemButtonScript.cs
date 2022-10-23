using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonScript : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public Item item;
    void Start()
    {
        
    }
    

    public void SetUpButton(Item passedItem)
    {
        item = passedItem;
        iconImage.sprite = passedItem.data.itemIcon;
    }
}
