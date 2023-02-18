using System;
using Model;
using Model.Player;
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
        OnLocationChanged(GlobalMapController.Instance.ChosenGroup.Location);
        GlobalMapController.Instance.ChosenGroup.GroupMovementLogic.LocationChange += OnLocationChanged;
        GlobalMapController.Instance.ChosenGroupChange += OnChosenGroupChange;
    }

    private void OnLocationChanged(Location loc)
    {
        if (text != null)
            text.text = loc.name;
        
    }

    private void OnChosenGroupChange(Group currentGroup, Group newGroup)
    {
        currentGroup.GroupMovementLogic.LocationChange -= OnLocationChanged;
        newGroup.GroupMovementLogic.LocationChange += OnLocationChanged;
    }
}