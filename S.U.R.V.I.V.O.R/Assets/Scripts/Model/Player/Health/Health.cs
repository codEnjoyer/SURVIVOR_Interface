using global::System.Collections.Generic;

namespace Assets.Scripts.Model
{

    public class Health
    {
        private readonly float Radiation;
        private readonly List<HealthProperties> HealthProperties;

        public void AddProperty(HealthProperties healthProperty)
        {
            HealthProperties.Add(healthProperty);//TODO добавлять только если такого свойства нет
        }

        public void DeletePropery(HealthProperties healthProperty)
        {
            HealthProperties.Remove(healthProperty);
        }
    }
}