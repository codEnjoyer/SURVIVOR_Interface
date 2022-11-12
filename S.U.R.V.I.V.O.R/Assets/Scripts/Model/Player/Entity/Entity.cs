using System;
using UnityEngine;

public abstract class Entity
{
    public Body Body { get; protected set; }
    protected IMainGun mainGun;
    protected ISecondaryGun secondaryGun;
    protected IMeleeWeapon meleeWeapon;

    public void Attack(Automat weapon, Body target)
    {
        //TODO Реализовать метод стрельбы из weapon по target вызывает у gun метод стрельбы, который формирует служебный класс Shoot
        throw new NotImplementedException();
    }
}