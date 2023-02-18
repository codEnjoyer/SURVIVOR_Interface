using System.Collections.Generic;
using Model.Entities.Characters;
using Model.GameEntity;
using Model.Player;
using UnityEngine;

public class FightData
{
    public readonly IEnumerable<GameObject> enemies;
    public readonly CharacterSave[] characterSaves;


    public FightData(IEnumerable<GameObject> enemies, CharacterSave[] characterSaves)
    {
        this.enemies = enemies;
        this.characterSaves= characterSaves;
    }
}