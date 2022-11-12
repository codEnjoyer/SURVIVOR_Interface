using System.Collections.Generic;

public interface IWeapon
{
    public void Attack(List<BodyPart> targets, float distance, Skills skills);
}