using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : Entity
{
    public Sprite Sprite { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public IGun PrimaryGun { get; set; }
    public IGun SecondaryGun { get; set; }
    public IMeleeWeapon MeleeWeapon { get; set; }
    public readonly Skills skills = new Skills();
    public readonly ManBody body = new ManBody();
    
    public override Body Body => body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте
    
    public override void Attack(List<BodyPart> targets, float distance)
    {
        throw new NotImplementedException();
    }
}