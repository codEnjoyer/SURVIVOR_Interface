using System.Collections.Generic;

public interface IWeapon
{
    public float AttackDistance { get; }
    public bool IsMeleeWeapon => AttackDistance < 10;
    public void Attack(List<BodyPart> targets, float distance, Skills skills);
}