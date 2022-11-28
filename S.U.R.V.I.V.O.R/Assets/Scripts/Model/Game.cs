using System;
using Player;
using UnityEngine;

public class Game: MonoBehaviour
{
    public static Game Instance { get; private set; }
    [SerializeField] private Group chosenGroup;
    public Group ChosenGroup => chosenGroup;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}