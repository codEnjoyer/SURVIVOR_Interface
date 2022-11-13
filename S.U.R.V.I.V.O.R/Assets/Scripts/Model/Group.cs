using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;


public class Group : MonoBehaviour
{
    public int MaxOnGlobalMapGroupEndurance;
    public int CurrentOnGlobalMapGroupEndurance;
    public List<Character> currentGroupMembers = new ();
    private int maxGroupMembers;

    public UnityEvent OnEnergyChanged;
    public UnityEvent OnWaterChanged;
    public UnityEvent OnFoodChanged;
    public UnityEvent OnHealthChanged;

    public Location location => GetComponent<GroupMovementLogic>().CurrentNode.GetComponentInParent<Location>();

    void Start()
    {
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
        currentGroupMembers.Add(new Character());
    }

    public BaseItem Loot()
    {
        throw new NotImplementedException();
    }

    private void SubtractEnergy()
    {
        OnEnergyChanged.Invoke();
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Energy--;
        }
    }

    private void SubtractWater()
    {
        OnWaterChanged.Invoke();
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Water--;
        }
    }

    private void SubtractSatiety()
    {
        OnFoodChanged.Invoke();
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Hunger--;
        }
    }

    private void AddExtraEnergy()
    {
        OnEnergyChanged.Invoke();
        foreach (var groupMember in currentGroupMembers)
        {
            if (groupMember.Body.Hunger >= 8)
                groupMember.Body.Energy++;
            if (groupMember.Body.Water >= 8)
                groupMember.Body.Energy++;
        }
    }

    private void ResetAllTurnCharacteristics()
    {
        CurrentOnGlobalMapGroupEndurance = MaxOnGlobalMapGroupEndurance;
    }

    public void OnTurnEnd()
    {
        if (CurrentOnGlobalMapGroupEndurance != MaxOnGlobalMapGroupEndurance)
            SubtractEnergy();
        SubtractSatiety();
        SubtractWater();
        AddExtraEnergy();
        ResetAllTurnCharacteristics();
        //Вычислить все характеристки при окончании хода
    }
}