using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;

public class Knife : MeleeWeapon
{
    public DamageInfo Damage { get; set; }

    public Knife(DamageInfo damage)
    {
        Damage = damage;
    }

    public override float OptimalFireDistance => 10;
}