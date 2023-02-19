using System.Collections.Generic;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public abstract class MeleeWeapon: MonoBehaviour, IWeapon
{
    public abstract float OptimalFireDistance { get; }
    public void Attack(Vector3 targetPoint, Skills skills)
    {
        throw new System.NotImplementedException();
    }
}

