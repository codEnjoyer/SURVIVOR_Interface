using System.Collections.Generic;
public class BodyHealth: IHealth
{
    public IAlive Target { get; }
    public ICollection<IHealthProperty> HealthProperties { get; }

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
        HealthProperties = new List<IHealthProperty>();
    }
}