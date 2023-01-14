using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class BodyIndicator : MonoBehaviour
{
    [SerializeField] private Image head;
    [SerializeField] private Image chest;
    [SerializeField] private Image stomach;
    [SerializeField] private Image leftArm;
    [SerializeField] private Image leftLeg;
    [SerializeField] private Image rightArm;
    [SerializeField] private Image rightLeg;

    private List<Image> allImagesArray;

    private Character character;

    public Character Character
    {
        get => character;
        set
        {
            character = value;
            Init();
        }
    }

    public void Init()
    {
        ChangeBodyPartParameters(head,GetColorByNumber(character.body.Head.Hp/character.body.Head.MaxHp),character.body.Head.Hp);
        ChangeBodyPartParameters(chest,GetColorByNumber(character.body.Chest.Hp/character.body.Chest.Hp),character.body.Chest.Hp);
        ChangeBodyPartParameters(stomach,GetColorByNumber(character.body.Stomach.Hp/character.body.Stomach.Hp),character.body.Stomach.Hp);
        ChangeBodyPartParameters(leftArm,GetColorByNumber(character.body.LeftArm.Hp/character.body.LeftArm.Hp),character.body.LeftArm.Hp);
        ChangeBodyPartParameters(rightArm,GetColorByNumber(character.body.RightArm.Hp/character.body.RightArm.Hp),character.body.RightArm.Hp);
        ChangeBodyPartParameters(leftLeg,GetColorByNumber(character.body.LeftLeg.Hp/character.body.LeftLeg.Hp),character.body.LeftLeg.Hp);
        ChangeBodyPartParameters(rightLeg,GetColorByNumber(character.body.RightLeg.Hp/character.body.RightLeg.Hp),character.body.RightLeg.Hp);
    }

    private void ChangeBodyPartParameters(Image img, Color32 color, float numberOfHp)
    {
        img.color = color;
        var text = img.GetComponentInChildren<Text>();
        if (text != null)
            text.text = numberOfHp.ToString(CultureInfo.InvariantCulture);
    }
    
    private Color32 GetColorByNumber(float number)
    {
        switch (number)
        {
            case 0:
                return new Color32(0, 0, 0,255);
            case < 0.2f:
                return new Color32(214, 66, 31,255);
            case < 0.4f:
                return new Color32(211, 120, 20,255);
            case < 0.6f:
                return new Color32(219, 185, 0,255);
            case < 0.8f:
                return new Color32(173, 211, 26,255);
            default:
                return new Color32(101, 202, 31,255);
        }
    }
}
