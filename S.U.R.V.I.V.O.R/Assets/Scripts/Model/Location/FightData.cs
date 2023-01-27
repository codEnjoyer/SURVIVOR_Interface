using System.Collections.Generic;
using Model.Entities.Characters;
using Model.GameEntity;

public class FightData
{
    public readonly IEnumerable<Entity> enemies;
    public readonly IEnumerable<Character> ally;


    public FightData(IEnumerable<Entity> enemies, IEnumerable<Character> ally)
    {
        this.enemies = enemies;
        this.ally = ally;
    }
}