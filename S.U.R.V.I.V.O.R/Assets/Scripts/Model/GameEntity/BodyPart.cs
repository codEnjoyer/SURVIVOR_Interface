using System;
using System.Data;
using Model.GameEntity.Health;

namespace Model.GameEntity
{
    public class BodyPart : IAlive
    {
        public readonly Body body;
        public readonly BodyPathHealth health;
        private int maxHp;
        private float size;

        public int MaxHp
        {
            get => maxHp;
            private set
            {
                if (value <= 0)
                    throw new ConstraintException("Максимальное здоровье не может быть ниже нуля!");
                maxHp = value;
            }
        }

        public float Size
        {
            get => size;
            private set
            {
                if (value <= 0)
                    throw new ConstraintException("Размер части тела не может быть ниже нуля!");
                size = value;
            }
        }

        public float Hp { get; private set; }
        public event Action<BodyPart> OnZeroHp;

        protected BodyPart(Body body, int maxHp = 100, int size = 100)
        {
            health = new BodyPathHealth(this);
            this.body = body;
            MaxHp = maxHp;
            Size = size;
            Hp = MaxHp;
        }

        public void TakeDamage(DamageInfo damage)
        {
            //throw new NotImplementedException();
            //TODO реализовать метод получения урона в зависимоти от выстрела


            //var blockedDamage = Clothes.Sum(cloth => cloth.CalculateBlockedDamage(damage));
            TakeDamage(damage.Damage);
        }

        protected void TakeDamage(float damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                OnZeroHp?.Invoke(this);
                body.LossBodyParts(this);
            }
        }

        public void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }
    }
}