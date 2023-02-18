using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity.EntityHealth;
using Model.SaveSystem;
using Model.ServiceClasses;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.GameEntity
{
    public class Body : MonoBehaviour, IAlive, ISaved<BodyData>
    {
        public Health Health { get; private set; }

        [SerializeField] [FormerlySerializedAs("bodyParts")]
        private List<BodyPartRegister> bodyPartRegisters = new();

        private readonly List<BodyPart> bodyParts = new();
        public int CurrentCriticalLoses { get; private set; }
        public event Action Died;

        public IReadOnlyList<BodyPart> BodyParts => bodyParts;
        public float Hp => BodyParts.Sum(part => part.Hp);
        public int MaxCriticalLoses => bodyPartRegisters.Count;

        public bool IsDied => CurrentCriticalLoses >= MaxCriticalLoses;

        private void AddBodyPart(BodyPartRegister bodyPartRegister)
        {
            var bodyPart = bodyPartRegister.BodyPart;
            var significance = bodyPartRegister.Significance;

            if (bodyPart == null) throw new ArgumentException();
            bodyParts.Add(bodyPart);

            if (bodyPart.IsDied)
                CurrentCriticalLoses += significance;
            else
                bodyPart.Died += () =>
                {
                    CurrentCriticalLoses += significance;
                    if (IsDied)
                        Died?.Invoke();
                };
        }

        public virtual void TakeDamage(DamageInfo damage)
        {
            foreach (var bodyPart in bodyParts)
            {
                bodyPart.TakeDamage(damage);
            }
        }

        public virtual void Heal(HealInfo heal)
        {
            throw new NotImplementedException();
        }

        protected virtual void Awake()
        {
            if (bodyPartRegisters.Count == 0)
                throw new Exception();

            Health = new Health(this);
            foreach (var bodyPartRegister in bodyPartRegisters)
                AddBodyPart(bodyPartRegister);

            if (
                bodyParts.Any(x => x == null) ||
                bodyParts.Count != bodyParts.Distinct().Count()
            )
                throw new Exception();
        }

        public virtual BodyData CreateData()
        {
            return new BodyData()
            {
                healthProperties = Health.HealthProperties.ToArray(),
                bodyPartSaves = bodyParts.Select(x => x.CreateData()).ToArray()
            };
        }

        public virtual void Restore(BodyData data)
        {
            Health = new Health(this, data.healthProperties);
            for (int i = 0; i < bodyParts.Count; i++)
                bodyParts[i].Restore(data.bodyPartSaves[i]);
        }
    }

    [DataContract]
    public class BodyData
    {
        [DataMember] public IHealthProperty[] healthProperties;
        [DataMember] public BodyPartData[] bodyPartSaves;
    }
}