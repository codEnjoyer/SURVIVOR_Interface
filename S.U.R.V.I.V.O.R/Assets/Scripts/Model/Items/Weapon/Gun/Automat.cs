using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Automat : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;
    
    public override void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        throw new NotImplementedException();
    }
}