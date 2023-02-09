using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Entities.Characters;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCartLogic : MonoBehaviour
{
    [SerializeField] private ChangeCharacterOnButtonInfo nextCharacterButton;
    [SerializeField] private ChangeCharacterOnButtonInfo previousCharacterButton;
    [SerializeField] private PlayerLayerLogic playerLayerLogic;

    private Character currentCharacter; 
    private Character previousCharacter; 
    private Character nextCharacter;

    public Character CurrentCharacter
    {
        set
        {
            playerLayerLogic.CurrentCharacter = value;
            currentCharacter = value;
        }
    }

    public Character PreviousCharacter
    {
        set
        {
            previousCharacterButton.CurrentCharacter = value;
            previousCharacter = value;
        }
    }

    public Character NextCharacter
    {
        set
        {
            nextCharacterButton.CurrentCharacter = value;
            nextCharacter = value;
        }
    }

    public void ReDraw(PlayerCartReDrawInfo cartInfo)
    {
        CurrentCharacter = cartInfo.currentCharacter;
        PreviousCharacter = cartInfo.previousCharacter;
        NextCharacter = cartInfo.nextCharacter;
    }

    public void Init(PlayerCartReDrawInfo cart)
    {
        nextCharacterButton.Init();
        previousCharacterButton.Init();
        ReDraw(cart);
    }

    public void DoSomethingIfYouAreNotNeeded()
    {
        
    }
}
