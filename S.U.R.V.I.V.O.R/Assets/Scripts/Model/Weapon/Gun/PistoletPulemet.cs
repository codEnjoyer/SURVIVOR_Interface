using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PistoletPulemet : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;
    
    protected override int GetAmountOfShots(Skills skills)
    {
        switch (fireType)
        {
            case FireType.Semi://TODO передергивание затвора при семи режиме (ТОЛЬКО В МЕТОДЕ ДЛЯ БОЛТОВОК) // При макс навыке болтовок, передергивать не нужно
                return 1;
            case FireType.SemiAutomatic:
                return 1;
            case FireType.Burst:
                return 3;//TODO Сделать константой, зависящей от навыков обращения с оружием и переопределить методы для каждого типа оружия. 
            case FireType.Auto:
                return Data.FireRate;
            default:
                return 1;
        }
    }
}