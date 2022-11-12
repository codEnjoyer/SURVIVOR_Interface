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
    
    public UnityEvent OnFoodChanged = new();
    public UnityEvent OnWaterChanged = new();
    public UnityEvent OnEnergyChanged = new();
    public UnityEvent OnHealthChanged = new();
    
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
        OnEnergyChanged.AddListener(FirstPlayerLayer.OnEnegryChanged);
        OnEnergyChanged.AddListener(GroupFirstPlayerLayer.OnEnegryChanged);
        /*
        OnEnergyChanged.AddListener(SecondPlayerLayer.OnEnegryChanged);
        OnEnergyChanged.AddListener(ThirdPlayerLayer.OnEnegryChanged);
        OnEnergyChanged.AddListener(FourthPlayerLayer.OnEnegryChanged);
        */
        
        OnFoodChanged.AddListener(FirstPlayerLayer.OnFoodChanged);
        OnFoodChanged.AddListener(GroupFirstPlayerLayer.OnFoodChanged);
        /*
        OnFoodChanged.AddListener(SecondPlayerLayer.OnFoodChanged);
        OnFoodChanged.AddListener(ThirdPlayerLayer.OnFoodChanged);
        OnFoodChanged.AddListener(FourthPlayerLayer.OnFoodChanged);
        */
        
        OnWaterChanged.AddListener(FirstPlayerLayer.OnWaterChanged);
        OnWaterChanged.AddListener(GroupFirstPlayerLayer.OnWaterChanged);
        /*
        OnWaterChanged.AddListener(SecondPlayerLayer.OnWaterChanged);
        OnWaterChanged.AddListener(ThirdPlayerLayer.OnWaterChanged);
        OnWaterChanged.AddListener(FourthPlayerLayer.OnWaterChanged);
        */
        
        OnHealthChanged.AddListener(FirstPlayerLayer.OnHealthChanged);
        OnHealthChanged.AddListener(GroupFirstPlayerLayer.OnHealthChanged);
        /*
        OnHealthChanged.AddListener(SecondPlayerLayer.OnHealthChanged);
        OnHealthChanged.AddListener(ThirdPlayerLayer.OnHealthChanged);
        OnHealthChanged.AddListener(ThirdPlayerLayer.OnHealthChanged);
        */
        OnGroupLayerOpen.AddListener(GroupFirstPlayerLayer.OnOpen);
        OnFirstPlayerLayerOpen.AddListener(FirstPlayerLayer.OnOpen);
    }
}
