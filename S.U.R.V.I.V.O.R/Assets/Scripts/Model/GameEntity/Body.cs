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
    public class Body : MonoBehaviour, IAlive, ISaved<BodySave>
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

        public virtual BodySave CreateSave()
        {
            return new BodySave()
            {
                healthProperties = Health.HealthProperties.ToArray(),
                bodyPartSaves = bodyParts.Select(x => x.CreateSave()).ToArray()
            };
        }

        public virtual void Restore(BodySave save)
        {
            Health = new Health(this, save.healthProperties);
            for (int i = 0; i < bodyParts.Count; i++)
                bodyParts[i].Restore(save.bodyPartSaves[i]);
        }
    }

    [DataContract]
    public class BodySave
    {
        [DataMember] public IHealthProperty[] healthProperties;
        [DataMember] public BodyPartSave[] bodyPartSaves;
    }
}