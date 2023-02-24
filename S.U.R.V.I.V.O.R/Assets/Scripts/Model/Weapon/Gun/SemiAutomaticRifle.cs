
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SemiAutomaticRifle : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;
    
    protected override int GetAmountOfShots(Skills skills)
    {
        switch (fireType)
        {
            case FireType.SemiAutomatic:
                return 1;
            case FireType.Burst:
                return 3;//TODO Сделать константой, зависящей от навыков обращения с оружием и переопределить методы для каждого типа оружия. 
            default:
                return 1;
        }
    }
}
