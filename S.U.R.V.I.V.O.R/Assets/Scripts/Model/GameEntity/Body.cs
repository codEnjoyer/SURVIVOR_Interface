using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.Health;

namespace Model.GameEntity
{
    [DataContract]
    public abstract class Body : IAlive
    {
        [DataMember] private int currentCriticalLoses;
        [DataMember] private int maxCriticalLoses;
        [IgnoreDataMember] protected readonly List<BodyPart> bodyParts = new();
        [DataMember] private BodyHealth health;
        public BodyHealth Health => health;

        protected Body()
        {
            health = new BodyHealth(this);
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

        public IEnumerable<BodyPart> BodyParts => bodyParts;
        public float Hp => BodyParts.Sum(part => part.Hp);
        public event Action Died;

        public void LossBodyParts(BodyPart bodyPart)
        {
            bodyParts.Remove(bodyPart);
            currentCriticalLoses += bodyPart.Significance;
            if (currentCriticalLoses >= MaxCriticalLoses)
            {
                Died?.Invoke();
            }
        }

        public void TakeDamage(DamageInfo damage)
        {
            throw new NotImplementedException();
        }

        public void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }
    }
}