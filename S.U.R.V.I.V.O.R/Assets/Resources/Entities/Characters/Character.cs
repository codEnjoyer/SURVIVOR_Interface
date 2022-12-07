﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : Entity
{
    
    [SerializeField] private Sprite sprite; 
    [SerializeField] private string firstName;
    [SerializeField] private string surname;

    public Sprite Sprite => sprite;
    public string FirstName => firstName;
    public string Surname => surname;

    public IGun PrimaryGun { get; set; }
    public IGun SecondaryGun { get; set; }
    public IMeleeWeapon MeleeWeapon { get; set; }
    public readonly Skills skills = new Skills();
    public readonly ManBody body = new ManBody();
    
    public override Body Body => body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте
    
    public override void Attack(ICollection<BodyPart> targets, float distance)
    {
        targets.First().TakeDamage(new DamageInfo(15f));
        Debug.Log(targets.First().Hp);
    }
}