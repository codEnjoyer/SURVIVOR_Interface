using System.Collections.Generic;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using UnityEngine;

public interface IWeapon
{
    public void Attack(Vector3 targetPoint,Skills skills);
}