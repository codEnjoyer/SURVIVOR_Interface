using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model.GameEntity.EntityHealth
{
    public sealed class Health
    {
        public IAlive Target { get; private set; }
        private readonly List<IHealthProperty> healthProperties;

        public Health(IAlive target)
        {
            Target = target;
            healthProperties = new List<IHealthProperty>();
        }

        public Health(IAlive target, IEnumerable<IHealthProperty> healthProperties)
        {
            Target = target;
            this.healthProperties = healthProperties.ToList();
        }

        public IReadOnlyCollection<IHealthProperty> HealthProperties => healthProperties;

        public void AddProperty(IHealthProperty property)
        {
            if (HealthProperties.Contains(property))
                return;
            healthProperties.Add(property);
            property.InitialAction(this);
        }

        public void DeleteProperty(IHealthProperty property)
        {
            healthProperties.Remove(property);
            property.FinalAction(this);
        }

        public void OnTurnEnd()
        {
            foreach (var healthProperty in HealthProperties)
                healthProperty.TurnEndAction(this);
        }
    }
}