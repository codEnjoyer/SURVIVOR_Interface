using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Body : IAlive
{
    private int currentCriticalLoses;
    private int maxCriticalLoses;
    protected List<BodyPart> bodyParts = new();
    public BodyHealth Health { get; }

    protected Body()
    {
        Health = new BodyHealth(this);
    }

    protected int MaxCriticalLoses
    {
        get => maxCriticalLoses;
        set
        {
            if (value > 0 && value <= bodyParts.Count)
                maxCriticalLoses = value;
            else
                throw new ConstraintException($"Нарушино устовие 0 < {value} <= {bodyParts.Count}. {GetType()}");
        }
    }

    public IEnumerable<BodyPart> BodyParts => bodyParts;
    public float Hp => BodyParts.Sum(part => part.Hp);
    public float TotalWeight => BodyParts.Sum(part => part.Weight);
    public event Action Died;
    
    public void LossBodyParts(BodyPart bodyPart)
    {
        bodyParts.Remove(bodyPart);
        currentCriticalLoses++;
        if (currentCriticalLoses >= MaxCriticalLoses)
        {
            Died?.Invoke();
        }
    }

    public void TakeDamage(DamageInfo damage)
    {
        throw new NotImplementedException();
    }

    public void Healing(HealInfo heal)
    {
        throw new NotImplementedException();
    }
}