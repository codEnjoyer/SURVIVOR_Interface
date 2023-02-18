using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Model;
using Model.Entities.Characters;
using Model.Player;
using UnityEngine;

public class PlayerCartReDrawInfo
{
    public Character previousCharacter;
    public Character currentCharacter;
    public Character nextCharacter;
        
    public PlayerCartReDrawInfo(Character previous, Character current, Character next)
    {
        nextCharacter = next;
        currentCharacter = current;
        previousCharacter = previous;
    }

    public void Clear()
    {
        previousCharacter = null;
        currentCharacter = null;
        nextCharacter = null;
    }
}

public class GroupLayerLogic : MonoBehaviour
{

    [SerializeField] private PlayerCartLogic leftCart;

    [SerializeField] private PlayerCartLogic rightCart;
    
    [SerializeField] private List<Character> charactersList;

    private PlayerCartReDrawInfo leftCartReDrawInfo;
    private PlayerCartReDrawInfo rightCartReDrawInfo;
    
    public void ChangeCharactersOnGroupLayer(bool isSwitchOnLeftCart, bool isSwitchOnUpCharacter)
    {
        SwitchPointers(isSwitchOnLeftCart ? leftCartReDrawInfo : rightCartReDrawInfo,
            !isSwitchOnLeftCart ? leftCartReDrawInfo : rightCartReDrawInfo,
            isSwitchOnUpCharacter);
        
        // Debug.Log(leftCartReDrawInfo.previousCharacter);
        // Debug.Log(leftCartReDrawInfo.currentCharacter);
        // Debug.Log(leftCartReDrawInfo.nextCharacter);
        // Debug.Log("-----------------------");
        // Debug.Log(rightCartReDrawInfo.previousCharacter);
        // Debug.Log(rightCartReDrawInfo.currentCharacter);
        // Debug.Log(rightCartReDrawInfo.nextCharacter);
        
        leftCart.ReDraw(leftCartReDrawInfo);
        rightCart.ReDraw(rightCartReDrawInfo);
        
    }

    private void SwitchPointers(PlayerCartReDrawInfo cartToSwitch, PlayerCartReDrawInfo cartToRedraw, bool isSearchForward)
    {
        if (isSearchForward)
        {
            SwitchPoints(ref cartToSwitch.nextCharacter,ref cartToSwitch.currentCharacter, ref cartToSwitch.previousCharacter, isSearchForward);
        }
        else
        {
            SwitchPoints(ref cartToSwitch.previousCharacter, ref cartToSwitch.currentCharacter,
                ref cartToSwitch.nextCharacter, isSearchForward);
        }
        ReDrawCart(cartToRedraw);

        void ReDrawCart(PlayerCartReDrawInfo cart)
        {
            var index = charactersList.IndexOf(cart.currentCharacter);
            cart.previousCharacter = FindCharacter(index, false);
            cart.nextCharacter = FindCharacter(index, true);
        }
        
        void SwitchPoints(ref Character characterBehind, ref Character characterCurrent,
            ref Character characterForward, bool isSearchForward)
        {
            var characterToChange = FindCharacter(charactersList.IndexOf(characterCurrent), isSearchForward);
            if (characterToChange == null)//Оба персонажа демонстрируются
            {
                characterBehind = characterCurrent;
                characterForward = characterCurrent;
                return;
            }
            characterCurrent = characterToChange;
            var characterToChangeForward = FindCharacter(charactersList.IndexOf(characterCurrent), !isSearchForward);
            var characterToChangeBehind = FindCharacter(charactersList.IndexOf(characterCurrent), isSearchForward);
            characterBehind = characterToChangeBehind == null ? characterCurrent : characterToChangeBehind;
            characterForward = characterToChangeForward == null ? characterCurrent : characterToChangeForward;
        }
        
        Character FindCharacter(int index, bool isSearchForward)
        {
            if (index == -1)
                return null;
            var iter = index;
            while (true)
            {
                var lenght = charactersList.Count;
                if (lenght == 2 && leftCartReDrawInfo.currentCharacter != rightCartReDrawInfo.currentCharacter)
                    return charactersList[index];
                var currentCharacter = charactersList[iter];
                if (leftCartReDrawInfo.currentCharacter != currentCharacter &&
                    rightCartReDrawInfo.currentCharacter != currentCharacter)
                    return currentCharacter;
                iter = !isSearchForward ? iter - 1 : iter + 1;

                if (iter == index) 
                    return null;
                if (iter == -1) 
                    iter = lenght - 1;
                if (iter == lenght) 
                    iter = 0;
            }
        }
    }

    public void Init()
    {
        charactersList = GlobalMapController.Instance.ChosenGroup.CurrentGroupMembers.ToList();
        GlobalMapController.Instance.ChosenGroupChange += OnGroupChanged;
        leftCartReDrawInfo = new PlayerCartReDrawInfo(null, null, null);
        rightCartReDrawInfo = new PlayerCartReDrawInfo(null, null, null);
        ReCalculateCartsInfo();
        leftCart.Init(leftCartReDrawInfo);
        rightCart.Init(rightCartReDrawInfo);
    }

    private void ReCalculateCartsInfo()
    {
        leftCartReDrawInfo.currentCharacter = charactersList[0];
        SwitchPointers(leftCartReDrawInfo,rightCartReDrawInfo,true);
        if (charactersList.Count > 1)
        {
            rightCartReDrawInfo.currentCharacter = charactersList[1];
            SwitchPointers(rightCartReDrawInfo,leftCartReDrawInfo,true);
        }
        else
        {
            rightCart.DoSomethingIfYouAreNotNeeded();
        }
    }
    
    private void OnGroupChanged(Group oldGroup, Group newGroup)
    {
        charactersList = newGroup.CurrentGroupMembers.ToList();
        ReCalculateCartsInfo();
    }
}
