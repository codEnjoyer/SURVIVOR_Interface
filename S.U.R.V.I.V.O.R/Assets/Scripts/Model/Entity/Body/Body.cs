using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Body : IAlive
{
    private int counterForNumberLostBodyParts;
    private int criticalLoses;
    public abstract ICollection<BodyPart> BodyParts { get; }
    protected abstract int CriticalLoses { get; }
    public BodyHealth Health { get; }
    
    public float Hp => BodyParts.Sum(part => part.Hp);
    public float TotalWeight => BodyParts.Sum(part => part.Weight);
    public event Action Died;

    protected Body()
    {
        Health = new BodyHealth(this);
    }

    public void LossBodyParts()
    {
        Debug.Log(Died?.GetInvocationList().Count());
        Debug.Log(CriticalLoses);
        counterForNumberLostBodyParts++;
        if (counterForNumberLostBodyParts == CriticalLoses)
        {
            Debug.Log("LossPart " + counterForNumberLostBodyParts);
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