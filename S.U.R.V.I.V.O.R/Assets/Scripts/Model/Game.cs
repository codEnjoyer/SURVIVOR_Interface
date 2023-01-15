using System;
using System.Collections.Generic;
using Graph_and_Map;
using Player;
using Player.GroupMovement;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Game>();
                instance.Init();
            }

            return instance;
        }
    }

    [SerializeField] private List<Group> groups;
    [SerializeField] private Group chosenGroup;
    [SerializeField] private Node startNode;
    [SerializeField] private Canvas mainCanvas;

    [SerializeField] private InterfaceController interfaceController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private ContextMenuController contextMenuController;
    [SerializeField] private TurnController turnController;
    [SerializeField] private GroupsMovementController groupsMovementController;
    [SerializeField] private Selector selector;

    private MonoBehaviour[] allControllers;
    public event Action<Group, Group> ChosenGroupChange;
    public Group ChosenGroup => chosenGroup;
    public Node StartNode => startNode;
    public Canvas MainCanvas => mainCanvas;
    public IEnumerable<Group> Groups => groups;
    public InterfaceController InterfaceController => interfaceController;
    public InventoryController InventoryController => inventoryController;
    public ContextMenuController ContextMenuController => contextMenuController;
    public TurnController TurnController => turnController;
    public GroupsMovementController GroupsMovementController => groupsMovementController;
    public Selector Selector => selector;

    private void SetChosenGroup(Group newGroup)
    {
        ChosenGroupChange?.Invoke(chosenGroup, newGroup);
        chosenGroup = newGroup;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (instance == null)
        {
            instance = this;
            Init();
        }
    }

    private void Init()
    {
        groups = new List<Group>();
        if (chosenGroup != null)
            groups.Add(chosenGroup);

        allControllers = new MonoBehaviour[]
        {
            selector,
            interfaceController,
            inventoryController,
            contextMenuController,
            turnController,
            groupsMovementController
        };

        foreach (var controller in allControllers)
        {
            controller.gameObject.SetActive(true);
        }
    }
}