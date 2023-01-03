using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LocationInventory : MonoBehaviour
{
    public static LocationInventory Instance { get; private set; }

    [SerializeField] private Text text;
    [FormerlySerializedAs("itemGrid")] [SerializeField] private InventoryGrid inventoryGrid;
    public InventoryGrid LocationInventoryGrid => inventoryGrid;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
            Destroy(gameObject);
    }

    public void Start()
    {
        OnLocationChanged(Game.Instance.ChosenGroup.Location.Data.LocationName);
        Game.Instance.ChosenGroup.GroupMovementLogic.LocationChange += OnLocationChanged;
        Game.Instance.ChosenGroupChange += OnChosenGroupChange;
    }

    private void OnLocationChanged(string value) => text.text = value;

    private void OnChosenGroupChange(Group currentGroup, Group newGroup)
    {
        currentGroup.GroupMovementLogic.LocationChange -= OnLocationChanged;
        newGroup.GroupMovementLogic.LocationChange += OnLocationChanged;
    }
}