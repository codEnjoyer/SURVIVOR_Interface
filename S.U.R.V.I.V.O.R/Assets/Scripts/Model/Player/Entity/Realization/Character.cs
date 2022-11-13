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

    public readonly ManBody body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

    public Character()
    {
        body = new ManBody();
    }
}