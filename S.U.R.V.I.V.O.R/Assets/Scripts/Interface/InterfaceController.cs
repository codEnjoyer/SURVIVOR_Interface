using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [SerializeField]
    private InterfaceGruopLogicController interfaceGruopLogicController;
    [SerializeField]
    private GameObject mainInfoPanelLayer;
    [SerializeField]
    private GameObject groupButtonsLayer;
    [SerializeField]
    private GameObject groupInfoLayer;
    [SerializeField]
    private GameObject charactersButtonsLayer;
    [SerializeField]
    private GameObject firstPlayerLayer;
    [SerializeField]
    private GameObject secondPlayerLayer;
    [SerializeField]
    private GameObject thirdPlayerLayer;
    [SerializeField]
    private GameObject fourthPlayerLayer;

    public InterfaceGruopLogicController InterfaceGruopLogicController => interfaceGruopLogicController;
    public GameObject MainInfoPanelLayer => mainInfoPanelLayer;
    public GameObject GroupButtonsLayer => groupButtonsLayer;
    public GameObject GroupInfoLayer => groupInfoLayer;
    public GameObject CharactersButtonsLayer => charactersButtonsLayer;
    public GameObject FirstPlayerLayer => firstPlayerLayer;
    public GameObject SecondPlayerLayer => secondPlayerLayer;
    public GameObject ThirdPlayerLayer => thirdPlayerLayer;
    public GameObject FourthPlayerLayer => fourthPlayerLayer;

    public GameObject CurrentPlayerLayer { get; private set; }


    void Awake()
    {
        NothingActive = new NothingActive(this, interfaceStateMachine);
        CharacterPanelActive = new CharacterPanelActive(this, interfaceStateMachine);
        GroupLayerActive = new GroupLayerActive(this, interfaceStateMachine);
        PlayerLayerActive = new PlayerLayerActive(this, interfaceStateMachine);
        Selector.Instance.Activate();
    }

    private void Start()
    {
        SetActiveInterface(true);
        SetActiveInterface(false);
        interfaceStateMachine.Initialize(NothingActive);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            interfaceStateMachine.ChangeState(NothingActive);
    }

    private void SetActiveInterface(bool value)
    {
        MainInfoPanelLayer.SetActive(value);
        GroupButtonsLayer.SetActive(value);
        CharactersButtonsLayer.SetActive(value);
        GroupInfoLayer.SetActive(value);
        FirstPlayerLayer.SetActive(value);
        //SecondPlayerLayer.SetActive(value);
        //ThirdPlayerLayer.SetActive(value);
        //FourthPlayerLayer.SetActive(value);
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
        if(interfaceStateMachine.CurrentState == PlayerLayerActive && CurrentPlayerLayer == characterLayer)
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
