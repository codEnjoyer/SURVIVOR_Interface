using System;
using Model.GameEntity;

namespace Model.ServiceClasses
{
    public sealed class AttackTarget
    {
        private IAlive target;
        private float distanceToTarget;

        public IAlive Target
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

        public AttackTarget(IAlive target, float distanceToTarget)
        {
            Target = target;
            DistanceToTarget = distanceToTarget;
        }
    }
}