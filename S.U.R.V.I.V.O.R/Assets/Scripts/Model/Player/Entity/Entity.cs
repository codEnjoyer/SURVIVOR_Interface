using System;

public abstract class Entity
{
    public Body Body { get; protected set; }
    protected Gun PrimaryGun;
    protected Gun SecondaryGun;
    protected MeleeWeapon MeleeWeapon;
    protected MeleeWeapon ChosenWeapon;

    public void Attack(Gun weapon, Body target)
    {
        //TODO Реализовать метод стрельбы из weapon по target вызывает у gun метод стрельбы, который формирует служебный класс Shoot
        throw new NotImplementedException();
    }
}