using System;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;
using UnityEngine;

namespace Model.GameEntity
{
    [DataContract(Namespace = "Model.GameEntity")]
    public class BodyPart : MonoBehaviour, IAlive
    {
        [DataMember] public Health Health { get; private set; }
        [DataMember] private float maxHp;
        [DataMember] private float hp;
        [DataMember] private float size;
        public event Action Died;

        public BodyPart(int maxHp = 100, int size = 100)
        {
            Health = new Health(this);
            MaxHp = maxHp;
            Size = size;
            Hp = MaxHp;
        }

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

        public float Hp
        {
            get => hp;
            private set
            {
                if (value <= 0)
                {
                    Died?.Invoke();
                    return;
                }

                hp = value;
            }
        }

        public virtual void TakeDamage(DamageInfo damage)
        {
            //TODO реализовать метод получения урона в зависимоти от выстрела
            Hp -= damage.Damage;
        }

        public virtual void Healing(HealInfo heal)
        {
            Hp += heal.Heal;
        }
    }
}