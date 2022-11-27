using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Knife : MonoBehaviour, IMeleeWeapon
{
    public int Damage { get; set; }

    public Knife(int damage)
    {
        Damage = damage;
    }

    public float AttackDistance => 10;

    public void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        if (distance > AttackDistance)
            return;

        var target = targets.First();
        target.TakeDamage(new DamageInfo(Damage));
    }
}