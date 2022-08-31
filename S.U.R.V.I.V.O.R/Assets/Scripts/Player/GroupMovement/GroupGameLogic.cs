using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GroupGameLogic : MonoBehaviour
{
    public int MaxGroupEndurance;
    private IPlayer[] groupMembers;

    private void SubtractEnergy()
    {
        foreach (var groupMember in groupMembers)
        {
            groupMember.Energy--;
        }
    }

    private void SubtractWater()
    {
        foreach (var groupMember in groupMembers)
        {
            groupMember.Water--;
        }
    }

    private void SubtractSatiety()
    {
        foreach (var groupMember in groupMembers)
        {
            groupMember.Satiety--;
        }
    }
}
