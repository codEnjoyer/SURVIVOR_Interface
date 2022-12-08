using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class InterfaceGroupLogicController : MonoBehaviour
{
    public Group currentGroup;
    [SerializeField] private PlayerLayerLogic FirstPlayerLayer;
    [SerializeField] private PlayerLayerLogic SecondPlayerLayer;
    [SerializeField] private PlayerLayerLogic ThirdPlayerLayer;
    [SerializeField] private PlayerLayerLogic FourthPlayerLayer;
    
    [SerializeField] private PlayerLayerLogic GroupFirstPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupSecondPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupThirdPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupFourthPlayerLayer;

    [SerializeField] private LocationInventoryManager locationInventoryManager;
    
    public UnityEvent OnFirstPlayerLayerOpen = new();
    public UnityEvent OnGroupLayerOpen = new();
    public void Awake()
    {
        FirstPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[0];
        GroupFirstPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[0];
        locationInventoryManager.group = currentGroup;
        /*
        SecondPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[1];
        ThirdPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[2];
        FourthPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[3];
        */
    }
}
