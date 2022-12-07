
using System.Collections.Generic;

public class FightData
{
    public IEnumerable<Entity> enemies;
    public IEnumerable<Character> group;


    public FightData(IEnumerable<Entity> enemies, IEnumerable<Character> group)
    {
        this.enemies = enemies;
        this.group = group;
    }
}