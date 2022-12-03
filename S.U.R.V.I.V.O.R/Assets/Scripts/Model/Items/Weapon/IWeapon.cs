using System.Collections.Generic;

public interface IWeapon
{
    public float AttackDistance { get; }
    public void Attack(List<BodyPart> targets, float distance, Skills skills);
}