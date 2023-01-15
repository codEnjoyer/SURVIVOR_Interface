using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interface.InterfaceStates;
using Unity.VisualScripting;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public readonly StateMachine interfaceStateMachine = new();
    public NothingActive NothingActive { get; private set; }
    public CharacterPanelActive CharacterPanelActive { get; private set; }
    public GroupLayerActive GroupLayerActive { get; private set; }
    public PlayerLayerActive PlayerLayerActive { get; private set; }

    private State memoryState;
    
    [SerializeField] private GameObject mainInfoPanelLayer;
    [SerializeField] private GameObject groupButtonsLayer;
    [SerializeField] private GameObject groupInfoLayer;
    [SerializeField] private GameObject charactersButtonsLayer;
    [SerializeField] private GameObject firstPlayerLayer;
    [SerializeField] private GameObject secondPlayerLayer;
    [SerializeField] private GameObject thirdPlayerLayer;
    [SerializeField] private GameObject fourthPlayerLayer;
    
    
    [SerializeField] private CharactersPlateLayerLogic charactersPlateLayerLogic;
    private PlayerLayerLogic firstPlayerLayerLogic => firstPlayerLayer.GetComponent<PlayerLayerLogic>();
    private PlayerLayerLogic secondPlayerLayerLogic => secondPlayerLayer.GetComponent<PlayerLayerLogic>();
    private PlayerLayerLogic thirdfPlayerLayerLogic => thirdPlayerLayer.GetComponent<PlayerLayerLogic>();
    private PlayerLayerLogic fourthPlayerLayerLogic => fourthPlayerLayer.GetComponent<PlayerLayerLogic>();
    
    public GameObject MainInfoPanelLayer => mainInfoPanelLayer;
    public GameObject GroupButtonsLayer => groupButtonsLayer;
    public GameObject GroupInfoLayer => groupInfoLayer;
    public GameObject CharactersButtonsLayer => charactersButtonsLayer;
    public GameObject FirstPlayerLayer => firstPlayerLayer;
    public GameObject SecondPlayerLayer => secondPlayerLayer;
    public GameObject ThirdPlayerLayer => thirdPlayerLayer;
    public GameObject FourthPlayerLayer => fourthPlayerLayer;

    public GameObject CurrentPlayerLayer { get; private set; }

    private bool IsFirstFrameSkiped;
    
    public void Awake()
    {
        NothingActive = new NothingActive(this, interfaceStateMachine);
        CharacterPanelActive = new CharacterPanelActive(this, interfaceStateMachine);
        GroupLayerActive = new GroupLayerActive(this, interfaceStateMachine);
        PlayerLayerActive = new PlayerLayerActive(this, interfaceStateMachine);

        var gMemebers = Game.Instance.ChosenGroup.CurrentGroupMembers.ToArray(); 
        firstPlayerLayerLogic.Init(gMemebers[0]);
        secondPlayerLayerLogic.Init(gMemebers[1]);
        thirdfPlayerLayerLogic.Init(gMemebers[2]);
        fourthPlayerLayerLogic.Init(gMemebers[3]);
        
        Selector.Instance.Activate();
        InitializeInterface();
        interfaceStateMachine.Initialize(NothingActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            interfaceStateMachine.ChangeState(NothingActive);
    }

    private void InitializeInterface()
    {
        MainInfoPanelLayer.SetActive(true);
        GroupButtonsLayer.SetActive(true);
        CharactersButtonsLayer.SetActive(true);
        GroupInfoLayer.SetActive(true);
        
        FirstPlayerLayer.SetActive(true);
        SecondPlayerLayer.SetActive(true);
        ThirdPlayerLayer.SetActive(true);
        FourthPlayerLayer.SetActive(true);

        MainInfoPanelLayer.SetActive(false);
        GroupButtonsLayer.SetActive(false);
        CharactersButtonsLayer.SetActive(false);
        GroupInfoLayer.SetActive(false);
        
        FirstPlayerLayer.SetActive(false);
        SecondPlayerLayer.SetActive(false);
        ThirdPlayerLayer.SetActive(false);
        FourthPlayerLayer.SetActive(false);
    }

    public void SetCharactersPanelActive()
    {
        if (interfaceStateMachine.CurrentState == CharacterPanelActive)
            interfaceStateMachine.ChangeState(NothingActive);
        else
            interfaceStateMachine.ChangeState(CharacterPanelActive);
    }

    public void SetGroupLayerActive()
    {
        if (interfaceStateMachine.CurrentState == GroupLayerActive)
            interfaceStateMachine.ChangeState(memoryState);
        else
        {
            memoryState = interfaceStateMachine.CurrentState;
            interfaceStateMachine.ChangeState(GroupLayerActive);
        }
    }

    private void SetPlayerLayerActive(GameObject characterLayer)
    {
        if (interfaceStateMachine.CurrentState == PlayerLayerActive && CurrentPlayerLayer == characterLayer)
            interfaceStateMachine.ChangeState(CharacterPanelActive);
        else
        {
            CurrentPlayerLayer = characterLayer;
            interfaceStateMachine.ChangeState(PlayerLayerActive);
        }
    }


    public void ChooseFirstPlayer()
    {
        SetPlayerLayerActive(firstPlayerLayer);
    }

    public void ChooseSecondPlayer() => SetPlayerLayerActive(secondPlayerLayer);
    public void ChooseThirdPlayer() => SetPlayerLayerActive(thirdPlayerLayer);
    public void ChooseFourthPlayer() => SetPlayerLayerActive(fourthPlayerLayer);
}