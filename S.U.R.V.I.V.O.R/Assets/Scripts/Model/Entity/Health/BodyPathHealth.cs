using System.Collections.Generic;

public class BodyPathHealth: Health
{
    public override IAlive Target { get; }
    public override ICollection<HealthProperty> HealthProperties { get; }

    public BodyPathHealth(BodyPart bodyPart)
    {
        Target = bodyPart;
        HealthProperties = new List<HealthProperty>();
    }
}