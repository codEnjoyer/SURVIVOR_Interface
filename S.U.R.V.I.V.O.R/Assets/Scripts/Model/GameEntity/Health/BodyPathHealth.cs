using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model.GameEntity.Health
{
    [DataContract]
    public class BodyPathHealth : Health
    {
        [DataMember] private BodyPart target;
        [DataMember] private List<HealthProperty> healthProperties;
        public override IAlive Target => target;
        public override ICollection<HealthProperty> HealthProperties => healthProperties;

        public BodyPathHealth(BodyPart bodyPart)
        {
            target = bodyPart;
            healthProperties = new List<HealthProperty>();
        }
    }
}