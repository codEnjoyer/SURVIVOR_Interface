using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    enum InterfaceState
    {
        GroupLayerActive,
        PlayerLayerActive,
        CharacterPanelActive,
        NothingActive
    }
    [SerializeField]
    private GameObject MainInfoPanelLayer;
    [SerializeField]
    private GameObject GroupButtonsLayer;
    [SerializeField]
    private GameObject GroupInfoLayer;
    [SerializeField]
    private GameObject CharactersButtonsLayer;
    [SerializeField]
    private GameObject FirstPlayerLayer;
    [SerializeField]
    private GameObject SecondPlayerLayer;
    [SerializeField]
    private GameObject ThirdPlayerLayer;
    [SerializeField]
    private GameObject FourthPlayerLayer;

    private GameObject activePlayerLayer;

    private InterfaceState currentState;

    private bool IsPlayerWasOnCharactersPlate;

    void Awake()
    {
        FirstPlayerLayer.SetActive(false);
        SecondPlayerLayer.SetActive(false);
        ThirdPlayerLayer.SetActive(false);
        FourthPlayerLayer.SetActive(false);
        MainInfoPanelLayer.SetActive(true);
        GroupButtonsLayer.SetActive(true);
        GroupInfoLayer.SetActive(false);
        CharactersButtonsLayer.SetActive(false);
        currentState = InterfaceState.NothingActive;

    }

    public void SetCharactersPanelActive()
    {
        switch (currentState)
        {
            case InterfaceState.GroupLayerActive:
                GroupButtonsLayer.SetActive(true);
                GroupInfoLayer.SetActive(false);
                CharactersButtonsLayer.SetActive(true);
                break;
            case InterfaceState.PlayerLayerActive:
                activePlayerLayer.SetActive(false);
                break;
            case InterfaceState.NothingActive:
                CharactersButtonsLayer.SetActive(true);
                break;
            case InterfaceState.CharacterPanelActive:
                CharactersButtonsLayer.SetActive(false);
                currentState = InterfaceState.NothingActive;
                return;
        }
        currentState = InterfaceState.CharacterPanelActive;
    }

    public void SetGroupLayerActive()
    {
        switch (currentState)
        {
            case InterfaceState.GroupLayerActive:
                if (IsPlayerWasOnCharactersPlate)
                {
                    CharactersButtonsLayer.SetActive(true);
                    IsPlayerWasOnCharactersPlate = false;
                    currentState = InterfaceState.CharacterPanelActive;
                }
                else
                {
                    currentState = InterfaceState.NothingActive;
                }
                GroupButtonsLayer.SetActive(true);
                GroupInfoLayer.SetActive(false);
                return;
            case InterfaceState.NothingActive:
                GroupButtonsLayer.SetActive(false);
                GroupInfoLayer.SetActive(true);
                break;
            case InterfaceState.PlayerLayerActive:
                IsPlayerWasOnCharactersPlate = true;
                activePlayerLayer.SetActive(false);
                CharactersButtonsLayer.SetActive(false);
                GroupInfoLayer.SetActive(true);
                break;
            case InterfaceState.CharacterPanelActive:
                IsPlayerWasOnCharactersPlate = true;
                GroupButtonsLayer.SetActive(false);
                CharactersButtonsLayer.SetActive(false);
                GroupInfoLayer.SetActive(true);
                break;
        }
        currentState = InterfaceState.GroupLayerActive;
    }

    private void SetPlayerLayerActive(GameObject characterLayer)
    {
        switch (currentState)
        {
            case InterfaceState.GroupLayerActive:
                GroupInfoLayer.SetActive(false);
                CharactersButtonsLayer.SetActive(true);
                characterLayer.SetActive(true);
                break;
            case InterfaceState.PlayerLayerActive:
                if (characterLayer.activeInHierarchy)
                {
                    characterLayer.SetActive(false);
                    break;
                }
                activePlayerLayer.SetActive(false);
                characterLayer.SetActive(true);
                break;
            case InterfaceState.CharacterPanelActive:
                GroupButtonsLayer.SetActive(true);
                characterLayer.SetActive(true);
                break;
        }
        currentState = InterfaceState.PlayerLayerActive;
    }


    public void ChooseFirstPlayer()
    {
        SetPlayerLayerActive(FirstPlayerLayer);
        activePlayerLayer = FirstPlayerLayer;
    }
    public void ChooseSecondPlayer()
    {
        SetPlayerLayerActive(SecondPlayerLayer);
        activePlayerLayer = SecondPlayerLayer;
    }
    public void ChooseThirdPlayer()
    {
        SetPlayerLayerActive(ThirdPlayerLayer);
        activePlayerLayer = ThirdPlayerLayer;
    }
    public void ChooseFourthPlayer()
    {
        SetPlayerLayerActive(FourthPlayerLayer);
        activePlayerLayer = FourthPlayerLayer;
    }
}
