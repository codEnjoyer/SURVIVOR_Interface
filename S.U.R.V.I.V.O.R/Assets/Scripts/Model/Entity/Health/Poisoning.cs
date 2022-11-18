
using System;

public class Poisoning: IHealthProperty
{
    public int Duration { get; private set; }
    public readonly int damage;
    public HealthPropertyType Type { get; }

    public Poisoning(int damage = 1, int duration = 4)
    {
        this.Duration = duration;
        this.damage = damage;
    }

    public void InitialAction(IHealth health) {}

    public void OnTurnEnd(IHealth health)
    {
        health.Target.TakeDamage(damage);
        Duration--;
        if (Duration == 0)
            health.DeleteProperty(this);
    }

    public void FinalAction(IHealth health) {}

    public override bool Equals(object obj) => obj is Poisoning;
}