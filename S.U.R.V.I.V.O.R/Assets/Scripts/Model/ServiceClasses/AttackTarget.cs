using System;
using Model.GameEntity;

namespace Model.ServiceClasses
{
    public sealed class AttackTarget
    {
        private ITakingDamage target;
        private float distanceToTarget;

        public ITakingDamage Target
        {
            get => target;
            private set => target = value ?? throw new InvalidOperationException();
        }

        public float DistanceToTarget
        {
            get => distanceToTarget;
            private set
            {
                if (value < 0)
                    throw new InvalidOperationException();
                distanceToTarget = value;
            }
        }

        public AttackTarget(ITakingDamage target, float distanceToTarget)
        {
            Target = target;
            DistanceToTarget = distanceToTarget;
        }
    }
}