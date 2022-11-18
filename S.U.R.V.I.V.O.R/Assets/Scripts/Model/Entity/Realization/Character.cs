using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character: IEntity
{
    public Sprite sprite { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
    public IGun PrimaryGun { get; set; }
    public IGun SecondaryGun { get; set; }
    public IMeleeWeapon MeleeWeapon { get; set; }
    public readonly Skills Skills;
    public readonly ManBody body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

    public Character()
    {
        Skills = new Skills();
        body = new ManBody();
    }
    
    public void Attack(List<BodyPart> targets, float distance)
    {
        throw new NotImplementedException();
    }
}