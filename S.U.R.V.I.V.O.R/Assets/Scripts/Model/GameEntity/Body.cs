using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Model.Entities.Characters.BodyParts;
using Model.GameEntity.Health;

namespace Model.GameEntity
{
    [DataContract(Namespace = "Model.GameEntity")]
    public class Body : IAlive
    {
        [DataMember] private int currentCriticalLoses;
        [DataMember] private int maxCriticalLoses;
        [IgnoreDataMember] private List<BodyPart> bodyParts;
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

        public void TakeDamage(DamageInfo damage)
        {
            throw new NotImplementedException();
        }

        public void Healing(HealInfo heal)
        {
            throw new NotImplementedException();
        }

        public virtual void RestoreBodyParts() => bodyParts = new List<BodyPart>();
    }
}