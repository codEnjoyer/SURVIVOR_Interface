using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;

namespace Model.GameEntity
{
    [DataContract(Namespace = "Model.GameEntity")]
    public class Body : IAlive
    {
        [DataMember] private int currentCriticalLoses;
        [DataMember] private int maxCriticalLoses;
        [IgnoreDataMember] private List<BodyPart> bodyParts;
        [DataMember] public Health Health { get; private set; }

        public Body(IEnumerable<BodyPart> bodyParts) : this()
        {
            this.bodyParts = bodyParts.ToList();
        }
        public Body()
        {
            Health = new Health(this);
        }


        protected int MaxCriticalLoses
        {
            get => maxCriticalLoses;
            set
            {
                if (value > 0 && value <= bodyParts.Count)
                    maxCriticalLoses = value;
                else
                    throw new ConstraintException($"Нарушино устовие 0 < {value} <= {bodyParts.Count}. {GetType()}");
            }
        }

        public IReadOnlyCollection<BodyPart> BodyParts => bodyParts;
        public float Hp => BodyParts.Sum(part => part.Hp);
        public event Action Died;

        protected void AddBodyPart(BodyPart bodyPart, int significance)
        {
            bodyParts.Add(bodyPart);
            bodyPart.Died += () =>
            {
                currentCriticalLoses += significance;
                if (currentCriticalLoses >= maxCriticalLoses)
                    Died?.Invoke();
            };
        }

        public virtual void TakeDamage(DamageInfo damage)
        {
            throw new NotImplementedException();
        }

        public virtual void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }

        public virtual void RestoreBodyParts() => bodyParts = new List<BodyPart>();
    }
}