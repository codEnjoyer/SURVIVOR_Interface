using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class BodyPart : IAlive
{ 
    protected readonly List<Clothes> clothesList = new();
    public readonly Body body;
    public BodyPathHealth Health { get; }
    public abstract int MaxHp { get; }
    public abstract float Hp { get; protected set; }
    public abstract float Size { get; }
    public IEnumerable<Clothes> GetClothes => clothesList;
    public float Weight => GetClothes.Sum(cloth => cloth.TotalWeight);
    public event Action<BodyPart> OnZeroHp;

    public BodyPart(Body body)
    {
        Health = new BodyPathHealth(this);
        this.body = body;
    }

    public void TakeDamage(DamageInfo damage)
    {
        //throw new NotImplementedException();
        //TODO реализовать метод получения урона в зависимоти от выстрела


        //var blockedDamage = Clothes.Sum(cloth => cloth.CalculateBlockedDamage(damage));
        TakeDamage(damage.Damage);
    }

    private void TakeDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            //OnZeroHp?.Invoke(this);
            body.LossBodyParts(this);
        }
    }

    public void Healing(HealInfo heal)
    {
        throw new NotImplementedException();
    }
}