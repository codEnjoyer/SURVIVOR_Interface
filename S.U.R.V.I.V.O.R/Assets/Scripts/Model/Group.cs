using System;
using System.Collections.Generic;
using Player;
using UnityEngine;


public class Group : MonoBehaviour
{
    public int MaxOnGlobalMapGroupEndurance;
    public int CurrentOnGlobalMapGroupEndurance;

    public readonly List<Character> currentGroupMembers = new ();
    private int maxGroupMembers;

    public Location location { get; set; }

    void Start()
    {
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
        currentGroupMembers.Add(new Character());
        location = GetComponent<GroupMovementLogic>().CurrentNode.GetComponentInParent<Location>();
    }

    public BaseItem Loot()
    {
        throw new NotImplementedException();
    }

    private void SubtractEnergy()
    {
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Energy--;
        }
    }

    private void SubtractWater()
    {
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Water--;
        }
    }

    private void SubtractSatiety()
    {
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Food--;
        }
    }

    private void AddExtraEnergy()
    {
        foreach (var groupMember in currentGroupMembers)
        {
            if (groupMember.Body.Food >= 8)
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
        SubtractEnergy();
        if (CurrentOnGlobalMapGroupEndurance != MaxOnGlobalMapGroupEndurance)
            SubtractEnergy();
        SubtractSatiety();
        SubtractWater();
        AddExtraEnergy();
        ResetAllTurnCharacteristics();
        //Вычислить все характеристки при окончании хода
    }
}