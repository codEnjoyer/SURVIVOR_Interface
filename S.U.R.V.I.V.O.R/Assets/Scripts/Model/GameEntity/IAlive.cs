using System;
using Model.GameEntity.EntityHealth;

namespace Model.GameEntity
{
    public interface IAlive: ITakingDamage, IHealing
    {
        public float Hp { get; }
        public Health Health { get; }
        public event Action Died;

        public bool IsDied { get; }
    }
}