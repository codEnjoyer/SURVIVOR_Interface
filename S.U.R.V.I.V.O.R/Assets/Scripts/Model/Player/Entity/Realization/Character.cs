using System;
using UnityEngine;
[Serializable]
public class Character : Entity
{
    public Sprite sprite;
    public string name;
    public string surname;
    public IMainGun PrimaryGun;
    public ISecondaryGun SecondaryGun;
    public IMeleeWeapon MeleeWeapon;
    public readonly Skills Skills;

    public readonly ManBody Body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

    public Character()
    {
        Body = new ManBody();
    }
}