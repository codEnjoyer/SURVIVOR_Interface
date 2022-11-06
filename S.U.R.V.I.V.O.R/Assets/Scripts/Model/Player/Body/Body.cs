using global::System;
using global::System.Collections.Generic;
using global::System.Linq;


public abstract class Body
{
    protected List<BodyPart> BodyParts;
    public float TotalHp => BodyParts.Sum(path => path.Hp); 
    public float TotalWeight => BodyParts.Sum(path => path.TotalWeight);
    
    public readonly Health Health;
    public event Action Died;

    public Body()
    {
        Health = new Health(this);
    }

    public void TakeDamage(Shoot shoot)
    {
        //TODO Реализовать метод распределения урона от класса Shoot по частям тела
        throw new NotImplementedException();
        if (TotalHp <= 0)
            Died?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        //TODO Реализовать метод распределения урона по частям тела
        throw new NotImplementedException();
        if (TotalHp <= 0)
            Died?.Invoke();
    }
    
    public void Healing(float heal)
    {
        //TODO Реализовать метод распределения лечения по частям тела
        throw new NotImplementedException();
    }
}
