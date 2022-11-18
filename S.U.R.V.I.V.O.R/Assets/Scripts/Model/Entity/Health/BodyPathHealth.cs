using System.Collections.Generic;

public class BodyPathHealth: IHealth
{
    public IAlive Target { get; }
    public ICollection<IHealthProperty> HealthProperties { get; }

    public BodyPathHealth(BodyPart bodyPart)
    {
        Target = bodyPart;
        HealthProperties = new List<IHealthProperty>();
    }
}