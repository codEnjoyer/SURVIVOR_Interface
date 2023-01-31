using System;

namespace Model.GameEntity
{
    public interface IAlive: ITakingDamage, IHealing
    {
        public float Hp { get; }
        public event Action Died;
    }
}