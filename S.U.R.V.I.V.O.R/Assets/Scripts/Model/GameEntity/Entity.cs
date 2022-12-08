using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Model.GameEntity
{
    public abstract class Entity: MonoBehaviour, IAlive
    {
        [SerializeField] [Min(1)] private float initiative = 1;
        [SerializeField] [Min(1)] private int speedInFightScene = 1;
        public abstract Body Body { get; }
        public abstract void Attack(IEnumerable<BodyPart> targets, float distance);

        public float Initiative
        {
            get => initiative;
            set
            {
                if (value < 0)
                    throw new ConstraintException($"{nameof(initiative)} < 0");
                initiative = value;
            }
        }

        public int SpeedInFightScene
        {
            get => speedInFightScene;
            set
            {
                if (value < 0)
                    throw new ConstraintException($"{nameof(speedInFightScene)} < 0");
                speedInFightScene = value;
            }
        }

        
        public void TakeDamage(DamageInfo damage) => Body.TakeDamage(damage);
        public void Healing(HealInfo heal) => Body.Healing(heal);
        public float Hp => Body.Hp;
    }
}