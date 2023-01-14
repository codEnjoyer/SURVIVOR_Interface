using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacterOnButtonInfo : MonoBehaviour
{
    [SerializeField] private bool isButtonOnLeftCart;
    [SerializeField] private bool isButtonUpper;
    [SerializeField] private GroupLayerLogic groupLayerLogic;
    [SerializeField] private BodyIndicator characterBodyIndicator;
    [SerializeField] private PlayerCharacteristicsPanel playerCharacteristicsPanel;
    public Button button;

    private Character currentCharacter;

    public Character CurrentCharacter
    {
        set
        {
            characterBodyIndicator.Character = value;
            playerCharacteristicsPanel.Player = value;
            currentCharacter = value;
        }
    }

    public void Init()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        groupLayerLogic.ChangeCharactersOnGroupLayer(isButtonOnLeftCart, isButtonUpper);
    }
}
