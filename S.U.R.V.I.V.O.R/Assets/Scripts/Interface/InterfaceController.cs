using System.Linq;
using Interface.InterfaceStates;
using Model;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public static InterfaceController Instance { get; private set; }

    public readonly StateMachine interfaceStateMachine = new();
    public NothingState NothingState { get; private set; }
    public CharactersState CharactersState { get; private set; }
    public GroupState GroupState { get; private set; }
    public CharacterState FirstCharacterState { get; private set; }
    public CharacterState SecondCharacterState { get; private set; }
    public CharacterState ThirdCharacterState { get; private set; }
    public CharacterState FourthCharacterState { get; private set; }

    [SerializeField] private GameObject mainInfoPanelLayer;
    [SerializeField] private GameObject groupButtonsLayer;
    [SerializeField] private GameObject groupInfoLayer;
    [SerializeField] private GameObject charactersButtonsLayer;
    [SerializeField] private PlayerLayerLogic firstPlayerLayer;
    [SerializeField] private PlayerLayerLogic secondPlayerLayer;
    [SerializeField] private PlayerLayerLogic thirdPlayerLayer;
    [SerializeField] private PlayerLayerLogic fourthPlayerLayer;

    [SerializeField] private CharactersPlateLayerLogic charactersPlateLayerLogic;
    private GroupLayerLogic groupLayerLogic => groupInfoLayer.GetComponent<GroupLayerLogic>();
    
    public GameObject MainInfoPanelLayer => mainInfoPanelLayer;
    public GameObject GroupButtonsLayer => groupButtonsLayer;
    public GameObject GroupInfoLayer => groupInfoLayer;
    public GameObject CharactersButtonsLayer => charactersButtonsLayer;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Init();
        }
    }

    public void Init()
    {
        NothingState = new NothingState(this, interfaceStateMachine);
        CharactersState = new CharactersState(this, interfaceStateMachine);
        GroupState = new GroupState(this, interfaceStateMachine);
        FirstCharacterState = new CharacterState(this, interfaceStateMachine, firstPlayerLayer);
        SecondCharacterState = new CharacterState(this, interfaceStateMachine, secondPlayerLayer);
        ThirdCharacterState = new CharacterState(this, interfaceStateMachine, thirdPlayerLayer);
        FourthCharacterState = new CharacterState(this, interfaceStateMachine, fourthPlayerLayer);

        var gMemebers = Game.Instance.ChosenGroup.CurrentGroupMembers.ToArray();
        firstPlayerLayer.Init(gMemebers[0]);
        secondPlayerLayer.Init(gMemebers[1]);
        thirdPlayerLayer.Init(gMemebers[2]);
        fourthPlayerLayer.Init(gMemebers[3]);
       
        groupLayerLogic.Init();
        charactersPlateLayerLogic.Init();

        Selector.Instance.gameObject.SetActive(true);
        InitializeInterface();
        interfaceStateMachine.Initialize(NothingState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            interfaceStateMachine.ChangeState(NothingState);
    }

    private void InitializeInterface()
    {
        MainInfoPanelLayer.SetActive(true);
        GroupButtonsLayer.SetActive(true);
        CharactersButtonsLayer.SetActive(true);
        GroupInfoLayer.SetActive(true);

        firstPlayerLayer.gameObject.SetActive(true);
        secondPlayerLayer.gameObject.SetActive(true);
        thirdPlayerLayer.gameObject.SetActive(true);
        fourthPlayerLayer.gameObject.SetActive(true);

        MainInfoPanelLayer.SetActive(false);
        GroupButtonsLayer.SetActive(false);
        CharactersButtonsLayer.SetActive(false);
        GroupInfoLayer.SetActive(false);

        firstPlayerLayer.gameObject.SetActive(false);
        secondPlayerLayer.gameObject.SetActive(false);
        thirdPlayerLayer.gameObject.SetActive(false);
        fourthPlayerLayer.gameObject.SetActive(false);
    }

    public void SetCharactersPanelActive() => interfaceStateMachine.ChangeState(CharactersState);
    public void SetGroupLayerActive() => interfaceStateMachine.ChangeState(GroupState);
    public void ChooseFirstPlayer() => interfaceStateMachine.ChangeState(FirstCharacterState);
    public void ChooseSecondPlayer() => interfaceStateMachine.ChangeState(SecondCharacterState);
    public void ChooseThirdPlayer() => interfaceStateMachine.ChangeState(ThirdCharacterState);
    public void ChooseFourthPlayer() => interfaceStateMachine.ChangeState(FourthCharacterState);
}