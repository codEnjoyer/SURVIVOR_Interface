using System;
using Model.GameEntity;

namespace Model.ServiceClasses
{
    public class AttackTarget
    {
        private ITakingDamage target;
        private float distance;

        public ITakingDamage Target
        {
            get => target;
            private set => target = value ?? throw new InvalidOperationException();
        }

        public float Distance
        {
            get => distance;
            private set
            {
                if (value < 0)
                    throw new InvalidOperationException();
                distance = value;
            }
        }

        public AttackTarget(ITakingDamage target, float distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}