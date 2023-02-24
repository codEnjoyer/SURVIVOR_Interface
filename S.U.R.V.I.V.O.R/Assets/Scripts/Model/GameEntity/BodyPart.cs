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
            set
            {
                if (value <= 0)
                {
                    Died?.Invoke();
                    hp = 0;
                    return;
                }

                hp = Math.Min(value,MaxHp);
            }
        }

        public bool IsDied => hp <= 0;

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