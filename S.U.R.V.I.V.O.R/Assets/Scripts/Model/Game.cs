using System;
using System.Collections.Generic;
using Graph_and_Map;
using Player;
using UnityEngine;

public class Game: MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private List<Group> groups;
    [SerializeField] private Group chosenGroup;
    [SerializeField] private Node startNode;
    [SerializeField] private Canvas mainCanvas;
    public event Action<Group, Group> ChosenGroupChange;
    public Group ChosenGroup => chosenGroup;
    public Node StartNode => startNode;
    public Canvas MainCanvas => mainCanvas;
    public IEnumerable<Group> Group => groups;
    private void SetChosenGroup(Group newGroup)
    {
        ChosenGroupChange?.Invoke(chosenGroup, newGroup);
        chosenGroup = newGroup;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else if (Instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        groups = new List<Group>();
        if(chosenGroup == null)
            groups.Add(chosenGroup);
    }

    
}