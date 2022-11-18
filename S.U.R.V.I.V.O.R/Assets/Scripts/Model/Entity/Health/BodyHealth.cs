using System.Collections.Generic;
public class BodyHealth: Health
{
    public override IAlive Target { get; }
    public override ICollection<HealthProperty> HealthProperties { get; }

    private float radiation;
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
        Target = body;
        HealthProperties = new List<HealthProperty>();
    }
}