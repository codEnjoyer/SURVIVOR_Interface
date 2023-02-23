using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Model.GameEntity.EntityHealth;
using Model.ServiceClasses;
using UnityEngine;

namespace Model.GameEntity
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] [Min(1)] private float initiative = 1;
        [SerializeField] [Min(1)] private int speedInFightScene = 1;
        [SerializeField] private MeleeAttack meleeAttack;
        [field: SerializeField] public Body Body { get; private set; }

        protected virtual void Awake()
        { }

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

        public virtual void Attack(Vector3 targetPoint)
        {
            var rayDirection = (targetPoint - transform.position).normalized;
            var hitted = Physics.Raycast(new Ray(transform.position, rayDirection), out var hit);
            var target = hit.transform.gameObject.GetComponent<BodyPart>();
            if (hitted && target != null)
            {
                target.Hp -= meleeAttack.Damage;
            }
        }
    }
}