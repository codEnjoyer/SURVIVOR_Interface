﻿using System;
using System.Linq;
public abstract class BodyPart
{
    protected readonly int MaxHp;
    public float TotalWeight => Clothes.Sum(cloth => cloth.TotalWeight);
    public float Hp { get; protected set; }

    private float size;

    public float Size
    {
        get => size;
        set
        {
            if (value <= 0)
                throw new ArgumentException();
            size = value;
        }
    }
    
    protected readonly int MaxClothesAmount;
    protected readonly Clothes[] Clothes;
    public event Action OnZeroHp;

    protected BodyPart()
    {
        Clothes = new Clothes[MaxClothesAmount];
    }


    public void TakeDamage(Shoot shot)
    {
        throw new NotImplementedException();
        //TODO реализовать метод получения урона в зависимоти от выстрела
        var blockedDamage = Clothes.Sum(cloth => cloth.CalculateBlockedDamage(shot));
        if (Hp <= 0)
            OnZeroHp?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException();
        Hp -= damage;
        if (Hp <= 0)
            OnZeroHp?.Invoke();
    }

    public void Healing(float heal)
    {
        if (heal < 0)
            throw new ArgumentException();
        Hp += heal;
    }
}