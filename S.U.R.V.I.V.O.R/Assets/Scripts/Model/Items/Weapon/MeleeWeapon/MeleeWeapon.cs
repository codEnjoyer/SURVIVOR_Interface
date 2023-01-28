using System.Collections.Generic;
using Model.GameEntity;
using Model.GameEntity.Skills;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public abstract class MeleeWeapon: MonoBehaviour, IWeapon
{
    public abstract float AttackDistance { get; }
    public abstract void Attack(List<BodyPart> targets, float distance, Skills skills);
}

