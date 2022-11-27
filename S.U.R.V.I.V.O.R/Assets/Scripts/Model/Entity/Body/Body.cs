using System;
using System.Collections.Generic;
using System.Linq;


public abstract class Body : IAlive
{
    private int counterForNumberLostBodyParts;
    private int criticalLoses;
    public abstract ICollection<BodyPart> BodyParts { get; }
    protected abstract int CriticalLoses { get; }
    public BodyHealth Health { get; }
    
    public float Hp => BodyParts.Sum(path => path.Hp);
    public float TotalWeight => BodyParts.Sum(path => path.Weight);
    public event Action Died;

    protected Body()
    {
        Health = new BodyHealth(this);
    }

    public void LossBodyParts()
    {
        counterForNumberLostBodyParts++;
        if (counterForNumberLostBodyParts == CriticalLoses)
            Died?.Invoke();
    }

    public void TakeDamage(DamageInfo damage)
    {
        throw new NotImplementedException();
    }

    public void Healing(float heal)
    {
        throw new NotImplementedException();
    }
}