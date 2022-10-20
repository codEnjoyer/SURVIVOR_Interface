using System.Collections.Generic;
using System.Linq;


public class Group
{
    public int MaxOnGlobalMapGroupEndurance;
    public int CurrentOnGlobalMapGroupEndurance;

    public readonly List<Character> currentGroupMembers;
    private int maxGroupMembers;


    public readonly Location location;

    public Item Loot()
    {
        return location.GetLoot();
    }

    public void SubstracOnMove()
    {
        //Вычесть характеристике всех игрков группы при перемещении по карте
    }

    public void OnTurnEnd()
    {
        currentGroupMembers.Select(character => character.Body.Energy--);
        //Вычислить все характеристки при окончании хода
    }

    private void SubtractEnergy()
    {
        foreach (var groupMember in currentGroupMembers)
        {
            groupMember.Body.Food--;
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
}