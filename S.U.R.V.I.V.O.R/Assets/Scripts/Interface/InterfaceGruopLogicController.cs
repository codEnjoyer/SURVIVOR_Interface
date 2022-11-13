using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterfaceGruopLogicController : MonoBehaviour
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
    
    
    public UnityEvent OnFirstPlayerLayerOpen = new();
    public UnityEvent OnGroupLayerOpen = new();
    public void Start()
    {
        FirstPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[0];
        GroupFirstPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[0];
        /*
        SecondPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[1];
        ThirdPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[2];
        FourthPlayerLayer.CurrentCharacter = currentGroup.currentGroupMembers[3];
        */
        currentGroup.OnEnergyChanged.AddListener(FirstPlayerLayer.OnEnegryChanged);
        currentGroup.OnEnergyChanged.AddListener(GroupFirstPlayerLayer.OnEnegryChanged);
        /*
        OnEnergyChanged.AddListener(SecondPlayerLayer.OnEnegryChanged);
        OnEnergyChanged.AddListener(ThirdPlayerLayer.OnEnegryChanged);
        OnEnergyChanged.AddListener(FourthPlayerLayer.OnEnegryChanged);
        */
        
        currentGroup.OnFoodChanged.AddListener(FirstPlayerLayer.OnFoodChanged);
        currentGroup.OnFoodChanged.AddListener(GroupFirstPlayerLayer.OnFoodChanged);
        /*
        OnFoodChanged.AddListener(SecondPlayerLayer.OnFoodChanged);
        OnFoodChanged.AddListener(ThirdPlayerLayer.OnFoodChanged);
        OnFoodChanged.AddListener(FourthPlayerLayer.OnFoodChanged);
        */
        
        currentGroup.OnWaterChanged.AddListener(FirstPlayerLayer.OnWaterChanged);
        currentGroup.OnWaterChanged.AddListener(GroupFirstPlayerLayer.OnWaterChanged);
        /*
        OnWaterChanged.AddListener(SecondPlayerLayer.OnWaterChanged);
        OnWaterChanged.AddListener(ThirdPlayerLayer.OnWaterChanged);
        OnWaterChanged.AddListener(FourthPlayerLayer.OnWaterChanged);
        */
        
        currentGroup.OnHealthChanged.AddListener(FirstPlayerLayer.OnHealthChanged);
        currentGroup.OnHealthChanged.AddListener(GroupFirstPlayerLayer.OnHealthChanged);
        /*
        OnHealthChanged.AddListener(SecondPlayerLayer.OnHealthChanged);
        OnHealthChanged.AddListener(ThirdPlayerLayer.OnHealthChanged);
        OnHealthChanged.AddListener(ThirdPlayerLayer.OnHealthChanged);
        */
        OnGroupLayerOpen.AddListener(GroupFirstPlayerLayer.OnOpen);
        OnFirstPlayerLayerOpen.AddListener(FirstPlayerLayer.OnOpen);
    }
}
