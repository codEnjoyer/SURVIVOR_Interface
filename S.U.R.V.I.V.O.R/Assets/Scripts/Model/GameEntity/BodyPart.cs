using System;
using System.Runtime.Serialization;
using Model.GameEntity.Health;

namespace Model.GameEntity
{
    [DataContract]
    public abstract class BodyPart : IAlive
    {
        [DataMember] public readonly Body body;
        [DataMember] public readonly BodyPathHealth health;
        [DataMember] private float maxHp;
        [DataMember] private float hp;
        [DataMember] private float size;
        [DataMember] private int significance;

        public float MaxHp
        {
            get => maxHp;
            set
            {
                value = Math.Max(1, value);
                var multiplier = value / maxHp;
                maxHp = value;
                Hp *= multiplier;
            }
        }

        public float Size
        {
            get => size;
            set => size = Math.Max(1, value);
        }

        public int Significance
        {
            get => significance;
            set => significance = Math.Max(1, value);
        }


        public float Hp
        {
            get => hp;
            private set
            {
                if (value <= 0)
                {
                    OnZeroHp?.Invoke(this);
                    body.LossBodyParts(this);
                    return;
                }

                hp = value;
            }
        }

        public event Action<BodyPart> OnZeroHp;

        protected BodyPart(Body body, int maxHp = 100, int size = 100, int significance = 10)
        {
            health = new BodyPathHealth(this);
            this.body = body;
            MaxHp = maxHp;
            Size = size;
            Hp = MaxHp;
            Significance = significance;
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
        }

        public void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }
    }
}