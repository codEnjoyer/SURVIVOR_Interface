using System;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;
using Model.SaveSystem;
using UnityEngine;
using Random = System.Random;

namespace Model.GameEntity
{
    public class BodyPart : MonoBehaviour, IAlive, ISaved<BodyPartData>
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
            var rnd = new Random();
            //TODO реализовать метод получения урона в зависимоти от выстрела
            var clothesBP = GetComponent<BodyPathWearableClothes>();
            var damageToBodyPart = damage.FullDamage * damage.KeneeticDamage;
            if (clothesBP != null && clothesBP.currentArmor == 0 || clothesBP == null) //Броня кончилась или ее нет
            {
                damageToBodyPart = damage.FullDamage * damage.RandomCoefficientOfDamage;
            }
            else//Броня есть
            {
                if (rnd.NextDouble() <= damage.ArmorPenetratingChance)//Броня пробита
                {
                    clothesBP.GetDamageToArmor(damage.ArmorDamageOnPenetration);
                    damageToBodyPart += damage.FullDamage * damage.OnArmorPenetrationDamage;
                }
                else//Броня не пробита
                {
                    clothesBP.GetDamageToArmor(damage.ArmorDamageOnNonPenetration);
                    damageToBodyPart += damage.FullDamage * damage.UnderArmorDamage;
                }
            }
            //TODO добавить перелом и кровотечение

            Hp -= damageToBodyPart;
        }

        public virtual void Heal(HealInfo heal)
        {
            Hp += heal.Heal;
        }
        
        protected virtual void Awake()
        {
            Health = new Health(this);
            Hp = maxHp;
        }

        public virtual BodyPartData CreateData()
        {
            return new BodyPartData()
            {
                healthProperties = Health.HealthProperties.ToArray(),
                maxHp = MaxHp,
                hp = Hp,
                size = Size
            };
        }

        public virtual void Restore(BodyPartData data)
        {
            Health = new Health(this, data.healthProperties);
            MaxHp = data.maxHp;
            Hp = data.hp;
            Size = data.size;
        }
    }

    [DataContract]
    public class BodyPartData
    {
        [DataMember] public IHealthProperty[] healthProperties;
        [DataMember] public float maxHp;
        [DataMember] public float hp;
        [DataMember] public float size;
    }
}