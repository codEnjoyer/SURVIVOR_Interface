using System;
using System.Collections.Generic;
using System.Linq;using UnityEngine;

public abstract class BodyPart : IAlive
{
    public readonly Body body;
    public BodyPathHealth Health { get; }
    public abstract int MaxHp { get; }
    public abstract float Hp { get; protected set; }
    public abstract float Size { get; }
    public abstract IEnumerable<Clothes> Clothes { get; }
    public float Weight => Clothes.Sum(cloth => cloth.TotalWeight);
    public event Action<BodyPart> OnZeroHp;

    public BodyPart(Body body)
    {
        Health = new BodyPathHealth(this);
        this.body = body;
    }

    public void TakeDamage(Shoot shot)
    {
        throw new NotImplementedException();
        //TODO реализовать метод получения урона в зависимоти от выстрела
        
        
        var blockedDamage = Clothes.Sum(cloth => cloth.CalculateBlockedDamage(shot));
        var damage = 1;
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException(nameof(damage));
        Hp -= damage;
        if (Hp <= 0)
        {
            OnZeroHp?.Invoke(this);
            body.LossBodyParts();
        }
    }
    public void Healing(float heal)
    {
        throw new NotImplementedException();
    }
}
