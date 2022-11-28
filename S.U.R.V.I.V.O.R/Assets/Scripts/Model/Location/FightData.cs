
using System.Collections.Generic;

public class FightData
{
    public List<Entity> enemies;
    public List<Character> group;


    public FightData(List<Entity> enemies, List<Character> group)
    {
        this.enemies = enemies;
        this.group = group;
    }
}