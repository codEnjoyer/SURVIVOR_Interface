
using System;

public class DamageInfo
{
    private float damage;
    public float Damage
    {
        get => damage;
        private set
        {
            if (damage < 0)
                throw new ArgumentException("Урон не может быть меньше нуля!");
            damage = value;
        }
    }

    public DamageInfo(float damage)
    {
        Damage = damage;
    }
}
