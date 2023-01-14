using System;
using System.Data;
using Model.GameEntity.Health;
using UnityEngine;

namespace Model.GameEntity
{
    public abstract class BodyPart : IAlive
    {
        public readonly Body body;
        public readonly BodyPathHealth health;
        private int maxHp;
        private float size;
        private int significance = 10; // Temporary!!!

        public int MaxHp
        {
            get => maxHp;
            set
            {
                maxHp = Math.Max(1, value);
                Hp = Math.Min(maxHp, Hp);
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

        private float hp;
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
        }

        public void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }
    }
}