using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model.GameEntity.EntityHealth
{
    [DataContract(Namespace = "Model.GameEntity.Health")]
    [KnownType("GetKnownTypes")]
    public sealed class Health
    {
        [DataMember] public IAlive Target { get; private set; }
        [DataMember] private List<IHealthProperty> healthProperties;

        public Health(IAlive target)
        {
            Target = target;
            healthProperties = new List<IHealthProperty>();
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


        #region Save

        private static Type[] knownTypes;

        private static Type[] GetKnownTypes()
        {
            if (knownTypes == null)
            {
                var type = typeof(IHealthProperty);
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                    .ToArray();
                knownTypes = types;
            }

            return knownTypes;
        }

        #endregion
    }
}