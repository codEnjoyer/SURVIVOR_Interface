using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Drobovik : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;
    public override void Attack(Vector3 targetPoint, Skills skills)
    {
        throw new NotImplementedException();
    }
}