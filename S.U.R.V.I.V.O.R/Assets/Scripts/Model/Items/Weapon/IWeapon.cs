using System.Collections.Generic;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;

public interface IWeapon
{
    public float AttackDistance { get; }
    public void Attack(List<BodyPart> targets, float distance, Skills skills);
}