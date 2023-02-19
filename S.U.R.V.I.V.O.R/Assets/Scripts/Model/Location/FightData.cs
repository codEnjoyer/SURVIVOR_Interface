﻿using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters;
using Model.GameEntity;

public class FightData
{
    public readonly IEnumerable<Entity> enemies;
    public readonly IEnumerable<CharacterData> ally;


    public FightData(IEnumerable<Entity> enemies, IEnumerable<Character> ally)
    {
        this.enemies = enemies.ToList();
        this.ally = ally.Select(x => x.CreateData()).ToList();
    }
    
    public FightData(IEnumerable<Entity> enemies, IEnumerable<CharacterData> ally)
    {
        this.enemies = enemies.ToList();
        this.ally = ally.ToList();
    }
}