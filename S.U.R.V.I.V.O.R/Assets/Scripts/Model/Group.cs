using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Group : MonoBehaviour
{
    public int maxOnGlobalMapGroupEndurance = 10;
    public int currentOnGlobalMapGroupEndurance = 10;

    public readonly List<Character> currentGroupMembers = new ();
    private int maxGroupMembers;


    public readonly Location location;

    void Start()
    {
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
        currentGroupMembers.Add(new Character());
    }

    public Item Loot()
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
        currentOnGlobalMapGroupEndurance = maxOnGlobalMapGroupEndurance;
    }

    public void OnTurnEnd()
    {
        SubtractEnergy();
        if (currentOnGlobalMapGroupEndurance != maxOnGlobalMapGroupEndurance)
            SubtractEnergy();
        SubtractSatiety();
        SubtractWater();
        AddExtraEnergy();
        ResetAllTurnCharacteristics();
        //Вычислить все характеристки при окончании хода
    }
}