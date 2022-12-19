using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Model.GameEntity;
using Model.GameEntity.Skills;

public class Knife : MeleeWeapon
{
    public DamageInfo Damage { get; set; }

    public Knife(DamageInfo damage)
    {
        Damage = damage;
    }

    public override float AttackDistance => 10;

    public override void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        if (distance > AttackDistance)
            return;

        var target = targets.First();
        target.TakeDamage(Damage);
    }
}