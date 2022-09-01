using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GroupGameLogic : MonoBehaviour
{
    public int MaxGroupEndurance;
    public int CurrentGroupEndurance;
    public IPlayer[] GroupMembers;

    public void Awake()
    {
        GroupMembers = new IPlayer[1];
        GroupMembers[0] = new CommonPlayer
        {
            Hp = 100,
            Water = 10,
            Satiety = 10,
            Energy = 10
        };
    }

    public void Start()
    {
        InputAggregator.OnTurnEndEvent += OnTurnEnd;
        CurrentGroupEndurance = MaxGroupEndurance;
    }


    public void OnGroupMoves()
    {
        SubtractEnergy();
    }

    private void OnTurnEnd()
    {
        if(CurrentGroupEndurance != MaxGroupEndurance)
            SubtractEnergy();
        SubtractSatiety();
        SubtractWater();
        AddExtraEnergy();
        ResetAllTurnCharacteristics();
    }

    private void SubtractEnergy()
    {
        foreach (var groupMember in GroupMembers)
        {
            groupMember.Energy--;
        }
    }

    private void SubtractWater()
    {
        foreach (var groupMember in GroupMembers)
        {
            groupMember.Water--;
        }
    }

    private void SubtractSatiety()
    {
        foreach (var groupMember in GroupMembers)
        {
            groupMember.Satiety--;
        }
    }

    private void AddExtraEnergy()
    {
        foreach (var groupMember in GroupMembers)
        {
            if (groupMember.Satiety >= 8)
                groupMember.Energy++;
            if (groupMember.Water >= 8)
                groupMember.Energy++;
        }
    }

    private void ResetAllTurnCharacteristics()
    {
        CurrentGroupEndurance = MaxGroupEndurance;
    }
}
