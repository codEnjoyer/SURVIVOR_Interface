using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model.GameEntity.Health
{
    [DataContract(Namespace = "Model.GameEntity.Health")]
    public class BodyHealth: Health
    {
        [DataMember] private Body target;
        [DataMember] private List<HealthProperty> healthProperties;
        [DataMember] private float radiation;
        public override IAlive Target => target;
        public override ICollection<HealthProperty> HealthProperties => healthProperties;
        public float Radiation
        {
            get => radiation;
            set
            {
                if (value >= 0)
                    radiation = value;
            }
        }

        public BodyHealth(Body body)
        {
            target = body;
            healthProperties = new List<HealthProperty>();
        }
    }
}