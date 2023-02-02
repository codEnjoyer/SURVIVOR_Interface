using System;
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
        [SerializeField][Min(1)] private float hp = 100;
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
        
        protected virtual void Awake()
        {
            Health = new Health(this);
        }

        public BodyPartSave CreateSave()
        {
            throw new NotImplementedException();
        }

        public void Restore(BodyPartSave save)
        {
            throw new NotImplementedException();
        }
    }

    public class BodyPartSave
    {
        
    }
}