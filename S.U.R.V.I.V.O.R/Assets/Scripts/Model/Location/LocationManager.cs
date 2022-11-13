using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{ 
    public static LocationManager Instance { get; private set; }
    [SerializeField] private ItemGrid itemGrid;

    public ItemGrid ItemGrid => itemGrid;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; ;
        }
        else if (Instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}