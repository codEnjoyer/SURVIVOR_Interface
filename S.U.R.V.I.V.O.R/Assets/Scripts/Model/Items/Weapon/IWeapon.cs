using System.Collections.Generic;
using Model.GameEntity;
using Model.GameEntity.Skills;

public interface IWeapon
{
    public float AttackDistance { get; }
    public void Attack(List<BodyPart> targets, float distance, Skills skills);
}