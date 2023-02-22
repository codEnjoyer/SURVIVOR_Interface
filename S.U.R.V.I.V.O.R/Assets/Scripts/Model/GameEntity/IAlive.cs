using System;
using Model.GameEntity.EntityHealth;

namespace Model.GameEntity
{
    public interface IAlive
    {
        public float Hp { get; set; }
        public Health Health { get; }
        public event Action Died;

        public bool IsDied { get; }
    }
}