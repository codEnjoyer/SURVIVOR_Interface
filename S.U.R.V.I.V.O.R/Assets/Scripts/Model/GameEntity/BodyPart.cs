using System;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;
using Model.SaveSystem;
using UnityEngine;

namespace Model.GameEntity
{
    public class BodyPart : MonoBehaviour, IAlive, ISaved<BodyPartSave>
    {
        public Health Health { get; private set; }
        [SerializeField][Min(1)] private float maxHp = 100;
        private float hp;
        [SerializeField][Min(1)] private float size = 100;
        public event Action Died;
        
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
                    hp = 0;
                    return;
                }

                hp = value;
            }
        }

        public bool IsDied => hp <= 0;

        public virtual void TakeDamage(DamageInfo damage)
        {
            //TODO реализовать метод получения урона в зависимоти от выстрела
            Hp -= damage.Damage;
        }

        public virtual void Healing(HealInfo heal)
        {
            Hp += heal.Heal;
        }
        
        protected virtual void Awake()
        {
            Health = new Health(this);
            hp = maxHp;
        }

        public virtual BodyPartSave CreateSave()
        {
            return new BodyPartSave()
            {
                healthProperties = Health.HealthProperties.ToArray(),
                maxHp = MaxHp,
                hp = Hp,
                size = Size
            };
        }

        public virtual void Restore(BodyPartSave save)
        {
            Health = new Health(this, save.healthProperties);
            MaxHp = save.maxHp;
            Hp = save.hp;
            Size = save.size;
        }
    }

    [DataContract]
    [KnownType(typeof(Poisoning))]
    public class BodyPartSave
    {
        [DataMember] public IHealthProperty[] healthProperties;
        [DataMember] public float maxHp;
        [DataMember] public float hp;
        [DataMember] public float size;
    }
}