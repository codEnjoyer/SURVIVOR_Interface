using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;
using Model.SaveSystem;
using UnityEngine;

namespace Model.GameEntity
{
    public class Body : MonoBehaviour, IAlive
    {
        public Health Health { get; private set; }
        public int MaxCriticalLoses => bodyParts.Count; 
        public int CurrentCriticalLoses { get; private set; }
        private readonly List<BodyPart> bodyParts = new ();
        public IReadOnlyCollection<BodyPart> BodyParts => bodyParts;
        public float Hp => BodyParts.Sum(part => part.Hp);
        public event Action Died;

        protected void AddBodyPart(BodyPart bodyPart, int significance)
        {
            if (bodyPart == null)
                throw new ArgumentException();
            bodyParts.Add(bodyPart);
            
            bodyPart.Died += () =>
            {
                CurrentCriticalLoses += significance;
                if (CurrentCriticalLoses >= MaxCriticalLoses)
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

        protected virtual void Awake()
        {
            Health = new Health(this);
        }
    }
}