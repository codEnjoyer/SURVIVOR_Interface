using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpecialClothCell : SpecialCell
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private ClothType type;
    private RectTransform rectTransform;
    public Character currentCharacter { get; set; }

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public override void PlaceItem(BaseItem item)
    {
        if (item.rotated)
            item.Rotated();
        placedItem = item;
        ChangeCharacterClothes();
        InventoryController.SelectedItem = null;
    }
    
    public override void GiveItem()
    {
        if (PlacedItem == null) return;
        PlacedItem.GetComponent<RectTransform>().sizeDelta = PlacedItem.OnAwakeRectTransformSize;
        PlacedItem.GetComponent<RectTransform>().localScale = PlacedItem.OnAwakeRectTransformScale;
        PlacedItem.GetComponent<RectTransform>().SetParent(canvasTransform);
        InventoryController.PickUpItem(PlacedItem);
        PlaceNullItem();
        ChangeCharacterClothes();
    }

    protected override bool CanInsertIntoSlot()
    {
        return InventoryController.SelectedItem.GetComponent<Clothes>() &&
               InventoryController.SelectedItem.GetComponent<Clothes>().Data.ClothType == type;
    }

    private void ChangeCharacterClothes()
    {
        var currentCloth = placedItem?.GetComponent<Clothes>();
        switch (type)
        {
            case ClothType.Backpack:
                currentCharacter.body.chest.Backpack = currentCloth;
                break;
            case ClothType.Jacket:
                currentCharacter.body.chest.Jacket = currentCloth;
                break;
            case ClothType.Underwear:
                currentCharacter.body.chest.Underwear = currentCloth;
                break;
            case ClothType.Boots:
                currentCharacter.body.leftLeg.Boots = currentCloth;
                currentCharacter.body.rightLeg.Boots = currentCloth;
                break;
            case ClothType.Hat:
                currentCharacter.body.head.Hat = currentCloth;
                break;
            case ClothType.Pants:
                currentCharacter.body.stomach.Pants = currentCloth;
                break;
            case ClothType.Vest:
                currentCharacter.body.chest.Vest = currentCloth;
                break;

        }  
    }
}
