using global::System;
using global::System.Collections.Generic;
using global::System.Linq;

namespace Assets.Scripts.Model
{
    public abstract class Body
    {
        protected List<BodyPart> BodyParts;
        public float TotalHp => BodyParts.Sum(path => path.Hp);
        public float TotalWeight => BodyParts.Sum(path => path.TotalWeight);
        public readonly Health Health = new();
        public event Action? Died;

        public void GetDamage(Shoot shoot)
        {
            //TODO Реализовать метод распределения урона от класса Shoot по частям тела
            if (TotalHp <= 0)
                Died?.Invoke();
        }
    }
}