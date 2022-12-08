using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class InterfaceGruopLogicController : MonoBehaviour
{
    [SerializeField] private PlayerLayerLogic FirstPlayerLayer;
    [SerializeField] private PlayerLayerLogic SecondPlayerLayer;
    [SerializeField] private PlayerLayerLogic ThirdPlayerLayer;
    [SerializeField] private PlayerLayerLogic FourthPlayerLayer;
    
    [SerializeField] private PlayerLayerLogic GroupFirstPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupSecondPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupThirdPlayerLayer;
    [SerializeField] private PlayerLayerLogic GroupFourthPlayerLayer;
    
    
    public UnityEvent OnFirstPlayerLayerOpen = new();
    public UnityEvent OnGroupLayerOpen = new();
    public void Awake()
    {
        FirstPlayerLayer.CurrentCharacter = Game.Instance.ChosenGroup.CurrentGroupMembers.First();
        GroupFirstPlayerLayer.CurrentCharacter = Game.Instance.ChosenGroup.CurrentGroupMembers.First();
        /*
        SecondPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[1];
        ThirdPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[2];
        FourthPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[3];
        */
    }
}
