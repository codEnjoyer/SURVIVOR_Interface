using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Knife : MeleeWeapon
{
    public int Damage { get; set; }

    public Knife(int damage)
    {
        Damage = damage;
    }

    public override float AttackDistance => 10;

    public override void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        if (distance > AttackDistance)
            return;

        var target = targets.First();
        target.TakeDamage(new DamageInfo(Damage));
    }
}