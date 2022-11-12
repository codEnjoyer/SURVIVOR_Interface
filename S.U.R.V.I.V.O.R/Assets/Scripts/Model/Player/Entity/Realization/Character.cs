using System;
using UnityEngine;
[Serializable]
public class Character : Entity
{
    public Sprite sprite;
    public string name;
    public string surname;
    
    public readonly Skills Skills;

    public readonly ManBody Body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

    public Character()
    {
        Body = new ManBody();
    }
}